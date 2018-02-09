using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DE RID: 222
public class IAPPayFormatChanger : MonoBehaviour
{
	// Token: 0x06000608 RID: 1544 RVA: 0x00024563 File Offset: 0x00022963
	public void Start()
	{
		this.myText.text = string.Empty;
		this.getPayText = string.Empty;
		this.mainBtn.interactable = false;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0002458C File Offset: 0x0002298C
	public void Update()
	{
		if (this.getPayText != this.payText.text)
		{
			this.myText.text = this.payText.text;
			this.getPayText = this.payText.text;
			this.mainBtn.interactable = true;
		}
	}

	// Token: 0x04000538 RID: 1336
	[SerializeField]
	private Button mainBtn;

	// Token: 0x04000539 RID: 1337
	[SerializeField]
	private Text myText;

	// Token: 0x0400053A RID: 1338
	[SerializeField]
	private Text payText;

	// Token: 0x0400053B RID: 1339
	[SerializeField]
	private string getPayText;
}
