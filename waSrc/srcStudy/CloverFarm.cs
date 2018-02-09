using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000042 RID: 66
public class CloverFarm : MonoBehaviour
{
	// Token: 0x06000271 RID: 625 RVA: 0x00008CD4 File Offset: 0x000070D4
	public bool CheckHitClover(Vector2 mousePoint)
	{
		bool result = false;
		for (int i = this.cloversObj.Count - 1; i >= 0; i--)
		{
			if (this.cloversObj[i].GetComponent<Clover>().CloverHitCheck(mousePoint))
			{
				result = true;
			}
		}
		return result;
	}

	// Token: 0x06000272 RID: 626 RVA: 0x00008D20 File Offset: 0x00007120
	public void CloverProc()
	{
		for (int i = this.cloversObj.Count - 1; i >= 0; i--)
		{
			if (this.cloversObj[i].GetComponent<Clover>().Proc())
			{
				UnityEngine.Object.Destroy(this.cloversObj[i]);
				this.cloversObj[i] = null;
				this.cloversObj.RemoveAt(i);
			}
		}
	}

	// Token: 0x06000273 RID: 627 RVA: 0x00008D90 File Offset: 0x00007190
	public void checkCloverCreate()
	{
		this.cloverList = SuperGameMaster.GetCloverList();
		bool flag = false;
		if (this.cloverList.Count == 0)
		{
			flag = true;
			Debug.Log("[CloverFarm] クローバーの初期化フラグが立ちました。四葉を生成します");
		}
		if (this.cloverList.Count < this.cloverMax)
		{
			Debug.Log(string.Concat(new object[]
			{
				"[CloverFarm] クローバーの数を調整します：",
				this.cloverList.Count,
				" > ",
				this.cloverMax
			}));
		}
		while (this.cloverList.Count < this.cloverMax)
		{
			CloverDataFormat cloverDataFormat = new CloverDataFormat();
			cloverDataFormat.lastHarvest = new DateTime(1970, 1, 1);
			cloverDataFormat.timeSpanSec = -this.cloverList.Count - 1;
			cloverDataFormat.newFlag = true;
			this.cloverList.Add(cloverDataFormat);
		}
		if (this.cloverList.Count > this.cloverMax)
		{
			Debug.Log(string.Concat(new object[]
			{
				"[CloverFarm] クローバーの数を調整します：",
				this.cloverList.Count,
				" > ",
				this.cloverMax
			}));
			this.cloverList.RemoveRange(this.cloverMax - 1, this.cloverList.Count - this.cloverMax);
		}
		this.cloversObj = new List<GameObject>();
		for (int i = 0; i < this.cloverList.Count; i++)
		{
			if (!this.cloverList[i].newFlag && this.cloverList[i].timeSpanSec <= 0)
			{
				this.cloversObj.Add(this.LoadCloverObject(i, this.cloverList[i]));
			}
		}
		int num = 0;
		for (int j = 0; j < this.cloverList.Count; j++)
		{
			if (this.cloverList[j].newFlag)
			{
				if (!flag)
				{
					this.cloversObj.Add(this.NewCloverObject(j, this.cloverList[j], this.cloversObj));
				}
				else
				{
					this.cloversObj.Add(this.NewCloverObject(j, this.cloverList[j], this.cloversObj, true));
					flag = false;
				}
				this.cloverList[j].x = this.cloversObj[this.cloversObj.Count - 1].transform.localPosition.x;
				this.cloverList[j].y = this.cloversObj[this.cloversObj.Count - 1].transform.localPosition.y;
				Clover component = this.cloversObj[this.cloversObj.Count - 1].GetComponent<Clover>();
				this.cloverList[j].element = component.element;
				this.cloverList[j].spriteNum = component.spriteNum;
				this.cloverList[j].point = component.point;
				this.cloverList[j].newFlag = false;
				num++;
			}
		}
		foreach (GameObject gameObject in this.cloversObj)
		{
			int num2 = this.cloverOrderInLayer;
			foreach (GameObject gameObject2 in this.cloversObj)
			{
				if (gameObject.transform.position.y < gameObject2.transform.position.y)
				{
					num2++;
				}
			}
			gameObject.GetComponent<SpriteRenderer>().sortingOrder = num2;
		}
		Debug.Log(string.Concat(new object[]
		{
			"[CloverFarm] クローバー生成完了：",
			this.cloversObj.Count,
			"\u3000/ (新規：",
			num,
			")"
		}));
	}

