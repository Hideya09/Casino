using UnityEngine;
using System.Collections;

public class cCardControl : MonoBehaviour {

	public cSelectCardModel m_cModel;

	private bool m_HitFlag;

	private bool m_TouchFlag;

	// Use this for initialization
	void Start () {
		m_HitFlag = false;
		m_TouchFlag = false;

		m_cModel.InitPosition (transform.localPosition);
	}
	
	// Update is called once per frame
	void Update () {

		//入力処理
		if (Input.GetMouseButtonDown (0)) {
			if (m_HitFlag == true) {
				m_cModel.SetPosition (Input.mousePosition);
				m_TouchFlag = true;
			} else {
				m_TouchFlag = false;
			}
		}

		if (Input.GetMouseButtonUp (0)) {
			if (m_HitFlag == true) {
				m_cModel.ConfirmCard ();
			} else {
				m_cModel.UnTapCard ();
			}
		}

		if (Input.GetMouseButton (0)) {
			if (m_TouchFlag == true) {
				m_cModel.SelectCard (Input.mousePosition);
			}
		}
	}

	//マウスがオブジェクトに当たっているかを取得
	void OnMouseOver(){
		m_HitFlag = true;
	}

	void OnMouseExit(){
		m_HitFlag = false;
	}
}
