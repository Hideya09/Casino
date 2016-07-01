using UnityEngine;
using System.Collections;

public class cBattleCardModel : cCardModel {

	private Vector2 m_BasePosition;

	//カードの傾け処理用
	public float m_MaxAngle;
	public float m_CookAngle;
	public float m_Speed;
	public float m_Stop;

	private float m_AngleSpeed;
	private float m_AngleAcceleration;
	private float m_ReturnAngleSpeed;
	private float m_ReturnAngleAcceleration;

	//ポジションのセット
	public override void InitPosition( Vector2 position ){
		m_BasePosition = position;

		m_RotationY = 0.0f;

		m_RotationZ = 0.0f;
	}

	//初期化処理
	public void Init(){
		m_Position = m_BasePosition;

		m_RotationY = 0.0f;
		m_RotationZ = 0.0f;

		m_DrawMode = eDrawMode.eDrawMode_None;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;
	}

	//カードのセット
	public void SetCard(){
		m_DrawMode = eDrawMode.eDrawMode_Front;
		m_Size = eSize.eSize_Medium;
		m_OutLineMode = eOutLineMode.eOutLineMode_Red;

		m_Fade = 1.0f;
	}

	//カードセット後回転角セット
	public void SetMove( float reachingSecond , float returnSecond ){
		m_AngleAcceleration = (m_CookAngle / reachingSecond / reachingSecond);

		m_AngleSpeed = 0.0f;

		m_ReturnAngleAcceleration = Mathf.DeltaAngle (m_CookAngle, m_MaxAngle);

		m_ReturnAngleAcceleration = (m_ReturnAngleAcceleration / returnSecond / returnSecond);

		m_ReturnAngleSpeed = 0.0f;
	}

	//振り上げ回転
	public void MoveAngle(){
		m_RotationZ +=( m_AngleSpeed += ( m_AngleAcceleration * Time.deltaTime )) * Time.deltaTime;
	}

	//振り下ろし回転
	public void ReturnAngle(){
		m_RotationZ += ( m_ReturnAngleSpeed += ( m_ReturnAngleAcceleration * Time.deltaTime )) * Time.deltaTime;
	}

	//角度を正常に戻す
	public void SetAngle(){
		m_RotationZ = m_MaxAngle;
	}

	//負けた時の回転と移動
	public bool SnapMove(){
		m_RotationZ = -m_MaxAngle;

		m_Position.x += m_Speed * Time.deltaTime;

		if (m_Position.x >= m_Stop) {
			m_Position.x = m_Stop;

			return true;
		}

		return false;
	}
}
