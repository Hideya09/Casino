using UnityEngine;
using System.Collections;

public class cEffectWinModel : ScriptableObject {

	private Vector3 m_LightScale;
	private Vector3 m_TextScale;

	private bool m_DrawFlag;

	public float m_LightSpeed;
	public float m_TextSpeed;

	public float m_TextOnScale;

	void OnEnable(){
		Init ();
	}

	public void Init(){
		m_DrawFlag = false;

		m_LightScale = Vector3.zero;
		m_TextScale = Vector3.zero;
	}

	public Vector3 GetLightScale(){
		return m_LightScale;
	}

	public Vector3 GetTextScale(){
		return m_TextScale;
	}

	public bool GetDraw(){
		return m_DrawFlag;
	}

	public bool EffectOn(){

		m_DrawFlag = true;

		float secound = Time.deltaTime;

		m_LightScale += new Vector3 (m_LightSpeed * secound, m_LightSpeed * secound, 1.0f);

		if (m_TextOnScale <= m_LightScale.x) {
			m_TextScale += new Vector3 (m_TextSpeed * secound, m_TextSpeed * secound, 1.0f);
		}

		if (m_LightScale.x >= 1.0f) {
			m_LightScale = new Vector3 (1.0f, 1.0f, 1.0f);

			if (m_TextScale.x >= 1.0f) {
				m_TextScale = new Vector3 (1.0f, 1.0f, 1.0f);
				return true;
			}
		}

		return false;
	}
}
