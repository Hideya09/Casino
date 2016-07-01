using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cMoneyView : MonoBehaviour {

	public cGameData m_GameData;

	public Image[] m_MoneyText;

	public cCardSpriteManager m_MoneySprite;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//現在の所持金を表示

		string maxMoney = m_GameData.m_Money.ToString();

		int i;
		int j = 0;
		for (i = maxMoney.Length - 1; i >= 0; --i) {
			m_MoneyText [j].sprite = m_MoneySprite.sprite [int.Parse (maxMoney [i].ToString ())];
			++j;
		}

		for (; j < m_MoneyText.Length; ++j) {
			m_MoneyText [j].sprite = m_MoneySprite.sprite [0];
		}
	}
}
