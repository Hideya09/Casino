using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class cGameData : ScriptableObject {
	private int m_WinningStreak;
	private int m_DuelStartWinningStreak;

	public float[] m_PayBack = new float[5];

	public bool m_WinLose{ get; set; }

	public int m_MaxMoney{ get; private set; }

	public int m_Money{ get; private set; }
	private int m_StartMoney;

	private int m_Bet;

	private int m_BufBet;

	public int m_Prise{ get; private set; }

	private int m_Card;//{ get; set; }

	public int m_PlayerHitPoint{ get; set; }

	private bool m_DoubleFlag;

	void OnEnable(){
		m_Money = 100;
	}

	public void StartMoneySet(){
		m_StartMoney = m_Money;
	}

	public void MoneyBet( int money ){
		m_Bet = money;
		m_BufBet = m_Bet;
	}

	public int GetBet(){
		if (m_Bet > m_Money) {
			m_Bet = 100;
		}

		return m_Bet;
	}

	public int GetPayBack(){
		if (m_WinningStreak < 0) {
			return 0;
		}

		if (m_DoubleFlag == true) {
			return Mathf.RoundToInt ((m_Bet * m_PayBack [m_WinningStreak - 1] * 2));
		} else {
			return Mathf.RoundToInt ((m_Bet * m_PayBack [m_WinningStreak - 1]));
		}
	}

	public int GetPayBack( int winningStreak ){
		if (winningStreak <= 0 || winningStreak > 5) {
			return 0;
		}

		if (m_DoubleFlag == true) {
			return Mathf.RoundToInt ((m_Bet * m_PayBack [winningStreak - 1] * 2));
		} else {
			return Mathf.RoundToInt ((m_Bet * m_PayBack [winningStreak - 1]));
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

	public void SubWin(){
		--m_WinningStreak;

		if (m_WinningStreak <= 0) {
			m_Prise = 0;
		} else if (m_DoubleFlag == true) {
			m_Prise = Mathf.RoundToInt ((m_Bet * m_PayBack [m_WinningStreak - 1] * 2));
		} else {
			m_Prise = Mathf.RoundToInt ((m_Bet * m_PayBack [m_WinningStreak - 1]));
		}
	}

	public int GetWin(){
		return m_WinningStreak;
	}

	public void AddStartWin(){
		m_DuelStartWinningStreak = m_WinningStreak;
	}
	public int GetStartWin(){
		if (m_WinLose == true) {
			return m_DuelStartWinningStreak;
		} else {
			return m_DuelStartWinningStreak + 5;
		}
	}

	public void InitWin(){
		m_WinningStreak = 0;
		m_WinLose = true;
		m_DuelStartWinningStreak = m_WinningStreak;

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

		m_Bet = 100;
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

	public bool PriseReturn( bool returnMoney = true ){
		int secound = Mathf.RoundToInt (Time.deltaTime * 1000);
		if (m_Prise - secound <= 0) {
			secound = m_Prise;

			m_Prise = 0;
		} else {
			m_Prise -= secound;
		}

		if (returnMoney == true) {
			m_Money += secound;

			if (m_Money > 999999999) {
				m_Money = 999999999;
			}
		}

		if (m_Prise <= 0) {
			if (m_Money > m_MaxMoney) {
				m_MaxMoney = m_Money;
			}
			return true;
		}

		return false;
	}

	public void PriseReturnSoon( bool returnMoney = true ){
		if (returnMoney == true) {
			m_Money += m_Prise;

			if (m_Money > 999999999) {
				m_Money = 999999999;
			}
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
