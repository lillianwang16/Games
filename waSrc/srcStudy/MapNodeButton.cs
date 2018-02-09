using System;
using Node;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000039 RID: 57
public class MapNodeButton : MonoBehaviour
{
	// Token: 0x0600023E RID: 574 RVA: 0x0000774C File Offset: 0x00005B4C
	public void CngIdText(int id)
	{
		this.nodeId = id;
		id %= 1000;
		this.IdText.GetComponent<Text>().text = id.ToString();
	}

	// Token: 0x0600023F RID: 575 RVA: 0x0000777B File Offset: 0x00005B7B
	public int getNodeId()
	{
		return this.nodeId;
	}

	// Token: 0x06000240 RID: 576 RVA: 0x00007784 File Offset: 0x00005B84
	public void CngColor(NodeType area)
	{
		switch (area + 1)
		{
		case NodeType.NONE:
			base.GetComponent<Image>().color = new Color(1f, 0.7f, 0.7f);
			break;
		case NodeType.Goal:
			base.GetComponent<Image>().color = new Color(1f, 1f, 1f);
			break;
		case NodeType.Path:
			base.GetComponent<Image>().color = new Color(0.7f, 0.7f, 1f);
			break;
		case NodeType.Detour:
			base.GetComponent<Image>().color = new Color(1f, 1f, 0.7f);
			break;
		case NodeType._NodeTypeMax:
			base.GetComponent<Image>().color = new Color(1f, 0.7f, 1f);
			break;
		}
	}

	// Token: 0x06000241 RID: 577 RVA: 0x00007866 File Offset: 0x00005C66
	public void PushNode()
	{
		base.GetComponentInParent<MapPanel>().setNode(this.nodeId);
	}

	// Token: 0x06000242 RID: 578 RVA: 0x0000787C File Offset: 0x00005C7C
	public void ConnectMark(bool L, bool U, bool R, bool D)
	{
		if (L)
		{
			this.L_Text.SetActive(true);
		}
		if (U)
		{
			this.U_Text.SetActive(true);
		}
		if (R)
		{
			this.R_Text.SetActive(true);
		}
		if (D)
		{
			this.D_Text.SetActive(true);
		}
	}

	// Token: 0x06000243 RID: 579 RVA: 0x000078D4 File Offset: 0x00005CD4
	public void CngColorText(int colorNum)
	{
		if (colorNum == 0)
		{
			this.IdText.GetComponent<Text>().fontSize = 12;
			this.IdText.GetComponent<Text>().color = new Color(1f, 0f, 0f);
		}
		else if (colorNum == 1)
		{
			this.IdText.GetComponent<Text>().fontSize = 14;
			this.IdText.GetComponent<Text>().color = new Color(0f, 0.5f, 0f);
		}
		else
		{
			this.IdText.GetComponent<Text>().fontSize = 10;
			this.IdText.GetComponent<Text>().color = new Color(0f, 0f, 0f);
		}
	}

	// Token: 0x040000F6 RID: 246
	public GameObject IdText;

	// Token: 0x040000F7 RID: 247
	public GameObject L_Text;

	// Token: 0x040000F8 RID: 248
	public GameObject U_Text;

	// Token: 0x040000F9 RID: 249
	public GameObject R_Text;

	// Token: 0x040000FA RID: 250
	public GameObject D_Text;

	// Token: 0x040000FB RID: 251
	public int nodeId;
}
