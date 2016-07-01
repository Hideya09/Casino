using UnityEngine;
using System.Collections;

public class cNextDialogModel : cDialogModel {

	//ダイアログ内のステート
	private enum eNextState{
		eNextState_Start,
		eNextState_Blink,
		eNextState_Main,
		eNextState_End,
	}

	public cGameData m_GameData;

	public cBlinkModel m_blinkModel;

	private eNextState m_State;

	//次に移動するシーン
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
			} else if (m_buttonModel [0].GetTouch ()) {
				m_blinkModel.Init ();
				m_State = eNextState.eNextState_Main;
			}
			break;
		case eNextState.eNextState_Main:
			//ボタンが押されたか調べる
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					//ゲーム内ステートをデュエルに移動
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Duel;

					m_State = eNextState.eNextState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					break;
				} else if (number == 2) {
					//ゲーム内ステートを精算に移動
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Pay;

					m_State = eNextState.eNextState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

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

		m_blinkModel.Init2 ();

		InitPositionUp ();

		for (int i = 0; i < m_buttonModel.Length; ++i) {
			m_buttonModel [i].Init ();
		}

		//続行かの判断において必要な情報をセット
		m_NumberData = new int[5];
		m_NumberData [0] = m_GameData.GetWin ();
		m_NumberData [1] = m_GameData.GetCard ();
		m_NumberData [2] = m_GameData.m_PlayerHitPoint;
		m_NumberData [3] = m_GameData.GetPayBack (m_GameData.GetWin () + 1);
		m_NumberData [4] = m_GameData.GetPayBack();
	}
}
