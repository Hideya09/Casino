using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cEnemyDeckView : MonoBehaviour {

	public Text m_BaseText;
	public Text m_NumberText;
	public cEnemyDeckModel m_edModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//敵の合計枚数の表示
		if (m_edModel.m_TotalNumber != 0) {
			m_NumberText.enabled = true;
			m_BaseText.enabled = true;

			m_NumberText.text = m_edModel.m_TotalNumber.ToString ();
		} else {
			m_NumberText.enabled = false;
			m_BaseText.enabled = false;
		}
	}
}
