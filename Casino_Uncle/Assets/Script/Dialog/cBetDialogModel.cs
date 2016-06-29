using UnityEngine;
using System.Collections;

public class cBetDialogModel : cDialogModel {

	private enum eBetState{
		eBetState_Start,
		eBetState_StartUp,
		eBetState_Bet,
		eBetState_Main,
		eBetState_Money,
		eBetState_End,
	}

	public cGameData m_GameData;

	public cBetMoneyModel m_betMoneyModel;

	public cTableModel m_tModel;

	private eBetState m_State;

	private cGameScene.eGameSceneList m_RetScene;

	public override cGameScene.eGameSceneList DialogExec(){
		m_NumberData [0] = m_GameData.m_Money;

		switch (m_State) {
		case eBetState.eBetState_Start:
			if (StartDown () == true) {
				m_State = eBetState.eBetState_Bet;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eBetState.eBetState_StartUp:
			if (StartUp () == true) {
				m_State = eBetState.eBetState_Bet;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eBetState.eBetState_Bet:
			if (m_betMoneyModel.GetNumber () >= 100 && m_betMoneyModel.GetNumber () <= m_NumberData [0]) {
				m_State = eBetState.eBetState_Main;
			}

			m_tModel.FadeIn ();

			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					m_buttonModel [i].Init ();
					m_buttonModel [i].Start ();
				}
				else if (number == 2) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_MoveTitle;

					m_State = eBetState.eBetState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cancel);
					break;
				}
			}
			break;
		case eBetState.eBetState_Main:
			m_tModel.FadeOut ();

			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Duel;

					m_State = eBetState.eBetState_Money;

					m_GameData.MoneyBet( m_betMoneyModel.GetNumber () );

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					break;
				} else if (number == 2) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_MoveTitle;

					m_State = eBetState.eBetState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cancel);

					break;
				}
			}

			if (m_betMoneyModel.GetNumber () < 100 || m_betMoneyModel.GetNumber () > m_NumberData [0]) {
				m_State = eBetState.eBetState_Bet;
			}

			break;
		case eBetState.eBetState_Money:
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Count);

			if (m_GameData.Bet () == true) {
				m_State = eBetState.eBetState_End;
			} else if (m_buttonModel [0].GetTouch ()) {
				m_GameData.BetSoon ();
				m_State = eBetState.eBetState_End;
			}
			break;
		case eBetState.eBetState_End:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			if( EndDown() == true ){
				return m_RetScene;
			}
			break;
		}

		return cGameScene.eGameSceneList.eGameSceneList_BetDialog;
	}

	public override void Init (bool upPosition = true){

		m_RetScene = cGameScene.eGameSceneList.eGameSceneList_BetDialog;

		m_betMoneyModel.Init ();

		if (upPosition == true) {
			InitPositionUp ();
			m_State = eBetState.eBetState_Start;
		} else {
			InitPositionDpwn ();
			m_State = eBetState.eBetState_StartUp;
		}

		for (int i = 0; i < m_buttonModel.Length; ++i) {
			m_buttonModel [i].Init ();
		}

		m_NumberData = new int[1];
		m_NumberData [0] = m_GameData.m_Money;

		m_NumberData2 = new float[5];
		for (int i = 0; i < m_NumberData2.Length; ++i) {
			m_NumberData2 [i] = m_GameData.m_PayBack [i];
		}

		m_tModel.Init ();
	}
}
