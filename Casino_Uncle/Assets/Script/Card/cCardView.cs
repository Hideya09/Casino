using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cCardView : MonoBehaviour {

	public cCardModel m_cModel;

	public cCardSpriteManager m_Sprite;

	private Image m_Renderer;
	private Outline m_OutLine;

	private static int m_Back = 14;
	private int m_Number;

	void Awake(){
		m_cModel.InitPosition (transform.localPosition);
	}

	// Use this for initialization
	void Start () {
		m_Renderer = GetComponent< Image > ();

		m_OutLine = GetComponent< Outline > ();

		m_Number = m_Back;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = m_cModel.GetPosition ();

		if (m_cModel.m_DrawMode == cCardModel.eDrawMode.eDrawMode_Back) {
			m_Number = m_Back;
			m_Renderer.sprite = m_Sprite.sprite [m_Number];
		} else if (m_Number != m_cModel.m_CardNumber) {
			m_Number = m_cModel.m_CardNumber;
			m_Renderer.sprite = m_Sprite.sprite [m_Number];
		}

		if (m_cModel.m_DrawMode == cCardModel.eDrawMode.eDrawMode_None) {
			m_Renderer.enabled = false;
		} else {
			m_Renderer.enabled = true;
		}

		if (m_cModel.m_DrawMode == cCardModel.eDrawMode.eDrawMode_Dark) {
			Color color = m_Renderer.color;
			m_Renderer.color = new Color (0.5f, 0.5f, 0.5f , color.a);

		} else {
			Color color = m_Renderer.color;
			m_Renderer.color = new Color (1.0f, 1.0f, 1.0f , color.a);
		}

		switch (m_cModel.m_Size) {
		case cCardModel.eSize.eSize_Small:
			transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
			break;
		case cCardModel.eSize.eSize_Medium:
			transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			break;
		case cCardModel.eSize.eSize_Large:
			transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
			break;
		}

		switch (m_cModel.m_OutLineMode) {
		case cCardModel.eOutLineMode.eOutLineMode_None:
			m_OutLine.enabled = false;
			break;
		case cCardModel.eOutLineMode.eOutLineMode_Yellow:
			m_OutLine.enabled = true;
			m_OutLine.effectColor = new Color (1.0f, 1.0f, 0.0f, 1.0f);
			break;
		case cCardModel.eOutLineMode.eOutLineMode_Blue:
			m_OutLine.enabled = true;
			m_OutLine.effectColor = new Color (0.0f, 0.0f, 1.0f, 1.0f);
			break;
		case cCardModel.eOutLineMode.eOutLineMode_Red:
			m_OutLine.enabled = true;
			m_OutLine.effectColor = new Color (1.0f, 0.0f, 0.0f, 1.0f);
			break;
		}
	}
}
