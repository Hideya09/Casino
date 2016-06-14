using UnityEngine;
using System.Collections;

public class cButtonModel : ScriptableObject {
	private bool m_Start;

	private bool m_TapFlag;

	private bool m_Selct;

	public int m_ButtonNumber;

	public void Init(){
		m_Start = false;

		m_TapFlag = false;

		m_Selct = false;
	}

	public void Start(){
		m_Start = true;
	}

	public void End(){
		m_Start = false;
	}

	public void TapButton(){
		if (m_Start == true) {
			m_TapFlag = true;
		}
	}

	public void UnTapButton(){
		if (m_Start == true) {
			m_TapFlag = false;
		}
	}

	public void SelectButton(){
		if (m_Start == true) {
			m_Selct = true;
		}
	}

	public bool GetTap(){
		return m_TapFlag;
	}

	public int GetSelect(){
		if (m_Selct == true) {
			return m_ButtonNumber;
		}

		return 0;
	}
}
