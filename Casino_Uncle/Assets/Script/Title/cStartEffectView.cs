using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cStartEffectView : MonoBehaviour {

	public cStartEffectModel m_startModel;

	private Image m_Image;

	// Use this for initialization
	void Start () {
		m_Image = GetComponent< Image > ();
	}
	
	// Update is called once per frame
	void Update () {
		m_Image.color = new Color (1.0f, 1.0f, 1.0f, m_startModel.GetArpha ());
	}
}
