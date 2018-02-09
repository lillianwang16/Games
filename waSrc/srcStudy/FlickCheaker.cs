using System;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000DC RID: 220
public class FlickCheaker : MonoBehaviour
{
	// Token: 0x060005F9 RID: 1529 RVA: 0x00023F34 File Offset: 0x00022334
	private void Start()
	{
		this.resultVector = Vector2.zero;
		this.reFlickUp = true;
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00023F48 File Offset: 0x00022348
	public void FlickInit()
	{
		this.Start();
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x00023F50 File Offset: 0x00022350
	public void FlickUpdate()
	{
		if (!this.stopFlickCheck)
		{
			if (Input.GetMouseButtonDown(0))
			{
				this.startPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				this.reFlickUp = false;
			}
			if (Input.GetMouseButtonUp(0) && !this.reFlickUp)
			{
				this.endPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
				this.resultVector = new Vector2(this.endPos.x - this.startPos.x, this.endPos.y - this.startPos.y);
				if (this.fixLeft && this.resultVector.x < 0f)
				{
					this.resultVector.x = 0f;
				}
				if (this.fixRight && this.resultVector.x > 0f)
				{
					this.resultVector.x = 0f;
				}
				if (this.fixUp && this.resultVector.y > 0f)
				{
					this.resultVector.y = 0f;
				}
				if (this.fixDown && this.resultVector.y < 0f)
				{
					this.resultVector.y = 0f;
				}
				this.CallBackCheck();
			}
		}
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x000240DC File Offset: 0x000224DC
	private void CallBackCheck()
	{
		if (this.resultVector.magnitude >= this.flickMin)
		{
			Vector2 rhs = this.ResultDirection();
			if (Vector2.left == rhs)
			{
				this.LeftFlick.Invoke();
			}
			if (Vector2.right == rhs)
			{
				this.RightFlick.Invoke();
			}
			if (Vector2.up == rhs)
			{
				this.UpFlick.Invoke();
			}
			if (Vector2.down == rhs)
			{
				this.DownFlick.Invoke();
			}
		}
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x00024172 File Offset: 0x00022572
	public void stopFlick(bool stop)
	{
		this.stopFlickCheck = stop;
		this.reFlickUp = true;
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x00024184 File Offset: 0x00022584
	public Vector2 nowFlickVector()
	{
		if (Input.GetMouseButton(0) && !this.stopFlickCheck)
		{
			return new Vector2(Input.mousePosition.x - this.startPos.x, Input.mousePosition.y - this.startPos.y);
		}
		return Vector2.zero;
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x000241E4 File Offset: 0x000225E4
	public Vector2 resultFlickVector()
	{
		return this.resultVector;
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x000241EC File Offset: 0x000225EC
	public Vector2 nowDirection()
	{
		return this.flickDirection(true);
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x000241F5 File Offset: 0x000225F5
	public Vector2 ResultDirection()
	{
		return this.flickDirection(false);
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x00024200 File Offset: 0x00022600
	public Vector2 flickDirection(bool now)
	{
		Vector2 result = Vector2.zero;
		Vector2 vector;
		if (now)
		{
			vector = this.nowFlickVector();
			if (this.fixLeft && vector.x < 0f)
			{
				vector.x = 0f;
			}
			if (this.fixRight && vector.x > 0f)
			{
				vector.x = 0f;
			}
			if (this.fixUp && vector.y > 0f)
			{
				vector.y = 0f;
			}
			if (this.fixDown && vector.y < 0f)
			{
				vector.y = 0f;
			}
		}
		else
		{
			vector = this.resultVector;
		}
		if (vector.x < 0f)
		{
			result = Vector2.left;
		}
		else if (vector.x > 0f)
		{
			result = Vector2.right;
		}
		if (Mathf.Abs(vector.y) > Mathf.Abs(vector.x))
		{
			if (vector.y > 0f)
			{
				result = Vector2.up;
			}
			else if (vector.y < 0f)
			{
				result = Vector2.down;
			}
		}
		return result;
	}

	// Token: 0x04000527 RID: 1319
	public bool stopFlickCheck;

	// Token: 0x04000528 RID: 1320
	private bool reFlickUp;

	// Token: 0x04000529 RID: 1321
	public float flickMin = 384f;

	// Token: 0x0400052A RID: 1322
	public bool fixLeft;

	// Token: 0x0400052B RID: 1323
	public bool fixRight;

	// Token: 0x0400052C RID: 1324
	public bool fixUp;

	// Token: 0x0400052D RID: 1325
	public bool fixDown;

	// Token: 0x0400052E RID: 1326
	private Vector2 startPos;

	// Token: 0x0400052F RID: 1327
	private Vector2 endPos;

	// Token: 0x04000530 RID: 1328
	public Vector2 resultVector;

	// Token: 0x04000531 RID: 1329
	[SerializeField]
	private UnityEvent LeftFlick = new UnityEvent();

	// Token: 0x04000532 RID: 1330
	[SerializeField]
	private UnityEvent RightFlick = new UnityEvent();

	// Token: 0x04000533 RID: 1331
	[SerializeField]
	private UnityEvent UpFlick = new UnityEvent();

	// Token: 0x04000534 RID: 1332
	[SerializeField]
	private UnityEvent DownFlick = new UnityEvent();
}
