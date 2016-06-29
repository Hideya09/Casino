using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cEnemyDeckView : MonoBehaviour {

	public Text m_Text;
	public cEnemyDeckModel m_edModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (m_edModel.m_TotalNumber != 0) {
			m_Text.enabled = true;

			m_Text.text = m_edModel.m_TotalNumber.ToString ();
		} else {
			m_Text.enabled = false;
		}
	}
}
