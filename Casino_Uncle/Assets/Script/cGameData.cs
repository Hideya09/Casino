using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class cGameData : ScriptableObject {
	private int m_WinningStreak;

	public float[] m_PayBack = new float[5];

	public int m_MaxMoney{ get; private set; }

	public int m_Money{ get; private set; }
	private int m_StartMoney;

	private int m_Bet;

	private int m_BufBet;

	public int m_Prise{ get; private set; }

	private int m_Card;//{ get; set; }

	public int m_PlayerHitPoint{ get; set; }

	private bool m_DoubleFlag;

	public void StartMoneySet(){
		m_StartMoney = m_Money;
	}

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
			return Mathf.RoundToInt ((m_Bet * m_PayBack [m_WinningStreak - 1]));
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

		TextAsset file = (TextAsset)Resources.Load ("SaveFile/save");

		StringReader reader = new StringReader (file.text);

		string data = reader.ReadLine ();

		string[] moneyData = data.Split (',');

		m_MaxMoney = int.Parse (moneyData[0]);
		m_Money = int.Parse (moneyData[1]);
	}

	public void Save(){
		if (m_Money < 100) {
			m_Money = 100;
		}

		FileInfo file = new FileInfo( Application.dataPath + "/Resources/SaveFile/save.csv");

		StreamWriter write = file.CreateText ();

		string str = m_MaxMoney.ToString () + ',' + m_Money.ToString ();

		write.WriteLine(str);

		write.Flush ();
		write.Close ();
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

		if (m_Money > 999999999) {
			m_Money = 999999999;
		}

		if (m_Prise <= 0) {
			if (m_Money > m_MaxMoney) {
				m_MaxMoney = m_Money;
			}
			return true;
		}

		return false;
	}

	public void PriseReturnSoon(){
		m_Money += m_Prise;

		if (m_Money > 999999999) {
			m_Money = 999999999;
		}

		if (m_Money > m_MaxMoney) {
			m_MaxMoney = m_Money;
		}

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
