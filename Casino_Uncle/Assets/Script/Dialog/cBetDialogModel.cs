using UnityEngine;
using System.Collections;

public class cBetDialogModel : cDialogModel {

	//ダイアログ内のステート
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

	public cWarningModel m_wModel;

	private eBetState m_State;

	//次に移動するシーン
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
			//数値が範囲内なら次のステートへ
			if (m_betMoneyModel.GetNumber () >= 100 && m_betMoneyModel.GetNumber () <= m_NumberData [0]) {
				//ベット額の更新
				m_GameData.MoneyBet (m_betMoneyModel.GetNumber ());
				m_NumberData [1] = m_GameData.GetPayBack (5);
				m_State = eBetState.eBetState_Main;
			}else {
				//文字を切り替える
				m_wModel.NoncomformityIn ();
			}

			//ボタンが押されたかを調べる
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					//初期化
					m_buttonModel [i].Init ();
					m_buttonModel [i].Start ();
				}
				else if (number == 2) {
					//ゲーム内ステートをタイトルに移動させるか確認するステートに変える
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_MoveTitle;

					m_State = eBetState.eBetState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cancel);
					break;
				}
			}
			break;
		case eBetState.eBetState_Main:
			//文字を切り替える
			m_wModel.WarningIn ();
			//ベット額の更新
			m_GameData.MoneyBet (m_betMoneyModel.GetNumber ());
			m_NumberData [1] = m_GameData.GetPayBack (5);

			//ボタンが押されたかを調べる
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int number = m_buttonModel [i].GetSelect ();
				if (number == 1) {
					//ゲーム内ステートをデュエルに変える
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_Duel;

					m_State = eBetState.eBetState_Money;

					m_GameData.MoneyBet( m_betMoneyModel.GetNumber () );

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					break;
				} else if (number == 2) {
					//ゲーム内ステートをタイトルに移動させるか確認するステートに変える
					m_RetScene = cGameScene.eGameSceneList.eGameSceneList_MoveTitle;

					m_State = eBetState.eBetState_End;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Cancel);

					break;
				}
			}

			//範囲外になったら前のステートに戻す
			if (m_betMoneyModel.GetNumber () < 100 || m_betMoneyModel.GetNumber () > m_NumberData [0]) {
				m_State = eBetState.eBetState_Bet;
				m_GameData.MoneyBet (0);
				m_NumberData [1] = m_GameData.GetPayBack (5);
			}

			break;
		case eBetState.eBetState_Money:
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Count);

			//ベット額分所持金を減らす
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

		m_betMoneyModel.SetNumber (m_GameData.GetBet ());

		//所持金と稼げる貞井金額をセット
		m_NumberData = new int[2];
		m_NumberData [0] = m_GameData.m_Money;
		m_NumberData [1] = m_GameData.GetPayBack (5);

		//倍率をセット
		m_NumberData2 = new float[5];
		for (int i = 0; i < m_NumberData2.Length; ++i) {
			m_NumberData2 [i] = m_GameData.m_PayBack [i];
		}

		m_wModel.Init ();
	}
}
