using UnityEngine;
using System.Collections;

public class cBattleCardModel : cCardModel {

	private Vector2 m_BasePosition;

	public float m_MaxAngle;
	public float m_CookAngle;
	public float m_Speed;
	public float m_Stop;

	private float m_AngleSpeed;
	private float m_AngleAcceleration;
	private float m_ReturnAngleSpeed;
	private float m_ReturnAngleAcceleration;

	public override void InitPosition( Vector2 position ){
		m_BasePosition = position;

		m_Rotation = Vector3.zero;
	}

	public void Init(){
		m_Position = m_BasePosition;

		m_Rotation = Vector3.zero;

		m_DrawMode = eDrawMode.eDrawMode_None;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;
	}

	public void SetCard(){
		m_DrawMode = eDrawMode.eDrawMode_Front;
		m_Size = eSize.eSize_Medium;
		m_OutLineMode = eOutLineMode.eOutLineMode_Red;
	}

	public void SetMove( float reachingSecond , float returnSecond ){
		m_AngleAcceleration = (m_CookAngle / reachingSecond / reachingSecond);

		m_AngleSpeed = 0.0f;

		m_ReturnAngleAcceleration = Mathf.DeltaAngle (m_CookAngle, m_MaxAngle);

		m_ReturnAngleAcceleration = (m_ReturnAngleAcceleration / returnSecond / returnSecond);

		m_ReturnAngleSpeed = 0.0f;
	}

	public void MoveAngle(){
		m_Rotation.z +=( m_AngleSpeed += ( m_AngleAcceleration * Time.deltaTime )) * Time.deltaTime;
	}

	public void ReturnAngle(){
		m_Rotation.z += ( m_ReturnAngleSpeed += ( m_ReturnAngleAcceleration * Time.deltaTime )) * Time.deltaTime;
	}

	public void SetAngle(){
		m_Rotation.z = m_MaxAngle;
	}

	public void SnapMove(){
		m_Rotation.z = -m_MaxAngle;

		m_Position.x += m_Speed * Time.deltaTime;

		if (m_Position.x >= m_Stop) {
			m_Position.x = m_Stop;
		}
	}
}
