using UnityEngine;
using System.Collections;

public class cSelectCardModel : cCardModel {

	public bool m_MoveFlag{ set; private get; }

	private bool m_TapFlag;
	private bool m_SelectFlag;

	private Vector2 m_BufPosition;

	private Vector2 m_BasePosition;

	private Vector2 m_Movement;
	private float m_Count;
	private float m_MaxCount;

	public static float m_Line = 0.0f;

	void OnEnable(){
		CardInit ();

		m_TapFlag = false;

		m_SelectFlag = false;

		m_MoveFlag = false;
	}

	public override void InitPosition( Vector2 position ){
		m_BasePosition = position;

		m_Rotation = Vector3.zero;
	}

	public void Init( Vector2 setPosition , float speed ){
		m_DrawMode = eDrawMode.eDrawMode_Back;
		m_Size = eSize.eSize_Medium;

		m_Fade = 0.0f;

		m_Position = setPosition;

		m_BufPosition = m_Position;

		m_MaxCount = Vector2.Distance (m_BasePosition, m_Position) / speed;

		m_Movement = ( m_BasePosition - m_Position ) / m_MaxCount;

		m_Count = 0;
	}

	public void SetSelect(){
		CardInit ();

		m_TapFlag = false;

		m_SelectFlag = false;
	}

	public bool Move(){

		m_Fade = 1.0f;

		m_Count += Time.deltaTime;

		if (m_Count < m_MaxCount) {
			m_Position += (Time.deltaTime * m_Movement);

			return false;
		} else {
			m_Position = m_BasePosition;

			return true;
		}
	}

	public void ConfirmCard(){
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

			m_DrawMode = eDrawMode.eDrawMode_Front;
			m_Size = eSize.eSize_Large;
		}
	}

	public void SetPosition( Vector2 position ){
		m_BufPosition = position;
	}

	public void UnTapCard(){
		if (m_TapFlag == true) {
			m_TapFlag = false;
			m_Size = eSize.eSize_Medium;
		}
	}

	public bool MoveSelectCard( Vector2 position ){
		m_Position = position;

		m_DrawMode = eDrawMode.eDrawMode_None;

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
