using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cEnemyDeckView : MonoBehaviour {

	public cEnemyDeckModel m_dModel;

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

		m_Image.color = new Color (1.0f, 1.0f, 1.0f, m_dModel.m_Arpha);

		//デッキ描画を行うかどうか
		if (m_dModel.m_DeckMax - 2 > 0) {
			m_Image.sprite = m_ThereSprite;
		} else {
			m_Image.sprite = m_NoneSprite;
		}
	}
}
