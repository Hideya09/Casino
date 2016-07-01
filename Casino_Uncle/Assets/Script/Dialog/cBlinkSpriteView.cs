using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cBlinkSpriteView : MonoBehaviour {

	private Image m_Image;
	public cBlinkModel m_blinkModel;

	// Use this for initialization
	void Start () {
		m_Image = GetComponent< Image > ();
	}
	
	// Update is called once per frame
	void Update () {

		//画像を明滅させる

		Color color = m_Image.color;
		color.a = m_blinkModel.m_Alpha;
		m_Image.color = color;
	}
}
