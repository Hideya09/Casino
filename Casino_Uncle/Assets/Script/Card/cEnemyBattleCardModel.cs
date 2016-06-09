using UnityEngine;
using System.Collections;

public class cEnemyBattleCardModel : cCardModel {

	private Vector2 m_BasePosition;
	public Vector2 m_StartPosition;

	public Vector2 m_MoveAdd;

	private float m_Second;
	private float m_SecondMax;

	public override void InitPosition( Vector2 position ){
		m_BasePosition = position;
		m_Position = m_StartPosition;
	}

	public void Init(){
		m_DrawMode = eDrawMode.eDrawMode_None;
		m_Size = eSize.eSize_Medium;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;

		m_Position = m_StartPosition;

		m_Second = 0.0f;
		m_SecondMax = 0.0f;
	}

	public void SetCard(){
		m_DrawMode = eDrawMode.eDrawMode_Back;
		m_Size = eSize.eSize_Medium;
		m_OutLineMode = eOutLineMode.eOutLineMode_Red;
	}

	public void OpenCard(){
		m_DrawMode = eDrawMode.eDrawMode_Front;
	}

	public void MoveSet( float reachingSecond ){
		m_MoveAdd = m_BasePosition - m_StartPosition;
		m_MoveAdd *= (1 / reachingSecond);

		m_Second = 0.0f;
		m_SecondMax = reachingSecond;
	}

	public bool Move(){
		m_Position += (m_MoveAdd * Time.deltaTime);

		m_Second += Time.deltaTime;

		if (m_Second >= m_SecondMax) {
			m_Position = m_BasePosition;

			return true;
		} else {
			return false;
		}
	}
}