using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cCardNumberView : MonoBehaviour {

	public cDeckModel m_dModel;

	public cGameData m_GameData;

	private Image m_Image;

	public Sprite[] m_Sprite;

	// Use this for initialization
	void Start () {
		m_Image = GetComponent< Image > ();
	}
	
	// Update is called once per frame
	void Update () {

		m_Image.color = new Color (1.0f, 1.0f, 1.0f, m_dModel.m_Fade);

		int number = m_GameData.GetCard() - 2;

		if (number < 0) {
			number = 0;
		}
		if (number < 12) {
			m_Image.enabled = true;
			m_Image.sprite = m_Sprite [number];
		} else {
			m_Image.enabled = false;
		}
	}
}
