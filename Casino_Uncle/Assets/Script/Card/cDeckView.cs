using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cDeckView : MonoBehaviour {

	public cDeckModel m_dModel;

	private Image m_Image;

	// Use this for initialization
	void Start () {
		m_dModel.SetPosition (transform.localPosition);

		m_Image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = m_dModel.GetPosition ();

		m_Image.color = new Color (1.0f, 1.0f, 1.0f, m_dModel.m_Fade);
	}
}
