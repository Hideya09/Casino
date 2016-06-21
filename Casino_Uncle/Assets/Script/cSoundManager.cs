using UnityEngine;
using System.Collections;
using System.IO;

public class cSoundManager : MonoBehaviour {

	public enum eSoundSE{
		eSoundSE_Cancel,
		eSoundSE_Decision,
		eSoundSE_GameEnd,
		eSoundSE_Slide,
		eSoundSE_Start,
		eSoundSE_Lock,
		eSoundSE_UnLock,
		eSoundSE_Count,
		eSoundSE_FiveWin,
		eSoundSE_Cock,
		eSoundSE_Confirm,
		eSoundSE_Cross,
		eSoundSE_DuelStart,
		eSoundSE_In,
		eSoundSE_Lose,
		eSoundSE_Repelled,
		eSoundSE_Slash,
		eSoundSE_Win,
		eSoundSE_Max
	}

	public AudioSource m_BGM;

	public AudioSource m_SE;

	public AudioClip m_BGMSound;
	public AudioClip[] m_SESound;

	private static ArrayList m_SENumber = new ArrayList ();

	private static bool m_BGMPlayFlag = false;

	private static bool m_BGMDown = false;

	//private static int m_SoundNumber = 0;

	// Use this for initialization
	void Start () {
		m_BGM.volume = 1.0f;

		m_BGMPlayFlag = false;
		m_BGMDown = false;

		m_BGM.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_BGMPlayFlag == true) {
			m_BGM.Play ();
			if (m_BGMDown == true) {
				m_BGM.volume -= Time.deltaTime * 0.25f;

				if (m_BGM.volume <= 0.0f) {
					m_BGM.volume = 0.0f;
					m_BGM.Stop ();
					m_BGMPlayFlag = false;
					m_BGMDown = false;
				}
			}
		}

		for (int i = 0; i < m_SENumber.Count; ++i) {
			//m_SE [m_SoundNumber].Stop ();
			//m_SE [m_SoundNumber].clip = m_SESound [(int)m_SENumber [i]];
			//m_SE [m_SoundNumber].Play ();

			//++m_SoundNumber;
			//if (m_SoundNumber == m_SE.Length) {
			//	m_SoundNumber = 0;
			//}

			m_SE.PlayOneShot (m_SESound [(int)m_SENumber [i]]);
		}

		m_SENumber.Clear ();
	}

	public static void BGMPlay (){
		m_BGMPlayFlag = true;
	}

	public static void SEPlay( eSoundSE playSE ){
		m_SENumber.Add (playSE);
	}

	public static void BGMDown(){
		m_BGMDown = true;
	}
}
