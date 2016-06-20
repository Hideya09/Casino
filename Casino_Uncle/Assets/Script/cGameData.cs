using UnityEngine;
using System.Collections;

public class cGameData : ScriptableObject {
	private int m_WinningStreak;

	public float[] m_PayBack = new float[5];

	private int m_MaxMoney;

	public int m_Money;//{ get; set; }
	public int m_StartMoney;

	private int m_Bet;

	private int m_BufBet;

	public int m_Prise{ get; private set; }

	private int m_Card;//{ get; set; }

	public int m_PlayerHitPoint{ get; set; }

	private bool m_DoubleFlag;

	public void MoneyBet( int money ){
		m_Bet = money;
		m_BufBet = m_Bet;
	}

	public int GetPayBack(){
		if (m_WinningStreak == 0) {
			return 0;
		}

		if (m_DoubleFlag == true) {
			return Mathf.RoundToInt ((m_Bet * m_PayBack [m_WinningStreak - 1] * 2));
		} else {
			return Mathf.RoundToInt ((m_Bet * m_PayBack [m_WinningStreak - 1] * 2));
		}
	}

	public void AddWin(){
		++m_WinningStreak;
		if (m_DoubleFlag == true) {
			m_Prise = Mathf.RoundToInt ((m_Bet * m_PayBack [m_WinningStreak - 1] * 2));
		} else {
			m_Prise = Mathf.RoundToInt ((m_Bet * m_PayBack [m_WinningStreak - 1]));
		}
	}
	public int GetWin(){
		return m_WinningStreak;
	}
	public void InitWin(){
		m_WinningStreak = 0;

		m_Prise = 0;

		m_DoubleFlag = false;
	}

	public void Load(){
		m_DoubleFlag = false;
	}

	public void Save(){
	}

	public bool GetDouble(){
		return m_DoubleFlag;
	}

	public void SetDouble(){
		m_DoubleFlag = true;
	}

	public int GetProfit(){
		return m_Money - m_StartMoney;
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

	public bool PriseReturn(){
		int secound = Mathf.RoundToInt (Time.deltaTime * 1000);
		if (m_Prise - secound <= 0) {
			secound = m_Prise;

			m_Prise = 0;
		} else {
			m_Prise -= secound;
		}

		m_Money += secound;

		if (m_Prise <= 0) {
			return true;
		}

		return false;
	}

	public void PriseReturnSoon(){
		m_Money += m_Prise;

		m_Prise = 0;

	}

	public bool Bet(){
		int secound = Mathf.RoundToInt (Time.deltaTime * 1000);
		if (m_BufBet - secound <= 0) {
			secound = m_BufBet;

			m_BufBet = 0;
		} else {
			m_BufBet -= secound;
		}

		m_Money -= secound;

		if (m_BufBet <= 0) {
			return true;
		}

		return false;
	}

	public void BetSoon(){
		m_Money -= m_BufBet;

		m_BufBet = 0;

	}
}
