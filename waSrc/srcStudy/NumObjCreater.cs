using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000E0 RID: 224
public class NumObjCreater : MonoBehaviour
{
	// Token: 0x0600060C RID: 1548 RVA: 0x00024604 File Offset: 0x00022A04
	public void createNumObj(int value, Vector3 drawPos, int numWidth)
	{
		if (this.obj != null)
		{
			foreach (GameObject gameObject in this.obj)
			{
				UnityEngine.Object.Destroy(gameObject);
			}
		}
		if (value > this.numMax)
		{
			value = this.numMax;
		}
		int length = value.ToString().Length;
		this.obj = new GameObject[length];
		for (int j = 0; j < this.obj.Length; j++)
		{
			int num = value % 10;
			value /= 10;
			this.obj[j] = UnityEngine.Object.Instantiate<GameObject>(this.prefub, base.transform.position, Quaternion.identity);
			this.obj[j].GetComponent<Image>().sprite = this.numSprites[num];
			this.obj[j].transform.SetParent(base.transform);
			this.obj[j].transform.localScale = Vector3.one;
			this.obj[j].transform.localPosition = drawPos;
			drawPos.x -= (float)numWidth;
		}
	}

	// Token: 0x0400053C RID: 1340
	public int numMax = 99999;

	// Token: 0x0400053D RID: 1341
	public GameObject prefub;

	// Token: 0x0400053E RID: 1342
	public Sprite[] numSprites;

	// Token: 0x0400053F RID: 1343
	private GameObject[] obj;
}
