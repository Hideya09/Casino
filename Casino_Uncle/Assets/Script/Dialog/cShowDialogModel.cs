﻿using UnityEngine;
using System.Collections;

public class cShowDialogModel : cDialogModel {

	private enum eShowState{
		eShowState_Start,
		eShowState_Blink,
		eShowState_Main,
		eShowState_End,
	}

	public cGameData m_GameData;

	public cBlinkModel m_blinkModel;

	private eShowState m_State;

	private cGameScene.eGameSceneList m_RetScene;

	public override cGameScene.eGameSceneList DialogExec(){
		switch (m_State) {
		case eShowState.eShowState_Start:
			if (StartDown () == true) {
				if (m_GameData.m_Money < 100) {
					m_State = eShowState.eShowState_Blink;
				} else {
					m_State = eShowState.eShowState_Main;
				}
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eShowState.eShowState_Blink:
			if (m_blinkModel.Blink () == true) {
				m_State = eShowState.eShowState_Main;
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Lose);
			}
			break;
		case eShowState.eShowState_Main:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_FadeOut;

					m_State = eShowState.eShowState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					break;
				}
			}
			break;
		case eShowState.eShowState_End:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			return m_RetScene;
		}

		return cGameScene.eGameSceneList.eGameSceneList_Show;
	}

	public override void Init (bool upPosition = true){

		m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Show;

		m_State = eShowState.eShowState_Start;

		InitPositionUp ();

		m_blinkModel.Init2 ();

		for (int i = 0; i < m_buttonModel.Length; ++i) {
			m_buttonModel [i].Init ();
		}

		m_NumberData = new int[2];
		m_NumberData [0] = m_GameData.m_Money;
		m_NumberData [1] = m_GameData.GetProfit ();

	}
}
