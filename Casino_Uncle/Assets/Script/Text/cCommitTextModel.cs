using UnityEngine;
using System.Collections;

public class cCommitTextModel : ScriptableObject {

	public enum eCommitText{
		eCommitText_Win,
		eCommitText_Lose,
		eCommitText_Draw
	};

	private enum eCommitState{
		eCommitState_Apperance,
		eCommitState_Stop,
		eCommitState_Disappearance,
		eCommitState_End
	};

	public float m_ApperanceTime;
	public float m_StopTime;
	public float m_DisappearanceTime;

	private float m_Time;

	private eCommitText m_Text;

	private eCommitState m_State;

	private Vector2 m_BasePosition;
	private Vector2 m_Position;

	private float m_Progression;

	public void Init(){
		m_Progression = 0.0f;

		m_Position = m_BasePosition;
	}

	public void InitPosition( Vector2 setPosition ){
		m_BasePosition = setPosition;
		m_Position = setPosition;
	}

	public bool Move(){
		switch (m_State) {
		case eCommitState.eCommitState_Apperance:
			m_Progression += (Time.deltaTime * (1.0f / m_ApperanceTime));
			m_Time += Time.deltaTime;
			if (m_Time >= m_ApperanceTime) {
				++m_State;

				m_Time = 0.0f;
			}
			break;
		case eCommitState.eCommitState_Stop:
			m_Time += Time.deltaTime;
			if (m_Time >= m_StopTime) {
				++m_State;

				m_Time = 0.0f;
			}
			break;
		case eCommitState.eCommitState_Disappearance:
			m_Progression -= (Time.deltaTime * (1.0f / m_DisappearanceTime));

			m_Time += Time.deltaTime;

			m_Position.y += 1.0f;
			if (m_Time >= m_DisappearanceTime) {
				++m_State;

				m_Time = 0.0f;
			}
			break;
		case eCommitState.eCommitState_End:
			m_State = eCommitState.eCommitState_Apperance;
			return true;
		}

		return false;
	}

	public void SetText( eCommitText setText ){
		m_Text = setText;
	}

	public eCommitText GetText(){
		return m_Text;
	}

	public float GetProgression(){
		return m_Progression;
	}

	public Vector2 GetPosition(){
		return m_Position;
	}
}
