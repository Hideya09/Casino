using UnityEngine;
using System.Collections;

public abstract class cSceneBase : ScriptableObject {
	//しーんごとの処理
	public abstract cGameSceneManager.eGameScene SceneExec();

	//シーンの初期化
	public abstract void SceneInit();
}
