using UnityEngine;
using System.Collections;

public class cTapEffectView : MonoBehaviour {

	private ParticleSystem m_Particle;

	// Use this for initialization
	void Start () {
		m_Particle = GetComponent< ParticleSystem > ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_Particle.isPlaying == false) {
			Destroy (gameObject);
		}
	}
}
