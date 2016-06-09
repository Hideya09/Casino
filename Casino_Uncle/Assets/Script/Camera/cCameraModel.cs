using UnityEngine;
using System.Collections;

public class cCameraModel : ScriptableObject {

	private Vector2 m_Position;

	private Vector2 m_MoveAdd;
	private Vector2 m_ReturnAdd;

	private float m_Second;

	private float m_SecondMoveMax;
	private float m_SecondReturnMax;

	private Vector2 m_BasePosition;
	public Vector2 m_ReturnPosition;

	public void InitPosition( Vector2 setPosition ){
		m_Position = setPosition;

		m_BasePosition = setPosition;
	}

	public void MoveSet( float reachingSecond , float returnSecond ){
		m_Position = m_BasePosition;

		m_MoveAdd = m_ReturnPosition - m_BasePosition;
		m_MoveAdd *= (1 / reachingSecond);

		m_ReturnAdd = m_BasePosition - m_ReturnPosition;
		m_ReturnAdd *= (1 / returnSecond);

		m_Second = 0.0f;
		m_SecondMoveMax = reachingSecond;
		m_SecondReturnMax = m_SecondMoveMax + returnSecond;
	}

	public bool Move(){
		m_Second += Time.deltaTime;

		if (m_Second >= m_SecondMoveMax) {
			m_Position += (m_ReturnAdd * Time.deltaTime);

			if (m_Second >= m_SecondReturnMax) {
				m_Position = m_BasePosition;
			}

			return true;
		} else {
			m_Position += (m_MoveAdd * Time.deltaTime);

			return false;
		}
	}

	public Vector2 GetPosition(){
		return m_Position;
	}
}
