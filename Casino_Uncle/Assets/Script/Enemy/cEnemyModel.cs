using UnityEngine;
using System.Collections;

public class cEnemyModel : ScriptableObject {

	public cEffectModel m_Damage;

	private Vector3 m_Position;
	private Vector3 m_BasePosition;

	public void InitPosition(Vector3 setPosition){
		m_BasePosition = setPosition;
		m_Position = m_BasePosition;
	}

	public void Init(){
		m_Position = m_BasePosition;
	}

	public void Vibration(){
		m_Damage.EffectStart ();
		m_Position.y = m_BasePosition.y + Random.Range (-0.1f, 0.1f);
	}

	public void StopVibration(){
		m_Damage.EffectEnd ();

		m_Position = m_BasePosition;
	}

	public Vector3 GetPosition(){
		return m_Position;
	}

	public void Start(){
		
	}

	public void End(){
		
	}
}
