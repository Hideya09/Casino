using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cCommitTextView : MonoBehaviour {

	public cCommitTextModel m_ctModel;

	private Text m_Text;

	void Awake () {
		m_ctModel.InitPosition (transform.localPosition);
	}

	// Use this for initialization
	void Start () {
		m_Text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = m_ctModel.GetPosition ();

		switch (m_ctModel.GetText ()) {
		case cCommitTextModel.eCommitText.eCommitText_Win:
			m_Text.text = "アタック成功！";
			break;
		case cCommitTextModel.eCommitText.eCommitText_Lose:
			m_Text.text = "アタック失敗";
			break;
		case cCommitTextModel.eCommitText.eCommitText_Draw:
			m_Text.text = "拮抗";
			break;
		}

		m_Text.color = new Color (1.0f, 1.0f, 1.0f, m_ctModel.GetProgression ());
	}
}
