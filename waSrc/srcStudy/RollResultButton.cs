using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200003C RID: 60
public class RollResultButton : MonoBehaviour
{
	// Token: 0x06000255 RID: 597 RVA: 0x00007B6A File Offset: 0x00005F6A
	public void CngResultText(string str)
	{
		this.ResultText.GetComponent<Text>().text = str;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00007B7D File Offset: 0x00005F7D
	public void CngImage(int ImgNum)
	{
		this.ResultImage.GetComponent<Image>().sprite = this.ResultSprites[ImgNum];
	}

	// Token: 0x0400010A RID: 266
	public GameObject ResultImage;

	// Token: 0x0400010B RID: 267
	public GameObject ResultText;

	// Token: 0x0400010C RID: 268
	public Sprite[] ResultSprites;
}
