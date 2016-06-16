using UnityEngine;
using System.Collections;

public class cHitPointManager : ScriptableObject {

	public cHitPointModel[] m_hpModel;

	private int m_CutBackTotalNumber;

	public int m_HitPointMax;

	public int m_Damage{ set; private get; }

	public void Init(){
		m_CutBackTotalNumber = 0;

		for (int i = 0; i < m_hpModel.Length; ++i) {
			m_hpModel [i].Init ();
		}
	}

	public bool CutBack(){
		bool retFlag = true;

		for (int i = m_CutBackTotalNumber; i < (m_CutBackTotalNumber + m_Damage) && i < m_hpModel.Length; ++i) {
			retFlag &= m_hpModel [i].CutBack ();
		}

		if (retFlag == true) {
			m_CutBackTotalNumber += m_Damage;

			m_Damage = 0;
		}

		return retFlag;
	}

	public bool HitPointCheck(){
		return m_CutBackTotalNumber >= m_HitPointMax;
	}

	public int GetHitPoint(){
		return m_HitPointMax - m_CutBackTotalNumber;
	}

	public void FadeInit(){
		for (int i = 0; i < m_hpModel.Length; ++i) {
			m_hpModel [i].FadeInit ();
		}
	}

	public bool Fade(){
		bool endFlag = true;

		for (int i = 0; i < m_hpModel.Length; ++i) {
			endFlag &= m_hpModel [i].Fade ();
		}

		return endFlag;
	}

	public void MoveSet(){
		for (int i = 0; i < m_hpModel.Length; ++i) {
			m_hpModel [i].MoveSet ();
		}
	}

	public bool Move(){
		bool ret = true;

		for (int i = 0; i < m_hpModel.Length; ++i) {
			ret &= m_hpModel [i].Move ();
		}

		return ret;
	}

	public void ReturnSet(){
		for (int i = 0; i < m_hpModel.Length; ++i) {
			m_hpModel [i].ReturnSet ();
		}
	}

	public bool Return(){
		bool ret = true;

		for (int i = 0; i < m_hpModel.Length; ++i) {
			ret &= m_hpModel [i].Return ();
		}

		return ret;
	}

	public void BackSet(){
		for (int i = 0; i < m_hpModel.Length; ++i) {
			m_hpModel [i].BackSet ();
		}
	}

	public bool Back(){
		bool ret = true;

		for (int i = 0; i < m_hpModel.Length; ++i) {
			ret &= m_hpModel [i].Back ();
		}

		return ret;
	}
}
