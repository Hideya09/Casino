using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cHitPointView : MonoBehaviour {

	private Image m_MyImage;
	public Image m_Image;
	public cHitPointModel m_hpModel;

	// Use this for initialization
	void Start () {
		m_hpModel.SetPosition (transform.localPosition);

		m_MyImage = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = m_hpModel.GetPosition ();

		m_Image.color = new Color (m_hpModel.m_Black, m_hpModel.m_Black, m_hpModel.m_Black, m_hpModel.m_Color);

		m_MyImage.color = new Color (m_hpModel.m_Black, m_hpModel.m_Black, m_hpModel.m_Black, m_hpModel.m_Fade);
	}
}
