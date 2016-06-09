using UnityEngine;
using System.Collections;

public class cGameScene : cSceneBase {

	public cDeckModel m_dModel;
	public cEnemyDeckModel m_edModel;
	public cGameData m_gData;
	public cCommitTextModel m_ctModel;
	public cCameraModel m_cameraModel;

	private int m_Damage;

	public enum eGameSceneList{
		eGameSceneList_Init,
		eGameSceneList_BattleInit,
		eGameSceneList_Shuffle,
		eGameSceneList_Select,
		eGameSceneList_Move,
		eGameSceneList_EnemyShuffle,
		eGameSceneList_BattleEffect,
		eGameSceneList_EnemyCardOpen,
		eGameSceneList_Calc,
		eGameSceneList_CommitEffect,
		eGameSceneList_CommitEffectWin,
		eGameSceneList_CommitEffectLose,
		eGameSceneList_CommitEffectDraw,
		eGameSceneList_End,
		eGameSceneList_Win,
		eGameSceneList_Lose,
		eGameSceneList_FadeIn,
	}

	private eGameSceneList m_State;

	void OnEnable(){
		m_State = eGameSceneList.eGameSceneList_Init;
	}

	public override cGameSceneManager.eGameScene SceneExec ()
	{
		switch (m_State) {
		case eGameSceneList.eGameSceneList_Init:
			Init ();
			break;
		case eGameSceneList.eGameSceneList_BattleInit:
			BattleInit ();
			break;
		case eGameSceneList.eGameSceneList_Shuffle:
			Shuffle ();
			break;
		case eGameSceneList.eGameSceneList_Select:
			Select ();
			break;
		case eGameSceneList.eGameSceneList_Move:
			Move ();
			break;
		case eGameSceneList.eGameSceneList_EnemyShuffle:
			EnemyShuffle ();
			break;
		case eGameSceneList.eGameSceneList_BattleEffect:
			BattleEffect ();
			break;
		case eGameSceneList.eGameSceneList_EnemyCardOpen:
			EnemyCardOpen ();
			break;
		case eGameSceneList.eGameSceneList_Calc:
			Calc ();
			break;
		case eGameSceneList.eGameSceneList_CommitEffect:
			CommitEffect ();
			break;
		case eGameSceneList.eGameSceneList_CommitEffectWin:
			CommitEffectWin ();
			break;
		case eGameSceneList.eGameSceneList_CommitEffectLose:
			CommitEffectLose ();
			break;
		case eGameSceneList.eGameSceneList_CommitEffectDraw:
			CommitEffectDraw ();
			break;
		case eGameSceneList.eGameSceneList_Win:
			Win ();
			break;
		case eGameSceneList.eGameSceneList_Lose:
			Lose ();
			break;
		case eGameSceneList.eGameSceneList_End:
			End ();
			break;
		}
		return cGameSceneManager.eGameScene.GameScene_Game;
	}

	private void Init(){
		m_dModel.Init ();
		m_gData.InitWin ();
		++m_State;
	}

	private void BattleInit(){
		m_gData.InitLife ();
		m_edModel.Init ();
		m_dModel.RandomSet ();
		++m_State;
	}

	private void Shuffle(){
		m_dModel.EditCard ();
		m_edModel.Hind ();
		++m_State;
	}

	private void Select(){
		if (m_dModel.CardCheck ()) {
			++m_State;
		}
	}

	private void Move(){
		m_dModel.CardMove ();
		m_cameraModel.MoveSet (1.0f, 0.3f);
		++m_State;
	}

	private void EnemyShuffle(){
		m_edModel.Select ( 0.3f );
		++m_State;
	}

	private void BattleEffect(){
		if (m_cameraModel.Move () == true) {
			if (m_edModel.Move () == true) {
				++m_State;
			}
		}
	}

	private void EnemyCardOpen(){
		m_edModel.Open ();

		++m_State;
	}

