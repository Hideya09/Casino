using UnityEngine;
using System.Collections;

public class cSelectCardRotateView : MonoBehaviour {

	public cCardModel m_BattleCardModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.rotation = Quaternion.AngleAxis (m_BattleCardModel.GetRotationZ (), Vector3.forward);
	}
}