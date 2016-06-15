﻿using UnityEngine;
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

	public float m_RunbleCount;
	public float m_RunbleMaxPower;
	public float m_RunblePower;
	public bool m_RunbleFlag;

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

	public void RunbleInit(){
		m_RunbleMaxPower = 48.0f;
		m_RunblePower = Random.Range (m_RunbleMaxPower - 20.0f, m_RunbleMaxPower);
		m_RunbleCount = 0.0f;
		m_RunbleFlag = true;
	}

	public void Runble(){
		m_RunbleCount += Time.deltaTime;

		if (m_RunbleCount >= 0.1f) {
			m_RunbleMaxPower -= m_RunbleCount *  48.0f;

			if (m_RunbleMaxPower < 0.0f) {
				m_RunbleMaxPower = 0.0f;

				m_RunblePower = m_BasePosition.y - m_RunblePower;

				m_RunbleFlag = false;
			} else {
				m_RunblePower = m_RunbleMaxPower;

				m_RunbleCount = 0.0f;

				m_RunbleFlag ^= true;
			}

			m_RunbleCount = 0.0f;
		}

		if (m_RunbleCount >= 0.05f  && m_RunbleMaxPower > 0.0f) {
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
	}

	public void StopRunble(){
		m_Position = m_BasePosition;
	}

	public Vector2 GetPosition(){
		return m_Position;
	}
}
