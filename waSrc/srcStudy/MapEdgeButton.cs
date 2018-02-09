using System;
using Node;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000038 RID: 56
public class MapEdgeButton : MonoBehaviour
{
	// Token: 0x0600023A RID: 570 RVA: 0x00007653 File Offset: 0x00005A53
	public void CngIdText(int id)
	{
		this.edgeId = id;
		id %= 1000;
		this.IdText.GetComponent<Text>().text = id.ToString();
	}

	// Token: 0x0600023B RID: 571 RVA: 0x00007682 File Offset: 0x00005A82
	public int getEdgeId()
	{
		return this.edgeId;
	}

	// Token: 0x0600023C RID: 572 RVA: 0x0000768C File Offset: 0x00005A8C
	public void CngColor(WayType way)
	{
		switch (way)
		{
		case WayType.NONE:
			base.GetComponent<Image>().color = new Color(1f, 1f, 1f);
			break;
		case WayType.Mountain:
			base.GetComponent<Image>().color = new Color(0.5f, 1f, 0.5f);
			break;
		case WayType.Sea:
			base.GetComponent<Image>().color = new Color(0.5f, 0.5f, 1f);
			break;
		case WayType.Cave:
			base.GetComponent<Image>().color = new Color(1f, 1f, 0.5f);
			break;
		}
	}

	// Token: 0x040000F4 RID: 244
	public GameObject IdText;

	// Token: 0x040000F5 RID: 245
	public int edgeId;
}
