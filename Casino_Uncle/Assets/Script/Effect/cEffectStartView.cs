using UnityEngine;
using System.Collections;

public class cEffectStartView : MonoBehaviour {

	public cEffectStartModel m_effectStartModel;

	public Transform m_TextTransform;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 scale = transform.localScale;
		scale.y = m_effectStartModel.GetSize ();
		transform.localScale = scale;

		m_TextTransform.localPosition = m_effectStartModel.GetPosition ();
	}
}
