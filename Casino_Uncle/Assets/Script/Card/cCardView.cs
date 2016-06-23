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
		//位置設定

		transform.localPosition = m_cModel.GetPosition ();
		transform.rotation = Quaternion.Euler (m_cModel.GetRotation ());

		float fade = m_cModel.GetFade ();

		//サイズの変更
		switch (m_cModel.m_Size) {
		case cCardModel.eSize.eSize_Small:
			transform.localScale = new Vector3 (0.8f, 0.8f, 0.8f);
			transform.SetAsFirstSibling ();
			break;
		case cCardModel.eSize.eSize_Medium:
			transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			transform.SetAsFirstSibling ();
			break;
		case cCardModel.eSize.eSize_Large:
			transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
			transform.SetAsLastSibling ();
			break;
		}

		//アウトラインの変更
		switch (m_cModel.m_OutLineMode) {
		case cCardModel.eOutLineMode.eOutLineMode_None:
			m_OutLine.enabled = false;
			m_Renderer.color = new Color (1.0f, 1.0f, 1.0f, fade);
			break;
		case cCardModel.eOutLineMode.eOutLineMode_Yellow:
			m_OutLine.enabled = true;
			m_OutLine.effectColor = new Color (1.0f, 1.0f, 1.0f - fade, fade);
			m_Renderer.color = new Color (1.0f - ((1.0f - fade)), 1.0f - ((1.0f - fade)), 1.0f, fade);
			break;
		case cCardModel.eOutLineMode.eOutLineMode_Blue:
			m_OutLine.enabled = true;
			m_OutLine.effectColor = new Color (1.0f - fade, 1.0f - fade, 1.0f, m_cModel.GetFade ());
			m_Renderer.color = new Color (1.0f, 1.0f, 1.0f - ((1.0f - fade)), fade);
			break;
		case cCardModel.eOutLineMode.eOutLineMode_Red:
			m_OutLine.enabled = true;
			m_OutLine.effectColor = new Color (1.0f, 1.0f - fade, 1.0f - fade, m_cModel.GetFade ());
			m_Renderer.color = new Color (1.0f - ((1.0f - fade)), 1.0f, 1.0f, fade);
			break;
		}

		//描画モードに応じた変更
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
			//Color color = m_Renderer.color;
			m_Renderer.color *= new Color (0.5f, 0.5f, 0.5f, 1.0f);

		} else {
			//Color color = m_Renderer.color;
			m_Renderer.color *= new Color (1.0f, 1.0f, 1.0f, 1.0f);
		}
	}
}
