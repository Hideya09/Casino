using UnityEngine;
using System.Collections;

public class cEffectView : MonoBehaviour {

	public cEffectModel m_effectModel;

	private ParticleSystem m_Particle;

	private bool m_PlayOnFlag;

	// Use this for initialization
	void Start () {
		m_Particle = GetComponent< ParticleSystem > ();

		m_PlayOnFlag = false;
	}
	
	// Update is called once per frame
	void Update () {
		//エフェクトが出てないときにフラグが立ったら、エフェクトを開始させる
		if (m_effectModel.GetEffectOn () == true && m_PlayOnFlag == false) {
			m_Particle.Play ();
			m_PlayOnFlag = true;
		} else if (m_effectModel.GetEffectOn () == false && m_PlayOnFlag == true) {
			m_Particle.Stop ();

			m_PlayOnFlag = false;
		}
	}
}
