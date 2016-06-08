using UnityEngine;
using System.Collections;

public class cCardControl : MonoBehaviour {

	public cSelectCardModel m_cModel;

	private bool m_HitFlag;

	// Use this for initialization
	void Start () {
		m_HitFlag = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			if (m_HitFlag == true) {
				m_cModel.SetPosition (Input.mousePosition);
			} else {
				m_cModel.UnTapCard ();
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			if (m_HitFlag == true) {
				m_cModel.ConfirmCard ();
			}
		}

		if (Input.GetMouseButton (0)) {
			m_cModel.SelectCard (Input.mousePosition);
		}
	}

	void OnMouseOver(){
		m_HitFlag = true;
	}

	void OnMouseExit(){
		m_HitFlag = false;
	}
}
