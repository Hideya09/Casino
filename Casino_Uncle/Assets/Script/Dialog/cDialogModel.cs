using UnityEngine;
using System.Collections;

public abstract class cDialogModel : ScriptableObject {
	private Vector3 m_Position;

	//開始位置
	public float m_StartPosition;

	//初期回転量
	public float m_RotationStart;

	private float m_Rotation;

	private float m_Count;

	//開始と終わりの時間
	public float m_StartCountMax;
	public float m_EndCountMax;

	//ダイアログ内で表示する数値のバッファ
	protected int[] m_NumberData;
	protected float[] m_NumberData2;

	//使用するボタンのモデル
	public cButtonModel[] m_buttonModel;

	//ダイアログごとの処理
	public abstract cGameScene.eGameSceneList DialogExec();

	//ダイアログごとの初期化処理
	public abstract void Init (bool upPosition = true);

	//出現位置が上からの時の初期化
	protected void InitPositionUp(){
		m_Position = new Vector3 (0.0f, m_StartPosition , -705.0f);
		m_Rotation = -m_RotationStart;

		m_Count = 0.0f;
	}

	//出現位置が下からの時の初期化
	protected void InitPositionDpwn(){
		m_Position = new Vector3 (0.0f, -m_StartPosition , -705.0f);
		m_Rotation = -m_RotationStart;

		m_Count = 0.0f;
	}

	//下がりながら画面に入る
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

	//上がりながら画面に入る
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

	//下がりながら画面から出る
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

	//上がりながら画面から出る
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
