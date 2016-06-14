using UnityEngine;
using System.Collections;

public class cCardModel : ScriptableObject {
	protected Vector2 m_Position;
	protected float m_Rotation;

	private bool m_EndOpen;

	public enum eDrawMode{
		eDrawMode_None,
		eDrawMode_Front,
		eDrawMode_Dark,
		eDrawMode_Back
	}

	public enum eOutLineMode{
		eOutLineMode_None,
		eOutLineMode_Yellow,
		eOutLineMode_Blue,
		eOutLineMode_Red,
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

	public virtual void InitPosition( Vector2 position ){
		m_Position = position;
		m_Rotation = 0;
	}

	public Vector2 GetPosition(){
		return m_Position;
	}

	public float GetRotation(){
		return m_Rotation;
	}

	public void CardInit(){
		m_DrawMode = eDrawMode.eDrawMode_Front;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;
		m_Size = eSize.eSize_Medium;

		m_EndOpen = false;
	}

	public bool Open(){
		m_EndOpen = false;

		if (m_DrawMode == eDrawMode.eDrawMode_Back) {
			m_Rotation += 360 * Time.deltaTime;

			if (m_Rotation >= 90) {
				m_Rotation = 270 + (m_Rotation - 90);
				m_DrawMode = eDrawMode.eDrawMode_Front;
			}
		} else if (m_DrawMode == eDrawMode.eDrawMode_Front) {
			m_Rotation += 360 * Time.deltaTime;

			if (m_Rotation >= 360 || m_Rotation < 45) {
				m_Rotation = 0;

				m_EndOpen = true;
			}
			return true;
		}

		return false;
	}

	public bool Close(){
		m_EndOpen = true;

		if (m_DrawMode == eDrawMode.eDrawMode_Front) {
			m_Rotation += 360 * Time.deltaTime;

			if (m_Rotation >= 90) {
				m_Rotation = 270 + (m_Rotation - 90);
				m_DrawMode = eDrawMode.eDrawMode_Back;
			}
		} else if (m_DrawMode == eDrawMode.eDrawMode_Back) {
			m_Rotation += 360 * Time.deltaTime;

			if (m_Rotation >= 360 || m_Rotation < 45) {
				m_Rotation = 0;

				m_EndOpen = false;
			}
			return true;
		}

		return false;
	}

	public bool GetOpen(){
		return m_EndOpen;
	}
}
