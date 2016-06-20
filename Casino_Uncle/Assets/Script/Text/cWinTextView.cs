using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cWinTextView : MonoBehaviour {

	public cGameData m_GameData;

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
		} else {
			m_Image.enabled = false;
		}
	}
}
