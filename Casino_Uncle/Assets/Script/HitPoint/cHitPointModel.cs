using UnityEngine;
using System.Collections;

public class cHitPointModel : ScriptableObject {
	private Vector3 m_Position;

	private float m_Count;
	private const float m_MaxCount = 1.5f;

	private const float m_Speed = 3.0f;

	public float m_Color{ get; private set; }

	private bool m_AddFlag;

	public void SetPosition( Vector3 setPosition ){
		m_Position = setPosition;
	}

	public void Init(){
		m_Count = 0.0f;

		m_Color = 1.0f;

		m_AddFlag = false;
	}

	public void MoveInit(){
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
					return true;
				}
			}
		}

		return false;
	}

	public bool Move(){
		return false;
	}

	public Vector3 GetPosition(){
		return m_Position;
	}


}
