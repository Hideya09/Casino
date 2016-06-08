using UnityEngine;
using System.Collections;

public class cBattleCardModel : cCardModel {
	public void Init(){
		m_DrawMode = eDrawMode.eDrawMode_None;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;
	}

	public void SetCard(){
		m_DrawMode = eDrawMode.eDrawMode_Front;
		m_OutLineMode = eOutLineMode.eOutLineMode_Red;
	}
}
