using UnityEngine;
using System.Collections;

public class cEnemyView : MonoBehaviour {

	public cEnemyModel m_eModel;

	private SpriteRenderer m_Sprite;

	// Use this for initialization
	void Start () {
		m_eModel.InitPosition (transform.position);

		m_Sprite = GetComponent< SpriteRenderer > ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = m_eModel.GetPosition ();

		m_Sprite.color = new Color (m_eModel.GetBlack (), m_eModel.GetBlack (), m_eModel.GetBlack (), m_eModel.GetFade ());
	}
}
