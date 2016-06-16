﻿using UnityEngine;
using System.Collections;

public class cCardModel : ScriptableObject {
	protected Vector2 m_Position;
	protected Vector3 m_Rotation;

	private bool m_EndOpen;

	protected float m_Fade;

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

	void OnEnable(){
		m_DrawMode = eDrawMode.eDrawMode_None;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;
		m_Size = eSize.eSize_Medium;
	}

	public virtual void InitPosition( Vector2 position ){
		m_Position = position;
		m_Rotation = Vector3.zero;
	}

	public Vector2 GetPosition(){
		return m_Position;
	}

	public Vector3 GetRotation(){
		return m_Rotation;
	}

	public void CardInit(){
		m_DrawMode = eDrawMode.eDrawMode_None;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;
		m_Size = eSize.eSize_Medium;

		m_Fade = 1.0f;

		m_EndOpen = false;
	}

	public bool Open(){
		m_EndOpen = false;

		if (m_DrawMode == eDrawMode.eDrawMode_Back) {
			m_Rotation.y += 360 * Time.deltaTime;

			if (m_Rotation.y >= 90) {
				m_Rotation.y = 270 + (m_Rotation.y - 90);
				m_DrawMode = eDrawMode.eDrawMode_Front;
			}
		} else if (m_DrawMode == eDrawMode.eDrawMode_Front) {
			m_Rotation.y += 360 * Time.deltaTime;

			if (m_Rotation.y >= 360 || m_Rotation.y < 45) {
				m_Rotation.y = 0;

				m_EndOpen = true;
			}
			return true;
		}

		return false;
	}

	public bool Close(){
		m_EndOpen = true;

		if (m_DrawMode == eDrawMode.eDrawMode_Front) {
			m_Rotation.y += 360 * Time.deltaTime;

			if (m_Rotation.y >= 90) {
				m_Rotation.y = 270 + (m_Rotation.y - 90);
				m_DrawMode = eDrawMode.eDrawMode_Back;
			}
		} else if (m_DrawMode == eDrawMode.eDrawMode_Back) {
			m_Rotation.y += 360 * Time.deltaTime;

			if (m_Rotation.y >= 360 || m_Rotation.y < 45) {
				m_Rotation.y = 0;

				m_EndOpen = false;
			}
			return true;
		}

		return false;
	}

	public bool Back(){
		m_Fade -= Time.deltaTime;
		m_Position.y -= Time.deltaTime * 10;
		if (m_Fade <= 0.0f) {
			return true;
		}

		return false;
	}

	public float GetFade(){
		return m_Fade;
	}

	public bool GetOpen(){
		return m_EndOpen;
	}
}
