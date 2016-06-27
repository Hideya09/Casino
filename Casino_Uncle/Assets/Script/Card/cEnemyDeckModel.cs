using UnityEngine;
using System.Collections;

public class cEnemyDeckModel : ScriptableObject {

	private bool[] m_Deck;

	private int m_DeckMax;

	public cEnemyBattleCardModel m_bcModel;

	public void Init(){
		m_Deck = new bool[14];
		for (int i = 0; i < 14; ++i) {
			m_Deck [i] = false;
		}

		m_bcModel.Init ();

		m_DeckMax = 14;
	}

	public void Hind(){
		m_bcModel.Init ();
	}

	public void Select( float setSecond , int[] PlayerSelectCard , int duelNumber ){
		int number = CardAI (PlayerSelectCard, duelNumber);

		m_Deck [number] = true;

		--m_DeckMax;

		m_bcModel.m_CardNumber = number;
		m_bcModel.SetCard ();

		m_bcModel.MoveSet (setSecond);
	}

	//カードのパターン調整
	public int CardAI(int[] playerSelectCard , int duelNumber){

		int count = 0;

		int bufNumber1 = -1;
		int bufNumber2 = -1;

		//カード枚数が少ないか調べる
		for (int i = 0; i < m_Deck.Length; ++i) {
			if (m_Deck [i] == false) {
				bufNumber2 = bufNumber1;
				bufNumber1 = i;
				++count;
			}
		}

		//少ないとき用選択処理
		if (count == 1) {
			return bufNumber1;
		} else if (count == 2) {
			if (Random.Range (0, 2) == 0) {
				return bufNumber1;
			} else {
				return bufNumber2;
			}
		}

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

		//プレイヤーの手が高い場合、１orJokerを一定確率で出す
		if (highMode > 0) {
			if (m_Deck [0] == false) {
				int selectRandom;
				if (duelNumber == 4) {
					selectRandom = Random.Range (0, 2 + highMode);
				} else {
					selectRandom = Random.Range (0, 10 - duelNumber);
				}

				if (selectRandom == 0) {
					return 0;
				}
			}

			if (m_Deck [1] == false) {
				int selectRandom;
				selectRandom = Random.Range (0, 6 - highMode);

				if (selectRandom == 0) {
					return 1;
				}
			}
		}

		//プレイヤーが１を持っていたら11~13をランダムで出す
		if (oneMode == true) {
			
			int random = Random.Range (0, 3 + (duelNumber * 2));
			if (random == 0) {
				if (m_Deck [11] == false) {
					array.Add (11);
					++oneNumber;
				}
				if (m_Deck [12] == false) {
					array.Add (12);
					++oneNumber;
				}
				if (m_Deck [13] == false) {
					array.Add (13);
					++oneNumber;
				}

				if (oneNumber > 0) {
					int selectRandom = Random.Range (0, array.Count);
					int selectNumber = (int)array [selectRandom];

					return selectNumber;
				}
			}
		}

		int select1;
		int select2;
		//通常時カード選択処理
		do {
			select1 = Random.Range( 0 , 14 );
		} while(m_Deck [select1] == true);

		do {
			select2 = Random.Range( 0 , 14 );
		} while(m_Deck [select2] == true || select1 == select2);

		int randomSelectNumber;

		//勝利数に応じ、選択するカードを変更
		if (duelNumber == 4) {
			randomSelectNumber = Mathf.Max (select1, select2);
		} else if (duelNumber == 0) {
			randomSelectNumber = Mathf.Min (select1, select2);
		} else if (duelNumber == 1) {
			int random = Random.Range (0, 3);
			if (random == 0) {
				randomSelectNumber = Mathf.Max (select1, select2);
			} else {
				randomSelectNumber = Mathf.Min (select1, select2);
			}
		} else {
			int random = Random.Range (0, 2);
			if (random == 0) {
				randomSelectNumber = Mathf.Max (select1, select2);
			} else {
				randomSelectNumber = Mathf.Min (select1, select2);
			}
		}

		return randomSelectNumber;
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

	public int GetBattleCardNumber(){
		return m_bcModel.m_CardNumber;
	}

	public void Snap(){
		m_bcModel.SnapMove ();
	}

	public bool Back( float m_FadeTime ){
		return m_bcModel.Back (m_FadeTime);
	}
}
