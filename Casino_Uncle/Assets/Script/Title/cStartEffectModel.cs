using UnityEngine;
using System.Collections;

public class cStartEffectModel : ScriptableObject {

	private float m_Arpha;

	private float m_Count;

	private enum eStartEffectState{
		eStartEffectState_ArphaUp,
		eStartEffectState_Wait,
		eStartEffectState_ArphaDown,
		eStartEffectState_End
	}

	private eStartEffectState m_State;

	void OnEnable(){
		m_Arpha = 0.7f;
		m_Count = 0.0f;

		m_State = eStartEffectState.eStartEffectState_ArphaUp;
	}

	public bool StartExec(){
		switch (m_State) {
		case eStartEffectState.eStartEffectState_ArphaUp:
			m_Arpha += Time.deltaTime;
			if (m_Arpha >= 1.0f) {
				m_State = eStartEffectState.eStartEffectState_Wait;
			}
			break;
		case eStartEffectState.eStartEffectState_Wait:
			m_Count += Time.deltaTime;
			if (m_Count >= 1.0f) {
				m_State = eStartEffectState.eStartEffectState_ArphaDown;
			}
			break;
		case eStartEffectState.eStartEffectState_ArphaDown:
			m_Arpha -= Time.deltaTime;
			if (m_Arpha <= 0.0f) {
				m_State = eStartEffectState.eStartEffectState_End;
			}
			break;
		case eStartEffectState.eStartEffectState_End:
			return true;
		}

		return false;
	}

	public float GetArpha(){
		return m_Arpha;
	}
}
