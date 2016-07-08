using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cBlinkView : MonoBehaviour {

	private Text m_Text;
	public cBlinkModel m_blinkModel;

	// Use this for initialization
	void Start () {
		m_Text = GetComponent< Text > ();
	}
	
	// Update is called once per frame
	void Update () {
		//テキストを明滅させる

		Color color = m_Text.color;
		color.a = m_blinkModel.m_Alpha;
		m_Text.color = color;
	}
}
