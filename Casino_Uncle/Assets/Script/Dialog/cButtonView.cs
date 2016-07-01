using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cButtonView : MonoBehaviour {

	public cButtonModel m_ButtonModel;

	private Image m_Sprite;

	private Color m_BaseColor;

	public Color m_SelectColor;

	// Use this for initialization
	void Start () {
		m_Sprite = GetComponent< Image > ();

		m_BaseColor = m_Sprite.color;
	}
	
	// Update is called once per frame
	void Update () {
		Color setColor = m_BaseColor;

		setColor.a = m_SelectColor.a;

		//押されている時とそうじゃない時とで色を変える
		if (m_ButtonModel.GetTap () == true) {
			m_Sprite.color = m_SelectColor;
		} else {
			m_Sprite.color = setColor;
		}
	}
}
