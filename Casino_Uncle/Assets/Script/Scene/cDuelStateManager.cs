using UnityEngine;
using System.Collections;

public class cDuelStateManager : ScriptableObject {

	public cDeckModel m_dModel;
	public cEnemyDeckModel m_edModel;
	public cGameData m_gData;
	public cCommitTextModel m_ctModel;
	public cCameraModel m_cameraModel;
	public cEffectModel m_effectModel;
	public cHitPointManager m_hpPManager;
	public cHitPointManager m_hpEManager;
	public cEnemyModel m_eModel;

	private int m_Damage;

	public enum eDuelState{
		eDuelState_BattleInit,
		eDuelState_Shuffle,
		eDuelState_HandOut,
		eDuelState_CardOpen,
		eDuelState_CardEdit,
		eDuelState_Select,
		eDuelState_Move,
		eDuelState_EnemyShuffle,
		eDuelState_BattleEffect,
		eDuelState_EnemyCardOpen,
		eDuelState_Calc,
		eDuelState_CommitEffect,
		eDuelState_CommitEffectWin,
		eDuelState_CommitEffectLose,
		eDuelState_CommitEffectDraw,
		eDuelState_Win,
		eDuelState_Lose,
		eDuelState_End
	}

	private eDuelState m_State;

	void OnEnable(){
		m_State = eDuelState.eDuelState_BattleInit;
	}

	public bool DuelExec ()
	{
		switch (m_State) {
		case eDuelState.eDuelState_BattleInit:
			BattleInit ();
			break;
		case eDuelState.eDuelState_Shuffle:
			Shuffle ();
			break;
		case eDuelState.eDuelState_HandOut:
			HandOut ();
			break;
		case eDuelState.eDuelState_CardOpen:
			CardOpen ();
			break;
		case eDuelState.eDuelState_CardEdit:
			CardEdit ();
			break;
		case eDuelState.eDuelState_Select:
			Select ();
			break;
		case eDuelState.eDuelState_Move:
			Move ();
			break;
		case eDuelState.eDuelState_EnemyShuffle:
			EnemyShuffle ();
			break;
		case eDuelState.eDuelState_BattleEffect:
			BattleEffect ();
			break;
		case eDuelState.eDuelState_EnemyCardOpen:
			EnemyCardOpen ();
			break;
		case eDuelState.eDuelState_Calc:
			Calc ();
			break;
		case eDuelState.eDuelState_CommitEffect:
			CommitEffect ();
			break;
		case eDuelState.eDuelState_CommitEffectWin:
			CommitEffectWin ();
			break;
		case eDuelState.eDuelState_CommitEffectLose:
			CommitEffectLose ();
			break;
		case eDuelState.eDuelState_CommitEffectDraw:
			CommitEffectDraw ();
			break;
		case eDuelState.eDuelState_Win:
			Win ();
			break;
		case eDuelState.eDuelState_Lose:
			Lose ();
			break;
		case eDuelState.eDuelState_End:
			m_State = eDuelState.eDuelState_BattleInit;
			return true;
		}
		return false;
	}

	public void Init(){
		m_dModel.Init ();
		m_hpPManager.Init ();
		m_gData.InitWin ();
		m_effectModel.Init ();
		m_eModel.Init ();
	}

	private void BattleInit(){
		m_hpEManager.Init ();
		m_edModel.Init ();
		m_eModel.Init ();
		++m_State;
	}

	private void Shuffle(){
		m_dModel.RandomSet ();
		m_edModel.Hind ();
		m_gData.InitCard ();
		++m_State;
	}

	private void HandOut(){
		if (m_dModel.HandOnCard () == true) {
			++m_State;
		}
	}

	private void CardOpen(){
		if (m_dModel.CardOpen () == true) {
			m_State = eDuelState.eDuelState_Select;
		}
	}

