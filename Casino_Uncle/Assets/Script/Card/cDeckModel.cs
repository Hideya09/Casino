using UnityEngine;
using System.Collections;

public class cDeckModel : ScriptableObject {

	public float HandOutSpeed;

	private Vector3 m_Position;

	private Vector3 m_BasePosition;
	private Vector3 m_Movement;

	public Vector3 m_StartPosition;
	public Vector3 m_ReturnPosition;

	private float m_ReturnCount;
	public float m_MaxReturnCount;
	private const float m_EndCount = 1.0f;

	public float m_Fade{ get; private set; }

	public int DeckMax;

	public cSelectCardModel[] m_selcModel;

	public cBattleCardModel m_bcModel;

	private bool[] m_Deck;

	private int m_SelectNumber;

	public bool m_LastBattle{ get; private set; }
	public bool m_DoubleBattle{ get; private set; }

	public void SetPosition( Vector3 setPosition ){
		m_BasePosition = setPosition;
	}

	public void Init(){
		m_bcModel.Init ();

		m_Deck = new bool[DeckMax];

		for (int i = 0; i < DeckMax; ++i) {
			m_Deck[i] = false;
		}

		for( int i = 0 ; i < m_selcModel.Length ; ++i ){
			m_selcModel [i].CardInit ();
		}

		m_LastBattle = false;

		m_Fade = 1.0f;

		m_Position = m_StartPosition;

		m_DoubleBattle = false;
	}

	public void RandomSet(){
		m_bcModel.Init ();

		int[] random = new int[m_selcModel.Length];

		for (int i = 0; i < m_selcModel.Length; ++i) {
			random [i] = cCardSpriteManager.Back;
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
			
		int doubleCharengeFlag = 0;

		for( int i = 0 ; i < m_selcModel.Length ; ++i ){
			m_selcModel [i].m_CardNumber = random [i];
			m_selcModel [i].SetSelect ();
			m_selcModel [i].Init ( m_Position , HandOutSpeed);

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

		m_selcModel [m_SelectNumber].Init (m_Position, HandOutSpeed);
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

		if (deckCheck > 0) {
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

	public void MoveAngleSet( float reachingSecond , float returnSecond ){
		m_bcModel.SetMove (reachingSecond, returnSecond);
	}

	public void MoveAngle(){
		m_bcModel.MoveAngle ();
	}

	public void ReturnAngle(){
		m_bcModel.ReturnAngle ();
	}

	public void SetAngle(){
		m_bcModel.SetAngle ();
	}

	public void Snap(){
		m_bcModel.SnapMove ();
	}

	public bool MoveBack(){
		bool endFlag = Back ();

		endFlag &= m_bcModel.Back ();

		for (int i = 0; i < m_selcModel.Length; ++i) {
			endFlag = m_selcModel [i].Back ();
		}

		return endFlag;
	}

	public void MoveSet(){
		m_Movement = m_ReturnPosition - m_Position;

		m_Movement /= m_MaxReturnCount;

		m_Position = m_StartPosition;
		m_Fade = 1.0f;

		m_ReturnCount = 0.0f;
	}

	public bool Move(){

		if (m_ReturnCount == 0.0f) {
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_In);
		}

		m_ReturnCount += Time.deltaTime;

		m_Position += m_Movement * Time.deltaTime;

		if (m_ReturnCount >= m_MaxReturnCount) {
			m_Position = m_ReturnPosition;
			return true;
		}

		return false;
	}

	public void ReturnSet(){
		m_Movement = m_BasePosition - m_Position;
		m_Movement /= m_EndCount;

		m_ReturnCount = 0.0f;
	}

	public bool Return(){
		m_ReturnCount += Time.deltaTime;

		m_Position += m_Movement * Time.deltaTime;

		if (m_ReturnCount >= m_EndCount) {
			m_Position = m_BasePosition;
			m_ReturnCount = m_EndCount;
			return true;
		}

		return false;
	}

	public void BackSet(){
		m_Movement = m_StartPosition - m_Position;
		m_Movement /= m_MaxReturnCount;

		m_ReturnCount = 0.0f;
	}

	public bool Back(){
		m_ReturnCount += Time.deltaTime;

		m_Position += m_Movement * Time.deltaTime;

		m_Fade -= Time.deltaTime;

		if (m_Fade <= 0.0f) {
			m_Position = m_StartPosition;
			return true;
		}

		return false;
	}

	public Vector3 GetPosition(){
		return m_Position;
	}

	public void SelectStop(){
		for (int i = 0; i < m_selcModel.Length; ++i) {
			m_selcModel [i].m_MoveFlag = false;
		}
	}
}
