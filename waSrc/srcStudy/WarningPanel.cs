using System;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006B RID: 107
public class WarningPanel : MonoBehaviour
{
	// Token: 0x060003B5 RID: 949 RVA: 0x00016662 File Offset: 0x00014A62
	public void enableWarningPanel()
	{
		base.gameObject.SetActive(true);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x000166A1 File Offset: 0x00014AA1
	public void disableWarningPanel()
	{
		base.gameObject.SetActive(false);
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x000166E0 File Offset: 0x00014AE0
	public void setWarningText(string changeWarningText)
	{
		this.warningText.GetComponent<Text>().text = changeWarningText;
	}

	// Token: 0x04000259 RID: 601
	public GameObject warningText;
}
