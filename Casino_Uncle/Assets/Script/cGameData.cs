using UnityEngine;
using System.Collections;

public class cGameData : ScriptableObject {
	private int m_WinningStreak;

	public float[] m_PayBack = new float[5];

	private int m_MaxMoney;

	public int m_Money;//{ get; set; }

	public int m_bet;//{ get; set; }

	private int m_Card;//{ get; set; }

	public int m_PlayerHitPoint{ get; set; }

	public int GetPayBack( bool doubleFlag ){
		if (m_WinningStreak == 0) {
			return m_bet;
		}

		if (doubleFlag == true) {
			return m_bet * (int)m_PayBack [m_WinningStreak - 1] * 2;
		} else {
			return m_bet * (int)m_PayBack [m_WinningStreak - 1];
		}
	}

	public void AddWin(){
		++m_WinningStreak;

		Debug.Log (m_WinningStreak.ToString() + "連勝");
	}
	public int GetWin(){
		return m_WinningStreak;
	}
	public void InitWin(){
		m_WinningStreak = 0;
	}

	public void Load(){
	}

	public int GetCard(){
		return m_Card;
	}

	public void InitCard(){
		m_Card = 14;
	}

	public void CardMinus(){
		--m_Card;
	}
}
