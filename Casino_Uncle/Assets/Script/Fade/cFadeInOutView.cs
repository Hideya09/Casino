using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cFadeInOutView : MonoBehaviour {

	public cFadeInOutModel m_fadeModel;
	public Image m_Sprite;

	public bool m_MainFade;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//α値を取得
		Color color = new Color (0.0f, 0.0f, 0.0f, m_fadeModel.GetArpha ());

		m_Sprite.color = color;

		if (m_MainFade == true) {
			if (m_fadeModel.GetArpha() > 0) {
				transform.SetAsLastSibling ();

				m_Sprite.enabled = true;
			} else {
				m_Sprite.enabled = false;
			}
		}
	}
}
