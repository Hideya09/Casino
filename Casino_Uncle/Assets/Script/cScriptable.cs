using UnityEngine;
using UnityEditor;

public class cScriptable : MonoBehaviour {
	void Start(){
		//スクリプタブルオブジェクトの作成
		cFadeHalfModel obj = ScriptableObject.CreateInstance<cFadeHalfModel> ();

		string path = AssetDatabase.GenerateUniqueAssetPath ("Assets/Resources/Scriptable/" + typeof( cFadeHalfModel ) + ".asset");

		AssetDatabase.CreateAsset (obj, path);
		AssetDatabase.SaveAssets ();
	}
}