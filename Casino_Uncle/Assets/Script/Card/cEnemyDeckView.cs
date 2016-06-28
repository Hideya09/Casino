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
		m_Text.text = m_edModel.m_TotalNumber.ToString ();
	}
}
