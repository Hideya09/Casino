using UnityEngine;
using System.Collections;

public class cDuelStateManager : ScriptableObject {

	//使用する要素
	public cDeckModel m_dModel;
	public cEnemyDeckModel m_edModel;
	public cGameData m_gData;
	public cCommitTextModel m_ctModel;
	public cCameraModel m_cameraModel;
	public cEffectModel m_effectModel;
	public cHitPointManager m_hpPManager;
	public cHitPointManager m_hpEManager;
	public cEnemyModel m_eModel;
	public cFadeHalfModel m_fadeHModel;
	public cEffectWinModel m_winModel;
	public cEffectLoseModel m_loseModel;
	public cEffectStartModel m_startModel;
	public cButtonModel m_buttonModel;

	//振り上げエフェクト用
	public float m_SwingTime;
	public float m_SwingDownTime;

	public float m_ButtleCardFadeTime;
	public float m_FadeTime;

	private int m_Damage;

	private bool m_Win;

	//デュエルの流れ
	public enum eDuelState{
		eDuelState_BattleInit,
		eDuelState_Start,
		eDuelState_Start2,
		eDuelState_Shuffle,
		eDuelState_HandOut,
		eDuelState_CardOpen,
		eDuelState_CardEdit,
		eDuelState_SelectStart,
		eDuelState_Select,
		eDuelState_Move,
		eDuelState_EnemySelect,
		eDuelState_EnemyMove,
		eDuelState_BattleEffect,
		eDuelState_EnemyCardOpen,
		eDuelState_Calc,
		eDuelState_CommitEffect,
		eDuelState_CommitEffectWin,
		eDuelState_CommitEffectLose,
		eDuelState_CommitEffectDraw,
		eDuelState_Win,
		eDuelState_Lose,
		eDuelState_NextButtle,
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
		case eDuelState.eDuelState_Start:
			Start ();
			break;
		case eDuelState.eDuelState_Start2:
			Start2 ();
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
		case eDuelState.eDuelState_SelectStart:
			SelectStart ();
			break;
		case eDuelState.eDuelState_Select:
			Select ();
			break;
		case eDuelState.eDuelState_Move:
			Move ();
			break;
		case eDuelState.eDuelState_EnemySelect:
			EnemySelect ();
			break;
		case eDuelState.eDuelState_EnemyMove:
			EnemyMove ();
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
		case eDuelState.eDuelState_NextButtle:
			NextButtle ();
			break;
		case eDuelState.eDuelState_End:
			m_State = eDuelState.eDuelState_BattleInit;
			return true;
		}
		return false;
	}

	public void Init(){
		//１ベットごとに初期化しなければならないものを初期化

		m_dModel.Init ();
		m_edModel.JokerInit ();
		m_edModel.Init ();
		m_hpPManager.Init ();
		m_hpEManager.Init ();
		m_gData.InitWin ();
		m_gData.InitCard ();
		m_effectModel.Init ();
		m_eModel.Init ();
		m_buttonModel.Init ();
		m_buttonModel.Black ();

		m_Win = false;

		m_State = eDuelState.eDuelState_BattleInit;
	}

	public void DeleteText(){
		//UI撤去後に行う処理

		//m_gData.m_WinLose = m_Win;

		m_Win = false;

		m_winModel.Init ();
		m_loseModel.Init ();
		m_eModel.Init ();
	}

	public bool GetWinNow(){
		return m_Win;
	}

	//もうカードがない場合
	public bool GetLast(){
		return m_dModel.m_LastBattle;
	}
		
	public void FadeInHalf(){
		m_fadeHModel.FadeExec ();
	}

	//動きを止める
	public void SelectStop(){
		m_dModel.SelectStop ();
	}
		
	private void BattleInit(){
		//１デュエルごとに初期化する部分

		m_hpEManager.Init ();
		m_edModel.Init ();
		m_eModel.Init ();

		m_startModel.Init ();

		m_dModel.MoveSet ();
		m_hpPManager.MoveSet ();
		m_hpEManager.FadeInit ();

		m_gData.AddStartWin ();

		m_Win = false;

		++m_State;
	}

	private void Start(){
		//プレイヤー用UIの出現
		bool endFlag = true;

		endFlag &= m_dModel.Move ();
		endFlag &= m_hpPManager.Move ();

		if (endFlag == true) {
			m_dModel.ReturnSet ();
			m_hpPManager.ReturnSet ();

			++m_State;
		}
	}

	private void Start2(){
		//プレイヤー用UIの出現とエネミーの出現
		bool endFlag = true;

		endFlag &= m_dModel.Return ();
		endFlag &= m_hpPManager.Return ();
		if (endFlag == true) {
			endFlag &= m_eModel.Start ();
			endFlag &= m_hpEManager.Fade ();
			if (endFlag == true) {
				if (m_startModel.Exec () == true) {
					++m_State;
				}
			}
		}
	}

	private void Shuffle(){
		//三枚のカードをランダムで決定
		m_dModel.RandomSet ();
		m_edModel.Hind ();
		m_edModel.RandomSet (m_dModel.GetSelect (), m_gData.GetWin ());
		m_gData.CardMinus ();

		if (m_dModel.m_DoubleBattle) {
			m_gData.SetDouble ();
		}
		++m_State;
	}

	private void HandOut(){
		//カードを配る
		if ((m_dModel.HandOnCard () & m_edModel.SelectCardMove ()) == true) {
			++m_State;
		}
	}

	private void CardOpen(){
		//カードを表にする
		if (m_dModel.CardOpen () == true) {
			m_State = eDuelState.eDuelState_SelectStart;
		}
	}

	private void CardEdit(){

		//カードを一枚手札に加える
		bool edit = m_dModel.EditCard ();
		m_edModel.Hind ();
		edit &= m_edModel.EditCard ();
		m_gData.CardMinus ();

		if (m_dModel.m_DoubleBattle) {
			m_gData.SetDouble ();
		}

		if (edit == true) {
			m_State = eDuelState.eDuelState_HandOut;
		} else {
			m_State = eDuelState.eDuelState_SelectStart;
		}
	}

	private void SelectStart(){
		//カードを選べるようになった合図
		if (m_dModel.SelectStart () == true) {
			m_State = eDuelState.eDuelState_Select;
		}
	}

	private void Select(){
		//カードを選ぶ
		m_buttonModel.Start ();

		if (m_dModel.CardCheck ()) {
			m_buttonModel.End ();
			m_buttonModel.Black ();
			++m_State;
		}
	}

	private void Move(){
		//選んだカードをバトル用カードにする
		m_dModel.CardMove ();
		m_cameraModel.MoveSet (m_SwingTime, m_SwingDownTime);
		m_dModel.MoveAngleSet (m_SwingTime, m_SwingDownTime);

		cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Confirm);

		++m_State;
	}

	private void EnemySelect(){
		//敵の出すカードを選ぶ
		m_edModel.Select (m_SwingDownTime, m_gData.GetWin ());
		++m_State;
	}

	private void EnemyMove(){
		//敵がカードを選ぶ演出
		if (m_edModel.SelectMove () == true) {
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cock);
			++m_State;
		}
	}

	private void BattleEffect(){
		bool moveEndFlag;
		//バトルエフェクトを表示
		if (m_cameraModel.Move ( out moveEndFlag ) == true) {

			m_dModel.ReturnAngle ();

			if (m_edModel.Move () == true && moveEndFlag == true) {
				m_dModel.SetAngle ();

				m_effectModel.EffectStart ();
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cross);
				++m_State;
			}
		} else {
			m_dModel.MoveAngle ();
		}
	}

	private void EnemyCardOpen(){
		//敵のカードを開く
		if (m_effectModel.EffectExec () == true) {
			if (m_edModel.Open () == true) {
				++m_State;
			}
		}
	}

	private void Calc(){
		m_dModel.CardEnd ();

		m_ctModel.Init ();

		//どちらが勝ったかを計算する

		int playerPower = m_dModel.GetBattleCardNumber ();
		int enemyPower = m_edModel.GetBattleCardNumber ();

		//同じカードの時
		if (playerPower == enemyPower) {
			//引き分けを表示
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Draw);
			m_State = eDuelState.eDuelState_CommitEffect;

			return;
		}

		//こちらがジョーカーを出した時
		if (playerPower == 0) {
			//１ダメージ与えて勝利
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Win);
			m_hpEManager.m_Damage = 1;
			m_State = eDuelState.eDuelState_CommitEffect;

			return;
		}

		//相手がジョーカーを出した時
		if (enemyPower == 0) {
			//１ダメージ受けて敗北
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Lose);
			m_hpPManager.m_Damage = 1;
			m_State = eDuelState.eDuelState_CommitEffect;

			return;
		}

		//こちらが１で相手が１１以上の時
		if (playerPower == 1) {
			if (enemyPower > 10) {
				//１ダメージ与えて勝利
				m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Win);
				m_hpEManager.m_Damage = 1;
				m_State = eDuelState.eDuelState_CommitEffect;

				return;
			}
		}

		//相手が１でこちらが１１以上の時
		if (enemyPower == 1) {
			if (playerPower > 10) {
				//１ダメージ受けて敗北
				m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Lose);
				m_hpPManager.m_Damage = 1;
				m_State = eDuelState.eDuelState_CommitEffect;

				return;
			}
		}

		//こちらと相手でどちらが大きい数字化調べ、差分をダメージにする
		if (playerPower > enemyPower) {
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Win);
			m_hpEManager.m_Damage = playerPower - enemyPower;
			m_State = eDuelState.eDuelState_CommitEffect;
		} else {
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Lose);
			m_hpPManager.m_Damage = enemyPower - playerPower;
			m_State = eDuelState.eDuelState_CommitEffect;
		}

	}

	private void CommitEffect(){
		//どちらがダメージを受けたかを表示する
		if (m_ctModel.Move () == true) {

			m_effectModel.EffectEnd ();

			cCommitTextModel.eCommitText commit = m_ctModel.GetText ();

			if (commit == cCommitTextModel.eCommitText.eCommitText_Win) {
				m_eModel.RunbleInit ();
				m_State = eDuelState.eDuelState_CommitEffectWin;
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Slash);
			} else if (commit == cCommitTextModel.eCommitText.eCommitText_Lose) {
				m_cameraModel.RunbleInit ();
				m_State = eDuelState.eDuelState_CommitEffectLose;
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Slash);
			} else {
				m_State = eDuelState.eDuelState_CommitEffectDraw;
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Repelled);
			}
		}
	}

	private void CommitEffectWin(){
		//勝利時演出
		m_edModel.Snap ();

		m_eModel.Runble ();

		if (m_hpEManager.CutBack () == true) {

			m_eModel.StopRunble ();

			//デュエルが続くかを調べる
			if (m_hpEManager.HitPointCheck () == true) {
				m_State = eDuelState.eDuelState_Win;
			} else if (m_dModel.m_LastBattle == true) {
				m_fadeHModel.SetState (cFadeInOutModel.eFadeState.FadeOut);
				m_State = eDuelState.eDuelState_Lose;
				cSoundManager.BGMVolumeDown ();
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Lose);
			} else {
				m_State = eDuelState.eDuelState_NextButtle;
			}
		}
	}

	private void CommitEffectLose(){
		//敗北時演出
		m_dModel.Snap ();

		m_cameraModel.Runble ();

		if (m_hpPManager.CutBack () == true) {

			m_cameraModel.StopRunble ();

			//デュエルが続くかを調べる
			if (m_hpPManager.HitPointCheck () == true) {
				m_fadeHModel.SetState (cFadeInOutModel.eFadeState.FadeOut);
				m_State = eDuelState.eDuelState_Lose;
				cSoundManager.BGMVolumeDown ();
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Lose);
			} else if (m_dModel.m_LastBattle == true) {
				m_fadeHModel.SetState (cFadeInOutModel.eFadeState.FadeOut);
				m_State = eDuelState.eDuelState_Lose;
				cSoundManager.BGMVolumeDown ();
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Lose);
			} else {
				m_State = eDuelState.eDuelState_NextButtle;
			}
		}
	}

	private void CommitEffectDraw(){
		//引き分け時演出

		bool endFlag = m_dModel.Snap ();
		endFlag &= m_edModel.Snap ();

		if (endFlag == true) {
			//デュエルが続くかを調べる
			if (m_dModel.m_LastBattle == true) {
				m_fadeHModel.SetState (cFadeInOutModel.eFadeState.FadeOut);
				m_State = eDuelState.eDuelState_Lose;
				cSoundManager.BGMVolumeDown ();
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Lose);
			} else {
				m_State = eDuelState.eDuelState_NextButtle;
			}
		}
	}

	private void Win(){
		//デュエルに勝利した時
		m_edModel.SelectCardOpen ();

		if (m_eModel.End () == true) {
			if (m_winModel.GetTapFlag () == true) {
				m_gData.m_PlayerHitPoint = m_hpPManager.GetHitPoint ();
				m_dModel.BackSet ();
				m_hpPManager.BackSet ();
				m_hpEManager.BackSet ();
				m_State = eDuelState.eDuelState_End;

				m_gData.AddWin ();

				m_Win = true;
				m_gData.m_WinLose = m_Win;
			}

			m_winModel.EffectOn ();
		}
	}

	private void Lose(){
		//デュエルに敗北した時
		m_edModel.SelectCardOpen ();

		m_fadeHModel.FadeExec ();
		if (m_loseModel.GetTapFlag () == true && m_fadeHModel.GetState () == cFadeInOutModel.eFadeState.FadeOutStop) {
			m_dModel.BackSet ();
			m_hpPManager.BackSet ();
			m_hpEManager.BackSet ();

			m_fadeHModel.SetState (cFadeInOutModel.eFadeState.FadeIn);

			m_State = eDuelState.eDuelState_End;

			m_Win = false;
			m_gData.m_WinLose = m_Win;
		}

		m_loseModel.EffectOn ();
	}

	private void NextButtle(){
		bool endFlag = true;

		//バトル用カードを片づける
		endFlag = m_dModel.ButtleCardBack (m_ButtleCardFadeTime);
		endFlag = m_edModel.Back (m_ButtleCardFadeTime);

		if (endFlag == true) {
			m_State = eDuelState.eDuelState_CardEdit;
		}
	}

	public bool End(){
		bool endFlag = true;

		//UIの退去
		endFlag &= m_dModel.MoveBack (m_FadeTime);
		endFlag &= m_edModel.Back (m_FadeTime , true);
		endFlag &= m_hpPManager.Back (m_FadeTime);
		endFlag &= m_hpEManager.Back (m_FadeTime);

		return endFlag;
	}

	public bool GetButton(){
		//ボタンが押されたかどうか

		if (m_buttonModel.GetSelect () == 1) {
			m_buttonModel.Init ();
			return true;
		}

		return false;
	}
}
