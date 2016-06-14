using UnityEngine;
using System.Collections;

public class cCameraModel : ScriptableObject {

	private Vector2 m_Position;

	private Vector2 m_MoveAdd;
	private Vector2 m_MoveAcceleration;
	private Vector2 m_ReturnAdd;
	private Vector2 m_ReturnAcceleration;

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

		m_MoveAcceleration = m_ReturnPosition - m_BasePosition;
		m_MoveAcceleration *= (1 / reachingSecond) * (1 / reachingSecond);

		m_MoveAdd = Vector2.zero;

		m_ReturnAcceleration = m_BasePosition - m_ReturnPosition;
		m_ReturnAcceleration *= (1 / returnSecond) * (1 / returnSecond);

		m_ReturnAdd = Vector2.zero;

		m_Second = 0.0f;
		m_SecondMoveMax = reachingSecond;
		m_SecondReturnMax = m_SecondMoveMax + returnSecond;
	}

	public bool Move( out bool endFlag ){

		m_Second += Time.deltaTime;

		endFlag = false;

		if (m_Second >= m_SecondMoveMax) {
			m_Position += ((m_ReturnAdd += (m_ReturnAcceleration * Time.deltaTime)) * Time.deltaTime);


			if (m_Second >= m_SecondReturnMax) {
				endFlag = true;

				m_Position = m_BasePosition;
			}
			return true;
		} else {
			m_Position += ((m_MoveAdd += (m_MoveAcceleration * Time.deltaTime)) * Time.deltaTime);

			return false;
		}
	}

	public void Vibration(){
		m_Position.y = m_BasePosition.y + Random.Range (-0.1f, 0.1f);
	}

	public void StopVibration(){
		m_Position = m_BasePosition;
	}

	public Vector2 GetPosition(){
		return m_Position;
	}
}
