using UnityEngine;
using System.Collections;

public class cGameData : ScriptableObject {
	private int m_WinningStreak;

	public float[] m_PayBack = new float[5];

	private int m_MaxMoney;

	public float m_Money;//{ get; set; }

	private float m_Bet;

	private float m_BufBet;

	public float m_Prise{ get; private set; }

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
			return (int)(m_Bet * m_PayBack [m_WinningStreak - 1] * 2);
		} else {
			return (int)(m_Bet * m_PayBack [m_WinningStreak - 1]);
		}
	}

	public void AddWin(){
		++m_WinningStreak;
		if (m_DoubleFlag == true) {
			m_Prise = (m_Bet * m_PayBack [m_WinningStreak - 1] * 2);
		} else {
			m_Prise = (m_Bet * m_PayBack [m_WinningStreak - 1]);
		}
	}
	public int GetWin(){
		return m_WinningStreak;
	}
	public void InitWin(){
		m_WinningStreak = 0;

		m_Prise = 0.0f;
	}

	public void Load(){
		m_DoubleFlag = false;
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
		float secound = Time.deltaTime * 1000;
		if (m_Prise - secound <= 0) {
			secound = m_Prise;

			m_Prise = 0.0f;
		} else {
			m_Prise -= secound;
		}

		m_Money += secound;

		if (m_Prise <= 0.0f) {
			m_Money = Mathf.Round (m_Money);
			return true;
		}

		return false;
	}

	public void PriseReturnSoon(){
		m_Money += m_Prise;

		m_Prise = 0.0f;

		m_Money = Mathf.Round (m_Money);
	}

	public bool Bet(){
		float secound = Time.deltaTime * 1000;
		if (m_BufBet - secound <= 0) {
			secound = m_BufBet;

			m_BufBet = 0.0f;
		} else {
			m_BufBet -= secound;
		}

		m_Money -= secound;

		if (m_BufBet <= 0.0f) {
			m_Money = Mathf.Round (m_Money);
			return true;
		}

		return false;
	}

	public void BetSoon(){
		m_Money -= m_BufBet;

		m_BufBet = 0.0f;

		m_Money = Mathf.Round (m_Money);
	}
}
