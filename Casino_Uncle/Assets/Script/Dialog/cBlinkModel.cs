using UnityEngine;
using System.Collections;

public class cBlinkModel : ScriptableObject {

	private float m_Count;
	public float m_MaxCount;

	public float m_Speed;

	public float m_Alpha{ get; private set; }

	private bool m_AddFlag;

	//最初から出ている場合の初期化
	public void Init(){
		m_Count = 0.0f;

		m_Alpha = 1.0f;

		m_AddFlag = false;
	}

	//最初は隠れている場合の初期化
	public void Init2(){
		m_Count = 0.0f;

		m_Alpha = 0.0f;

		m_AddFlag = true;
	}

	//数回明滅させる処理
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
