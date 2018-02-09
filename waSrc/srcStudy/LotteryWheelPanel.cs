using System;
using Prize;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000058 RID: 88
public class LotteryWheelPanel : MonoBehaviour
{
	// Token: 0x06000315 RID: 789 RVA: 0x0000ECF0 File Offset: 0x0000D0F0
	public void OpenPanel(Rank _result)
	{
		base.gameObject.SetActive(true);
		this.main_ball.GetComponent<Image>().sprite = this.RollResultBtn.ResultSprites[(int)_result];
		this.timer = 0f;
	}

	// Token: 0x06000316 RID: 790 RVA: 0x0000ED26 File Offset: 0x0000D126
	public void ClosePanel()
	{
		base.gameObject.SetActive(false);
		this.simu_ball.SetActive(false);
		this.main_ball.SetActive(false);
	}

	// Token: 0x06000317 RID: 791 RVA: 0x0000ED4C File Offset: 0x0000D14C
	public void BallDrop()
	{
		this.simu_ball.SetActive(true);
		this.simu_ball.transform.localPosition = new Vector3(600f, UnityEngine.Random.Range(300f, 400f));
		this.simu_ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
		this.simu_ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(UnityEngine.Random.Range(-300f, -175f), UnityEngine.Random.Range(-50f, 50f)));
		this.main_ball.SetActive(true);
		this.main_ball.transform.localPosition = this.simu_ball.transform.localPosition;
	}

	// Token: 0x06000318 RID: 792 RVA: 0x0000EE04 File Offset: 0x0000D204
	public void Proc()
	{
		this.timer += Time.deltaTime;
		if (this.timer >= 0.5f && this.timer - Time.deltaTime < 0.5f)
		{
			this.BallDrop();
		}
		if (this.timer >= this.timerMax)
		{
			this.ClosePanel();
			this.RaffelPanel.OpenResultButton();
		}
		if (this.timer >= 0.5f)
		{
			Vector3 localPosition = this.simu_ball.transform.localPosition;
			localPosition.y += (localPosition.x - 70f) * 0.333333f;
			this.main_ball.transform.localPosition = localPosition;
		}
	}

	// Token: 0x06000319 RID: 793 RVA: 0x0000EEC4 File Offset: 0x0000D2C4
	public void DebugBall()
	{
		this.timer = 0f;
	}

	// Token: 0x040001AC RID: 428
	[SerializeField]
	private RollResultButton RollResultBtn;

	// Token: 0x040001AD RID: 429
	[SerializeField]
	private RaffelPanel RaffelPanel;

	// Token: 0x040001AE RID: 430
	[SerializeField]
	private GameObject simu_ball;

	// Token: 0x040001AF RID: 431
	[SerializeField]
	private GameObject main_ball;

	// Token: 0x040001B0 RID: 432
	[SerializeField]
	private float timer;

	// Token: 0x040001B1 RID: 433
	private readonly float timerMax = 3f;
}
