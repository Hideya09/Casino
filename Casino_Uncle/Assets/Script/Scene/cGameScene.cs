using UnityEngine;
using System.Collections;

public class cGameScene : cSceneBase {
	public cDuelStateManager m_DuelStateManager;

	public cFadeInOutModel m_fadeModel;

	public cDialogModel m_Dialog;

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
			m_Dialog.Init ();
			m_State = eGameSceneList.eGameSceneList_Duel;
			break;
		case eGameSceneList.eGameSceneList_FadeIn:
			m_Dialog.DialogExec ();
			break;
		case eGameSceneList.eGameSceneList_BetDialog:
			
			break;
		case eGameSceneList.eGameSceneList_Duel:
			if (m_DuelStateManager.DuelExec () == true) {
				m_Dialog.Init ();
				m_State = eGameSceneList.eGameSceneList_Next;
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
			m_State = m_Dialog.DialogExec ();
			break;
		case eGameSceneList.eGameSceneList_Pay:
			m_DuelStateManager.DuelExec ();
			break;
		case eGameSceneList.eGameSceneList_Show:
			m_DuelStateManager.DuelExec ();
			break;
		}

		//m_DuelStateManager.SceneExec ();

		return cGameSceneManager.eGameScene.GameScene_Game;
	}


}
