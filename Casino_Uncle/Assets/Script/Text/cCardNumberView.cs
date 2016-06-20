using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cCardNumberView : MonoBehaviour {

	public cDeckModel m_dModel;

	private Image m_Image;

	public Sprite[] m_Sprite;

	// Use this for initialization
	void Start () {
		m_Image = GetComponent< Image > ();
	}
	
	// Update is called once per frame
	void Update () {

		int number = m_dModel.DeckMax - 3;

		if (number < 0) {
			number = 0;
		}

		m_Image.sprite = m_Sprite [number];
	}
}
