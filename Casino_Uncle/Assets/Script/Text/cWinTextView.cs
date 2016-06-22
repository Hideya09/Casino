using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cWinTextView : MonoBehaviour {

	public cGameData m_GameData;

	public cDeckModel m_dModel;

	private Image m_Image;

	public Sprite[] m_Sprite;

	// Use this for initialization
	void Start () {
		m_Image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_GameData.GetWin () > 0) {
			m_Image.enabled = true;

			m_Image.sprite = m_Sprite [m_GameData.GetWin () - 1];

			if (m_dModel != null) {
				m_Image.color = new Color (1.0f, 1.0f, 1.0f, m_dModel.m_Fade);
			}
		} else {
			m_Image.enabled = false;
		}
	}
}
