using UnityEngine;
using System.Collections;

public class cGameData : ScriptableObject {
	private int m_WinningStreak;

	private int m_MaxMoney;

	private int m_Money;

	public int LifeMax;

	private int m_playerLife;
	private int m_enemyLife;

	public void InitLife(){
		m_playerLife = LifeMax;
		m_enemyLife = LifeMax;
	}

	public void AddWin(){
		++m_WinningStreak;

		Debug.Log (m_WinningStreak.ToString() + "連勝");
	}
	public void InitWin(){
		m_WinningStreak = 0;
	}

	public void Load(){
		InitLife ();
	}

	public int PlayerDamege( int damege = 0 ){
		m_playerLife -= damege;
		if (m_playerLife < 0) {
			m_playerLife = 0;
		}

		Debug.Log ("プレイヤー " + m_playerLife.ToString() + "  エネミー " + m_enemyLife.ToString());

		return m_playerLife;
	}
	public int EnemyDamege( int damege = 0 ){
		m_enemyLife -= damege;
		if (m_enemyLife < 0) {
			m_enemyLife = 0;
		}

		Debug.Log ("プレイヤー " + m_playerLife.ToString() + "  エネミー " + m_enemyLife.ToString());

		return m_enemyLife;
	}
}
