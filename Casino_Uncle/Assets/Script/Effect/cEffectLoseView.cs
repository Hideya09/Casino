using UnityEngine;
using System.Collections;

public class cEffectLoseView : MonoBehaviour {

	public cEffectLoseModel m_effectLoseModel;

	private SpriteRenderer m_Sprite;

	// Use this for initialization
	void Start () {
		m_Sprite = GetComponent< SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		m_Sprite.color = new Color (1.0f, 1.0f, 1.0f, m_effectLoseModel.GetFade ());
		m_Sprite.enabled = m_effectLoseModel.GetDraw ();
	}
}
