using UnityEngine;
using System.Collections;

//ヒットポイントオブジェクトの管理
public class cHitPointManager : ScriptableObject {

	public cHitPointModel[] m_hpModel;

	//今まで受けた総ダメージ
	private int m_CutBackTotalNumber;

	//ヒットポイントの最大値
	public int m_HitPointMax;

	//受けるダメージ
	public int m_Damage{ set; private get; }

	public void Init(){
		m_CutBackTotalNumber = 0;

		for (int i = 0; i < m_hpModel.Length; ++i) {
			m_hpModel [i].Init ();
		}
	}
		
	public bool CutBack(){
		bool retFlag = true;

		//受けたダメージ文HPオブジェクトを演出させる
		for (int i = m_CutBackTotalNumber; i < (m_CutBackTotalNumber + m_Damage) && i < m_hpModel.Length; ++i) {
			retFlag &= m_hpModel [i].CutBack ();
		}

		//演出が終わったら総ダメージを増やす
		if (retFlag == true) {
			m_CutBackTotalNumber += m_Damage;

			m_Damage = 0;
		}

		return retFlag;
	}

	//生死判定
	public bool HitPointCheck(){
		return m_CutBackTotalNumber >= m_HitPointMax;
	}

	//現在のヒットポイント
	public int GetHitPoint(){
		return m_HitPointMax - m_CutBackTotalNumber;
	}

	//以下入退場の処理
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

	public bool Back( float m_FadeTime ){
		bool ret = true;

		for (int i = 0; i < m_hpModel.Length; ++i) {
			ret &= m_hpModel [i].Back (m_FadeTime);
		}

		return ret;
	}
}
