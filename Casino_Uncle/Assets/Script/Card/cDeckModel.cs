using UnityEngine;
using System.Collections;

public class cDeckModel : ScriptableObject {

	//カードを配る速さ
	public float HandOutSpeed;

	//デッキを描画するかどうか
	private bool m_DeckViewFlag;

	//位置情報
	private Vector3 m_Position;

	private Vector3 m_BasePosition;
	private Vector3 m_Movement;

	public Vector3 m_StartPosition;
	public Vector3 m_ReturnPosition;

	private float m_ReturnCount;
	public float m_MaxReturnCount;
	public float m_EndCount;

	public float m_Fade{ get; private set; }

	public int DeckMax;

	//選択用カード
	public cSelectCardModel[] m_selcModel;

	//バトル用カード
	public cBattleCardModel m_bcModel;

	//使用したかどうか
	private bool[] m_Deck;

	//どの手札を選択したか
	private int m_SelectNumber;

	//カードが残り一枚の時
	public bool m_LastBattle{ get; private set; }
	//賞金が２倍になるかどうか
	public bool m_DoubleBattle{ get; private set; }

	public void SetPosition( Vector3 setPosition ){
		m_BasePosition = setPosition;
	}

	//５戦開始前初期化処理
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

		m_DeckViewFlag = true;
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
			for (int i = 0; i < m_selcModel.Length; ++i) {
				m_selcModel [i].m_CardNumber = random [i];
				m_selcModel [i].SetSelect ();
				m_selcModel [i].Init (m_Position, HandOutSpeed);

				if (random [i] == 2 || random [i] == 3 || random [i] == 4) {
					++doubleCharengeFlag;
				}
			}

			if (doubleCharengeFlag == 3) {
				m_DoubleBattle = true;
			}
		} else {
			//デッキに３枚ない時は初期化のみ
			for (int i = 0; i < m_selcModel.Length; ++i) {
				if (m_SelectNumber == i) {
					m_selcModel [i].End ();
				} else if (m_selcModel [i].GetUsed () == true) {
					m_selcModel [i].SetSelect ();
					m_selcModel [i].Init (m_Position, HandOutSpeed);
				}
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

			int doubleCharengeFlag = 0;
			for (int i = 0; i < m_selcModel.Length; ++i) {
				if (m_selcModel [i].m_CardNumber == 2 || m_selcModel [i].m_CardNumber == 3 || m_selcModel [i].m_CardNumber == 4) {
					++doubleCharengeFlag;
				}
			}

			if (doubleCharengeFlag == 3) {
				m_DoubleBattle = true;
			}

			m_selcModel [m_SelectNumber].Init (m_Position, HandOutSpeed);

			return true;
		} else {
			//初期化のみ行う

			for (int i = 0; i < m_selcModel.Length; ++i) {
				m_selcModel [i].SetSelect ();
			}

			m_selcModel [m_SelectNumber].End ();

			return false;
		}
	}

	//カード選択処理
	public bool CardCheck(){
		int number = -1;

		bool selectFlag = false;

		for (int i = 0; i < m_selcModel.Length; ++i) {
			if (m_selcModel [i].GetUsed () == true) {
				m_selcModel [i].m_MoveFlag = true;
			}
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
				if (number != i) {
					m_selcModel [i].SetSelect ();
				}
			}
			m_SelectNumber = number;
			return true;
		} else if (number >= 0) {
			for (int i = 0; i < m_selcModel.Length; ++i) {
				if (number != i) {
					if (m_selcModel [i].GetUsed () == true) {
						m_selcModel [i].NotSelectCard ();
					}
				}
			}
		} else {
			for (int i = 0; i < m_selcModel.Length; ++i) {
				if (m_selcModel [i].GetUsed () == true) {
					m_selcModel [i].InitSelectCard ();
				}
			}
		}

		return false;
	}

	//カード選択後の処理
	public bool CardMove(){
		if (m_selcModel [m_SelectNumber].MoveSelectCard (m_bcModel.GetPosition())) {
			m_bcModel.m_CardNumber = m_selcModel [m_SelectNumber].m_CardNumber;
			m_bcModel.SetCard ();
			return true;
		}
		return false;
	}

	//カード使用後処理
	public void CardEnd(){
		m_Deck [m_bcModel.m_CardNumber] = true;
		m_selcModel [m_SelectNumber].m_CardNumber = DeckMax;
	}

	//バトルに使うカードの番号
	public int GetBattleCardNumber(){
		return m_bcModel.m_CardNumber;
	}

	//デッキを描画するかどうか
	public bool GetDeckView(){
		return m_DeckViewFlag;
	}

	//デッキ枚数による処理
	private bool DeckCheck(){
		int deckCheck = 0;

		for (int i = 0; i < DeckMax; ++i) {
			if (m_Deck [i] == false) {
				++deckCheck;
			}
		}

		if (deckCheck <= 3) {
			m_DeckViewFlag = false;
		}

		if (deckCheck == 1) {
			m_LastBattle = true;
		}

		if (deckCheck > 2) {
			return true;
		}

		return false;
	}

	//カード配り演出
	public bool HandOnCard(){
		bool endFlag = true;

		for (int i = 0; i < m_selcModel.Length; ++i) {
			endFlag &= m_selcModel [i].Move ();
		}

		return endFlag;
	}

	//カードを開く演出
	public bool CardOpen(){
		bool endFlag = true;

		for (int i = m_selcModel.Length - 1; i >= 0; --i) {
			if (m_selcModel [i].GetUsed () == true) {
				if (m_selcModel [i].Open () == true) {
					endFlag &= m_selcModel [i].GetOpen ();
				} else {
					endFlag = false;
					break;
				}
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

	public bool Snap(){
		return m_bcModel.SnapMove ();
	}

	public bool MoveBack( float m_FadeTime ){
		bool endFlag = Back (m_FadeTime);

		endFlag &= m_bcModel.Back (m_FadeTime);

		for (int i = 0; i < m_selcModel.Length; ++i) {
			endFlag = m_selcModel [i].Back ( m_FadeTime );
		}

		return endFlag;
	}

	public bool ButtleCardBack( float m_FadeTime ){
		return m_bcModel.Back ( m_FadeTime );
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

	public bool Back( float m_FadeTime ){
		m_ReturnCount += Time.deltaTime;

		m_Position += m_Movement * Time.deltaTime;

		m_Fade -= (Time.deltaTime * m_FadeTime);

		if (m_Fade <= 0.0f) {
			m_Position = m_StartPosition;

			m_DeckViewFlag = true;
			return true;
		}

		return false;
	}

	public Vector3 GetPosition(){
		return m_Position;
	}

	public bool SelectStart(){
		bool endFlag = true;
		for (int i = 0; i < m_selcModel.Length; ++i) {
			endFlag &= m_selcModel [i].OutLineBlink ();
		}

		return endFlag;
	}

	//選択を止める
	public void SelectStop(){
		for (int i = 0; i < m_selcModel.Length; ++i) {
			m_selcModel [i].m_MoveFlag = false;
		}
	}

	//プレイヤーの手札のカードの番号を取得
	public int[] GetSelect(){
		int[] select = new int[3];

		for (int i = 0; i < 3; ++i) {
			if (m_selcModel [i].GetUsed () == true) {
				select [i] = m_selcModel [i].m_CardNumber;
			} else {
				select [i] = -1;
			}
		}

		return select;
	}
}
