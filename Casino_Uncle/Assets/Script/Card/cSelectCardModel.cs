using UnityEngine;
using System.Collections;

public class cSelectCardModel : cCardModel {

	public bool m_MoveFlag{ set; private get; }

	//選択処理用フラグ
	private bool m_FixedFlag;
	private bool m_TapFlag;
	private bool m_SelectFlag;

	//使用フラグ
	private bool m_UsedFlag;

	//前回位置
	private Vector2 m_BufPosition;

	//基準となる位置
	private Vector2 m_BasePosition;

	private Vector2 m_Movement;
	private float m_Count;
	private float m_MaxCount;

	//明滅関連
	private float m_BlinkCount;
	public float m_BlinkMaxCount;
	private int m_BlinkNumber;
	public int m_BlinkMaxNumber;

	public static float m_Line = 0.0f;

	void OnEnable(){
		CardInit ();

		m_TapFlag = false;

		m_SelectFlag = false;

		m_MoveFlag = false;

		m_UsedFlag = true;

		m_FixedFlag = false;
	}

	//初期位置取得
	public override void InitPosition( Vector2 position ){
		m_BasePosition = position;

		m_RotationY = 0.0f;

		m_UsedFlag = true;
	}

	//初期化
	public void Init( Vector2 setPosition , float speed ){
		m_DrawMode = eDrawMode.eDrawMode_Back;
		m_Size = eSize.eSize_Medium;

		m_Fade = 0.0f;

		m_Position = setPosition;

		m_BufPosition = m_Position;

		m_MaxCount = Vector2.Distance (m_BasePosition, m_Position) / speed;

		m_Movement = ( m_BasePosition - m_Position ) / m_MaxCount;

		m_Count = 0;

		m_UsedFlag = true;

		m_BlinkCount = 0.0f;
		m_BlinkNumber = 0;
	}

	//もうその５戦では使わない時
	public void End(){
		m_DrawMode = eDrawMode.eDrawMode_None;
		m_Size = eSize.eSize_Medium;

		//m_CardNumber = cCardSpriteManager.Back;

		m_Position = m_BufPosition;

		m_UsedFlag = false;

		m_TapFlag = false;

		m_SelectFlag = false;

		m_BlinkCount = 0.0f;
		m_BlinkNumber = 0;
	}

	//選択可能状態にする
	public void SetSelect(){
		CardInit ();

		if (m_UsedFlag == true) {
			m_DrawMode = eDrawMode.eDrawMode_Front;
		}

		m_TapFlag = false;

		m_SelectFlag = false;

		m_FixedFlag = false;

		m_BlinkCount = 0.0f;
		m_BlinkNumber = 0;
	}

	//カード配り
	public bool Move(){

		m_Fade = 1.0f;

		if (m_Count == 0.0f) {
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Slide);
		}

		m_Count += Time.deltaTime;

		if (m_Count < m_MaxCount) {
			m_Position += (Time.deltaTime * m_Movement);

			return false;
		} else {
			m_Position = m_BasePosition;

			return true;
		}
	}

	public bool OutLineBlink(){
		if (m_UsedFlag == false) {
			return true;
		}

		m_BlinkCount += Time.deltaTime;

		if (m_OutLineMode == eOutLineMode.eOutLineMode_None) {
			if (m_BlinkCount >= m_BlinkMaxCount) {
				m_OutLineMode = eOutLineMode.eOutLineMode_Yellow;
				m_BlinkCount -= m_BlinkMaxCount;
			}
		} else if (m_OutLineMode == eOutLineMode.eOutLineMode_Yellow) {
			if (m_BlinkCount >= m_BlinkMaxCount) {
				m_OutLineMode = eOutLineMode.eOutLineMode_None;
				m_BlinkCount -= m_BlinkMaxCount;

				++m_BlinkNumber;
			}
		}

		if (m_BlinkNumber >= m_BlinkMaxNumber) {
			return true;
		}

		return false;
	}

	//カードを選択した時
	public void ConfirmCard(){
		if (m_TapFlag == true) {
			m_OutLineMode = eOutLineMode.eOutLineMode_None;

			m_SelectFlag = true;

			//if (m_Line < m_Position.y) {
			//	m_SelectFlag = true;
			//} else {
			//	m_Position = m_BasePosition;
			//	m_BufPosition = m_Position;
			//}
		} else if (m_MoveFlag == true && m_UsedFlag == true && m_FixedFlag == true) {
			m_TapFlag = true;

			m_DrawMode = eDrawMode.eDrawMode_Front;
			m_Size = eSize.eSize_Large;
		}
	}

	//位置を設定
	public void SetPosition( Vector2 position ){
		m_BufPosition = position;

		m_FixedFlag = true;

		if (m_MoveFlag == true && m_UsedFlag == true) {
			cSoundManager.SEPlay (cSoundManager.eSoundSE.eSoundSE_Decision);
		}
	}

	public void FixedEnd(){
		m_FixedFlag = false;
	}

	//カードの状態を戻す
	public void UnTapCard(){
		if (m_TapFlag == true) {
			m_TapFlag = false;
			m_Size = eSize.eSize_Medium;
		}
	}

	//カードを所定の位置に移動させる
	public bool MoveSelectCard( Vector2 position ){
		m_Position = position;

		m_DrawMode = eDrawMode.eDrawMode_None;

		m_Size = eSize.eSize_Medium;

		return true;
	}

	//カードが選択されていない時
	public void NotSelectCard(){
		m_DrawMode = eDrawMode.eDrawMode_Dark;
		m_Size = eSize.eSize_Small;
	}

	//カードの状態を普通にする
	public void InitSelectCard(){
		m_DrawMode = eDrawMode.eDrawMode_Front;
		m_Size = eSize.eSize_Medium;
	}

	//カードの位置をマウスと連動させる
	public void SelectCard( Vector2 position ){
		//if (m_TapFlag == true) {
		//
		//	m_Position += ( position - m_BufPosition );
		//
		//	m_BufPosition = position;
		//
		//	if (m_Line < m_Position.y) {
		//		m_OutLineMode = eOutLineMode.eOutLineMode_Yellow;
		//	} else {
		//		m_OutLineMode = eOutLineMode.eOutLineMode_Blue;
		//	}
		//}
	}

	public bool GetTap(){
		return m_TapFlag;
	}

	public bool GetSelect(){
		return m_SelectFlag;
	}

	public bool GetUsed(){
		return m_UsedFlag;
	}
}
