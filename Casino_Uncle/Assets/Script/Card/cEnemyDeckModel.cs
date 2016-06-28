﻿using UnityEngine;
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

	public void Init(){
		m_Deck = new bool[14];
		for (int i = 0; i < 14; ++i) {
			m_Deck [i] = false;
		}

		m_bcModel.Init ();

		m_DeckMax = 14;

		m_SelectNumber = -1;

		m_selcModel.Initialize ();
	}

	public void Hind(){
		m_bcModel.Init ();
	}

	public void Select( float setSecond , int[] PlayerSelectCard , int duelNumber ){
		//int number = CardAI (PlayerSelectCard, duelNumber);

		do {
			m_SelectNumber = Random.Range (0, 3);
		} while(m_selcModel [m_SelectNumber].GetUsed () == false);

		m_Deck [m_SelectNumber] = true;

		--m_DeckMax;

		m_bcModel.m_CardNumber = m_selcModel[m_SelectNumber].m_CardNumber;
		m_bcModel.SetCard ();

		m_bcModel.MoveSet (setSecond);
	}

	//カードのパターン調整
	public int CardAI(int[] playerSelectCard , int duelNumber){

		bool doubleMode = true;
		int highMode = 0;
		int noneMode = 0;
		bool oneMode = false;

		ArrayList array = new ArrayList ();
		int doubleNumber = 0;
		int oneNumber = 0;

		//プレイヤーカードをチェック
		for (int i = 0; i < playerSelectCard.Length; ++i) {
			if (playerSelectCard [i] > 1 && playerSelectCard [i] < 5) {
				doubleMode &= true;
			} else {
				doubleMode = false;
			}

			if (playerSelectCard [i] > 10) {
				++highMode;
			}

			if (playerSelectCard [i] == 1) {
				oneMode = true;
			}

			if (playerSelectCard [i] == -1) {
				++noneMode;
			}
		}

		//プレイヤーの手が最弱の場合、一定の確率で勝てるようにする
		if (doubleMode == true) {
			int random = Random.Range (0, 3 + ( duelNumber * 3 ));
			if (random == 0) {
				if (m_Deck [1] == false) {
					array.Add (1);
					++doubleNumber;
				}
				if (m_Deck [2] == false) {
					array.Add (2);
					++doubleNumber;
				}
				if (m_Deck [3] == false) {
					array.Add (3);
					++doubleNumber;
				}

				if (doubleNumber > 0) {
					int selectRandom = Random.Range (0, array.Count);
					int selectNumber = (int)array [selectRandom];

					return selectNumber;
				}
			}
		}

		return 0;
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

		if (DeckCheck () == true) {
			//使用した部分に新しいカードを設定し、他は選択可能にする

			for (int i = 0; i < m_selcModel.Length; ++i) {

				m_selcModel [i].SetSelect ();

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

				m_selcModel [i].CardMostSmall ();
			}

			m_selcModel [m_SelectNumber].Init (m_Position, m_Speed);

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
		return m_selcModel [m_SelectNumber].Back (0.5f,true);
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

	public bool Back( float m_FadeTime ){
		bool endFlag = true;

		for (int i = 0; i < m_selcModel.Length; ++i) {
			endFlag &= m_selcModel [i].Back (m_FadeTime,true);
		}

		endFlag &= m_bcModel.Back (m_FadeTime);

		return endFlag;
	}
}
