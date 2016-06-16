using UnityEngine;
using System.Collections;

public class cEffectLoseModel : ScriptableObject {
	private float m_Fade;

	private bool m_DrawFlag;

	public float m_Speed;

	void OnEnable(){
		Init ();
	}

	public void Init(){
		m_DrawFlag = false;

		m_Fade = 0.0f;
	}

	public float GetFade(){
		return m_Fade;
	}

	public bool GetDraw(){
		return m_DrawFlag;
	}

	public bool EffectOn(){

		m_DrawFlag = true;

		float secound = Time.deltaTime;

		m_Fade += m_Speed * secound;

		if (m_Fade >= 1.0f) {
			m_Fade = 1.0f;

			return true;
		}

		return false;
	}
}
