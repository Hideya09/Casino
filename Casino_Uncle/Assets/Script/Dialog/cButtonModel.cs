﻿using UnityEngine;
using System.Collections;

public class cButtonModel : ScriptableObject {
	private bool m_Start;

	private bool m_TapFlag;

	private bool m_Select;

	private bool m_Touch;

	public int m_ButtonNumber;

	public void Black(){
		m_TapFlag = true;
	}

	public void Init(){
		m_Start = false;

		m_TapFlag = false;

		m_Select = false;

		m_Touch = false;
	}

	public void Start(){
		m_Start = true;
	}

	public void End(){
		m_Start = false;
	}

	//ボタンの入力受付中の時タップされたとする
	public void TapButton(){
		if (m_Start == true) {
			m_TapFlag = true;
		}
	}

	//タップされていない
	public void UnTapButton(){
		if (m_Start == true) {
			m_TapFlag = false;
		}
	}

	//ボタンが押された
	public void SelectButton(){
		if (m_Start == true && m_TapFlag == true) {
			m_Select ^= true;
		}
	}

	public bool GetTouch(){
		return m_Touch;
	}

	//画面がタッチされた
	public void Touch(){
		m_Touch = true;
	}
	public void UnTouch(){
		m_Touch = false;
	}

	public bool GetTap(){
		return m_TapFlag;
	}

	//ボタンが押された際に自身に設定された値を返す
	public int GetSelect(){
		if (m_Select == true) {
			return m_ButtonNumber;
		}

		return 0;
	}
}
