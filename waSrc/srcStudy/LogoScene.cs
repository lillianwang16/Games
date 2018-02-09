using System;
using UnityEngine;

// Token: 0x020000C4 RID: 196
public class LogoScene : MonoBehaviour
{
	// Token: 0x060004F6 RID: 1270 RVA: 0x00022119 File Offset: 0x00020519
	public void Start()
	{
		this.LogoImage.setFadeOut(this.InOutTime);
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0002212C File Offset: 0x0002052C
	public void Update()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= this.waitTime + this.InOutTime && this.timer - Time.deltaTime < this.waitTime + this.InOutTime)
		{
			this.LogoImage.setFadeIn(this.InOutTime);
		}
	}

	// Token: 0x060004F8 RID: 1272 RVA: 0x00022194 File Offset: 0x00020594
	public void PushScreen()
	{
		if (this.timer < this.InOutTime)
		{
			return;
		}
		if (this.timer >= this.waitTime + this.InOutTime)
		{
			return;
		}
		this.timer = this.waitTime + this.InOutTime;
		this.LogoImage.setFadeIn(this.InOutTime);
	}

	// Token: 0x040004CC RID: 1228
	[SerializeField]
	private float InOutTime = 1f;

	// Token: 0x040004CD RID: 1229
	[SerializeField]
	private float waitTime = 2f;

	// Token: 0x040004CE RID: 1230
	[SerializeField]
	private FadeController LogoImage;

	// Token: 0x040004CF RID: 1231
	private float timer;
}
