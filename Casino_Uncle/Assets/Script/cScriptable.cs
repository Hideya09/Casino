using UnityEngine;
using UnityEditor;

public class cScriptable : MonoBehaviour {
	void Start(){
		//スクリプタブルオブジェクトの作成
		cTableModel obj = ScriptableObject.CreateInstance<cTableModel> ();

		string path = AssetDatabase.GenerateUniqueAssetPath ("Assets/Resources/Scriptable/" + typeof( cTableModel ) + ".asset");

		AssetDatabase.CreateAsset (obj, path);
		AssetDatabase.SaveAssets ();
	}
}