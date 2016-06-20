using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cButtonView : MonoBehaviour {

	public cButtonModel m_ButtonModel;

	private Image m_Sprite;

	private Color m_BaseColor;

	// Use this for initialization
	void Start () {
		m_Sprite = GetComponent< Image > ();

		m_BaseColor = m_Sprite.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_ButtonModel.GetTap () == true) {
			m_Sprite.color = new Color (1.0f, 1.0f, 1.0f, 1.0f);
		} else {
			m_Sprite.color = m_BaseColor;
		}
	}
}
