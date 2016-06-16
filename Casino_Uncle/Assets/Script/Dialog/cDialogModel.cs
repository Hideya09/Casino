using UnityEngine;
using System.Collections;

public abstract class cDialogModel : ScriptableObject {
	private Vector3 m_Position;
	private float m_Rotation;

	private float m_Count;

	protected int[] m_NumberData;

	public cButtonModel[] m_buttonModel;

	public abstract cGameScene.eGameSceneList DialogExec();

	public abstract void Init (bool upPosition = true);

	protected void InitPositionUp(){
		m_Position = new Vector3 (0.0f, 600.0f , -705.0f);
		m_Rotation = -15;

		m_Count = 0.0f;
	}

	protected void InitPositionDpwn(){
		m_Position = new Vector3 (0.0f, -600.0f , -705.0f);
		m_Rotation = -15;

		m_Count = 0.0f;
	}

	protected bool StartDown(){
		m_Position.y -= 600.0f * Time.deltaTime;

		m_Rotation += 15.0f * Time.deltaTime;

		m_Count += Time.deltaTime;
		if (m_Count >= 1.0f) {
			m_Position.y = 0.0f;
			m_Rotation = 0.0f;

			m_Count = 0.0f;

			return true;
		}

		return false;
	}

	protected bool StartUp(){
		m_Position.y += 600.0f * Time.deltaTime;

		m_Rotation += 15.0f * Time.deltaTime;

		m_Count += Time.deltaTime;
		if (m_Count >= 1.0f) {
			m_Position.y = 0.0f;
			m_Rotation = 0.0f;

			m_Count = 0.0f;

			return true;
		}

		return false;
	}

	protected bool EndDown(){
		m_Position.y -= 600.0f * Time.deltaTime;

		m_Rotation -= 15.0f * Time.deltaTime;

		m_Count += Time.deltaTime;
		if (m_Count >= 1.0f) {
			m_Position.y = -600.0f;
			m_Rotation = -15.0f;

			m_Count = 0.0f;

			return true;
		}

		return false;
	}

	protected bool EndUp (){
		m_Position.y += 600.0f * Time.deltaTime;

		m_Rotation -= 15.0f * Time.deltaTime;

		m_Count += Time.deltaTime;
		if (m_Count >= 1.0f) {
			m_Position.y = 600.0f;
			m_Rotation = -15.0f;

			m_Count = 0.0f;

			return true;
		}

		return false;
	}

	public Vector3 GetPosition(){
		return m_Position;
	}

	public float GetRotation(){
		return m_Rotation;
	}

	public int[] GetNumberData(){
		return m_NumberData;
	}
}
