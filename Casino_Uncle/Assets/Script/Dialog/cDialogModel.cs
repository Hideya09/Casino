using UnityEngine;
using System.Collections;

public abstract class cDialogModel : ScriptableObject {
	private Vector3 m_Position;

	public float m_StartPosition = 600.0f;

	public float m_RotationStart;

	private float m_Rotation;

	private float m_Count;

	public float m_StartCountMax;
	public float m_EndCountMax;

	protected int[] m_NumberData;
	protected float[] m_NumberData2;

	public cButtonModel[] m_buttonModel;

	public abstract cGameScene.eGameSceneList DialogExec();

	public abstract void Init (bool upPosition = true);

	protected void InitPositionUp(){
		m_Position = new Vector3 (0.0f, m_StartPosition , -705.0f);
		m_Rotation = -m_RotationStart;

		m_Count = 0.0f;
	}

	protected void InitPositionDpwn(){
		m_Position = new Vector3 (0.0f, -m_StartPosition , -705.0f);
		m_Rotation = -m_RotationStart;

		m_Count = 0.0f;
	}

	protected bool StartDown(){
		m_Position.y -= m_StartPosition * (Time.deltaTime / m_StartCountMax);

		m_Rotation += m_RotationStart * (Time.deltaTime / m_StartCountMax);

		if (m_Count == 0.0f) {
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Slide);
		}

		m_Count += Time.deltaTime;
		if (m_Count >= m_StartCountMax) {
			m_Position.y = 0.0f;
			m_Rotation = 0.0f;

			m_Count = 0.0f;

			return true;
		}

		return false;
	}

	protected bool StartUp(){
		m_Position.y += m_StartPosition * (Time.deltaTime / m_StartCountMax);

		m_Rotation += m_RotationStart * (Time.deltaTime / m_StartCountMax);

		if (m_Count == 0.0f) {
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Slide);
		}

		m_Count += Time.deltaTime;
		if (m_Count >= m_StartCountMax) {
			m_Position.y = 0.0f;
			m_Rotation = 0.0f;

			m_Count = 0.0f;

			return true;
		}

		return false;
	}

	protected bool EndDown(){
		m_Position.y -= m_StartPosition * (Time.deltaTime / m_EndCountMax);

		m_Rotation -= m_RotationStart * (Time.deltaTime / m_EndCountMax);

		if (m_Count == 0.0f) {
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Slide);
		}

		m_Count += Time.deltaTime;
		if (m_Count >= m_EndCountMax) {
			m_Position.y = -m_StartPosition;
			m_Rotation = -m_RotationStart;

			m_Count = 0.0f;

			return true;
		}

		return false;
	}

	protected bool EndUp (){
		m_Position.y += m_StartPosition * (Time.deltaTime / m_EndCountMax);

		m_Rotation -= m_RotationStart * (Time.deltaTime / m_EndCountMax);

		if (m_Count == 0.0f) {
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Slide);
		}

		m_Count += Time.deltaTime;
		if (m_Count >= m_EndCountMax) {
			m_Position.y = m_StartPosition;
			m_Rotation = -m_RotationStart;

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

	public float[] GetNumberData2(){
		return m_NumberData2;
	}
}
