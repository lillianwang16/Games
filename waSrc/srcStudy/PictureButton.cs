using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003A RID: 58
public class PictureButton : MonoBehaviour
{
	// Token: 0x06000245 RID: 581 RVA: 0x000079A2 File Offset: 0x00005DA2
	public void setIndex(int _index)
	{
		this.index = _index;
	}

	// Token: 0x06000246 RID: 582 RVA: 0x000079AB File Offset: 0x00005DAB
	public void setDateTime(DateTime _dateTime)
	{
	}

	// Token: 0x06000247 RID: 583 RVA: 0x000079AD File Offset: 0x00005DAD
	public void CngImage(Sprite image)
	{
		this.Image.GetComponent<Image>().sprite = image;
	}

	// Token: 0x06000248 RID: 584 RVA: 0x000079C0 File Offset: 0x00005DC0
	public void OnClick()
	{
		if (this.onClickCancel)
		{
			this.onClickCancel = false;
			return;
		}
		base.GetComponentInParent<PicturePanel>().SetPanelData(this.index, base.transform.localPosition);
	}

	// Token: 0x06000249 RID: 585 RVA: 0x000079F4 File Offset: 0x00005DF4
	public void SetDeleteButton(bool flag)
	{
		this.DeleteBtn.SetActive(flag);
		if (flag)
		{
			this.Image.GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
		}
		else
		{
			this.Image.GetComponent<Image>().color = new Color(1f, 1f, 1f);
		}
	}

	// Token: 0x0600024A RID: 586 RVA: 0x00007A60 File Offset: 0x00005E60
	public void HoldButton()
	{
		base.GetComponentInParent<PicturePanel>().ChgDeleteMode();
		this.OnClick();
		this.onClickCancel = true;
	}

	// Token: 0x0600024B RID: 587 RVA: 0x00007A7A File Offset: 0x00005E7A
	public void ResetClickCancel()
	{
		this.onClickCancel = false;
	}

	// Token: 0x0600024C RID: 588 RVA: 0x00007A83 File Offset: 0x00005E83
	public void DeleteButton()
	{
		base.GetComponentInParent<PicturePanel>().DeleteItemBtn(this.index);
	}

	// Token: 0x040000FC RID: 252
	public GameObject Image;

	// Token: 0x040000FD RID: 253
	public GameObject DeleteBtn;

	// Token: 0x040000FE RID: 254
	public GameObject DateText;

	// Token: 0x040000FF RID: 255
	public GameObject NameText;

	// Token: 0x04000100 RID: 256
	public bool onClickCancel;

	// Token: 0x04000101 RID: 257
	public int index;

	// Token: 0x04000102 RID: 258
	public DateTime dateTime;
}
