using UnityEngine;
using System.Collections;

public class cGameScene : cSceneBase {
	public cDuelStateManager m_DuelStateManager;

	public cGameData m_gData;

	private GameObject m_DialogParent;
	private GameObject m_Dialog;

	private bool m_DeleteEnd;

	public cFadeInOutModel m_fadeModel;

	public cBetDialogModel m_BetDialog;
	public cNextDialogModel m_NextDialog;
	public cPayBackDialogModel m_PayBackDialog;

	public enum eGameSceneList{
		eGameSceneList_Init,
		eGameSceneList_FadeIn,
		eGameSceneList_BetDialog,
		eGameSceneList_Duel,
		eGameSceneList_Menu,
		eGameSceneList_MoveEnd,
		eGameSceneList_MoveTitle,
		eGameSceneList_Next,
		eGameSceneList_Pay,
		eGameSceneList_Show
	}

	private eGameSceneList m_State;

	void OnEnable(){
		m_State = eGameSceneList.eGameSceneList_Init;
	}

	public override cGameSceneManager.eGameScene SceneExec ()
	{
		switch (m_State) {
		case eGameSceneList.eGameSceneList_Init:
			m_DuelStateManager.Init ();

			m_DialogParent = GameObject.Find ("Canvas");

			m_BetDialog.Init ();

			GameObject prefab = (GameObject)Resources.Load ("Prefab/BetDialog");
			m_Dialog = (GameObject)Instantiate (prefab);
			m_Dialog.transform.SetParent (m_DialogParent.transform, false);

			m_State = eGameSceneList.eGameSceneList_BetDialog;
			break;
		case eGameSceneList.eGameSceneList_FadeIn:
			m_NextDialog.DialogExec ();
			break;
		case eGameSceneList.eGameSceneList_BetDialog:
			m_DuelStateManager.FadeInHalf ();
			m_State = m_BetDialog.DialogExec ();

			if (m_State != eGameSceneList.eGameSceneList_BetDialog) {
				Destroy (m_Dialog);
				m_Dialog = null;

				m_DuelStateManager.Init ();
			}
			break;
		case eGameSceneList.eGameSceneList_Duel:
			if (m_DuelStateManager.DuelExec () == true) {
				m_DeleteEnd = false;

				if (m_gData.GetWin () == 5 || m_gData.GetWin () == 0) {
					m_PayBackDialog.Init ();

					GameObject obj = (GameObject)Resources.Load ("Prefab/PayBackDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform , false);

					m_State = eGameSceneList.eGameSceneList_Pay;
				} else {
					m_NextDialog.Init ();

					GameObject obj = (GameObject)Resources.Load ("Prefab/NextDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform, false);

					m_State = eGameSceneList.eGameSceneList_Next;
				}
			}
			break;
		case eGameSceneList.eGameSceneList_Menu:
			m_DuelStateManager.DuelExec ();
			break;
		case eGameSceneList.eGameSceneList_MoveEnd:
			m_DuelStateManager.DuelExec ();
			break;
		case eGameSceneList.eGameSceneList_MoveTitle:
			m_DuelStateManager.DuelExec ();
			break;
		case eGameSceneList.eGameSceneList_Next:
			m_State = m_NextDialog.DialogExec ();

			if (m_DeleteEnd == false) {
				if (m_DuelStateManager.End () == true) {
					m_DuelStateManager.DeleteText ();

					m_DeleteEnd = true;
				}
			}

			if (m_State != eGameSceneList.eGameSceneList_Next) {
				Destroy (m_Dialog);
				m_Dialog = null;
				if (m_State == eGameSceneList.eGameSceneList_Pay) {

					m_PayBackDialog.Init ();

					GameObject obj = (GameObject)Resources.Load ("Prefab/PayBackDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform, false);
				}
			}
			break;
		case eGameSceneList.eGameSceneList_Pay:
			m_State = m_PayBackDialog.DialogExec ();

			if (m_DeleteEnd == false) {
				if (m_DuelStateManager.End () == true) {
					m_DuelStateManager.DeleteText ();

					m_DeleteEnd = true;
				}
			}

			if (m_State != eGameSceneList.eGameSceneList_Pay) {
				Destroy (m_Dialog);
				m_Dialog = null;

				m_BetDialog.Init ();

				GameObject obj = (GameObject)Resources.Load ("Prefab/BetDialog");
				m_Dialog = (GameObject)Instantiate (obj);
				m_Dialog.transform.SetParent (m_DialogParent.transform, false);
			}
			break;
		case eGameSceneList.eGameSceneList_Show:
			m_DuelStateManager.DuelExec ();
			break;
		}

		//m_DuelStateManager.SceneExec ();

		return cGameSceneManager.eGameScene.GameScene_Game;
	}

	public override void SceneInit(){
		m_DialogParent = GameObject.Find ("Canvas");
		m_Dialog = null;
	}
}
