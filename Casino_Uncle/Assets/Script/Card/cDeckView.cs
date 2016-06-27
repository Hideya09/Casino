using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cDeckView : MonoBehaviour {

	public cDeckModel m_dModel;

	public cGameData m_GameData;

	private Image m_Image;

	public Sprite m_ThereSprite;
	public Sprite m_NoneSprite;

	// Use this for initialization
	void Start () {
		m_dModel.SetPosition (transform.localPosition);

		m_Image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = m_dModel.GetPosition ();

		m_Image.color = new Color (1.0f, 1.0f, 1.0f, m_dModel.m_Fade);

		if (m_GameData.GetCard () - 2 > 0 || m_dModel.GetDeckView () == true) {
			m_Image.sprite = m_ThereSprite;
		} else {
			m_Image.sprite = m_NoneSprite;
		}
	}
}
