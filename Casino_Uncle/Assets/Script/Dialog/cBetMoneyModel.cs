using UnityEngine;
using System.Collections;

public class cBetMoneyModel : ScriptableObject {
	private int m_Number;

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
