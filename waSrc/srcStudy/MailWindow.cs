using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000059 RID: 89
public class MailWindow : MonoBehaviour
{
	// Token: 0x0600031B RID: 795 RVA: 0x0000EEDC File Offset: 0x0000D2DC
	public void OpenWindow(int _mailId)
	{
		this.mailId = _mailId;
		this.mailIndex = SuperGameMaster.saveData.MailList.FindIndex((MailEventFormat mail) => mail.mailId.Equals(this.mailId));
		if (this.mailIndex == -1)
		{
			return;
		}
		MailEventFormat mailEventFormat = SuperGameMaster.saveData.MailList[this.mailIndex];
		base.gameObject.SetActive(true);
		this.titleText.transform.GetComponentInChildren<Text>().text = mailEventFormat.title;
		this.mainText.transform.GetComponentInChildren<Text>().text = mailEventFormat.message;
		if (!mailEventFormat.opened)
		{
			SuperGameMaster.saveData.MailList[this.mailIndex].opened = true;
			SuperGameMaster.getCloverPoint(mailEventFormat.CloverPoint);
			SuperGameMaster.GetTicket(mailEventFormat.ticket);
			if (mailEventFormat.itemId != -1)
			{
				SuperGameMaster.GetItem(mailEventFormat.itemId, mailEventFormat.itemStock);
			}
		}
		this.DeleteButton.GetComponent<Button>().interactable = !mailEventFormat.protect;
	}

	// Token: 0x0600031C RID: 796 RVA: 0x0000EFE8 File Offset: 0x0000D3E8
	public void PushCloseWindow()
	{
		base.gameObject.SetActive(false);
		base.GetComponentInParent<MailScrollView>().CloseMailEvt();
	}

	// Token: 0x0600031D RID: 797 RVA: 0x0000F001 File Offset: 0x0000D401
	public void PushDeleteButton()
	{
		base.gameObject.SetActive(false);
		SuperGameMaster.saveData.MailList.RemoveAt(this.mailIndex);
		base.GetComponentInParent<MailScrollView>().DeleteMailButton(this.mailId);
		base.GetComponentInParent<MailScrollView>().CloseMailEvt();
	}

	// Token: 0x0600031E RID: 798 RVA: 0x0000F040 File Offset: 0x0000D440
	public void PushProtectButton()
	{
		SuperGameMaster.saveData.MailList[this.mailIndex].protect = !SuperGameMaster.saveData.MailList[this.mailIndex].protect;
		this.DeleteButton.GetComponent<Button>().interactable = !SuperGameMaster.saveData.MailList[this.mailIndex].protect;
	}

	// Token: 0x040001B2 RID: 434
	public GameObject titleText;

	// Token: 0x040001B3 RID: 435
	public GameObject mainText;

	// Token: 0x040001B4 RID: 436
	public GameObject DeleteButton;

	// Token: 0x040001B5 RID: 437
	public GameObject ProtectButton;

	// Token: 0x040001B6 RID: 438
	public int mailId;

	// Token: 0x040001B7 RID: 439
	public int mailIndex;
}
