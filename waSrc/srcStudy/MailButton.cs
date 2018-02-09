using System;
using Mail;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000037 RID: 55
public class MailButton : MonoBehaviour
{
	// Token: 0x0600022E RID: 558 RVA: 0x00007314 File Offset: 0x00005714
	public void OnClick()
	{
		this.SetMailEvtId(this.mailEvtId, true);
		this.mailScrollView.GetComponent<MailScrollView>().PushMailEvt(this.mailId, this.mailEvtId);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
	}

	// Token: 0x0600022F RID: 559 RVA: 0x00007364 File Offset: 0x00005764
	public void OnPushButton()
	{
		this.SetMailEvtId(this.mailEvtId, true);
		this.mailScrollView.GetComponent<MailScrollView>().PushMailEvt(this.mailId, this.mailEvtId);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
	}

	// Token: 0x06000230 RID: 560 RVA: 0x000073B3 File Offset: 0x000057B3
	public void DeleteButton()
	{
		UnityEngine.Object.Destroy(base.gameObject);
	}

	// Token: 0x06000231 RID: 561 RVA: 0x000073C0 File Offset: 0x000057C0
	public void setId(int _mailId)
	{
		this.mailId = _mailId;
	}

	// Token: 0x06000232 RID: 562 RVA: 0x000073C9 File Offset: 0x000057C9
	public void CngTitleName(string titleStr)
	{
		this.MailTitleText.GetComponent<Text>().text = titleStr;
	}

	// Token: 0x06000233 RID: 563 RVA: 0x000073DC File Offset: 0x000057DC
	public void CngSenderImage(Sprite sprite)
	{
		if (sprite == null)
		{
			this.MailSenderImage.SetActive(false);
			return;
		}
		this.MailSenderImage.GetComponent<Image>().sprite = sprite;
		this.MailSenderImage.GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
	}

	// Token: 0x06000234 RID: 564 RVA: 0x00007444 File Offset: 0x00005844
	public void CngItemImage(Sprite image)
	{
		if (image == null)
		{
			this.MailItemImage.SetActive(false);
		}
		this.MailItemImage.GetComponent<Image>().sprite = image;
	}

	// Token: 0x06000235 RID: 565 RVA: 0x00007470 File Offset: 0x00005870
	public void SetImageAndNum(int val, Sprite sprite)
	{
		if (val > 0)
		{
			this.MailCloverImage.SetActive(true);
			this.CloverNumBase.SetActive(true);
			if (!this.MailSenderImage.activeSelf)
			{
				Vector3 localPosition = this.MailCloverImage.GetComponent<RectTransform>().localPosition;
				this.MailCloverImage.GetComponent<RectTransform>().localPosition = new Vector3(localPosition.x - 60f, localPosition.y, localPosition.z);
				localPosition = this.CloverNumBase.GetComponent<RectTransform>().localPosition;
				this.CloverNumBase.GetComponent<RectTransform>().localPosition = new Vector3(localPosition.x - 60f, localPosition.y, localPosition.z);
			}
			this.MailCloverImage.GetComponent<Image>().sprite = sprite;
			this.MailCloverImage.GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
			base.GetComponent<NumObjCreater>().createNumObj(val, this.CloverNumBase.transform.localPosition, (int)this.CloverNumBase.transform.GetComponent<RectTransform>().sizeDelta.x);
		}
		else
		{
			this.MailCloverImage.SetActive(false);
			this.CloverNumBase.SetActive(false);
		}
	}

	// Token: 0x06000236 RID: 566 RVA: 0x000075C8 File Offset: 0x000059C8
	public void SetImage(Sprite sprite)
	{
		this.MailCloverImage.SetActive(true);
		this.CloverNumBase.SetActive(false);
		this.MailCloverImage.GetComponent<Image>().sprite = sprite;
		this.MailCloverImage.GetComponent<RectTransform>().sizeDelta = new Vector2(sprite.rect.width, sprite.rect.height);
	}

	// Token: 0x06000237 RID: 567 RVA: 0x0000762F File Offset: 0x00005A2F
	public void CngbuttonString(string str)
	{
		this.AcceptButton.GetComponentInChildren<Text>().text = str;
	}

	// Token: 0x06000238 RID: 568 RVA: 0x00007642 File Offset: 0x00005A42
	public void SetMailEvtId(EvtId setEvtId, bool opened)
	{
		this.mailEvtId = setEvtId;
	}

	// Token: 0x040000EB RID: 235
	public GameObject mailScrollView;

	// Token: 0x040000EC RID: 236
	public int mailId;

	// Token: 0x040000ED RID: 237
	public EvtId mailEvtId;

	// Token: 0x040000EE RID: 238
	public GameObject MailTitleText;

	// Token: 0x040000EF RID: 239
	public GameObject MailSenderImage;

	// Token: 0x040000F0 RID: 240
	public GameObject MailItemImage;

	// Token: 0x040000F1 RID: 241
	public GameObject MailCloverImage;

	// Token: 0x040000F2 RID: 242
	public GameObject CloverNumBase;

	// Token: 0x040000F3 RID: 243
	public GameObject AcceptButton;
}
