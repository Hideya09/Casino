using UnityEngine;
using System.Collections;

public class cButtonControl : MonoBehaviour {

	private bool m_HitFlag;

	public cButtonModel m_buttonModel;

	// Use this for initialization
	void Start () {
		m_HitFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_HitFlag == true) {
			if (Input.GetMouseButtonDown (0)) {
				m_buttonModel.TapButton ();
			}

			if (Input.GetMouseButtonUp (0)) {
				m_buttonModel.SelectButton ();
				m_buttonModel.UnTapButton ();
			}
		} else {
			m_buttonModel.UnTapButton ();
		}

		if (Input.GetMouseButton (0)) {
			m_buttonModel.Touch ();
		} else {
			m_buttonModel.UnTouch ();
		}
	}

	void OnMouseOver(){
		m_HitFlag = true;
	}

	void OnMouseExit(){
		m_HitFlag = false;
	}
}
