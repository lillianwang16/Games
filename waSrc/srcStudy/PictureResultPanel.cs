using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000060 RID: 96
public class PictureResultPanel : MonoBehaviour
{
	// Token: 0x0600036E RID: 878 RVA: 0x00013A1C File Offset: 0x00011E1C
	public void CreateResult(Texture2D _tex, int _evtId, DateTime _dateTime, Sprite _socialImg)
	{
		this.tex = _tex;
		this.evtId = _evtId;
		this.evtDate = _dateTime;
		this.SocialBtn.GetComponent<Image>().sprite = _socialImg;
		this.sprite = Sprite.Create(this.tex, new Rect(0f, 0f, 500f, 350f), Vector2.zero);
		this.PictureImage.GetComponent<Image>().sprite = this.sprite;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x00013A95 File Offset: 0x00011E95
	public void PushClose()
	{
		this.Controller.GetComponent<ResultPanel>().CheckExit_PictureResult();
	}

	// Token: 0x06000370 RID: 880 RVA: 0x00013AA8 File Offset: 0x00011EA8
	public void PushSave()
	{
		this.Controller.GetComponent<FlickCheaker>().stopFlick(true);
		ConfilmPanel confilm;
		if (SuperGameMaster.GetPictureListCount(true) >= 60)
		{
			confilm = this.Controller.GetComponent<ResultPanel>().ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel_YesNo("写真がいっぱいです\n入れ替える写真を選択してください");
			confilm.ResetOnClick_Yes();
			confilm.SetOnClick_Yes(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Yes(delegate
			{
				SuperGameMaster.picture.ChangePictureData(this.evtId, this.evtDate);
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.GetComponentInParent<UIMaster>().changeScene(Scenes.Album);
			});
			confilm.ResetOnClick_No();
			confilm.SetOnClick_No(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_No(delegate
			{
				this.Controller.GetComponent<FlickCheaker>().stopFlick(false);
			});
			return;
		}
		SuperGameMaster.SavePictureList(true, this.tex, this.evtDate);
		this.Controller.GetComponentInParent<UIMaster>().OnSave();
		confilm = this.Controller.GetComponent<ResultPanel>().ConfilmUI.GetComponent<ConfilmPanel>();
		confilm.OpenPanel("写真を保存しました");
		confilm.ResetOnClick_Screen();
		confilm.SetOnClick_Screen(delegate
		{
			confilm.ClosePanel();
		});
		confilm.SetOnClick_Screen(delegate
		{
			this.Controller.GetComponent<ResultPanel>().DeletePictureResultPanel(this.evtId);
		});
		confilm.SetOnClick_Screen(delegate
		{
			this.Controller.GetComponent<FlickCheaker>().stopFlick(false);
		});
	}

	// Token: 0x06000371 RID: 881 RVA: 0x00013C34 File Offset: 0x00012034
	public void PushSocial()
	{
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
		Scenes nowScenes = SuperGameMaster.GetNowScenes();
		if (nowScenes != Scenes.MainOut)
		{
			if (nowScenes == Scenes.MainIn)
			{
				this.Controller.GetComponentInParent<UIMaster_MainIn>().GameMaster.GetComponent<GameMaster_MainIn>().SetReloadTimer(2f);
			}
		}
		else
		{
			this.Controller.GetComponentInParent<UIMaster_MainOut>().GameMaster.GetComponent<GameMaster_MainOut>().SetReloadTimer(2f);
		}
		base.StartCoroutine(base.GetComponent<SocialSender>().SendSocial("#旅かえる", string.Empty, this.tex, this.Controller.GetComponentInParent<UIMaster>()));
	}

	// Token: 0x040001FC RID: 508
	public GameObject Controller;

	// Token: 0x040001FD RID: 509
	public Texture2D tex;

	// Token: 0x040001FE RID: 510
	public Sprite sprite;

	// Token: 0x040001FF RID: 511
	public Image PictureImage;

	// Token: 0x04000200 RID: 512
	public GameObject DateText;

	// Token: 0x04000201 RID: 513
	public GameObject SocialBtn;

	// Token: 0x04000202 RID: 514
	public int evtId;

	// Token: 0x04000203 RID: 515
	public DateTime evtDate;
}
