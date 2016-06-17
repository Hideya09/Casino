using UnityEngine;
using System.Collections;

public class cFadeHalfModel : cFadeInOutModel{
	// Use this for initialization
	public override void OnEnable () {
		m_State = eFadeState.FadeInStop;

		m_Arpha = 0.0f;
	}
	
	// Update is called once per frame
	public override void FadeExec () {
		switch (m_State) {
		case eFadeState.FadeIn:
			//徐々に白いテクスチャを薄くしていく
			m_Arpha -= Time.deltaTime;

			if (m_Arpha <= 0.0f) {
				m_State = eFadeState.FadeInStop;
				m_Arpha = 0.0f;
			}
			break;
		case eFadeState.FadeInStop:
			break;
		case eFadeState.FadeOut:
			//徐々に白いテクスチャを濃くしていく
			m_Arpha += Time.deltaTime;

			if (m_Arpha >= 0.5f) {
				m_Arpha = 0.5f;
				m_State = eFadeState.FadeOutStop;
			}
			break;
		case eFadeState.FadeOutStop:
			break;
		default:
			//処理なし
			break;
		}
	}

	public void SetState(eFadeState setState){
		m_State = setState;
	}
}
