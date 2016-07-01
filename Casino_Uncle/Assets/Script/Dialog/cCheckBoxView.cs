using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cCheckBoxView : MonoBehaviour {
	public cButtonModel m_ButtonModel;

	public Image m_Sprite;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//押されるごとに描画を変える
		if (m_ButtonModel.GetSelect () != 0) {
			m_Sprite.enabled = true;
		} else {
			m_Sprite.enabled = false;
		}
	}
}
