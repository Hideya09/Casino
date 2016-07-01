using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cWarningView : MonoBehaviour {
	public cWarningModel m_wModel;

	//描画するもの
	public Sprite m_Warning;
	public Sprite m_Noncomformity;

	public Image m_Image;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//描画の切り替え
		if (m_wModel.GetDrawFlag () == cWarningModel.eDrawType.eDrawType_Warning) {
			m_Image.sprite = m_Warning;
		} else {
			m_Image.sprite = m_Noncomformity;
		}

		m_Image.color = new Color (1.0f, 1.0f, 1.0f, m_wModel.GetFade ());

		m_Image.SetNativeSize ();
	}
}
