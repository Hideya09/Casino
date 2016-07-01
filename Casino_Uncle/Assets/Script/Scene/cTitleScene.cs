using UnityEngine;
using System.Collections;

public class cTitleScene : cSceneBase {

	//シーン内のステート
	public enum eTitleSceneList{
		eTitleSceneList_Init,
		eTitleSceneList_Start,
		eTitleSceneList_FadeIn,
		eTitleSceneList_Main,
		eTitleSceneList_Dialog,
		eTitleSceneList_FadeOut,
		eTitleSceneList_End
	}

	private eTitleSceneList m_State;

	public cButtonModel[] m_buttonModel;

	public cFadeInOutModel m_fadeModel;
	public cFadeHalfModel m_fadeHalfModel;

	public cEndDialogModel m_dialogModel;

	public cStartEffectModel m_startModel;

	public cBlinkModel m_blinkModel;

	public cGameData m_GameData;

	void OnEnable(){
		m_State = eTitleSceneList.eTitleSceneList_Init;
	}

	public override cGameSceneManager.eGameScene SceneExec ()
	{
		switch (m_State) {
		case eTitleSceneList.eTitleSceneList_Init:
			m_GameData.Load ();

			m_fadeHalfModel.SetState (cFadeInOutModel.eFadeState.FadeIn);

			m_blinkModel.Init ();

			m_fadeModel.Init ();

			m_dialogModel.Init ();

			m_State = eTitleSceneList.eTitleSceneList_Start;
			break;
		case eTitleSceneList.eTitleSceneList_Start:
			//起動時のみの開始処理
			if (m_startModel.StartExec () == true) {
				m_State = eTitleSceneList.eTitleSceneList_Main;
				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Init ();
					m_buttonModel [i].Start ();
				}

				cSoundManager.BGMPlay ();
			}
			break;
		case eTitleSceneList.eTitleSceneList_FadeIn:
			//起動時以外でタイトルに来たときはフェード
			m_fadeModel.FadeExec ();
			m_blinkModel.Init ();
			if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeInStop) {
				m_State = eTitleSceneList.eTitleSceneList_Main;

				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Init ();
					m_buttonModel [i].Start ();
				}

				cSoundManager.BGMPlay ();
			}
			break;
		case eTitleSceneList.eTitleSceneList_Main:
			m_fadeHalfModel.FadeExec ();

			//ボタンが押されたかを調べ宇r
			for (int i = 0; i < m_buttonModel.Length; ++i) {
				int button = m_buttonModel [i].GetSelect ();
				if (button == 1) {
					//フェードアウトステートに移動
					m_State = eTitleSceneList.eTitleSceneList_FadeOut;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Start);

					m_blinkModel.Init ();

					for (int j = 0; j < m_buttonModel.Length; ++j) {
						m_buttonModel [j].End ();
					}
				} else if (button == 2) {
					m_fadeHalfModel.SetState (cFadeInOutModel.eFadeState.FadeOut);

					//ダイアログ表示ステートに移動
					m_State = eTitleSceneList.eTitleSceneList_Dialog;

					cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);

					m_dialogModel.Init ();

					for (int j = 0; j < m_buttonModel.Length; ++j) {
						m_buttonModel [j].End ();
					}
				}
			}
			break;
		case eTitleSceneList.eTitleSceneList_Dialog:
			//ゲーム終了ダイアログを表示

			m_fadeHalfModel.FadeExec ();

			m_State = m_dialogModel.TitleDialogExec ();

			if (m_State == eTitleSceneList.eTitleSceneList_Main) {
				m_fadeHalfModel.SetState (cFadeInOutModel.eFadeState.FadeIn);

				for (int i = 0; i < m_buttonModel.Length; ++i) {
					m_buttonModel [i].Init ();
					m_buttonModel [i].Start ();
				}
			}
			break;
		case eTitleSceneList.eTitleSceneList_FadeOut:
			//フェードアウト

			m_blinkModel.Blink ();

			m_fadeModel.FadeExec ();

			cSoundManager.BGMDown ();

			if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeOutStop) {
				m_State = eTitleSceneList.eTitleSceneList_FadeIn;
				//シーンを切り替える
				return cGameSceneManager.eGameScene.GameScene_Game;
			}
			break;
		case eTitleSceneList.eTitleSceneList_End:
			m_fadeModel.FadeExec ();

			cSoundManager.BGMDown ();

			if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeOutStop) {
				m_State = eTitleSceneList.eTitleSceneList_FadeIn;
				//ゲームを終了させる
				Application.Quit ();
			}
			break;
		}
		return cGameSceneManager.eGameScene.GameScene_Title;
	}

	public override void SceneInit(){
		//m_State = eTitleSceneList.eTitleSceneList_Init;
	}
}
