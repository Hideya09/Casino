using UnityEngine;
using System.Collections;

public class cEffectWinView : MonoBehaviour {

	public cEffectWinModel m_effectWinModel;

	public Transform m_TextTransform;

	private SpriteRenderer m_Sprite;

	public SpriteRenderer m_TextSprite;

	// Use this for initialization
	void Start () {
		m_Sprite = GetComponent< SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localScale = m_effectWinModel.GetLightScale ();
		m_TextTransform.localScale = m_effectWinModel.GetTextScale ();

		m_Sprite.enabled = m_effectWinModel.GetDraw ();
		m_TextSprite.enabled = m_effectWinModel.GetDraw ();
	}
}
