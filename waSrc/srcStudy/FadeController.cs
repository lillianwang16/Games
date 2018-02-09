using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000DB RID: 219
public class FadeController : MonoBehaviour
{
	// Token: 0x060005F1 RID: 1521 RVA: 0x00023D10 File Offset: 0x00022110
	private void Start()
	{
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x00023D14 File Offset: 0x00022114
	private void Update()
	{
		if (this.fadeFlag)
		{
			this.timer += Time.deltaTime;
			float num = this.afterAlpha - this.beforeAlpha;
			float num2 = 1f / this.interval * this.timer;
			num = this.afterAlpha - this.beforeAlpha;
			num2 = 1f / this.interval * this.timer;
			Color color = this.setColor;
			color = this.setColor;
			color.a = this.beforeAlpha + num * num2;
			if (color.a >= this.maxAlpha)
			{
				this.fadeFlag = false;
				color.a = this.maxAlpha;
			}
			if (color.a <= this.minAlpha)
			{
				this.fadeFlag = false;
				color.a = this.minAlpha;
			}
			base.GetComponent<Image>().color = color;
		}
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00023DF9 File Offset: 0x000221F9
	public void setFadeIn(float _interval)
	{
		this.fadeFlag = true;
		this.interval = _interval;
		this.timer = 0f;
		this.beforeAlpha = this.maxAlpha;
		this.afterAlpha = this.minAlpha;
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x00023E2C File Offset: 0x0002222C
	public void setFadeOut(float _interval)
	{
		this.fadeFlag = true;
		this.interval = _interval;
		this.timer = 0f;
		this.beforeAlpha = this.minAlpha;
		this.afterAlpha = this.maxAlpha;
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x00023E60 File Offset: 0x00022260
	public void setFadeColor(Color _setColor)
	{
		this.setColor = _setColor;
		if (this.setColor.a > this.maxAlpha)
		{
			this.setColor.a = this.maxAlpha;
		}
		if (this.setColor.a > this.minAlpha)
		{
			this.setColor.a = this.minAlpha;
		}
		base.GetComponent<Image>().color = this.setColor;
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x00023ED3 File Offset: 0x000222D3
	public void setFadeColor(Color _setColor, float _maxAlpha, float _minAlpha)
	{
		this.maxAlpha = _maxAlpha;
		this.minAlpha = _minAlpha;
		this.setFadeColor(_setColor);
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x00023EEA File Offset: 0x000222EA
	public bool checkFadeComplete()
	{
		return !this.fadeFlag;
	}

	// Token: 0x0400051F RID: 1311
	private bool fadeFlag;

	// Token: 0x04000520 RID: 1312
	public Color setColor = new Color(0f, 0f, 0f, 1f);

	// Token: 0x04000521 RID: 1313
	public float maxAlpha = 1f;

	// Token: 0x04000522 RID: 1314
	public float minAlpha;

	// Token: 0x04000523 RID: 1315
	private float beforeAlpha;

	// Token: 0x04000524 RID: 1316
	private float afterAlpha;

	// Token: 0x04000525 RID: 1317
	private float interval;

	// Token: 0x04000526 RID: 1318
	private float timer;
}
