using UnityEngine;
using System.Collections;
using System.IO;

public class cSoundManager : MonoBehaviour {

	//使用するSEリスト
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

	//BGM用
	public AudioSource m_BGM;

	//SE用
	public AudioSource m_SE;

	//音
	public AudioClip m_BGMSound;
	public AudioClip[] m_SESound;

	//ループ位置設定
	private float m_LoopCount;
	public float m_LoopStart;
	public float m_LoopEnd;

	//鳴らすSEのリスト
	private static ArrayList m_SENumber = new ArrayList ();

	//BGM用静的変数
	private static bool m_BGMPlayFlag = false;

	private static bool m_BGMDown = false;

	private static bool m_BGMVolume = true;

	//private static int m_SoundNumber = 0;

	// Use this for initialization
	void Start () {
		m_BGM.volume = 1.0f;

		m_LoopCount = 0;

		m_BGMPlayFlag = false;
		m_BGMDown = false;

		m_BGMVolume = true;

		m_BGM.Stop ();
	}
	
	// Update is called once per frame
	void Update () {


		//BGMを鳴らす
		if (m_BGMPlayFlag == true) {
			m_BGM.Play ();
			m_BGMPlayFlag = false;
		}

		if (m_BGM.isPlaying == true) {
			//BGMのループ処理
			m_LoopCount += Time.deltaTime;

			if (m_LoopCount >= m_LoopEnd) {
				m_LoopCount -= m_LoopEnd;
				m_LoopCount += m_LoopStart;
				m_BGM.time = m_LoopCount;
			}
		}

		if (m_BGMDown == true) {
			//BGMをフェードアウトさせる
			m_BGM.volume -= Time.deltaTime * 0.25f;

			if (m_BGM.volume <= 0.0f) {
				m_BGM.volume = 0.0f;
				m_BGM.Stop ();
			}
		} else if (m_BGMVolume == true) {
			//BGMの大きさを調整する
			m_BGM.volume = 1.0f;
		} else {
			m_BGM.volume = 0.4f;
		}

		//鳴らす予定のSEを鳴らす
		for (int i = 0; i < m_SENumber.Count; ++i) {
			m_SE.PlayOneShot (m_SESound [(int)m_SENumber [i]]);
		}

		m_SENumber.Clear ();
	}

	//BGMを鳴らす
	public static void BGMPlay (){
		m_BGMPlayFlag = true;
	}

	//SEを鳴らす
	public static void SEPlay( eSoundSE playSE ){
		m_SENumber.Add (playSE);
	}

	//BGMをフェードアウト
	public static void BGMDown(){
		m_BGMDown = true;
	}

	//BGMを大きく
	public static void BGMVolumeDown(){
		m_BGMVolume = false;
	}

	//BGMを小さく
	public static void BGMVolumeUp(){
		m_BGMVolume = true;
	}
}
