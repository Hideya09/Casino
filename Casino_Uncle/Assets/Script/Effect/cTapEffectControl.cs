using UnityEngine;
using System.Collections;

public class cTapEffectControl : MonoBehaviour {

	public cTapEffectModel m_tapModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp (0)) {
			Vector3 position = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			position.z = 50;
			m_tapModel.EffectOn (position);
		}
	}
}
