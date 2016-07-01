using UnityEngine;
using System.Collections;

public class cMenuDialogModel : cDialogModel {

	//ダイアログ内のステート
	private enum eMenuState{
		eMenuState_Start,
		eMenuState_DownStart,
		eMenuState_Main,
		eMenuState_End,
		eMenuState_UpEnd
	}

	private eMenuState m_State;

	//次に移動するシーン
	private cGameScene.eGameSceneList m_RetScene;

	public override cGameScene.eGameSceneList DialogExec(){
		switch (m_State) {
		case eMenuState.eMenuState_Start:
			if (StartDown () == true) {
				m_State = eMenuState.eMenuState_Main;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eMenuState.eMenuState_DownStart:
			if (StartUp () == true) {
				m_State = eMenuState.eMenuState_Main;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eMenuState.eMenuState_Main:
			//ボタンが押されたかを調べる
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					//ゲーム内ステートをタイトルに移動させるか確認するステートに変える
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_MoveTitle;

					m_State = eMenuState.eMenuState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					break;
				} else if (number == 2) {
					//ゲーム内ステートをゲーム終了させるか確認するステートに変える
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_MoveEnd;

					m_State = eMenuState.eMenuState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					break;
				} else if (number == 3) {
					//ゲーム内ステートをデュエルに変える
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Duel;

					m_State = eMenuState.eMenuState_UpEnd;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cancel);

					break;
				}
			}
			break;
		case eMenuState.eMenuState_End:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			if( EndDown() == true ){
				return m_RetScene;
			}
			break;
		case eMenuState.eMenuState_UpEnd:
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				m_buttonModel [i].End ();
			}
			if( EndUp() == true ){
				return m_RetScene;
			}
			break;
		}

		return cGameScene.eGameSceneList.eGameSceneList_Menu;
	}

	public override void Init (bool upPosition = true){

		m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Menu;

		if (upPosition == true) {

			m_State = eMenuState.eMenuState_Start;

			InitPositionUp ();
		} else {
			m_State = eMenuState.eMenuState_DownStart;

			InitPositionDpwn ();
		}

		for (int i = 0; i < m_buttonModel.Length; ++i) {
			m_buttonModel [i].Init ();
		}
	}
}
