using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cCommitTextView : MonoBehaviour {

	public cCommitTextModel m_ctModel;

	private Image m_Image;

	public Sprite[] m_Sprite;

	void Awake () {
		m_ctModel.InitPosition (transform.localPosition);
	}

	// Use this for initialization
	void Start () {
		m_Image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = m_ctModel.GetPosition ();

		m_Image.sprite = m_Sprite [(int)m_ctModel.GetText ()];

		m_Image.color = new Color (1.0f, 1.0f, 1.0f, m_ctModel.GetProgression ());
	}
}
