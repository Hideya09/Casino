using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cArrowView : MonoBehaviour {

	public cGameData m_GameData;

	public Image m_Image;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		m_Image.enabled = m_GameData.m_WinLose;
	}
}
