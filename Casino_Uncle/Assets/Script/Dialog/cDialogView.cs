using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class cDialogView : MonoBehaviour {

	public cDialogModel m_dialogModel;

	public Text[] m_text;
	public bool[] m_MoneyFlag;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = m_dialogModel.GetPosition ();

		transform.rotation = Quaternion.AngleAxis (m_dialogModel.GetRotation (), Vector3.forward);

		int[] data = m_dialogModel.GetNumberData ();

		for (int i = 0; i < m_text.Length && i < data.Length; ++i) {
			if (m_MoneyFlag[i] == true) {
				m_text [i].text = data [i].ToString ("C0");
			} else {
				m_text [i].text = data [i].ToString ();
			}
		}
	}
}