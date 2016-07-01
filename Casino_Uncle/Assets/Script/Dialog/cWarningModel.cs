using UnityEngine;
using System.Collections;

public class cWarningModel : ScriptableObject {

	private float m_Fade;

	//切り替わりの速さ
	public float m_AddFade;

	//表示する文字
	public enum eDrawType{
		eDrawType_Warning,
		eDrawType_Noncomformity
	};

	private eDrawType m_TableDrawType;

	public void Init(){
		m_Fade = 1.0f;
		m_TableDrawType = eDrawType.eDrawType_Warning;
	}

	//文字を切り替える
	public void WarningIn(){
		if (m_TableDrawType != eDrawType.eDrawType_Warning) {
			m_Fade -= (Time.deltaTime * m_AddFade);
			if (m_Fade <= 0.0f) {
				m_Fade = 0.0f;
				m_TableDrawType = eDrawType.eDrawType_Warning;
			}
		} else {
			m_Fade += (Time.deltaTime * m_AddFade);
			if (m_Fade >= 1.0f) {
				m_Fade = 1.0f;
			}
		}
	}
	public void NoncomformityIn(){
		if (m_TableDrawType != eDrawType.eDrawType_Noncomformity) {
			m_Fade -= (Time.deltaTime * m_AddFade);
			if (m_Fade <= 0.0f) {
				m_Fade = 0.0f;
				m_TableDrawType = eDrawType.eDrawType_Noncomformity;
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

	public eDrawType GetDrawFlag(){
		return m_TableDrawType;
	}
}
