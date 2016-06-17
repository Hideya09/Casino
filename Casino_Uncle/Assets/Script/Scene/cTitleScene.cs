using UnityEngine;
using System.Collections;

public class cTitleScene : cSceneBase {

	public override cGameSceneManager.eGameScene SceneExec ()
	{
		return cGameSceneManager.eGameScene.GameScene_Title;
	}

	public override void SceneInit(){
	}
}
