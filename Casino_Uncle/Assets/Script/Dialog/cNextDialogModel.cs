using UnityEngine;
using System.Collections;

public class cNextDialogModel : cDialogModel {

	private enum eNextState{
		eNextState_Start,
		eNextState_Blink,
		eNextState_Main,
		eNextState_End,
	}

	public cGameData m_GameData;

	public cBlinkModel m_blinkModel;

	private eNextState m_State;

	private cGameScene.eGameSceneList m_RetScene;

	public override cGameScene.eGameSceneList DialogExec(){
		switch (m_State) {
		case eNextState.eNextState_Start:
			if (StartDown () == true) {
				m_State = eNextState.eNextState_Blink;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eNextState.eNextState_Blink:
			if (m_blinkModel.Blink () == true) {
				m_State = eNextState.eNextState_Main;
			}
			break;
		case eNextState.eNextState_Main:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Duel;

					m_State = eNextState.eNextState_End;

					break;
				} else if (number == 2) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Pay;

					m_State = eNextState.eNextState_End;

					break;
				}
			}
			break;
		case eNextState.eNextState_End:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			if( EndDown() == true ){
				return m_RetScene;
			}
			break;
		}

		return cGameScene.eGameSceneList.eGameSceneList_Next;
	}

	public override void Init (bool upPosition = true){

		m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Next;

		m_State = eNextState.eNextState_Start;

		m_blinkModel.Init ();

		InitPositionUp ();

		for (int i = 0; i < m_buttonModel.Length; ++i) {
			m_buttonModel [i].Init ();
		}

		m_NumberData = new int[5];
		m_NumberData [0] = m_GameData.GetWin ();
		m_NumberData [1] = m_GameData.GetCard ();
		m_NumberData [2] = m_GameData.m_PlayerHitPoint;
		m_NumberData [3] = (int)m_GameData.m_Money;
		m_NumberData [4] = m_GameData.GetPayBack();

	}
}
