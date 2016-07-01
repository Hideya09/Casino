using UnityEngine;
using System.Collections;

public class cEffectModel : ScriptableObject {
	private float m_Count;
	public float m_MaxCount;

	private bool m_EffectOn;

	public void Init(){
		m_Count = 0;

		m_EffectOn = false;
	}

	//エフェクトを開始させる
	public void EffectStart(){
		m_Count = 0;

		m_EffectOn = true;
	}

	//時間を測る
	public bool EffectExec(){
		m_Count += Time.deltaTime;

		if (m_Count >= m_MaxCount) {
			return true;
		}

		return false;
	}

	//エフェクトを終わらせる
	public void EffectEnd(){
		m_Count = 0;

		m_EffectOn = false;
	}

	public bool GetEffectOn(){
		return m_EffectOn;
	}
}
