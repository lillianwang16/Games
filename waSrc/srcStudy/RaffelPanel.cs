using System;
using Flag;
using Prize;
using UnityEngine;

// Token: 0x02000062 RID: 98
public class RaffelPanel : MonoBehaviour
{
	// Token: 0x06000379 RID: 889 RVA: 0x000140E8 File Offset: 0x000124E8
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
	}

	// Token: 0x0600037A RID: 890 RVA: 0x00014104 File Offset: 0x00012504
	public void Init()
	{
		Vector2 v = this.FooterMainPanel.transform.localPosition;
		float num = Camera.main.orthographicSize / 4.8f;
		v.y -= (num * 960f - 960f) / 4.45f;
		this.FooterMainPanel.transform.localPosition = v;
	}

	// Token: 0x0600037B RID: 891 RVA: 0x00014170 File Offset: 0x00012570
	public void PushRollButton()
	{
		if (SuperGameMaster.TicketStock() < 5)
		{
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel("ふくびき券が足りません");
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
			return;
		}
		SuperGameMaster.GetTicket(-5);
		SuperGameMaster.set_FlagAdd(Flag.Type.ROLL_NUM, 1);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.LotteryCheck();
		this.ResultButton.GetComponent<RollResultButton>().CngImage((int)this.result);
		this.ResultButton.GetComponent<RollResultButton>().CngResultText(Define.PrizeBallName[this.result] + "がでました");
		this.LotteryWheelPanel.GetComponent<LotteryWheelPanel>().OpenPanel(this.result);
		SuperGameMaster.SetTmpRaffleResult((int)this.result);
		SuperGameMaster.SaveData();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Raffle"]);
		this.BackFunc();
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0001429C File Offset: 0x0001269C
	public void LotteryCheck()
	{
		int num = UnityEngine.Random.Range(0, Define.PrizeBalls[Rank.RankMax]);
		this.result = Rank.White;
		int i = 0;
		int num2 = 0;
		while (i < 5)
		{
			num2 += Define.PrizeBalls[(Rank)i];
			if (num < num2)
			{
				this.result = (Rank)i;
				break;
			}
			i++;
		}
	}

	// Token: 0x0600037D RID: 893 RVA: 0x000142F8 File Offset: 0x000126F8
	public void OpenResultButton()
	{
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.ResultButton.SetActive(true);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_RaffleResult"]);
	}

	// Token: 0x0600037E RID: 894 RVA: 0x0001434F File Offset: 0x0001274F
	public void SetTmpResult()
	{
		this.result = (Rank)SuperGameMaster.GetTmpRaffleResult();
		this.BackFunc();
	}

	// Token: 0x0600037F RID: 895 RVA: 0x00014364 File Offset: 0x00012764
	public void CloseResultButton()
	{
		this.ResultButton.SetActive(false);
		if (this.result == Rank.NONE)
		{
			base.GetComponentInParent<UIMaster>().freezeObject(false);
			base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
			return;
		}
		if (this.result != Rank.White)
		{
			this.PrizeScrollViewUI.GetComponent<PrizeScrollView>().OpenScrollView(this.result);
		}
		else
		{
			PrizeDataFormat prizeDataFormat = SuperGameMaster.sDataBase.get_PrizeDB_forId(Define.PRIZE_WHITE_ID);
			SuperGameMaster.GetTicket(prizeDataFormat.stock);
			SuperGameMaster.SetTmpRaffleResult(-1);
			base.GetComponentInParent<UIMaster>().OnSave();
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel(string.Empty);
			confilm.AddContents(UnityEngine.Object.Instantiate<GameObject>(this.AddConfirm_pref));
			confilm.ResetOnClick_Screen();
			confilm.SetOnClick_Screen(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.GetComponentInParent<UIMaster>().freezeObject(false);
			});
			confilm.SetOnClick_Screen(delegate
			{
				this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
			});
			base.GetComponentInParent<UIMaster_Raffle>().BackFunc();
		}
	}

	// Token: 0x04000207 RID: 519
	public GameObject LotteryWheelObj;

	// Token: 0x04000208 RID: 520
	public GameObject LotteryWheelPanel;

	// Token: 0x04000209 RID: 521
	public GameObject ResultButton;

	// Token: 0x0400020A RID: 522
	public GameObject PrizeScrollViewUI;

	// Token: 0x0400020B RID: 523
	public GameObject ConfilmUI;

	// Token: 0x0400020C RID: 524
	public GameObject AddConfirm_pref;

	// Token: 0x0400020D RID: 525
	public GameObject FooterMainPanel;

	// Token: 0x0400020E RID: 526
	public Rank result;
}
