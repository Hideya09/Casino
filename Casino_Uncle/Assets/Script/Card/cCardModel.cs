using UnityEngine;
using System.Collections;

public class cCardModel : ScriptableObject {
	protected Vector2 m_Position;

	public enum eDrawMode{
		eDrawMode_None,
		eDrawMode_Front,
		eDrawMode_Dark,
		eDrawMode_Back
	}

	public enum eOutLineMode{
		eOutLineMode_None,
		eOutLineMode_Yellow,
		eOutLineMode_Blue
	}

	public enum eSize{
		eSize_Small,
		eSize_Medium,
		eSize_Large
	}

	public eDrawMode m_DrawMode{ get; protected set; }
	public eOutLineMode m_OutLineMode{ get; protected set; }
	public eSize m_Size{ get; protected set; }

	public int m_CardNumber{ set; get; }

	public Vector2 GetPosition(){
		return m_Position;
	}

	public void CardInit(){
		m_DrawMode = eDrawMode.eDrawMode_Front;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;
		m_Size = eSize.eSize_Medium;
	}
}
