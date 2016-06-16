using UnityEngine;
using System.Collections;

public class cEffectLoseModel : ScriptableObject {
	private float m_Fade;

	private bool m_DrawFlag;

	private bool m_TapFlag;
	private bool m_EffectEndFlag;

	public float m_Speed;

	void OnEnable(){
		Init ();
	}

	public void Init(){
		m_DrawFlag = false;

		m_TapFlag = false;
		m_EffectEndFlag = false;

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

			m_EffectEndFlag = true;

			m_TapFlag = false;

			return true;
		}

		return false;
	}

	public void Tap(){
		m_TapFlag = true;
	}

	public bool GetTapFlag(){
		return m_TapFlag & m_EffectEndFlag;
	}
}
