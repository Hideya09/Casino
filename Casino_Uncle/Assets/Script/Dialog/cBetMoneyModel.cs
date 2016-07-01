using UnityEngine;
using System.Collections;

public class cBetMoneyModel : ScriptableObject {
	private int m_Number;

	private bool m_InputFlag;

	private bool m_TextSetFlag;
	private int m_SetNumber;

	void OnEnable(){
		Init ();
	}

	public void Init(){
		m_Number = 0;
		m_InputFlag = true;

		m_TextSetFlag = false;
		m_SetNumber = 100;
	}

	//初期数値をセット
	public void SetNumber( int setNumber ){
		m_SetNumber = setNumber;
		m_TextSetFlag = true;
	}

	//フラグが立っていたら数値を返す
	public int GetStartText(){
		if (m_TextSetFlag == true) {
			m_TextSetFlag = false;
			return m_SetNumber;
		}

		return -1;
	}

	//入力可能状態をセット
	public void SetInput(bool setInput){
		m_InputFlag = setInput;
	}

	public bool GetIput(){
		return m_InputFlag;
	}

	//入力されたテキストを数値に変換
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
