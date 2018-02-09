using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000061 RID: 97
public class PictureViewPanel : MonoBehaviour
{
	// Token: 0x06000373 RID: 883 RVA: 0x00013DA8 File Offset: 0x000121A8
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.PushCloseButton();
		});
	}

	// Token: 0x06000374 RID: 884 RVA: 0x00013DD4 File Offset: 0x000121D4
	public void OpenPanel(Sprite picture)
	{
		base.gameObject.SetActive(true);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.PictureImage.GetComponent<Image>().sprite = picture;
		this.PictureImage.GetComponent<RectTransform>().sizeDelta = picture.textureRect.size;
		this.PicturePanelUI.S_FlickChecker.stopFlick(true);
		Debug.Log("texRectSize = " + picture.textureRect.size);
		Debug.Log(this.PictureImage.GetComponentInParent<RectTransform>().sizeDelta);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
		this.BackFunc();
	}

	// Token: 0x06000375 RID: 885 RVA: 0x00013EBC File Offset: 0x000122BC
	public void PushCloseButton()
	{
		base.gameObject.SetActive(false);
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		this.PicturePanelUI.S_FlickChecker.stopFlick(false);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		base.GetComponentInParent<UIMaster_Album>().BackFunc();
	}

	// Token: 0x06000376 RID: 886 RVA: 0x00013F3C File Offset: 0x0001233C
	public void PushDeleteButton()
	{
		ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel_YesNo("選択中の写真を削除しますか？");
		confilm.ResetOnClick_Yes();
		confilm.SetOnClick_Yes(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Yes(delegate
		{
			this.GetComponentInParent<UIMaster>().freezeObject(false);
		});
		confilm.SetOnClick_Yes(delegate
		{
			this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		});
		confilm.SetOnClick_Yes(delegate
		{
			this.PicturePanelUI.GetComponent<FlickCheaker>().FlickInit();
		});
		confilm.SetOnClick_Yes(delegate
		{
			this.PicturePanelUI.SimpleDeleteItem(this.PicturePanelUI.selectIndex);
		});
		confilm.SetOnClick_Yes(delegate
		{
			this.PushCloseButton();
		});
		confilm.ResetOnClick_No();
		confilm.SetOnClick_No(delegate
		{
			confilm.ClosePanel();
		});
	}

	// Token: 0x04000204 RID: 516
	public PicturePanel PicturePanelUI;

	// Token: 0x04000205 RID: 517
	public GameObject PictureImage;

	// Token: 0x04000206 RID: 518
	public GameObject ConfilmUI;
}
