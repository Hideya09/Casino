using UnityEngine;
using System.Collections;

public class cDeckModel : ScriptableObject {

	public float HandOutSpeed;

	public Vector2 DeckPosition;

	public int DeckMax;

	public cSelectCardModel[] m_selcModel;

	public cBattleCardModel m_bcModel;

	private bool[] m_Deck;

	private int m_SelectNumber;

	public bool m_LastBattle{ get; private set; }
	public bool m_DoubleBattle{ get; private set; }

	public void Init(){
		m_bcModel.Init ();

		m_Deck = new bool[DeckMax];

		for (int i = 0; i < DeckMax; ++i) {
			m_Deck[i] = false;
		}

		m_LastBattle = false;
	}

	public void RandomSet(){
		m_bcModel.Init ();

		int[] random = new int[m_selcModel.Length];

		for (int i = 0; i < m_selcModel.Length; ++i) {
			random [i] = 14;
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

		m_DoubleBattle = false;
		int doubleCharengeFlag = 0;

		for( int i = 0 ; i < m_selcModel.Length ; ++i ){
			m_selcModel [i].m_CardNumber = random [i];
			m_selcModel [i].SetSelect ();
			m_selcModel [i].Init ( DeckPosition , HandOutSpeed);

			if (random [i] == 1 || random [i] == 2 || random [i] == 3) {
				++doubleCharengeFlag;
			}
		}

		if (doubleCharengeFlag == 3) {
			m_DoubleBattle = true;
		}
	}

	public void EditCard(){
		m_bcModel.Init ();

		if (DeckCheck () == true) {
			for (int i = 0; i < m_selcModel.Length; ++i) {

				m_selcModel [i].SetSelect ();

				if (m_selcModel [i].m_CardNumber == DeckMax) {
					do {
						int number = Random.Range (0, DeckMax);

						if (m_Deck [number] == true) {
							continue;
						}

						int j;

						for (j = 0; j < m_selcModel.Length; ++j) {
							if (m_selcModel [j].m_CardNumber == number) {
								break;
							}
						}
						if (j == m_selcModel.Length) {
							m_selcModel [i].m_CardNumber = number;
						}
					} while(m_selcModel [i].m_CardNumber == DeckMax);
				}
			}
		}

		m_selcModel [m_SelectNumber].Init (DeckPosition, HandOutSpeed);
	}

	public bool CardCheck(){
		int number = -1;

		bool selectFlag = false;

		for (int i = 0; i < m_selcModel.Length; ++i) {
			m_selcModel [i].m_MoveFlag = true;
		}

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
			m_bcModel.SetCard ();
			return true;
		}
		return false;
	}

	public void CardEnd(){
		m_Deck [m_bcModel.m_CardNumber] = true;
		m_selcModel [m_SelectNumber].m_CardNumber = DeckMax;
	}

	public int GetBattleCardNumber(){
		return m_bcModel.m_CardNumber;
	}

	private bool DeckCheck(){
		int deckCheck = 0;

		for (int i = 0; i < DeckMax; ++i) {
			if (m_Deck [i] == false) {
				++deckCheck;
			}
		}

		if (deckCheck > 2) {
			return true;
		}

		return false;
	}

	public bool HandOnCard(){
		bool endFlag = true;

		for (int i = 0; i < m_selcModel.Length; ++i) {
			endFlag &= m_selcModel [i].Move ();
		}

		return endFlag;
	}

	public bool CardOpen(){
		bool endFlag = true;

		for (int i = m_selcModel.Length - 1; i >= 0; --i) {
			if (m_selcModel [i].Open() == true) {
				endFlag &= m_selcModel [i].GetOpen ();
			} else {
				endFlag = false;
				break;
			}
		}

		return endFlag;
	}
}
