using UnityEngine;
using System.Collections;

public class cTitleDialogModel : cDialogModel {

	private enum eTitleState{
		eTitleState_Start,
		eTitleState_DownStart,
		eTitleState_Main,
		eTitleState_End,
		eTitleState_UpEnd
	}

	private eTitleState m_State;

	private cGameScene.eGameSceneList m_RetScene;

	public override cGameScene.eGameSceneList DialogExec(){
		switch (m_State) {
		case eTitleState.eTitleState_Start:
			if (StartDown () == true) {
				m_State = eTitleState.eTitleState_Main;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eTitleState.eTitleState_DownStart:
			if (StartUp () == true) {
				m_State = eTitleState.eTitleState_Main;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eTitleState.eTitleState_Main:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Show;

					m_State = eTitleState.eTitleState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					break;
				} else if (number == 2) {
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Back;

					m_State = eTitleState.eTitleState_UpEnd;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cancel);

					break;
				}
			}
			break;
		case eTitleState.eTitleState_End:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			if( EndDown() == true ){
				return m_RetScene;
			}
			break;
		case eTitleState.eTitleState_UpEnd:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			if( EndUp() == true ){
				return m_RetScene;
			}
			break;
		}

		return cGameScene.eGameSceneList.eGameSceneList_MoveTitle;
	}

	public override void Init (bool upPosition = true){

		m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Menu;

		if (upPosition == true) {

			m_State = eTitleState.eTitleState_Start;

			InitPositionUp ();
		} else {
			m_State = eTitleState.eTitleState_DownStart;

			InitPositionDpwn ();
		}

		for (int i = 0; i < m_buttonModel.Length; ++i) {
			m_buttonModel [i].Init ();
		}
	}
}
