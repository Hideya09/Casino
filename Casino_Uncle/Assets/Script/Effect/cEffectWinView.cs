using UnityEngine;
using System.Collections;

public class cEffectWinView : MonoBehaviour {

	public cEffectWinModel m_effectWinModel;

	public Transform m_TextTransform;

	private ParticleSystem m_Particle;

	public SpriteRenderer m_TextSprite;

	private bool m_PlayOnFlag;

	// Use this for initialization
	void Start () {
		m_Particle = GetComponent< ParticleSystem> ();

		m_PlayOnFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		//エフェクトが出ておらず、エフェクトフラグが立ったらパーティクルを出す
		if (m_effectWinModel.GetParthicle () == true && m_PlayOnFlag == false) {
			m_Particle.Play ();

			m_PlayOnFlag = true;
		} else if (m_effectWinModel.GetParthicle () == false && m_PlayOnFlag == true) {
			m_Particle.Stop ();

			m_PlayOnFlag = false;
		}

		m_TextTransform.localScale = m_effectWinModel.GetTextScale ();
		m_TextSprite.enabled = m_effectWinModel.GetDraw ();
	}
}
