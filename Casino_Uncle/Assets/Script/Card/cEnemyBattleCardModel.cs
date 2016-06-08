using UnityEngine;
using System.Collections;

public class cEnemyBattleCardModel : cCardModel {

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