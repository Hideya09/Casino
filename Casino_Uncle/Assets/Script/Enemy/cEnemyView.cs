using UnityEngine;
using System.Collections;

public class cEnemyView : MonoBehaviour {

	public cEnemyModel m_eModel;

	// Use this for initialization
	void Start () {
		m_eModel.InitPosition (transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = m_eModel.GetPosition ();
	}
}
