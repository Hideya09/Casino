using UnityEngine;
using System.Collections;

public class cEnemyModel : ScriptableObject {

	public cEffectModel m_Damage;

	private Vector3 m_Position;
	private Vector3 m_BasePosition;

	public float m_Runble;

	//出現時用
	private float m_Fade;
	private float m_Black;

	//出現演出のスピード
	public float m_FadeSpeed;
	public float m_BlackSpeed;

	//揺れるエフェクト用
	private float m_RunbleCount;
	private float m_RunbleMaxPower;
	private float m_RunblePower;
	private bool m_RunbleFlag;

	public void InitPosition(Vector3 setPosition){
		m_BasePosition = setPosition;
		m_Position = m_BasePosition;
	}

	public void Init(){
		m_Position = m_BasePosition;

		m_Damage.EffectEnd ();

		m_Fade = 0.0f;
		m_Black = 0.5f;
	}

	public void RunbleInit(){
		m_RunbleMaxPower = m_Runble * 0.25f;
		m_RunblePower = m_Runble;
		m_RunbleCount = 0.0f;
		m_RunbleFlag = true;

		m_Damage.EffectStart ();
	}

	//ダメージを受けた際のエフェクト
	public void Runble(){
		if (m_RunbleMaxPower > 0.0f) {
			m_RunbleCount += Time.deltaTime;

			if (m_RunbleCount >= 0.05f && m_RunbleMaxPower > 0.0f) {
				if (m_RunbleFlag == true) {
					m_Position.y -= m_RunblePower * Time.deltaTime;
				} else {
					m_Position.y += m_RunblePower * Time.deltaTime;
				}
			} else {
				if (m_RunbleFlag == true) {
					m_Position.y += m_RunblePower * Time.deltaTime;
				} else {
					m_Position.y -= m_RunblePower * Time.deltaTime;
				}
			}

			m_RunbleCount += Time.deltaTime;

			if (m_RunbleCount >= 0.1f) {
				m_RunbleMaxPower -= m_RunbleCount * 12.0f;

				if (m_RunbleMaxPower < 0.0f) {
					m_RunbleMaxPower = 0.0f;

					m_RunblePower = m_BasePosition.y - m_Position.y;

					m_RunbleFlag = false;
				} else {
					m_RunblePower = m_RunbleMaxPower;

					m_RunbleFlag ^= true;
				}

				m_RunbleCount = 0.0f;
			}
		} else {

			m_RunbleCount += Time.deltaTime;

			if (m_RunbleCount <= 0.2f) {
				m_Position.y += m_RunblePower * Time.deltaTime * 5;
			} else {
				m_Position.y = m_BasePosition.y;
			}
		}
	}

	//ダメージ演出が終わった時の処理
	public void StopRunble(){
		m_Position = m_BasePosition;

		m_Damage.EffectEnd ();
	}

	public Vector3 GetPosition(){
		return m_Position;
	}

	public float GetFade(){
		return m_Fade;
	}
	public float GetBlack(){
		return m_Black;
	}

	//出現時の処理
	public bool Start(){
		if (m_Fade == 0.0f) {
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_In);
		}

		m_Fade += Time.deltaTime * m_FadeSpeed;
		if (m_Fade >= 1.0f) {
			m_Fade = 1.0f;
			m_Black += Time.deltaTime * m_BlackSpeed;
			if (m_Black >= 1.0f) {
				m_Black = 1.0f;
				return true;
			}
		}

		return false;
	}

	//退去時の処理
	public bool End(){
		m_Black -= Time.deltaTime * m_FadeSpeed;
		if (m_Black <= 0.5f) {
			m_Black = 0.5f;
			m_Fade -= Time.deltaTime * m_BlackSpeed;
			if (m_Fade <= 0.0f) {
				m_Fade = 0.0f;
				return true;
			}
		}

		return false;
	}
}
