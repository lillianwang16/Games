using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000043 RID: 67
public class Frog : MonoBehaviour
{
	// Token: 0x0600027B RID: 635 RVA: 0x00009688 File Offset: 0x00007A88
	public void CreateCheck()
	{
		if (SuperGameMaster.GetHome())
		{
			base.gameObject.SetActive(true);
			this.pattern = SuperGameMaster.GetFrogMotion();
			if (this.pattern == -1)
			{
				this.pattern = UnityEngine.Random.RandomRange(0, Define.FrogPatternMax);
				SuperGameMaster.SetFrogMotion(this.pattern);
			}
			List<string> list = Define.Frogpattern[this.pattern];
			int num = (43200 - SuperGameMaster.GetRestTime()) / 3600;
			if (num < 0)
			{
				num = 0;
			}
			if (num >= list.Count)
			{
				num = list.Count - 1;
			}
			int num2 = Define.FrogMotionNum[list[num]];
			base.transform.position = Define.FrogMotionPos[num2];
			base.GetComponent<AnmAnimationObj>().SetUpAniAnimation(Define.FrogMotionName[num2]);
			base.GetComponent<BoxCollider2D>().offset = Define.FrogHitOffset[num2];
			base.GetComponent<BoxCollider2D>().size = Define.FrogHitSize[num2];
			this.CandleObj.SetActive(true);
			this.CandleObj.GetComponent<AnmAnimationObj>().SetUpAniAnimation();
			this.CandleObj_2.SetActive(true);
			this.CandleObj_2.GetComponent<AnmAnimationObj>().SetUpAniAnimation();
			this.Bousi.SetActive(true);
			this.Bousi.GetComponent<AnmAnimationObj>().SetUpAniAnimation();
			Debug.Log(string.Concat(new object[]
			{
				"pattern = ",
				this.pattern,
				" / span = ",
				num,
				" [",
				num2,
				"]"
			}));
		}
		else
		{
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600027C RID: 636 RVA: 0x00009843 File Offset: 0x00007C43
	public void OpenFrogStateUI()
	{
		this.FrogStateUI.GetComponent<FrogStatePanel>().OpenPanel();
	}

	// Token: 0x0400012F RID: 303
	public GameObject FrogStateUI;

	// Token: 0x04000130 RID: 304
	private int pattern;

	// Token: 0x04000131 RID: 305
	private int motionNum;

	// Token: 0x04000132 RID: 306
	public GameObject CandleObj;

	// Token: 0x04000133 RID: 307
	public GameObject CandleObj_2;

	// Token: 0x04000134 RID: 308
	public GameObject Bousi;
}
