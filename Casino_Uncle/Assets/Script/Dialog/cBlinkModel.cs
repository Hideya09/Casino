using UnityEngine;
using System.Collections;

public class cBlinkModel : ScriptableObject {

	private float m_Count;
	public float m_MaxCount;

	public float m_Speed;

	public float m_Alpha{ get; private set; }

	private bool m_AddFlag;

	public void Init(){
		m_Count = 0.0f;

		m_Alpha = 1.0f;

		m_AddFlag = false;
	}

	public void Init2(){
		m_Count = 0.0f;

		m_Alpha = 0.0f;

		m_AddFlag = true;
	}

	public bool Blink(){
		m_Count += Time.deltaTime;
		if (m_AddFlag == true) {
			m_Alpha += Time.deltaTime * m_Speed;
			if (m_Alpha >= 1.0f) {
				m_Alpha = 1.0f;
				m_AddFlag = false;

				if (m_Count >= m_MaxCount) {
					return true;
				}
			}
		} else {
			m_Alpha -= Time.deltaTime * m_Speed;
			if (m_Alpha <= 0.0f) {
				m_Alpha = 0.0f;
				m_AddFlag = true;
			}
		}

		return false;
	}
}
