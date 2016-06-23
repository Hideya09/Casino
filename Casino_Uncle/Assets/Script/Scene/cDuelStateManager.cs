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
	public cFadeHalfModel m_fadeHModel;
	public cEffectWinModel m_winModel;
	public cEffectLoseModel m_loseModel;
	public cEffectStartModel m_startModel;
	public cButtonModel m_buttonModel;

	public float cSwingTime;
	public float cSwingDownTime;

	private int m_Damage;

	private bool m_Win;

	public enum eDuelState{
		eDuelState_BattleInit,
		eDuelState_Start,
		eDuelState_Start2,
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
		m_hpEManager.Init ();
		m_gData.InitWin ();
		m_gData.InitCard ();
		m_effectModel.Init ();
		m_eModel.Init ();
		m_buttonModel.Init ();

		m_Win = false;

		m_State = eDuelState.eDuelState_BattleInit;
	}

	public void DeleteText(){
		if (m_Win == true) {
			m_gData.AddWin ();
		}

		m_Win = false;

		m_winModel.Init ();
		m_loseModel.Init ();
		m_eModel.Init ();
	}

	public bool GetWinNow(){
		return m_Win;
	}

	public bool GetLast(){
		return m_dModel.m_LastBattle;
	}

	public void FadeInHalf(){
		m_fadeHModel.FadeExec ();
	}

	public void SelectStop(){
		m_dModel.SelectStop ();
	}

	private void BattleInit(){
		m_hpEManager.Init ();
		m_edModel.Init ();
		m_eModel.Init ();

		m_startModel.Init ();

		m_dModel.MoveSet ();
		m_hpPManager.MoveSet ();
		m_hpEManager.FadeInit ();

		m_Win = false;

		++m_State;
	}

	private void Start(){
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
		m_dModel.RandomSet ();
		m_edModel.Hind ();
		m_gData.CardMinus ();

		if (m_dModel.m_DoubleBattle) {
			m_gData.SetDouble ();
		}
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


		bool edit = m_dModel.EditCard () == true;
		m_edModel.Hind ();
		m_gData.CardMinus ();

		if (m_dModel.m_DoubleBattle) {
			m_gData.SetDouble ();
		}

		if (edit == true) {
			m_State = eDuelState.eDuelState_HandOut;
		} else {
			m_State = eDuelState.eDuelState_Select;
		}
	}

	private void Select(){
		m_buttonModel.Start ();

		if (m_dModel.CardCheck ()) {
			m_buttonModel.End ();
			++m_State;
		}
	}

	private void Move(){
		m_dModel.CardMove ();
		m_cameraModel.MoveSet (cSwingTime, cSwingDownTime);
		m_dModel.MoveAngleSet (cSwingTime, cSwingDownTime);

		cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Confirm);

		++m_State;
	}

	private void EnemyShuffle(){
		m_edModel.Select ( cSwingDownTime );
		cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cock);
		++m_State;
	}

	private void BattleEffect(){
		bool moveEndFlag;

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

		if (playerPower == enemyPower) {
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Draw);
			m_State = eDuelState.eDuelState_CommitEffect;

			return;
		}

		if (playerPower == 0) {
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Win);
			m_hpEManager.m_Damage = 1;
			m_State = eDuelState.eDuelState_CommitEffect;

			return;
		}

		if (enemyPower == 0) {
			m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Lose);
			m_hpPManager.m_Damage = 1;
			m_State = eDuelState.eDuelState_CommitEffect;

			return;
		}

		if (playerPower == 1) {
			if (enemyPower > 10) {
				m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Win);
				m_hpEManager.m_Damage = 1;
				m_State = eDuelState.eDuelState_CommitEffect;

				return;
			}
		}

		if (enemyPower == 1) {
			if (playerPower > 10) {
				m_ctModel.SetText (cCommitTextModel.eCommitText.eCommitText_Lose);
				m_hpPManager.m_Damage = 1;
				m_State = eDuelState.eDuelState_CommitEffect;

				return;
			}
		}

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
		m_edModel.Snap ();

		m_eModel.Runble ();

		if (m_hpEManager.CutBack () == true) {

			m_eModel.StopRunble ();

			if (m_hpEManager.HitPointCheck () == true) {
				m_State = eDuelState.eDuelState_Win;
			} else if (m_dModel.m_LastBattle == true) {
				m_fadeHModel.SetState (cFadeInOutModel.eFadeState.FadeOut);
				m_State = eDuelState.eDuelState_Lose;
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Lose);
			} else {
				m_State = eDuelState.eDuelState_CardEdit;
			}
		}
	}

	private void CommitEffectLose(){
		m_dModel.Snap ();

		m_cameraModel.Runble ();

		if (m_hpPManager.CutBack () == true) {

			m_cameraModel.StopRunble ();

			if (m_hpPManager.HitPointCheck () == true) {
				m_fadeHModel.SetState (cFadeInOutModel.eFadeState.FadeOut);
				m_State = eDuelState.eDuelState_Lose;
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Lose);
			} else if (m_dModel.m_LastBattle == true) {
				m_fadeHModel.SetState (cFadeInOutModel.eFadeState.FadeOut);
				m_State = eDuelState.eDuelState_Lose;
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Lose);
			} else {
				m_State = eDuelState.eDuelState_CardEdit;
			}
		}
	}

	private void CommitEffectDraw(){
		m_dModel.Snap ();
		m_edModel.Snap ();

		if (m_dModel.m_LastBattle == true) {
			m_fadeHModel.SetState (cFadeInOutModel.eFadeState.FadeOut);
			m_State = eDuelState.eDuelState_Lose;
		} else {
			m_State = eDuelState.eDuelState_CardEdit;
		}
	}

	private void Win(){
		if (m_eModel.End () == true) {
			if (m_winModel.GetTapFlag () == true) {
				m_gData.m_PlayerHitPoint = m_hpPManager.GetHitPoint ();
				m_dModel.BackSet ();
				m_hpPManager.BackSet ();
				m_hpEManager.BackSet ();
				m_State = eDuelState.eDuelState_End;

				m_Win = true;
			}

			m_winModel.EffectOn ();
		}
	}

	private void Lose(){
		m_fadeHModel.FadeExec ();
		if (m_loseModel.GetTapFlag () == true && m_fadeHModel.GetState () == cFadeInOutModel.eFadeState.FadeOutStop) {
			m_gData.InitWin ();
			m_dModel.BackSet ();
			m_hpPManager.BackSet ();
			m_hpEManager.BackSet ();

			m_fadeHModel.SetState (cFadeInOutModel.eFadeState.FadeIn);

			m_State = eDuelState.eDuelState_End;

			m_Win = false;
		}

		m_loseModel.EffectOn ();
	}

	public bool End(){
		bool endFlag = true;

		endFlag &= m_dModel.MoveBack ();
		endFlag &= m_edModel.Back ();
		endFlag &= m_hpPManager.Back ();
		endFlag &= m_hpEManager.Back ();

		return endFlag;
	}

	public bool GetButton(){
		if (m_buttonModel.GetSelect () == 1) {
			m_buttonModel.Init ();
			return true;
		}

		return false;
	}
}
