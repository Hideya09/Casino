using UnityEngine;
using System.Collections;

public class cGameScene : cSceneBase {

	public cDeckModel m_dModel;

	public enum eGameSceneList{
		eGameSceneList_Init,
		eGameSceneList_Shuffle,
		eGameSceneList_Select,
		eGameSceneList_Move,
		eGameSceneList_End,
		eGameSceneList_FadeIn,
	}

	private eGameSceneList m_State;

	void OnEnable(){
		m_State = eGameSceneList.eGameSceneList_Init;
	}

	public override cGameSceneManager.eGameScene SceneExec ()
	{
		switch (m_State) {
		case eGameSceneList.eGameSceneList_Init:
			m_dModel.Init ();
			++m_State;
			break;
		case eGameSceneList.eGameSceneList_Shuffle:
			m_dModel.RandomSet ();
			++m_State;
			break;
		case eGameSceneList.eGameSceneList_Select:
			if (m_dModel.CardCheck ()) {
				++m_State;
			}
			break;
		case eGameSceneList.eGameSceneList_Move:
			m_dModel.CardMove ();
			++m_State;
			break;
		case eGameSceneList.eGameSceneList_End:
			m_dModel.DuelEnd ();
			if (m_dModel.m_LastBattle == true) {
				m_State = eGameSceneList.eGameSceneList_FadeIn;
			} else {
				m_State = eGameSceneList.eGameSceneList_Shuffle;
			}
			break;
		}
		return cGameSceneManager.eGameScene.GameScene_Game;
	}
}
