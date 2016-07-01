using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cDialogView : MonoBehaviour {

	public cDialogModel m_dialogModel;

	public Text[] m_text;

	public bool[] m_MoneyFlag;

	public Text[] m_text2;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		transform.localPosition = m_dialogModel.GetPosition ();

		transform.rotation = Quaternion.AngleAxis (m_dialogModel.GetRotation (), Vector3.forward);

		//数値情報を取得しテキストとして表示させる
		int[] data = m_dialogModel.GetNumberData ();

		float[] data2 = m_dialogModel.GetNumberData2 ();

		for (int i = 0; i < m_text.Length && i < data.Length; ++i) {
			if (m_MoneyFlag[i] == true) {
				//お金を表示する際の処理
				char[] text = data [i].ToString ("C0", new System.Globalization.CultureInfo ("ja-jp")).ToCharArray ();
				if (data [i] < 0) {
					text[0] = '¥';
					text[1] = '-';
				} else {
					text [0] = '¥';
				}
				m_text [i].text = new string (text);
			} else {
				m_text [i].text = data [i].ToString ();
			}
		}

		for (int i = 0; i < m_text2.Length && i < data2.Length; ++i) {
			m_text2 [i].text = data2 [i].ToString ();
		}
	}
}