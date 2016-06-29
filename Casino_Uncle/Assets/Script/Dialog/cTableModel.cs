using UnityEngine;
using System.Collections;

public class cTableModel : ScriptableObject {

	private float m_Fade;

	public float m_AddFade;

	public enum eDrawType{
		eDrawType_Table,
		eDrawType_Warning,
		eDrawType_Noncomformity
	};

	private eDrawType m_TableDrawType;

	public void Init(){
		m_Fade = 1.0f;
		m_TableDrawType = eDrawType.eDrawType_Table;
	}

	public void TableIn(){
		if (m_TableDrawType != eDrawType.eDrawType_Table) {
			m_Fade -= (Time.deltaTime * m_AddFade);
			if (m_Fade <= 0.0f) {
				m_Fade = 0.0f;
				m_TableDrawType = eDrawType.eDrawType_Table;
			}
		} else {
			m_Fade += (Time.deltaTime * m_AddFade);
			if (m_Fade >= 1.0f) {
				m_Fade = 1.0f;
			}
		}
	}

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
