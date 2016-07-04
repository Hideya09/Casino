using UnityEngine;
using System.Collections;

public class cTapEffectModel : ScriptableObject {

	private float m_TapCount;
	public float m_MaxTapCount;

	//エフェクトを生成
	public void EffectOn( Vector3 position ){
		if (m_TapCount >= m_MaxTapCount) {
			GameObject prefab = (GameObject)Resources.Load ("Prefab/TapEffect");
			GameObject.Instantiate (prefab, position, Quaternion.Euler (10, -180, -180));

			m_TapCount = 0;
		}
	}

	public void CountUp(){
		m_TapCount += Time.deltaTime;

		if (m_TapCount >= m_MaxTapCount) {
			m_TapCount = m_MaxTapCount;
		}
	}
}
