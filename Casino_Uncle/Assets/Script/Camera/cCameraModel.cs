using UnityEngine;
using System.Collections;

public class cCameraModel : ScriptableObject {

	private Vector2 m_Position;

	//カメラの移動用
	private Vector2 m_MoveAdd;
	private Vector2 m_MoveAcceleration;
	private Vector2 m_ReturnAdd;
	private Vector2 m_ReturnAcceleration;

	private float m_Second;

	private float m_SecondMoveMax;
	private float m_SecondReturnMax;

	//カメラの基本位置と折り返し地点
	private Vector2 m_BasePosition;
	public Vector2 m_ReturnPosition;

	//揺れ関連ステータス
	public float m_Runble;

	private float m_RunbleCount;
	private float m_RunbleMaxPower;
	private float m_RunblePower;
	private bool m_RunbleFlag;

	public void InitPosition( Vector2 setPosition ){
		m_Position = setPosition;

		m_BasePosition = setPosition;
	}

	//移動量等を計算
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

	//移動処理
	public bool Move( out bool endFlag ){

		m_Second += Time.deltaTime;

		endFlag = false;

		if (m_Second >= m_SecondMoveMax) {
			m_Position += ((m_ReturnAdd += (m_ReturnAcceleration * Time.deltaTime)) * Time.deltaTime);

			if (Vector3.Distance (m_ReturnPosition, m_Position) >= Vector3.Distance (m_ReturnPosition, m_BasePosition)) {
				m_Position = m_BasePosition;
			}
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

	//揺れ関連初期化
	public void RunbleInit(){
		m_RunbleMaxPower = m_Runble * 0.25f;
		m_RunblePower = m_Runble;
		m_RunbleCount = 0.0f;
		m_RunbleFlag = true;
	}

	//揺らす処理
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
				m_RunbleMaxPower -= m_RunbleCount * 24.0f;

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

	//揺れを止める
	public void StopRunble(){
		m_Position = m_BasePosition;
	}

	public Vector2 GetPosition(){
		return m_Position;
	}
}
