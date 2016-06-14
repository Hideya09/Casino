using UnityEngine;
using System.Collections;

public class cBattleCardModel : cCardModel {

	private Vector2 m_BasePosition;

	public float m_MaxAngle;
	public float m_Speed;

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



	public void SnapMove(){
		m_Position.x += m_Speed * Time.deltaTime;
	}
}
