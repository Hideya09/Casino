﻿using UnityEngine;
using System.Collections;

public class cEffectLoseControl : MonoBehaviour {

	public cEffectLoseModel m_effectLoseModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//入力処理
		if (Input.GetMouseButtonUp (0) == true) {
			m_effectLoseModel.Tap ();
		}
	}
}
