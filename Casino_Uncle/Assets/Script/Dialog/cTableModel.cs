using UnityEngine;
using System.Collections;

public class cTableModel : ScriptableObject {

	private float m_Fade;

	public float m_AddFade;

	private bool m_TableDrawFlag;

	public void Init(){
		m_Fade = 1.0f;
		m_TableDrawFlag = true;
	}

	public void FadeIn(){
		if (m_TableDrawFlag == false) {
			m_Fade -= (Time.deltaTime * m_AddFade);
			if (m_Fade <= 0.0f) {
				m_Fade = 0.0f;
				m_TableDrawFlag = true;
			}
		} else {
			m_Fade += (Time.deltaTime * m_AddFade);
			if (m_Fade >= 1.0f) {
				m_Fade = 1.0f;
			}
		}
	}

	public void FadeOut(){
		if (m_TableDrawFlag == true) {
			m_Fade -= (Time.deltaTime * m_AddFade);
			if (m_Fade <= 0.0f) {
				m_Fade = 0.0f;
				m_TableDrawFlag = false;
			}
		} else {
			m_Fade += (Time.deltaTime * m_AddFade);
			if (m_Fade >= 1.0f) {
				m_Fade = 1.0f;
			}
		}
	}

	public float GetFade(){
		return m_Fade;
	}

	public bool GetDrawFlag(){
		return m_TableDrawFlag;
	}
}
