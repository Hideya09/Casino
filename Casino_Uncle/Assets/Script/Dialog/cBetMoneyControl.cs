using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cBetMoneyControl : MonoBehaviour {

	public cBetMoneyModel m_betMoneyModel;

	public Text m_BetText;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//入力された文字をモデルに渡す
		m_betMoneyModel.SetString (m_BetText.text);
	}
}
