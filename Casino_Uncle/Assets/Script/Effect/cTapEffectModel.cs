using UnityEngine;
using System.Collections;

public class cTapEffectModel : ScriptableObject {

	private Vector3 m_MakePosition;

	private float m_TapCount;
	public float m_MaxTapCount;

	private bool m_EffectOnFlag;

	public GameObject m_EffectParent;

	void OnEnable(){
		m_TapCount = 0;
		m_EffectOnFlag = false;
	}

	//エフェクトを生成
	public void EffectOn( Vector3 position ){
		if (m_TapCount >= m_MaxTapCount) {
			m_EffectOnFlag = true;

			m_MakePosition = position;

			m_TapCount = 0;
		}
	}

	public void CountUp(){
		m_TapCount += Time.deltaTime;

		if (m_TapCount >= m_MaxTapCount) {
			m_TapCount = m_MaxTapCount;
		}
	}

	public bool EffectOn(){
		if (m_EffectOnFlag == true) {
			m_EffectOnFlag = false;
			return true;
		}

		return false;
	}

	public Vector3 GetMakePosition(){
		return m_MakePosition;
	}
}
