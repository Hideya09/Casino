﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cBetMoneyView : MonoBehaviour {

	public cBetMoneyModel m_betMoneyModel;
	public InputField m_Input;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_Input.interactable = m_betMoneyModel.GetIput();

		//初期数値が更新されていたらセットする
		int number = m_betMoneyModel.GetStartText ();
		if (number != -1) {
			m_Input.text = number.ToString ();
		}
	}
}