	private void CardEdit(){
		m_dModel.EditCard ();
		m_edModel.Hind ();
		m_gData.CardMinus ();
		m_State = eDuelState.eDuelState_HandOut;
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
				m_effectModel.EffectStart ();
				++m_State;
			}
		} else {
			
		}
	}

	private void EnemyCardOpen(){
		if (m_effectModel.EffectExec () == true) {
			if (m_edModel.Open () == true) {
				++m_State;
			}
		}
	}

	private void Calc(){
		m_dModel.CardEnd ();

		m_ctModel.Init ();

		int playerPower = m_dModel.GetBattleCardNumber ();
		int enemyPower = m_edModel.GetBattleCardNumber ();

		Debug.Log (playerPower.ToString() + "  " + enemyPower.ToString());

		if (playerPower == enemyPower) {
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Draw);
			m_State = eDuelState.eDuelState_CommitEffect;
			Debug.Log ("引き分け");

			return;
		}

		if (playerPower == 0) {
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Win);
			m_hpEManager.m_Damage = 1;
			m_State = eDuelState.eDuelState_CommitEffect;
			Debug.Log ("勝利");

			return;
		}

		if (enemyPower == 0) {
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Lose);
			m_hpPManager.m_Damage = 1;
			m_State = eDuelState.eDuelState_CommitEffect;
			Debug.Log ("敗北");

			return;
		}

		if (playerPower == 1) {
			if (enemyPower > 10) {
				m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Win);
				m_hpEManager.m_Damage = 1;
				m_State = eDuelState.eDuelState_CommitEffect;
				Debug.Log ("勝利");

				return;
			}
		}

		if (enemyPower == 1) {
			if (playerPower > 10) {
				m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Lose);
				m_hpPManager.m_Damage = 1;
				m_State = eDuelState.eDuelState_CommitEffect;
				Debug.Log ("敗北");

				return;
			}
		}

		if (playerPower > enemyPower) {
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Win);
			m_hpEManager.m_Damage = playerPower - enemyPower;
			m_State = eDuelState.eDuelState_CommitEffect;
			Debug.Log ("勝利");
		} else {
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Lose);
			m_hpPManager.m_Damage = enemyPower - playerPower;
			m_State = eDuelState.eDuelState_CommitEffect;
			Debug.Log ("敗北");
		}

	}

	private void CommitEffect(){
		if (m_ctModel.Move () == true) {

			m_effectModel.EffectEnd ();

			cCommitTextModel.eCommitText commit = m_ctModel.GetText ();

			if (commit == cCommitTextModel.eCommitText.eCommitText_Win) {
				m_State = eDuelState.eDuelState_CommitEffectWin;
			} else if (commit == cCommitTextModel.eCommitText.eCommitText_Lose) {
				m_State = eDuelState.eDuelState_CommitEffectLose;
			} else {
				m_State = eDuelState.eDuelState_CommitEffectDraw;
			}
		}
	}

	private void CommitEffectWin(){
		m_edModel.Snap ();

		m_eModel.Vibration ();

		if (m_hpEManager.CutBack () == true) {

			m_eModel.StopVibration ();

			if (m_hpEManager.HitPointCheck () == true) {
				m_State = eDuelState.eDuelState_Win;
			} else if (m_dModel.m_LastBattle == true) {
				m_State = eDuelState.eDuelState_Lose;
			} else {
				m_State = eDuelState.eDuelState_CardEdit;
			}
		}
	}

	private void CommitEffectLose(){
		m_dModel.Snap ();

		m_cameraModel.Vibration ();

		if (m_hpPManager.CutBack () == true) {

			m_cameraModel.StopVibration ();

			if (m_hpPManager.HitPointCheck () == true) {
				m_State = eDuelState.eDuelState_Lose;
			} else if (m_dModel.m_LastBattle == true) {
				m_State = eDuelState.eDuelState_Lose;
			} else {
				m_State = eDuelState.eDuelState_CardEdit;
			}
		}
	}

	private void CommitEffectDraw(){
		m_dModel.Snap ();
		m_edModel.Snap ();

		if (m_dModel.m_LastBattle == true) {
			m_State = eDuelState.eDuelState_Lose;
		} else {
			m_State = eDuelState.eDuelState_CardEdit;
		}
	}

	private void Win(){
		Debug.Log ("ゲームに勝利" );
		m_gData.m_PlayerHitPoint = m_hpPManager.GetHitPoint ();
		m_State = eDuelState.eDuelState_End;
		m_gData.AddWin ();
	}

	private void Lose(){
		Debug.Log ("ゲームに敗北" );
		m_State = eDuelState.eDuelState_End;
	}
}
