using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200006A RID: 106
public class TravelResultPanel : MonoBehaviour
{
	// Token: 0x060003B1 RID: 945 RVA: 0x00016228 File Offset: 0x00014628
	public void CreateResult(bool success, int _evtId, List<int> itemList, int goalNodeId)
	{
		this.evtId = _evtId;
		this.numBase_Clover.createNumObj(itemList[0], this.numBase_Clover.GetComponent<NumObjCreater>().prefub.transform.localPosition, (int)this.numBase_Clover.GetComponent<NumObjCreater>().prefub.transform.GetComponent<RectTransform>().sizeDelta.x);
		this.numBase_Ticket.createNumObj(itemList[1], this.numBase_Ticket.GetComponent<NumObjCreater>().prefub.transform.localPosition, (int)this.numBase_Ticket.GetComponent<NumObjCreater>().prefub.transform.GetComponent<RectTransform>().sizeDelta.x);
		itemList.RemoveAt(0);
		itemList.RemoveAt(0);
		if (!success)
		{
			this.MainUI.SetActive(false);
			this.SubUI.SetActive(true);
			return;
		}
		this.MainUI.SetActive(true);
		this.SubUI.SetActive(false);
		int num = itemList.FindIndex((int val) => val > 10000);
		if (num != -1)
		{
			CollectionDataFormat collectionDataFormat = SuperGameMaster.sDataBase.get_CollectDB_forId(itemList[num] - 10000);
			if (collectionDataFormat != null)
			{
				this.CollectionImage.GetComponent<Image>().sprite = collectionDataFormat.img;
			}
		}
		else
		{
			NodeDataFormat nodeDataFormat = SuperGameMaster.sDataBase.get_NodeDB_forId(goalNodeId);
			NodePrefDataFormat nodePrefDataFormat = SuperGameMaster.sDataBase.get_NodePrefDB_forId(nodeDataFormat.pathId);
			if (nodePrefDataFormat.collectionId == -1)
			{
				this.CollectionResultPanel.SetActive(false);
			}
			else if (SuperGameMaster.FindCollection(nodePrefDataFormat.collectionId))
			{
				CollectionDataFormat collectionDataFormat2 = SuperGameMaster.sDataBase.get_CollectDB_forId(nodePrefDataFormat.collectionId);
				if (collectionDataFormat2 != null)
				{
					this.CollectionImage.GetComponent<Image>().sprite = collectionDataFormat2.img;
					this.CompleteStampImage.SetActive(true);
				}
			}
			else
			{
				this.CollectionImage.GetComponent<Image>().sprite = this.QuestionImg;
				this.CollectionImage.GetComponent<RectTransform>().sizeDelta = new Vector2(this.QuestionImg.rect.width, this.QuestionImg.rect.height);
			}
		}
		for (int i = 0; i < this.SpecialtyMax * 2; i += 2)
		{
			if (i >= itemList.Count)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.itemPref);
				gameObject.transform.SetParent(this.SpecialtyImagePanel.GetComponent<RectTransform>(), false);
			}
			else
			{
				int num2 = itemList[i];
				int num3 = itemList[i + 1];
				if (num2 >= 10000)
				{
					itemList.RemoveAt(i);
					itemList.RemoveAt(i);
					i -= 2;
				}
				else
				{
					GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.itemPref);
					gameObject2.transform.SetParent(this.SpecialtyImagePanel.GetComponent<RectTransform>(), false);
					ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(num2);
					if (itemDataFormat != null)
					{
						gameObject2.GetComponent<Image>().enabled = true;
						gameObject2.transform.GetChild(0).GetComponentInChildren<Image>().sprite = itemDataFormat.img;
						if (num3 > 1)
						{
							gameObject2.transform.GetChild(1).gameObject.SetActive(true);
							gameObject2.GetComponentInChildren<Text>().text = "x" + num3;
							if (SuperGameMaster.FindItemStock(num2) + num3 > 99)
							{
								gameObject2.GetComponentInChildren<Text>().color = new Color(1f, 0f, 0f);
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x000165DC File Offset: 0x000149DC
	public void PushClose()
	{
		if (this.SubUI.activeSelf)
		{
			this.Controller.GetComponent<ResultPanel>().DeleteTravelResultPanel(this.evtId);
		}
		if (this.MainUI.activeSelf)
		{
			this.SubUI.SetActive(true);
			this.MainUI.SetActive(false);
			SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cursor"]);
		}
	}

	// Token: 0x0400024B RID: 587
	public GameObject Controller;

	// Token: 0x0400024C RID: 588
	public int evtId;

	// Token: 0x0400024D RID: 589
	[Header("【おみやげ画面】")]
	public GameObject MainUI;

	// Token: 0x0400024E RID: 590
	public GameObject CollectionImage;

	// Token: 0x0400024F RID: 591
	public GameObject CollectionResultPanel;

	// Token: 0x04000250 RID: 592
	public GameObject CompleteStampImage;

	// Token: 0x04000251 RID: 593
	public GameObject SpecialtyImagePanel;

	// Token: 0x04000252 RID: 594
	public GameObject itemPref;

	// Token: 0x04000253 RID: 595
	public int SpecialtyMax = 10;

	// Token: 0x04000254 RID: 596
	public Sprite QuestionImg;

	// Token: 0x04000255 RID: 597
	[Header("【報酬画面】")]
	public GameObject SubUI;

	// Token: 0x04000256 RID: 598
	public NumObjCreater numBase_Clover;

	// Token: 0x04000257 RID: 599
	public NumObjCreater numBase_Ticket;
}
