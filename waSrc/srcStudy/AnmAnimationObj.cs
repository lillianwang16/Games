using System;
using UnityEngine;

// Token: 0x0200003E RID: 62
public class AnmAnimationObj : MonoBehaviour
{
	// Token: 0x0600025A RID: 602 RVA: 0x00007C02 File Offset: 0x00006002
	public void SetUpAniAnimation(string _aniName)
	{
		this.aniName = _aniName;
		this.SetUpAniAnimation();
	}

	// Token: 0x0600025B RID: 603 RVA: 0x00007C14 File Offset: 0x00006014
	public void SetUpAniAnimation()
	{
		GameObject gameObject = AnmAnimation.CreateGameObject("aniObj", this.aniName, this.aniName);
		gameObject.GetComponent<AnmAnimation>().SetAction(this.act);
		gameObject.transform.SetParent(base.transform, false);
	}

	// Token: 0x0400010E RID: 270
	[SerializeField]
	private string aniName = "AniAnimation/";

	// Token: 0x0400010F RID: 271
	[SerializeField]
	private int act;
}
