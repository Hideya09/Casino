using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cTableView : MonoBehaviour {
	public cTableModel m_tModel;

	public Sprite m_Warning;
	public Sprite m_Table;

	public Image m_Image;

	public Text[] m_text;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_tModel.GetDrawFlag () == true) {
			m_Image.sprite = m_Table;

			for (int i = 0; i < m_text.Length; ++i) {
				Color textColor = m_text[i].color;
				textColor.a = m_tModel.GetFade ();

				m_text [i].color = textColor;
			}
		} else {
			m_Image.sprite = m_Warning;

			for (int i = 0; i < m_text.Length; ++i) {
				Color textColor = m_text[i].color;
				textColor.a = 0.0f;

				m_text [i].color = textColor;
			}
		}

		Color imageColor = m_Image.color;
		imageColor.a = m_tModel.GetFade ();

		m_Image.color = imageColor;
	}
}
