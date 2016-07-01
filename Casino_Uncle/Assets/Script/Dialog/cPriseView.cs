using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cPriseView : MonoBehaviour {

	public cGameData m_GameData;
	public Text m_Text;

	private Color m_BaseColor;

	// Use this for initialization
	void Start () {
		m_BaseColor = m_Text.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (m_GameData.m_WinLose == true) {
			m_Text.color = m_BaseColor;
		} else {
			m_Text.color = new Color (1.0f, 0.0f, 0.0f, 1.0f);
		}
	}
}
