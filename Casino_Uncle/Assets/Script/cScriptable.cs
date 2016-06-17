using UnityEngine;
using UnityEditor;

public class cScriptable : MonoBehaviour {
	void Start(){
		//スクリプタブルオブジェクトの作成
		cBetDialogModel obj = ScriptableObject.CreateInstance<cBetDialogModel> ();

		string path = AssetDatabase.GenerateUniqueAssetPath ("Assets/Resources/Scriptable/" + typeof( cBetDialogModel ) + ".asset");

		AssetDatabase.CreateAsset (obj, path);
		AssetDatabase.SaveAssets ();
	}
}