using UnityEngine;
using System.Collections;

public class cEffectStartModel : ScriptableObject {
	private float m_Size;

	public Vector3 m_StartPosition;

	public float m_Speed;

	public float m_StopCount;

	private Vector3 m_TextPosition;

	private float m_Count;

	enum eEffectStartState{
		eEffectStartState_Start,
		eEffectStartState_TextStart,
		eEffectStartState_TextStop,
		eEffectStartState_TextEnd,
		eEffectStartState_End
	}

	private eEffectStartState m_State;

	void OnEnable(){
		Init ();
	}

	public void Init(){
		m_TextPosition = m_StartPosition;

		m_Size = 0.0f;

		m_Count = 0.0f;

		m_State = eEffectStartState.eEffectStartState_Start;
	}

	public bool Exec(){
		switch (m_State) {
		case eEffectStartState.eEffectStartState_Start:
			m_Size += Time.deltaTime;
			if (m_Size >= 1.0f) {
				m_Size = 1.0f;
				++m_State;
			}
			break;
		case eEffectStartState.eEffectStartState_TextStart:
			m_TextPosition.x += m_Speed * Time.deltaTime;

			if (m_TextPosition.x >= 0) {
				m_TextPosition.x = 0.0f;
				++m_State;
			}
			break;
		case eEffectStartState.eEffectStartState_TextStop:
			m_Count += Time.deltaTime;
			if (m_Count >= m_StopCount) {
				++m_State;
			}
			break;
		case eEffectStartState.eEffectStartState_TextEnd:
			m_TextPosition.x += m_Speed * Time.deltaTime;

			if (m_TextPosition.x >= -m_StartPosition.x) {
				m_TextPosition.x = -m_StartPosition.x;
				++m_State;
			}
			break;
		case eEffectStartState.eEffectStartState_End:
			m_Size -= Time.deltaTime;
			if (m_Size <= 0.0f) {
				m_Size = 0.0f;
				m_State = eEffectStartState.eEffectStartState_Start;
				return true;
			}
			break;
		}
		return false;
	}

	public float GetSize(){
		return m_Size;
	}

	public Vector3 GetPosition(){
		return m_TextPosition;
	}
}
