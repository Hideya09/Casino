using UnityEngine;
using System.Collections;

public class cEnemyBattleCardModel : cCardModel {

	private Vector2 m_BasePosition;
	public Vector2 m_StartPosition;

	private Vector2 m_MoveAdd;
	private Vector2 m_MoveAcceleration;

	private float m_Second;
	private float m_SecondMax;

	public float m_MaxAngle;
	public float m_Speed;

	public override void InitPosition( Vector2 position ){
		m_BasePosition = position;
		m_Position = m_StartPosition;

		m_Rotation = Vector3.zero;
	}

	public void Init(){
		m_DrawMode = eDrawMode.eDrawMode_None;
		m_Size = eSize.eSize_Medium;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;

		m_Position = m_StartPosition;
		m_Rotation = Vector3.zero;

		m_Second = 0.0f;
		m_SecondMax = 0.0f;
	}

	public void SetCard(){
		m_DrawMode = eDrawMode.eDrawMode_Back;
		m_Size = eSize.eSize_Medium;
		m_OutLineMode = eOutLineMode.eOutLineMode_Red;
	}

	public void MoveSet( float reachingSecond ){
		m_MoveAcceleration = m_BasePosition - m_StartPosition;
		m_MoveAcceleration *= (1 / reachingSecond) * (1 / reachingSecond);

		m_MoveAdd = Vector2.zero;

		m_Second = 0.0f;
		m_SecondMax = reachingSecond;
	}

	public bool Move(){
		m_Position += ((m_MoveAdd += (m_MoveAcceleration * Time.deltaTime)) * Time.deltaTime);

		m_Rotation.z += m_MaxAngle * Time.deltaTime;

		m_Second += Time.deltaTime;

		if (m_Second >= m_SecondMax) {
			m_Position = m_BasePosition;

			m_Rotation.z = m_MaxAngle % 360;

			return true;
		} else {
			return false;
		}
	}

	public void SnapMove(){
		m_Rotation.z = -m_MaxAngle;
		m_Position.x -= m_Speed * Time.deltaTime;
	}
}