	// Token: 0x06000274 RID: 628 RVA: 0x00009214 File Offset: 0x00007614
	public GameObject LoadCloverObject(int idx, CloverDataFormat cloverData)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.basePrefab, Vector3.zero, Quaternion.identity);
		gameObject.GetComponent<Clover>().SetCloverData(idx, cloverData);
		int element = cloverData.element;
		if (element != 0)
		{
			if (element == 1)
			{
				gameObject.GetComponent<SpriteRenderer>().sprite = this.fourCloverSprite[cloverData.spriteNum];
			}
		}
		else
		{
			gameObject.GetComponent<SpriteRenderer>().sprite = this.cloverSprite[cloverData.spriteNum];
		}
		gameObject.transform.parent = base.transform;
		gameObject.transform.localScale = Vector3.one;
		Vector3 zero = Vector3.zero;
		zero.x = cloverData.x;
		zero.y = cloverData.y;
		gameObject.transform.localPosition = zero;
		return gameObject;
	}

	// Token: 0x06000275 RID: 629 RVA: 0x000092E5 File Offset: 0x000076E5
	public GameObject NewCloverObject(int index, CloverDataFormat cloverData, List<GameObject> cloversObj)
	{
		return this.NewCloverObject(index, cloverData, cloversObj, false);
	}

	// Token: 0x06000276 RID: 630 RVA: 0x000092F4 File Offset: 0x000076F4
	public GameObject NewCloverObject(int index, CloverDataFormat cloverData, List<GameObject> cloversObj, bool fourLeafFlag)
	{
		Vector2 size = base.GetComponent<BoxCollider2D>().size;
		PolygonCollider2D component = base.GetComponent<PolygonCollider2D>();
		Vector2 vector = new Vector2(base.GetComponent<BoxCollider2D>().offset.x - size.x / 2f, base.GetComponent<BoxCollider2D>().offset.y - size.y / 2f);
		int num = 0;
		bool flag;
		Vector3 vector2;
		do
		{
			flag = false;
			vector2 = new Vector2(vector.x + UnityEngine.Random.Range(0f, size.x), vector.y + UnityEngine.Random.Range(0f, size.y));
			if (!component.OverlapPoint(vector2 + base.transform.position))
			{
				flag = true;
			}
			else
			{
				for (int i = 0; i < cloversObj.Count; i++)
				{
					Vector2 size2 = cloversObj[i].GetComponent<BoxCollider2D>().size;
					if (Mathf.Abs(vector2.x - cloversObj[i].transform.localPosition.x) < size2.x / 2f && Mathf.Abs(vector2.y - cloversObj[i].transform.localPosition.y) < size2.y / 4f)
					{
						flag = true;
					}
				}
				num++;
				if (num >= 100)
				{
					break;
				}
			}
		}
		while (flag);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.basePrefab, Vector3.zero, Quaternion.identity);
		CloverDataFormat cloverDataFormat = new CloverDataFormat();
		cloverDataFormat.point = 1;
		cloverDataFormat.element = 0;
		if (UnityEngine.Random.Range(0f, 10000f) < this.fourLeaf_percent * 100f)
		{
			cloverDataFormat.element = 1;
		}
		if (fourLeafFlag)
		{
			cloverDataFormat.element = 1;
		}
		int element = cloverDataFormat.element;
		if (element != 0)
		{
			if (element == 1)
			{
				cloverDataFormat.spriteNum = UnityEngine.Random.Range(0, this.fourCloverSprite.Length);
				gameObject.GetComponent<SpriteRenderer>().sprite = this.fourCloverSprite[cloverDataFormat.spriteNum];
			}
		}
		else
		{
			cloverDataFormat.spriteNum = UnityEngine.Random.Range(0, this.cloverSprite.Length);
			gameObject.GetComponent<SpriteRenderer>().sprite = this.cloverSprite[cloverDataFormat.spriteNum];
		}
		gameObject.GetComponent<Clover>().SetCloverData(index, cloverDataFormat);
		gameObject.transform.parent = base.transform;
		gameObject.transform.localScale = Vector3.one;
		gameObject.transform.localPosition = vector2;
		return gameObject;
	}

	// Token: 0x06000277 RID: 631 RVA: 0x000095BC File Offset: 0x000079BC
	public void Recycle(int index)
	{
		float num = this.rand_normal((float)this.clover_mu, (float)this.clover_sigma);
		num = Mathf.Clamp(num, (float)this.cloverSpanMin, (float)this.cloverSpanMax);
		this.cloverList[index].lastHarvest = SuperGameMaster.GetLastDateTime();
		this.cloverList[index].timeSpanSec = (int)num;
		this.cloverList[index].newFlag = false;
	}

	// Token: 0x06000278 RID: 632 RVA: 0x0000962E File Offset: 0x00007A2E
	public void SaveClover()
	{
		SuperGameMaster.SaveCloverList(this.cloverList);
	}

	// Token: 0x06000279 RID: 633 RVA: 0x0000963C File Offset: 0x00007A3C
	public float rand_normal(float mu, float sigma)
	{
		float value = UnityEngine.Random.value;
		float value2 = UnityEngine.Random.value;
		float num = Mathf.Sqrt(-2f * Mathf.Log(value)) * Mathf.Cos(6.28318548f * value2);
		return num * sigma + mu;
	}

	// Token: 0x04000121 RID: 289
	public GameObject basePrefab;

	// Token: 0x04000122 RID: 290
	public Sprite[] cloverSprite = new Sprite[1];

	// Token: 0x04000123 RID: 291
	public Sprite[] fourCloverSprite = new Sprite[1];

	// Token: 0x04000124 RID: 292
	[Space(10f)]
	public GameObject ConfilmUI;

	// Token: 0x04000125 RID: 293
	public GameObject AddConfirm_pref;

	// Token: 0x04000126 RID: 294
	[Space(10f)]
	public int cloverOrderInLayer;

	// Token: 0x04000127 RID: 295
	public float fourLeaf_percent = 1f;

	// Token: 0x04000128 RID: 296
	public List<GameObject> cloversObj;

	// Token: 0x04000129 RID: 297
	private List<CloverDataFormat> cloverList;

	// Token: 0x0400012A RID: 298
	public int cloverMax = 20;

	// Token: 0x0400012B RID: 299
	public int clover_mu = 7200;

	// Token: 0x0400012C RID: 300
	public int clover_sigma = 1800;

	// Token: 0x0400012D RID: 301
	public int cloverSpanMin = 300;

	// Token: 0x0400012E RID: 302
	public int cloverSpanMax = 14400;
}
