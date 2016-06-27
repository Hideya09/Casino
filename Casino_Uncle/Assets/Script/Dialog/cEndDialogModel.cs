using UnityEngine;
using System.Collections;

public class cEndDialogModel : cDialogModel {

	private enum eEndState{
		eEndState_Start,
		eEndState_DownStart,
		eEndState_Main,
		eEndState_End,
		eEndState_UpEnd
	}

	private eEndState m_State;

	private cGameScene.eGameSceneList m_RetScene;

	public cTitleScene.eTitleSceneList TitleDialogExec(){
		switch (m_State) {
		case eEndState.eEndState_Start:
			if (StartDown () == true) {
				m_State = eEndState.eEndState_Main;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eEndState.eEndState_Main:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					m_State = eEndState.eEndState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					break;
				} else if (number == 2) {
					m_State = eEndState.eEndState_UpEnd;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cancel);

					break;
				}
			}
			break;
		case eEndState.eEndState_End:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			return cTitleScene.eTitleSceneList.eTitleSceneList_End;
		case eEndState.eEndState_UpEnd:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			if (EndUp () == true) {
				return cTitleScene.eTitleSceneList.eTitleSceneList_Main;
			}
			break;
		}

		return cTitleScene.eTitleSceneList.eTitleSceneList_Dialog;
	}

	public override cGameScene.eGameSceneList DialogExec(){
		switch (m_State) {
		case eEndState.eEndState_Start:
			if (StartDown () == true) {
				m_State = eEndState.eEndState_Main;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eEndState.eEndState_DownStart:
			if (StartUp () == true) {
				m_State = eEndState.eEndState_Main;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eEndState.eEndState_Main:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Show;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					m_State = eEndState.eEndState_End;

					break;
				} else if (number == 2) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Back;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cancel);

					m_State = eEndState.eEndState_UpEnd;

					break;
				}
			}
			break;
		case eEndState.eEndState_End:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			if( EndDown() == true ){
				return m_RetScene;
			}
			break;
		case eEndState.eEndState_UpEnd:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			if( EndUp() == true ){
				return m_RetScene;
			}
			break;
		}

		return cGameScene.eGameSceneList.eGameSceneList_MoveEnd;
	}

	public override void Init (bool upPosition = true){

		m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Menu;

		if (upPosition == true) {

			m_State = eEndState.eEndState_Start;

			InitPositionUp ();
		} else {
			m_State = eEndState.eEndState_DownStart;

			InitPositionDpwn ();
		}

		for (int i = 0; i < m_buttonModel.Length; ++i) {
			m_buttonModel [i].Init ();
		}
	}
}
