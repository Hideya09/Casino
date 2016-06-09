using UnityEngine;
using System.Collections;

public class cEnemyBattleCardModel : cCardModel {

	public Vector2 m_BasePosition;

	public override void InitPosition( Vector2 position ){
		m_BasePosition = position;
	}

	public void Init(){
		m_DrawMode = eDrawMode.eDrawMode_None;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;
	}

	public void SetCard(){
		m_DrawMode = eDrawMode.eDrawMode_Back;
		m_OutLineMode = eOutLineMode.eOutLineMode_Red;
	}

	public void OpenCard(){
		m_DrawMode = eDrawMode.eDrawMode_Front;
	}
}