using UnityEngine;
using System.Collections;

public class cEnemyDeckModel : ScriptableObject {

	[SerializeField]
	private bool[] m_Deck;

	private bool m_Joker;

	private int m_DeckMax;

	public cSelectCardModel[] m_selcModel;

	public cEnemyBattleCardModel m_bcModel;

	private int m_SelectNumber;

	public Vector3 m_Position;
	public float m_Speed;

	public float[] m_LowProbability;
	public float[] m_MiddleProbability;

	public void Init(){
		m_Deck = new bool[14];
		for (int i = 0; i < 14; ++i) {
			m_Deck [i] = false;
		}

		if (m_Joker == true) {
			m_Deck [0] = true;
		}

		m_bcModel.Init ();

		m_DeckMax = 14;

		m_SelectNumber = -1;

		for( int i = 0 ; i < m_selcModel.Length ; ++i ){
			m_selcModel [i].CardInit ();
		}
	}

	public void JokerInit(){
		m_Joker = false;
	}

	public void Hind(){
		m_bcModel.Init ();
	}

	public void Select( float setSecond , int duelNumber ){
		m_Deck [m_selcModel [m_SelectNumber].m_CardNumber] = true;

		--m_DeckMax;

		m_bcModel.m_CardNumber = m_selcModel[m_SelectNumber].m_CardNumber;
		m_bcModel.SetCard ();

		m_bcModel.MoveSet (setSecond);
	}

	//ランダムに３枚のカードを選ぶ
	public void RandomSet(bool start = false){
		m_bcModel.Init ();

		int[] random = new int[m_selcModel.Length];

		for (int i = 0; i < m_selcModel.Length; ++i) {
			random [i] = cCardSpriteManager.Back;
		}

		if (DeckCheck () == true) {
			int setNumber = 0;

			int trueNumber;

			do {
				trueNumber = Random.Range(0,m_Deck.Length);
			} while(m_Deck[trueNumber] == true);

			m_Deck [trueNumber] = true;

			int handNumber = Random.Range (0, 3);
			m_selcModel [handNumber].m_CardNumber = trueNumber;
			random [handNumber] = trueNumber;

			//カードをランダムに選び出す
			while (setNumber < m_selcModel.Length) {
				if (random [setNumber] != cCardSpriteManager.Back) {
					++setNumber;
					continue;
				}

				int number = Random.Range (0, m_Deck.Length);

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
					m_selcModel [setNumber].m_CardNumber = random [setNumber];
					++setNumber;
				}
			}
				
			for (int i = 0; i < m_selcModel.Length; ++i) {
				if (start == true) {
					m_selcModel [i].SetSelect ();
					m_selcModel [i].Init (m_Position, m_Speed);
					m_selcModel [i].CardMostSmall ();
				} else if (i == m_SelectNumber) {
					m_selcModel [i].SetSelect ();
					m_selcModel [i].Init (m_Position, m_Speed);
					m_selcModel [i].CardMostSmall ();
				}
			}

			m_SelectNumber = handNumber;
		} else {
			//デッキに３枚ない時は初期化のみ
			for (int i = 0; i < m_selcModel.Length; ++i) {
				if (m_SelectNumber == i) {
					m_selcModel [i].End ();
				}

				m_selcModel [i].CardMostSmall ();
			}

			do {
				m_SelectNumber = Random.Range (0, m_selcModel.Length);
			} while(m_selcModel [m_SelectNumber].GetUsed () == false);
		}
	}

	//デッキ枚数による処理
	private bool DeckCheck(){
		int deckCheck = 0;

		for (int i = 0; i < m_Deck.Length; ++i) {
			if (m_Deck [i] == false) {
				++deckCheck;
			}
		}
			
		if (deckCheck > 2) {
			return true;
		}

		return false;
	}

	public bool Move(){
		return m_bcModel.Move ();
	}

	public bool Open(){
		//バトルカードを開く
		m_bcModel.Open();
		if (m_bcModel.GetOpen ()) {
			return true;
		}

		return false;
	}

	public bool SelectCardMove(){
		bool endFlag = true;

		for (int i = 0; i < m_selcModel.Length; ++i) {
			endFlag &= m_selcModel [i].Move ();
		}

		return endFlag;
	}

	public bool SelectMove(){
		return m_selcModel [m_SelectNumber].Back (3.0f,true);
	}

	public bool SelectCardOpen(){
		bool endFlag = true;

		for (int i = 0; i < m_selcModel.Length; ++i) {
			endFlag &= m_selcModel [i].Open ();
		}

		return endFlag;
	}

	public int GetBattleCardNumber(){
		return m_bcModel.m_CardNumber;
	}

	public bool Snap(){
		return m_bcModel.SnapMove ();
	}

	public bool Back( float m_FadeTime , bool all = false ){
		bool endFlag = true;

		//シーンを抜ける時のみ
		if (all == true) {

			for (int i = 0; i < m_selcModel.Length; ++i) {
				endFlag &= m_selcModel [i].Back (m_FadeTime, true);
			}
		}

		endFlag &= m_bcModel.Back (m_FadeTime);

		return endFlag;
	}
}
