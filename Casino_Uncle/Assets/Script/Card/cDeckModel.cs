using UnityEngine;
using System.Collections;

public class cDeckModel : ScriptableObject {

	public int DeckMax;

	public cSelectCardModel[] m_selcModel;

	public cBattleCardModel m_bcModel;

	private bool[] m_Deck;

	private int m_SelectNumber;

	public bool m_LastBattle{ get; private set; }

	public void Init(){
		m_bcModel.Init ();

		m_Deck = new bool[DeckMax];

		for (int i = 0; i < DeckMax; ++i) {
			m_Deck[i] = false;
		}

		m_LastBattle = false;
	}

	public void RandomSet(){
		int[] random = new int[m_selcModel.Length];

		for (int i = 0; i < m_selcModel.Length; ++i) {
			random [i] = 14;
		}

		int deckCheck = 0;

		for (int i = 0; i < DeckMax; ++i) {
			if (m_Deck [i] == false) {
				++deckCheck;
			}
		}

		if (deckCheck == 3) {
			m_LastBattle = true;
		}

		int setNumber = 0;

		while (setNumber < m_selcModel.Length) {
			int number = Random.Range (0, DeckMax);

			if (m_Deck [number] == true) {
				continue;
			}

			int i;
			for (i = 0; i < setNumber; ++i) {
				if (random [i] == number) {
					break;
				}
			}

			if (i == setNumber) {
				random [setNumber] = number;
				++setNumber;
			}
		}

		for( int i = 0 ; i < m_selcModel.Length ; ++i ){
			m_selcModel [i].m_CardNumber = random [i];
			m_selcModel [i].Init ();
			m_selcModel [i].m_MoveFlag = true;
		}
	}

	public bool CardCheck(){
		int number = -1;

		bool selectFlag = false;

		for (int i = 0; i < m_selcModel.Length; ++i) {
			selectFlag = m_selcModel [i].GetSelect ();
			if (m_selcModel [i].GetTap () == true) {
				number = i;
				break;
			}
		}

		if (selectFlag == true) {
			for (int i = 0; i < m_selcModel.Length; ++i) {
				m_selcModel [i].m_MoveFlag = false;
			}
			m_SelectNumber = number;
			return true;
		} else if (number >= 0) {
			for (int i = 0; i < m_selcModel.Length; ++i) {
				if (number != i) {
					m_selcModel [i].NotSelectCard ();
				}
			}
		} else {
			for (int i = 0; i < m_selcModel.Length; ++i) {
				m_selcModel [i].InitSelectCard ();
			}
		}

		return false;
	}

	public bool CardMove(){
		if (m_selcModel [m_SelectNumber].MoveSelectCard (m_bcModel.GetPosition())) {
			m_bcModel.m_CardNumber = m_selcModel [m_SelectNumber].m_CardNumber;
			return true;
		}
		return false;
	}

	public void DuelEnd(){
		m_Deck [m_bcModel.m_CardNumber] = true;
	}
}
