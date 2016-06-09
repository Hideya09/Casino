using UnityEngine;
using System.Collections;

public class cCameraView : MonoBehaviour {

	public cCameraModel m_camereModel;

	void Awake () {
		m_camereModel.InitPosition (transform.position);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = m_camereModel.GetPosition ();
	}
}
