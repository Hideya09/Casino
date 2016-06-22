﻿using UnityEngine;
using System.Collections;

public class cGameScene : cSceneBase {
	public cDuelStateManager m_DuelStateManager;

	public cGameData m_gData;

	private GameObject m_DialogParent;
	private GameObject m_Dialog;

	private bool m_DeleteEnd;

	public cFadeInOutModel m_fadeModel;

	public cBetDialogModel m_BetDialog;
	public cMenuDialogModel m_MenuDialog;
	public cEndDialogModel m_EndDialog;
	public cTitleDialogModel m_TitleDialog;
	public cNextDialogModel m_NextDialog;
	public cPayBackDialogModel m_PayBackDialog;
	public cShowDialogModel m_ShowDialog;

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
		eGameSceneList_Show,
		eGameSceneList_FadeOut,
		eGameSceneList_Back,
	}

	private eGameSceneList m_State;
	private eGameSceneList m_BufState;

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

			m_gData.StartMoneySet ();

			m_State = eGameSceneList.eGameSceneList_FadeIn;

			m_BufState = eGameSceneList.eGameSceneList_Init;
			break;
		case eGameSceneList.eGameSceneList_FadeIn:
			m_fadeModel.FadeExec ();
			if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeInStop) {

				GameObject obj = (GameObject)Resources.Load ("Prefab/BetDialog");
				m_Dialog = (GameObject)Instantiate (obj);
				m_Dialog.transform.SetParent (m_DialogParent.transform, false);

				m_State = eGameSceneList.eGameSceneList_BetDialog;
				
				m_BufState = eGameSceneList.eGameSceneList_FadeIn;

				cSoundManager.BGMPlay ();
			}
			break;
		case eGameSceneList.eGameSceneList_BetDialog:
			m_DuelStateManager.FadeInHalf ();
			m_State = m_BetDialog.DialogExec ();

			if (m_State != eGameSceneList.eGameSceneList_BetDialog) {
				Destroy (m_Dialog);
				m_Dialog = null;

				m_BufState = eGameSceneList.eGameSceneList_BetDialog;

				if (m_State == eGameSceneList.eGameSceneList_Duel) {
					m_DuelStateManager.Init ();
				} else {
					m_BufState = eGameSceneList.eGameSceneList_BetDialog;

					Destroy (m_Dialog);
					m_Dialog = null;

					m_TitleDialog.Init ();

					GameObject obj = (GameObject)Resources.Load ("Prefab/TitleDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform, false);
				}
			}
			break;
		case eGameSceneList.eGameSceneList_Duel:
			if (m_DuelStateManager.DuelExec () == true) {
				m_BufState = eGameSceneList.eGameSceneList_Duel;

				m_DeleteEnd = false;

				if (m_gData.GetWin () == 5 || m_DuelStateManager.GetWinNow() == false) {
					m_PayBackDialog.Init ();

					GameObject obj = (GameObject)Resources.Load ("Prefab/PayBackDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform, false);

					m_State = eGameSceneList.eGameSceneList_Pay;
				} else {
					m_NextDialog.Init ();

					GameObject obj = (GameObject)Resources.Load ("Prefab/NextDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform, false);

					m_State = eGameSceneList.eGameSceneList_Next;
				}
			}

			if (m_DuelStateManager.GetButton () == true) {
				m_BufState = eGameSceneList.eGameSceneList_Duel;

				m_MenuDialog.Init ();

				GameObject obj = (GameObject)Resources.Load ("Prefab/MenuDialog");
				m_Dialog = (GameObject)Instantiate (obj);
				m_Dialog.transform.SetParent (m_DialogParent.transform, false);

				m_State = eGameSceneList.eGameSceneList_Menu;

				m_DuelStateManager.SelectStop ();
			}
			break;
		case eGameSceneList.eGameSceneList_Menu:
			m_State = m_MenuDialog.DialogExec ();

			if (m_State == eGameSceneList.eGameSceneList_MoveEnd) {
				m_BufState = eGameSceneList.eGameSceneList_Menu;

				Destroy (m_Dialog);
				m_Dialog = null;

				m_EndDialog.Init ();

				GameObject obj = (GameObject)Resources.Load ("Prefab/EndDialog");
				m_Dialog = (GameObject)Instantiate (obj);
				m_Dialog.transform.SetParent (m_DialogParent.transform, false);
			}

			if (m_State == eGameSceneList.eGameSceneList_MoveTitle) {
				m_BufState = eGameSceneList.eGameSceneList_Menu;

				Destroy (m_Dialog);
				m_Dialog = null;

				m_TitleDialog.Init ();

				GameObject obj = (GameObject)Resources.Load ("Prefab/TitleDialog");
				m_Dialog = (GameObject)Instantiate (obj);
				m_Dialog.transform.SetParent (m_DialogParent.transform, false);
			}

			if (m_State == eGameSceneList.eGameSceneList_Duel) {
				m_BufState = eGameSceneList.eGameSceneList_Menu;

				Destroy (m_Dialog);
				m_Dialog = null;
			}
			break;
		case eGameSceneList.eGameSceneList_MoveEnd:
			m_State = m_EndDialog.DialogExec ();

			if (m_State == eGameSceneList.eGameSceneList_Show) {
				m_BufState = eGameSceneList.eGameSceneList_MoveEnd;

				Destroy (m_Dialog);
				m_Dialog = null;

				m_ShowDialog.Init ();

				GameObject obj = (GameObject)Resources.Load ("Prefab/ShowDialog");
				m_Dialog = (GameObject)Instantiate (obj);
				m_Dialog.transform.SetParent (m_DialogParent.transform, false);
			}

			if (m_State == eGameSceneList.eGameSceneList_Back) {
				Destroy (m_Dialog);
				m_Dialog = null;

				if (m_BufState == eGameSceneList.eGameSceneList_Menu) {
					m_MenuDialog.Init (false);

					GameObject obj = (GameObject)Resources.Load ("Prefab/MenuDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform, false);

					m_State = eGameSceneList.eGameSceneList_Menu;
				}

				m_BufState = eGameSceneList.eGameSceneList_MoveEnd;
			}
			break;
		case eGameSceneList.eGameSceneList_MoveTitle:
			m_State = m_TitleDialog.DialogExec ();

			if (m_State == eGameSceneList.eGameSceneList_Show) {
				m_BufState = eGameSceneList.eGameSceneList_MoveTitle;

				Destroy (m_Dialog);
				m_Dialog = null;

				m_ShowDialog.Init ();

				GameObject obj = (GameObject)Resources.Load ("Prefab/ShowDialog");
				m_Dialog = (GameObject)Instantiate (obj);
				m_Dialog.transform.SetParent (m_DialogParent.transform, false);
			}

			if (m_State == eGameSceneList.eGameSceneList_Back) {
				Destroy (m_Dialog);
				m_Dialog = null;

				m_MenuDialog.Init (false);

				if (m_BufState == eGameSceneList.eGameSceneList_Menu) {
					m_MenuDialog.Init (false);

					GameObject obj = (GameObject)Resources.Load ("Prefab/MenuDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform, false);

					m_State = eGameSceneList.eGameSceneList_Menu;
				} else {
					m_BetDialog.Init (false);

					GameObject obj = (GameObject)Resources.Load ("Prefab/BetDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform, false);

					m_State = eGameSceneList.eGameSceneList_BetDialog;
				}

				m_BufState = eGameSceneList.eGameSceneList_MoveTitle;
			}
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
				m_BufState = eGameSceneList.eGameSceneList_Next;

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
				m_BufState = eGameSceneList.eGameSceneList_Pay;

				Destroy (m_Dialog);
				m_Dialog = null;

				if (m_gData.m_Money < 100) {
					m_ShowDialog.Init ();

					m_State = eGameSceneList.eGameSceneList_Show;

					GameObject obj = (GameObject)Resources.Load ("Prefab/ShowDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform, false);
				} else {
					m_BetDialog.Init ();

					GameObject obj = (GameObject)Resources.Load ("Prefab/BetDialog");
					m_Dialog = (GameObject)Instantiate (obj);
					m_Dialog.transform.SetParent (m_DialogParent.transform, false);
				}
			}
			break;
		case eGameSceneList.eGameSceneList_Show:
			m_State = m_ShowDialog.DialogExec ();
			break;
		case eGameSceneList.eGameSceneList_FadeOut:

			cSoundManager.BGMDown ();

			m_fadeModel.FadeExec ();
			if (m_fadeModel.GetState () == cFadeInOutModel.eFadeState.FadeOutStop) {
				m_gData.Save ();

				if (m_BufState == eGameSceneList.eGameSceneList_MoveEnd) {
					Application.Quit ();
				} else {
					Destroy (m_Dialog);
					m_Dialog = null;
					m_State = eGameSceneList.eGameSceneList_Init;
				}

				return cGameSceneManager.eGameScene.GameScene_Title;
			}
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
