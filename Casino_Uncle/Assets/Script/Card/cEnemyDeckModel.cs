using UnityEngine;
using System.Collections;

public class cEnemyDeckModel : ScriptableObject {

	private bool[] m_Deck;

	private int m_DeckMax;

	public cSelectCardModel[] m_selcModel;

	public cEnemyBattleCardModel m_bcModel;

	private int m_SelectNumber;

	public int m_TotalNumber{ get; private set; }

	public Vector3 m_Position;
	public float m_Speed;

	public float[] m_LowProbability;
	public float[] m_MiddleProbability;

	public void Init(){
		m_Deck = new bool[14];
		for (int i = 0; i < 14; ++i) {
			m_Deck [i] = false;
		}

		m_bcModel.Init ();

		m_DeckMax = 14;

		m_SelectNumber = -1;

		m_TotalNumber = 0;

		m_selcModel.Initialize ();
	}

	public void Hind(){
		m_bcModel.Init ();
	}

	public void Select( float setSecond , int duelNumber ){
		do {
			float random = Random.Range (0.0f, 1.0f);

			int selectNumber = m_selcModel[0].m_CardNumber;

			if (random < m_LowProbability [duelNumber]) {
				for (int i = 1; i < m_selcModel.Length; ++i) {
					if (selectNumber > m_selcModel [i].m_CardNumber) {
						selectNumber = m_selcModel [i].m_CardNumber;
					}
				}
			} else if (random < m_MiddleProbability [duelNumber]) {
				for (int i = 0; i < m_selcModel.Length; ++i) {
					int number1 = i + 1;
					if (number1 >= m_selcModel.Length) {
						number1 -= m_selcModel.Length;
					}
					int number2 = i + 2;
					if (number2 >= m_selcModel.Length) {
						number2 -= m_selcModel.Length;
					}

					if (m_selcModel [number1].m_CardNumber > m_selcModel [i].m_CardNumber) {
						if (m_selcModel [i].m_CardNumber > m_selcModel [number2].m_CardNumber) {
							selectNumber = m_selcModel [i].m_CardNumber;
							break;
						}
					}

					if (m_selcModel [number2].m_CardNumber > m_selcModel [i].m_CardNumber) {
						if (m_selcModel [i].m_CardNumber > m_selcModel [number1].m_CardNumber) {
							selectNumber = m_selcModel [i].m_CardNumber;
							break;
						}
					}
				}
			} else {
				for (int i = 1; i < m_selcModel.Length; ++i) {
					if (selectNumber < m_selcModel [i].m_CardNumber) {
						selectNumber = m_selcModel [i].m_CardNumber;
					}
				}
			}

			m_SelectNumber = selectNumber;
		} while(m_selcModel [m_SelectNumber].GetUsed () == false);

		m_Deck [m_selcModel [m_SelectNumber].m_CardNumber] = true;

		--m_DeckMax;

		m_bcModel.m_CardNumber = m_selcModel[m_SelectNumber].m_CardNumber;
		m_bcModel.SetCard ();

		m_bcModel.MoveSet (setSecond);
	}

	//カードのパターン調整
	public void CardAI(int[] playerSelectCard, int duelNumber){
		bool doubleMode = true;
		bool maxChangeMode = true;

		int enemyMin = m_selcModel [0].m_CardNumber;
		int playerMax = 0;

		for (int i = 1; i < m_selcModel.Length; ++i) {
			if (enemyMin > m_selcModel [i].m_CardNumber) {
				enemyMin = m_selcModel [i].m_CardNumber;
			}
		}

		//プレイヤーカードをチェック
		for (int i = 0; i < playerSelectCard.Length; ++i) {
			if (playerMax < playerSelectCard [i]) {
				playerMax = playerSelectCard [i];
			}

			if (playerSelectCard [i] > 1 && playerSelectCard [i] < 5) {
				doubleMode &= true;
				continue;
			} else {
				doubleMode = false;
			}

			if (playerSelectCard [i] > enemyMin) {
				maxChangeMode = false;
			}

			if (playerSelectCard [i] == -1) {
				return;
			}
		}

		//プレイヤーの手が弱い場合、一定の確率で勝ち目を作る

		int count;
		for (count = playerMax - 1; count >= 0; --count) {
			if (m_Deck [count] == false) {
				break;
			}
		}

		if (count == 0) {
			return;
		}

		if (maxChangeMode == true) {
			if (doubleMode == true || duelNumber < 2) {
				int random = Random.Range (0, 3);
				if (random == 0) {
					int change = Random.Range (0, m_selcModel.Length);

					do {
						int number = Random.Range (0, playerMax);

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
							m_selcModel [change].m_CardNumber = number;
						}
					} while(m_selcModel [change].m_CardNumber >= playerMax);
				}
			}
		}
	}

	//ランダムに３枚のカードを選ぶ
	public void RandomSet(int[] playerSelectCard, int duelNumber){
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

			CardAI (playerSelectCard, duelNumber);

			m_TotalNumber = 0;
			for (int i = 0; i < m_selcModel.Length; ++i) {
				m_selcModel [i].m_CardNumber = random [i];
				m_selcModel [i].SetSelect ();
				m_selcModel [i].Init (m_Position, m_Speed);

				m_TotalNumber += m_selcModel [i].m_CardNumber;

				m_selcModel [i].CardMostSmall ();
			}
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

			return true;
		} else {
			//初期化のみ行う

			for (int i = 0; i < m_selcModel.Length; ++i) {
				m_selcModel [i].SetSelect ();
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

	public void Snap(){
		m_bcModel.SnapMove ();
	}

	public bool Back( float m_FadeTime , bool all = false ){
		bool endFlag = true;

		if (all == true) {
			m_TotalNumber = 0;

			for (int i = 0; i < m_selcModel.Length; ++i) {
				endFlag &= m_selcModel [i].Back (m_FadeTime, true);
			}
		}

		endFlag &= m_bcModel.Back (m_FadeTime);

		return endFlag;
	}
}
