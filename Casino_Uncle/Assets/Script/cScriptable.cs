using UnityEngine;
using UnityEditor;

public class cScriptable : MonoBehaviour {
	void Start(){
		//スクリプタブルオブジェクトの作成
		cShowDialogModel obj = ScriptableObject.CreateInstance<cShowDialogModel> ();

		string path = AssetDatabase.GenerateUniqueAssetPath ("Assets/Resources/Scriptable/" + typeof( cShowDialogModel ) + ".asset");

		AssetDatabase.CreateAsset (obj, path);
		AssetDatabase.SaveAssets ();
	}
}