	private void Calc(){
		m_dModel.CardEnd ();

		m_ctModel.Init ();

		int playerPower = m_dModel.GetBattleCardNumber ();
		int enemyPower = m_edModel.GetBattleCardNumber ();

		Debug.Log (playerPower.ToString() + "  " + enemyPower.ToString());

		if (playerPower == enemyPower) {
			m_State = eGameSceneList.eGameSceneList_CommitEffect;
			Debug.Log ("引き分け");

			return;
		}

		if (playerPower == 0) {
			m_gData.EnemyDamege (1);
			m_State = eGameSceneList.eGameSceneList_CommitEffect;
			Debug.Log ("勝利");

			return;
		}

		if (enemyPower == 0) {
			m_gData.PlayerDamege (1);
			m_State = eGameSceneList.eGameSceneList_CommitEffect;
			Debug.Log ("敗北");

			return;
		}

		if (playerPower == 1) {
			if (enemyPower > 10) {
				m_gData.EnemyDamege (1);
				m_State = eGameSceneList.eGameSceneList_CommitEffect;
				Debug.Log ("勝利");

				return;
			}
		}

		if (enemyPower == 1) {
			if (playerPower > 10) {
				m_gData.PlayerDamege (1);
				m_State = eGameSceneList.eGameSceneList_CommitEffect;
				Debug.Log ("敗北");

				return;
			}
		}

		if (playerPower > enemyPower) {
			m_gData.EnemyDamege (playerPower - enemyPower);
			m_State = eGameSceneList.eGameSceneList_CommitEffect;
			Debug.Log ("勝利");
		} else {
			m_gData.PlayerDamege (enemyPower - playerPower);
			m_State = eGameSceneList.eGameSceneList_CommitEffect;
			Debug.Log ("敗北");
		}

	}

	private void CommitEffect(){
		if (m_ctModel.Move () == true) {

			cCommitTextModel.eCommitText commit = m_ctModel.GetText ();

			if (commit == cCommitTextModel.eCommitText.eCommitText_Win) {
				m_State = eGameSceneList.eGameSceneList_CommitEffectWin;
			} else if (commit == cCommitTextModel.eCommitText.eCommitText_Lose) {
				m_State = eGameSceneList.eGameSceneList_CommitEffectLose;
			} else {
				m_State = eGameSceneList.eGameSceneList_CommitEffectDraw;
			}
		}
	}

	private void CommitEffectWin(){
		if (m_gData.EnemyDamege () == 0) {
			m_State = eGameSceneList.eGameSceneList_Win;
		} else if (m_dModel.m_LastBattle == true) {
			m_State = eGameSceneList.eGameSceneList_Lose;
		} else {
			m_State = eGameSceneList.eGameSceneList_Shuffle;
		}
	}

	private void CommitEffectLose(){
		if (m_gData.PlayerDamege () == 0) {
			m_State = eGameSceneList.eGameSceneList_Lose;
		} else if (m_dModel.m_LastBattle == true) {
			m_State = eGameSceneList.eGameSceneList_Lose;
		} else {
			m_State = eGameSceneList.eGameSceneList_Shuffle;
		}
	}

	private void CommitEffectDraw(){
		if (m_dModel.m_LastBattle == true) {
			m_State = eGameSceneList.eGameSceneList_Lose;
		} else {
			m_State = eGameSceneList.eGameSceneList_Shuffle;
		}
	}

	private void Win(){
		Debug.Log ("ゲームに勝利" );
		m_State = eGameSceneList.eGameSceneList_BattleInit;
		m_gData.AddWin ();
	}

	private void Lose(){
		Debug.Log ("ゲームに敗北" );
		m_State = eGameSceneList.eGameSceneList_Init;
	}

	private void End(){
		if (m_dModel.m_LastBattle == true) {
			m_State = eGameSceneList.eGameSceneList_FadeIn;
		} else {
			m_State = eGameSceneList.eGameSceneList_Shuffle;
		}
	}
}
