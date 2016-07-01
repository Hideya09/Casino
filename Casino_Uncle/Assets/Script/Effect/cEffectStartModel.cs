using UnityEngine;
using System.Collections;

public class cEffectStartModel : ScriptableObject {
	private float m_Size;
	public float m_SizeMax;

	public float m_SizeCount;

	public Vector3 m_StartPosition;

	//テキストの流れる速さ
	public float m_Speed;

	//テキストが止まる時間
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
		//黒帯を大きくしてテキストを流す
		switch (m_State) {
		case eEffectStartState.eEffectStartState_Start:
			m_Size += Time.deltaTime / m_SizeCount;
			if (m_Size >= m_SizeMax) {
				m_Size = m_SizeMax;
				++m_State;
			}
			break;
		case eEffectStartState.eEffectStartState_TextStart:
			m_TextPosition.x += m_Speed * Time.deltaTime;

			if (m_TextPosition.x >= 0) {
				m_TextPosition.x = 0.0f;
				++m_State;

				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_DuelStart);
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
			m_Size -= Time.deltaTime / m_SizeCount;
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
