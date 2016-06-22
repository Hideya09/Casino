using UnityEngine;
using System.Collections;

public class cEffectWinModel : ScriptableObject {

	private Vector3 m_LightScale;
	private Vector3 m_TextScale;

	private float m_Angle;

	private bool m_DrawFlag;

	private bool m_TapFlag;
	private bool m_EffectEndFlag;

	public float m_LightSpeed;
	public float m_TextSpeed;

	public float m_TextOnScale;

	void OnEnable(){
		Init ();
	}

	public void Init(){
		m_DrawFlag = false;

		m_Angle = 0.0f;

		m_TapFlag = false;
		m_EffectEndFlag = false;

		m_LightScale = Vector3.zero;
		m_TextScale = Vector3.zero;
	}

	public Vector3 GetLightScale(){
		return m_LightScale;
	}

	public Vector3 GetTextScale(){
		return m_TextScale;
	}

	public float GetAngle(){
		return m_Angle;
	}

	public bool GetDraw(){
		return m_DrawFlag;
	}

	public bool EffectOn(){

		m_DrawFlag = true;

		float secound = Time.deltaTime;

		m_LightScale += new Vector3 (m_LightSpeed * secound, m_LightSpeed * secound, 1.0f);

		m_Angle += 60 * Time.deltaTime;

		m_Angle %= 360;

		if (m_TextOnScale <= m_LightScale.x) {
			if (m_TextScale == Vector3.zero) {
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Win);
			}
			m_TextScale += new Vector3 (m_TextSpeed * secound, m_TextSpeed * secound, 1.0f);
		}

		if (m_LightScale.x >= 1.0f) {
			m_LightScale = new Vector3 (1.0f, 1.0f, 1.0f);

			if (m_TextScale.x >= 1.0f) {
				m_TextScale = new Vector3 (1.0f, 1.0f, 1.0f);

				m_TapFlag = false;
				m_EffectEndFlag = true;

				return true;
			}
		}

		return false;
	}

	public void Tap(){
		m_TapFlag = true;
	}

	public bool GetTapFlag(){
		return m_TapFlag & m_EffectEndFlag;
	}
}
