using UnityEngine;
using UnityEditor;

public class cScriptable : MonoBehaviour {
	void Start(){
		//スクリプタブルオブジェクトの作成
		cEffectWinModel obj = ScriptableObject.CreateInstance<cEffectWinModel> ();

		string path = AssetDatabase.GenerateUniqueAssetPath ("Assets/Resources/Scriptable/" + typeof( cEffectWinModel ) + ".asset");

		AssetDatabase.CreateAsset (obj, path);
		AssetDatabase.SaveAssets ();
	}
}