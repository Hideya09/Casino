using UnityEngine;
using System.Collections;

public class cEnemyDeckModel : ScriptableObject {

	private bool[] m_Deck;

	public cEnemyBattleCardModel m_bcModel;

	public void Init(){
		m_Deck = new bool[14];
		for (int i = 0; i < 14; ++i) {
			m_Deck [i] = false;
		}

		m_bcModel.Init ();
	}

	public void Hind(){
		m_bcModel.Init ();
	}

	public void Select( float setSecond ){
		int number;
		do {
			number = Random.Range (0, 14);
		} while(m_Deck [number] == true);

		m_Deck [number] = false;

		m_bcModel.m_CardNumber = number;
		m_bcModel.SetCard ();

		m_bcModel.MoveSet (setSecond);
	}

	public bool Move(){
		return m_bcModel.Move ();
	}

	public void Open(){
		m_bcModel.OpenCard ();
	}

	public int GetBattleCardNumber(){
		return m_bcModel.m_CardNumber;
	}
}
