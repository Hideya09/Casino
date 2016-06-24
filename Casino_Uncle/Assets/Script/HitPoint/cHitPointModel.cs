using UnityEngine;
using System.Collections;

public class cHitPointModel : ScriptableObject {
	private Vector3 m_Position;

	private Vector3 m_BasePosition;
	private Vector3 m_Movement;

	public Vector3 m_StartPosition;
	public Vector3 m_ReturnPosition;

	private float m_ReturnCount;
	public float m_MaxReturnCount;
	public float m_EndCount;

	private float m_Count;
	public float m_MaxCount = 1.5f;

	public float m_Speed = 3.0f;

	public float m_Color{ get; private set; }
	public float m_Fade{ get; private set; }
	public float m_Black{ get; private set; }

	private bool m_AliveFlag;
	private bool m_AddFlag;

	public void SetPosition( Vector3 setPosition ){
		m_BasePosition = setPosition;
	}

	public void Init(){
		m_Count = 0.0f;

		m_Color = 1.0f;
		m_Fade = 1.0f;
		m_Black = 1.0f;

		m_Position = m_StartPosition;

		m_AliveFlag = true;

		m_AddFlag = false;
	}

	public void FadeInit(){
		m_Fade = 0.0f;
		m_Color = 0.0f;
		m_Black = 0.5f;

		m_Position = m_BasePosition;
	}

	public bool CutBack(){

		m_Count += Time.deltaTime;

		if (m_AddFlag == true) {
			m_Color += Time.deltaTime * m_Speed;
			if (m_Color >= 1.0f) {
				m_Color = 1.0f;
				m_AddFlag = false;
			}
		} else {
			m_Color -= Time.deltaTime * m_Speed;
			if (m_Color <= 0.0f) {
				m_Color = 0.0f;
				m_AddFlag = true;

				if (m_Count >= m_MaxCount) {
					m_AliveFlag = false;
					return true;
				}
			}
		}

		return false;
	}

	public void MoveSet(){
		m_Movement = m_ReturnPosition - m_StartPosition;

		m_Movement /= m_MaxReturnCount;

		m_Position = m_StartPosition;

		m_Fade = 1.0f;

		if (m_AliveFlag == true) {
			m_Color = 1.0f;
		}

		m_ReturnCount = 0.0f;
	}

	public bool Move(){

		m_ReturnCount += Time.deltaTime;

		m_Position += m_Movement * Time.deltaTime;

		if (m_ReturnCount >= m_MaxReturnCount) {
			m_Position = m_ReturnPosition;
			return true;
		}

		return false;
	}

	public void ReturnSet(){
		m_Movement = m_BasePosition - m_Position;
		m_Movement /= m_EndCount;

		m_ReturnCount = 0.0f;
	}

	public bool Return(){
		m_ReturnCount += Time.deltaTime;

		m_Position += m_Movement * Time.deltaTime;

		if (m_ReturnCount >= m_EndCount) {
			m_Position = m_BasePosition;
			m_ReturnCount = m_EndCount;
			return true;
		}

		return false;
	}

	public void BackSet(){
		m_Movement = m_StartPosition - m_Position;
		m_Movement /= m_MaxReturnCount;

		m_ReturnCount = 0.0f;
	}

	public bool Back(){
		m_ReturnCount += Time.deltaTime;

		m_Position += m_Movement * Time.deltaTime;

		m_Fade -= Time.deltaTime;

		if (m_Color >= m_Fade) {
			m_Color = m_Fade;
		}

		if (m_Fade <= 0.0f) {
			m_Position = m_StartPosition;
			return true;
		}

		return false;
	}

	public bool Fade(){
		m_Fade += Time.deltaTime;
		if (m_Color <= m_Fade) {
			m_Color = m_Fade;
		}
		if (m_Fade >= 1.0f) {
			m_Fade = 1.0f;
			m_Black += Time.deltaTime;
			if (m_Black >= 1.0f) {
				m_Black = 1.0f;
				return true;
			}
		}

		return false;
	}

	public Vector3 GetPosition(){
		return m_Position;
	}


}
