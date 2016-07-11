using UnityEngine;
using System.Collections;

public class cEnemyDeckModel : ScriptableObject {

	private bool[] m_Deck;

	private bool m_Joker;

	public int m_DeckMax{ get; private set; }

	public cSelectCardModel[] m_selcModel;

	public cEnemyBattleCardModel m_bcModel;

	private int m_SelectNumber;

	private Vector3 m_Position;
	public float m_Speed;

	public float m_Arpha{ get; private set; }

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

		m_Arpha = 0.0f;

		for( int i = 0 ; i < m_selcModel.Length ; ++i ){
			m_selcModel [i].CardInit ();
		}
	}

	public void SetPosition( Vector3 setPosition){
		m_Position = setPosition;
	}

	public Vector3 GetPosition(){
		return m_Position;
	}

	public void JokerInit(){
		m_Joker = false;
	}

	public void Hind(){
		m_bcModel.Init ();
	}

	public void Select( float setSecond ){

		do {
			m_SelectNumber = Random.Range (0, m_selcModel.Length);
		} while(m_selcModel [m_SelectNumber].GetUsed () == false);

		m_Deck [m_selcModel [m_SelectNumber].m_CardNumber] = true;

		m_bcModel.m_CardNumber = m_selcModel[m_SelectNumber].m_CardNumber;
		m_bcModel.SetCard ();

		m_bcModel.MoveSet (setSecond);
	}

	//ランダムに３枚のカードを選ぶ
	public void RandomSet(){
		m_bcModel.Init ();

		int[] random = new int[m_selcModel.Length];

		for (int i = 0; i < m_selcModel.Length; ++i) {
			random [i] = cCardSpriteManager.Back;
		}

		int setNumber = 0;

		if (DeckCheck () == true) {
			m_SelectNumber = -1;

			//カードをランダムに選び出す
			while (setNumber < m_selcModel.Length) {
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
					++setNumber;
				}
			}
				
			for (int i = 0; i < m_selcModel.Length; ++i) {
				m_selcModel [i].m_CardNumber = random [i];
				m_selcModel [i].SetSelect ();
				m_selcModel [i].Init (m_Position, m_Speed);

				m_selcModel [i].CardMostSmall ();
			}

			--m_DeckMax;
		} else {
			//デッキに３枚ない時は初期化のみ
			for (int i = 0; i < m_selcModel.Length; ++i) {
				if (m_SelectNumber == i) {
					m_selcModel [i].End ();
				} else if (m_selcModel [i].GetUsed () == true) {
					m_selcModel [i].SetSelect ();
					m_selcModel [i].Init (m_Position, m_Speed);
				}

				m_selcModel [i].CardMostSmall ();
			}

			m_SelectNumber = -1;
		}
	}

	public bool EditCard(){
		m_bcModel.Init ();

		m_selcModel [m_SelectNumber].m_CardNumber = cCardSpriteManager.Back;

		if (DeckCheck () == true) {
			//使用した部分に新しいカードを設定し、他は選択可能にする

			for (int i = 0; i < m_selcModel.Length; ++i) {

				m_selcModel [i].CardMostSmall ();

				if (m_selcModel [i].m_CardNumber == cCardSpriteManager.Back) {
					do {
						int number = Random.Range (0, m_Deck.Length);

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
					} while(m_selcModel [i].m_CardNumber == cCardSpriteManager.Back);
				}
			}

			m_selcModel [m_SelectNumber].Init (m_Position, m_Speed);
			m_selcModel [m_SelectNumber].CardMostSmall ();

			--m_DeckMax;

			return true;
		} else {
			//初期化のみ行う

			for (int i = 0; i < m_selcModel.Length; ++i) {
				m_selcModel [i].CardMostSmall ();
			}

			m_selcModel [m_SelectNumber].End ();

			return false;
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

	public bool Up(){
		m_Arpha += Time.deltaTime;

		if (m_Arpha >= 1.0f) {
			m_Arpha = 1.0f;

			return true;
		} else {
			return false;
		}
	}

	public bool Back( float m_FadeTime , bool all = false ){
		bool endFlag = true;

		//シーンを抜ける時のみ
		if (all == true) {

			m_Arpha -= Time.deltaTime * m_FadeTime;

			if (m_Arpha <= 0) {
				endFlag = true;
			} else {
				endFlag = false;
			}

			for (int i = 0; i < m_selcModel.Length; ++i) {
				endFlag &= m_selcModel [i].Back (m_FadeTime, true);
			}

			m_DeckMax = -1;
		}

		endFlag &= m_bcModel.Back (m_FadeTime);

		return endFlag;
	}
}
