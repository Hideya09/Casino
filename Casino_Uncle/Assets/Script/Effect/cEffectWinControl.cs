﻿using UnityEngine;
using System.Collections;

public class cEffectWinControl : MonoBehaviour {

	public cEffectWinModel m_effectWinModel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//入力処理
		if (Input.GetMouseButtonUp (0) == true) {
			m_effectWinModel.Tap ();
		}
	}
}