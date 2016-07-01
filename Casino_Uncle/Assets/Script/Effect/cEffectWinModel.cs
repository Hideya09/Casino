using UnityEngine;
using System.Collections;

public class cEffectWinModel : ScriptableObject {
	
	private Vector3 m_TextScale;

	public float m_MaxScale;

	private float m_Count;
	public float m_MaxCount;

	private bool m_ParthicleFlag;
	private bool m_DrawFlag;

	private bool m_TapFlag;
	private bool m_EffectEndFlag;

	public float m_TextSpeed;

	public float m_TextOnScale;

	void OnEnable(){
		Init ();
	}

	public void Init(){
		m_DrawFlag = false;
		m_ParthicleFlag = false;

		m_Count = 0.0f;

		m_TapFlag = false;
		m_EffectEndFlag = false;

		m_TextScale = Vector3.zero;
	}

	public Vector3 GetTextScale(){
		return m_TextScale;
	}

	public bool GetParthicle(){
		return m_ParthicleFlag;
	}

	public bool GetDraw(){
		return m_DrawFlag;
	}

	//文字を拡大しつつ表示する
	public bool EffectOn(){

		m_DrawFlag = true;
		m_ParthicleFlag = true;

		float secound = Time.deltaTime;

		m_Count +=  Time.deltaTime;

		if (m_Count >= m_MaxCount) {
			m_Count = m_MaxCount;
			if (m_TextScale == Vector3.zero) {
				cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Win);
			}
			m_TextScale += new Vector3 (m_TextSpeed * secound, m_TextSpeed * secound, 1.0f);
			if (m_TextScale.x >= m_MaxScale) {
				m_TextScale = new Vector3 (m_MaxScale, m_MaxScale, 1.0f);

				m_TapFlag = false;
				m_EffectEndFlag = true;

				return true;
			}
		}else{
			m_ParthicleFlag = true;
		}

		return false;
	}

	public void Tap(){
		m_TapFlag = true;
	}

	//拡大が終わった状態で、タップされたらパーティクルを終わらせtrueを返す
	public bool GetTapFlag(){
		if( m_TapFlag & m_EffectEndFlag == true ){
			m_ParthicleFlag = false;
			return true;
		}

		return false;
	}
}
