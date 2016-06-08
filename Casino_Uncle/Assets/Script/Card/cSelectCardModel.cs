using UnityEngine;
using System.Collections;

public class cSelectCardModel : cCardModel {

	public bool m_MoveFlag{ set; private get; }

	private bool m_TapFlag;
	private bool m_SelectFlag;

	private Vector2 m_BufPosition;

	public Vector2 m_BasePosition;

	public static float m_Line = 0.0f;

	void OnEnable(){
		Init ();
	}

	public void Init(){
		CardInit ();

		m_TapFlag = false;

		m_SelectFlag = false;

		m_MoveFlag = true;

		m_Position = m_BasePosition;

		m_BufPosition = m_Position;
	}

	public void ConfirmCard( Vector2 position ){
		if (m_TapFlag == true) {
			m_OutLineMode = eOutLineMode.eOutLineMode_None;

			if (m_Line < m_Position.y) {
				m_SelectFlag = true;
			} else {
				m_Position = m_BasePosition;
				m_BufPosition = m_Position;
			}
		} else if (m_MoveFlag == true) {
			m_TapFlag = true;
			m_BufPosition = position;

			m_Size = eSize.eSize_Large;
		}
	}

	public void UnTapCard(){
		if (m_TapFlag == true) {
			m_TapFlag = false;
			m_Size = eSize.eSize_Medium;
		}
	}

	public bool MoveSelectCard( Vector2 position ){
		m_Position = position;

		m_DrawMode = eDrawMode.eDrawMode_Front;

		m_Size = eSize.eSize_Medium;

		return true;
	}

	public void NotSelectCard(){
		m_DrawMode = eDrawMode.eDrawMode_Dark;
		m_Size = eSize.eSize_Small;
	}

	public void InitSelectCard(){
		m_DrawMode = eDrawMode.eDrawMode_Front;
		m_Size = eSize.eSize_Medium;
	}

	public void SelectCard( Vector2 position ){
		if (m_TapFlag == true) {

			m_Position += ( position - m_BufPosition );

			m_BufPosition = position;

			if (m_Line < m_Position.y) {
				m_OutLineMode = eOutLineMode.eOutLineMode_Yellow;
			} else {
				m_OutLineMode = eOutLineMode.eOutLineMode_Blue;
			}
		}
	}

	public bool GetTap(){
		return m_TapFlag;
	}

	public bool GetSelect(){
		return m_SelectFlag;
	}
}
