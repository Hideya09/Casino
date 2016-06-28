using UnityEngine;
using System.Collections;

public class cCardModel : ScriptableObject {
	protected Vector2 m_Position;
	protected float m_RotationY;
	protected float m_RotationZ;

	private bool m_EndOpen;

	protected float m_Fade;

	//カードの描画モード
	public enum eDrawMode{
		eDrawMode_None,
		eDrawMode_Front,
		eDrawMode_Dark,
		eDrawMode_Back
	}

	//カードのアウトラインの描画モード
	public enum eOutLineMode{
		eOutLineMode_None,
		eOutLineMode_Yellow,
		eOutLineMode_Blue,
		eOutLineMode_Red,
	}

	//カードの描画サイズ
	public enum eSize{
		eSize_MostSmall,
		eSize_Small,
		eSize_Medium,
		eSize_Large
	}
		
	public eDrawMode m_DrawMode{ get; protected set; }
	public eOutLineMode m_OutLineMode{ get; protected set; }
	public eSize m_Size{ get; protected set; }

	//カードのナンバー
	public int m_CardNumber{ set; get; }

	void OnEnable(){
		m_DrawMode = eDrawMode.eDrawMode_None;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;
		m_Size = eSize.eSize_Medium;
	}

	public virtual void InitPosition( Vector2 position ){
		m_Position = position;
		m_RotationY = 0.0f;
	}

	public Vector2 GetPosition(){
		return m_Position;
	}

	public float GetRotationY(){
		return m_RotationY;
	}

	public float GetRotationZ(){
		return m_RotationZ;
	}

	public void CardInit(){
		m_DrawMode = eDrawMode.eDrawMode_None;
		m_OutLineMode = eOutLineMode.eOutLineMode_None;
		m_Size = eSize.eSize_Medium;

		m_Fade = 1.0f;

		m_EndOpen = false;
	}

	//カードを裏から表に回転させる
	public bool Open(){
		m_EndOpen = false;

		if (m_DrawMode == eDrawMode.eDrawMode_Back) {
			m_RotationY += 360 * Time.deltaTime;

			if (m_RotationY >= 90) {
				m_RotationY = 270 + (m_RotationY - 90);
				m_DrawMode = eDrawMode.eDrawMode_Front;
			}
		} else if (m_DrawMode == eDrawMode.eDrawMode_Front) {
			m_RotationY += 360 * Time.deltaTime;

			if (m_RotationY >= 360 || m_RotationY < 45) {
				m_RotationY = 0;

				m_EndOpen = true;
			}
			return true;
		}

		return false;
	}

	//カードを表から裏に回転させる
	public bool Close(){
		m_EndOpen = true;

		if (m_DrawMode == eDrawMode.eDrawMode_Front) {
			m_RotationY += 360 * Time.deltaTime;

			if (m_RotationY >= 90) {
				m_RotationY = 270 + (m_RotationY - 90);
				m_DrawMode = eDrawMode.eDrawMode_Back;
			}
		} else if (m_DrawMode == eDrawMode.eDrawMode_Back) {
			m_RotationY += 360 * Time.deltaTime;

			if (m_RotationY >= 360 || m_RotationY < 45) {
				m_RotationY = 0;

				m_EndOpen = false;
			}
			return true;
		}

		return false;
	}

	//退去処理
	public bool Back( float m_FadeTime , bool up = false ){
		m_Fade -= (Time.deltaTime * m_FadeTime);

		if (up == true) {
			m_Position.y += Time.deltaTime * 10;
		} else {
			m_Position.y -= Time.deltaTime * 10;
		}
		if (m_Fade <= 0.0f) {
			return true;
		}

		return false;
	}

	public float GetFade(){
		return m_Fade;
	}

	public bool GetOpen(){
		return m_EndOpen;
	}

	public void CardMostSmall(){
		m_Size = eSize.eSize_MostSmall;
	}
}
