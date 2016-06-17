﻿using UnityEngine;
using System.Collections;

public class cBetMoneyModel : ScriptableObject {
	private int m_Number;

	private bool m_InputFlag;

	void OnEnable(){
		Init ();
	}

	public void Init(){
		m_Number = 0;
		m_InputFlag = true;
	}

	public void SetInput(bool setInput){
		m_InputFlag = setInput;
	}

	public bool GetIput(){
		return m_InputFlag;
	}

	public void SetString( string setNumber ){
		if (setNumber.Length == 0) {
			m_Number = 0;
		} else {
			m_Number = int.Parse (setNumber);
		}
	}

	public int GetNumber(){
		return m_Number;
	}
}