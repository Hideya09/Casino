using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cHitPointView : MonoBehaviour {

	public Image m_Image;
	public cHitPointModel m_hpModel;

	// Use this for initialization
	void Start () {
		m_hpModel.SetPosition (transform.localPosition);
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = m_hpModel.GetPosition ();

		m_Image.color = new Color (1.0f, 1.0f, 1.0f, m_hpModel.m_Color);
	}
}
