using UnityEngine;
using System.Collections;

public class cGameSceneManager : MonoBehaviour {

	//ゲームシーン
	public enum eGameScene{
		GameScene_Title, //タイトルシーン
		GameScene_Game, //ゲームシーン
	}

	private static GameObject m_SceneManager;

	//現在のシーン
	public eGameScene m_GameScene;

	//シーン
	public cSceneBase[] m_Scene;

	private cTapEffectModel m_tapModel;

	void Awake(){
		//ゲームシーンマネージャーは一つだけしか作らない
		if (m_SceneManager == null) {
			DontDestroyOnLoad (gameObject);

			m_SceneManager = gameObject;

			m_tapModel = (cTapEffectModel)Resources.Load ("Scriptable/Effect/cTapEffectModel");
		} else {
			Destroy (gameObject);
			return;
		}

		//フレームレート設定
		//Application.targetFrameRate = 60;
	}

	// Use this for initialization
	void Start () {
		//m_Scene [1].SceneInit ();
	}
	
	// Update is called once per frame
	void Update () {
		//返り値が現在のシーン番号と違う場合はシーンを切り替える

		eGameScene nextScene = m_Scene [(int)m_GameScene].SceneExec ();

		m_tapModel.CountUp ();

		if (nextScene != m_GameScene) {
			UnityEngine.SceneManagement.SceneManager.LoadScene ((int)nextScene);
			m_GameScene = nextScene;
		}
	}
}