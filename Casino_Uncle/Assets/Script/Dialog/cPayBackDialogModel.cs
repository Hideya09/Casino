﻿using UnityEngine;
using System.Collections;

public class cPayBackDialogModel : cDialogModel {

	private enum ePayBackState{
		ePayBackState_Start,
		ePayBackState_Blink,
		ePayBackState_Money,
		ePayBackState_Main,
		ePayBackState_End,
	}

	public cGameData m_GameData;

	public cBlinkModel m_blinkModel;

	private ePayBackState m_State;

	private cGameScene.eGameSceneList m_RetScene;

	public override cGameScene.eGameSceneList DialogExec(){
		m_NumberData [0] = m_GameData.m_Money;
		m_NumberData [1] = m_GameData.m_Prise;

		switch (m_State) {
		case ePayBackState.ePayBackState_Start:
			if (StartDown () == true) {
				m_State = ePayBackState.ePayBackState_Blink;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();

					if (m_GameData.GetWin () == 5) {
						cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_FiveWin);
					}
				}
			}
			break;
		case ePayBackState.ePayBackState_Blink:
			if (m_blinkModel.Blink () == true) {
				m_State = ePayBackState.ePayBackState_Money;
			} else if (m_buttonModel [0].GetTouch ()) {
				m_blinkModel.Init ();
				m_State = ePayBackState.ePayBackState_Money;
			}
			break;
		case ePayBackState.ePayBackState_Money:

			if (m_GameData.PriseReturn () == true) {
				m_State = ePayBackState.ePayBackState_Main;
				break;
			} else if (m_buttonModel [0].GetTouch ()) {
				m_GameData.PriseReturnSoon ();
				m_State = ePayBackState.ePayBackState_Main;
			}
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Count);
			break;
		case ePayBackState.ePayBackState_Main:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_BetDialog;

					m_State = ePayBackState.ePayBackState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					break;
				}
			}
			break;
		case ePayBackState.ePayBackState_End:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			if( EndDown() == true ){
				return m_RetScene;
			}
			break;
		}

		return cGameScene.eGameSceneList.eGameSceneList_Pay;
	}

	public override void Init (bool upPosition = true){

		m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Next;

		m_State = ePayBackState.ePayBackState_Start;

		m_blinkModel.Init2 ();

		InitPositionUp ();

		for (int i = 0; i < m_buttonModel.Length; ++i) {
			m_buttonModel [i].Init ();
		}

		m_NumberData = new int[3];
		m_NumberData [0] = m_GameData.m_Money;
		m_NumberData [1] = m_GameData.m_Prise;

	}
}
