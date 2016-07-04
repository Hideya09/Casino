using UnityEngine;
using System.Collections;

public class cTapEffectCreateView : MonoBehaviour {

	public cTapEffectModel m_tapModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_tapModel.EffectOn () == true) {
			GameObject prefab = (GameObject)Resources.Load ("Prefab/TapEffect");
			GameObject effect = (GameObject)Instantiate (prefab, m_tapModel.GetMakePosition (), Quaternion.Euler (10, -180, -180));

			effect.transform.SetParent (transform);
		}
	}
}
