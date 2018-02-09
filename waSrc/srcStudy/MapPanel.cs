using System;
using System.Collections;
using System.Collections.Generic;
using Item;
using Node;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200005A RID: 90
public class MapPanel : MonoBehaviour
{
	// Token: 0x06000321 RID: 801 RVA: 0x0000F0CC File Offset: 0x0000D4CC
	public void Enable()
	{
		base.gameObject.SetActive(true);
		base.transform.parent.GetComponentInParent<UIMaster>().freezeObject(true);
		base.transform.parent.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.goalNodeId = -1;
		this.travelResult = new TravelSimulator.Travel();
		this.eventResult = new List<TravelSimulator.Event>();
		this.pathNode = new List<int>();
		this.pathNodeSelect = false;
		this.areaFixMode = false;
		this.moveMode = TravelSimulator.TestMode.SemiAuto;
		this.PushModeChangeButton();
		this.CreateMap(this.nowArea);
	}

	// Token: 0x06000322 RID: 802 RVA: 0x0000F17C File Offset: 0x0000D57C
	public void Disable()
	{
		base.gameObject.SetActive(false);
		base.transform.parent.GetComponentInParent<UIMaster>().freezeObject(false);
		base.transform.parent.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		this.DeleteMap();
	}

	// Token: 0x06000323 RID: 803 RVA: 0x0000F1E0 File Offset: 0x0000D5E0
	public void CreateMap(AreaType createArea)
	{
		int id = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex(createArea);
		int num = SuperGameMaster.sDataBase.get_NodeDB_AreaIndex(createArea + 1);
		int i = SuperGameMaster.sDataBase.search_NodeDBIndex_forId(id);
		int num2 = 960;
		int num3 = 0;
		while (i < SuperGameMaster.sDataBase.count_NodeDB())
		{
			NodeDataFormat nodeDataFormat = SuperGameMaster.sDataBase.get_NodeDB(i);
			if (nodeDataFormat.id == num)
			{
				break;
			}
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.nodePrefab);
			gameObject.transform.SetParent(base.gameObject.transform, false);
			gameObject.transform.SetAsFirstSibling();
			gameObject.GetComponent<MapNodeButton>().CngIdText(nodeDataFormat.id);
			gameObject.GetComponent<MapNodeButton>().CngColor(nodeDataFormat.type);
			NodeConnectDataFormat nodeConnectDataFormat = SuperGameMaster.sDataBase.get_NodeConnectDB_forId(nodeDataFormat.id);
			gameObject.transform.localPosition = nodeConnectDataFormat.pos;
			List<NodeEdgeDataFormat> list = new List<NodeEdgeDataFormat>();
			foreach (int id2 in nodeConnectDataFormat.edge)
			{
				list.Add(SuperGameMaster.sDataBase.get_NodeEdgeDB_forId(id2));
			}
			List<NodeConnectDataFormat> list2 = new List<NodeConnectDataFormat>();
			foreach (NodeEdgeDataFormat nodeEdgeDataFormat in list)
			{
				if (nodeEdgeDataFormat.plug[0] != nodeDataFormat.id)
				{
					list2.Add(SuperGameMaster.sDataBase.get_NodeConnectDB_forId(nodeEdgeDataFormat.plug[0]));
				}
				if (nodeEdgeDataFormat.plug[1] != nodeDataFormat.id)
				{
					list2.Add(SuperGameMaster.sDataBase.get_NodeConnectDB_forId(nodeEdgeDataFormat.plug[1]));
				}
			}
			bool[] array = new bool[4];
			foreach (NodeConnectDataFormat nodeConnectDataFormat2 in list2)
			{
				if (nodeConnectDataFormat.pos.x > nodeConnectDataFormat2.pos.x)
				{
					array[0] = true;
				}
				if (nodeConnectDataFormat.pos.y > nodeConnectDataFormat2.pos.y)
				{
					array[1] = true;
				}
				if (nodeConnectDataFormat.pos.x < nodeConnectDataFormat2.pos.x)
				{
					array[2] = true;
				}
				if (nodeConnectDataFormat.pos.y < nodeConnectDataFormat2.pos.y)
				{
					array[3] = true;
				}
			}
			gameObject.GetComponent<MapNodeButton>().ConnectMark(array[0], array[1], array[2], array[3]);
			if ((float)num2 > nodeConnectDataFormat.pos.y)
			{
				num2 = (int)nodeConnectDataFormat.pos.y;
			}
			if ((float)num3 < nodeConnectDataFormat.pos.y)
			{
				num3 = (int)nodeConnectDataFormat.pos.y;
			}
			i++;
		}
		if (num3 + num2 < 960)
		{
			num3 += num2;
		}
		Vector2 sizeDelta = base.transform.GetComponent<RectTransform>().sizeDelta;
		sizeDelta.y = (float)num3;
		base.transform.GetComponent<RectTransform>().sizeDelta = sizeDelta;
		IEnumerator enumerator3 = base.transform.GetEnumerator();
		try
		{
			while (enumerator3.MoveNext())
			{
				object obj = enumerator3.Current;
				Transform transform = (Transform)obj;
				if (transform.name.Equals("MapNodeButton(Clone)"))
				{
					Vector2 v = transform.transform.localPosition;
					v.x -= sizeDelta.x / 2f;
					v.y += sizeDelta.y / 2f;
					v.y = (float)num3 - v.y;
					transform.transform.localPosition = v;
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator3 as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		for (i = SuperGameMaster.sDataBase.search_NodeEdgeDBIndex_forId(id); i < SuperGameMaster.sDataBase.count_NodeEdgeDB(); i++)
		{
			NodeEdgeDataFormat nodeEdgeDataFormat2 = SuperGameMaster.sDataBase.get_NodeEdgeDB(i);
			if (nodeEdgeDataFormat2.id == num)
			{
				break;
			}
			GameObject gameObject2 = UnityEngine.Object.Instantiate<GameObject>(this.edgePrefab);
			gameObject2.transform.SetParent(base.gameObject.transform, false);
			gameObject2.transform.SetAsFirstSibling();
			gameObject2.GetComponent<MapEdgeButton>().CngIdText(nodeEdgeDataFormat2.id);
			gameObject2.GetComponent<MapEdgeButton>().CngColor(nodeEdgeDataFormat2.wayType);
			Vector2[] array2 = new Vector2[2];
			IEnumerator enumerator4 = base.transform.GetEnumerator();
			try
			{
				while (enumerator4.MoveNext())
				{
					object obj2 = enumerator4.Current;
					Transform transform2 = (Transform)obj2;
					if (transform2.name.Equals("MapNodeButton(Clone)"))
					{
						for (int k = 0; k < 2; k++)
						{
							if (nodeEdgeDataFormat2.plug[k] == transform2.GetComponent<MapNodeButton>().getNodeId())
							{
								array2[k] = transform2.transform.localPosition;
							}
						}
					}
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator4 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
			Vector2 sizeDelta2 = new Vector2(15f, 15f);
			Vector2 v2 = array2[0];
			if (array2[0].x != array2[1].x)
			{
				sizeDelta2.x = Mathf.Abs(array2[1].x - array2[0].x);
				v2.x = array2[0].x + (array2[1].x - array2[0].x) / 2f;
			}
			if (array2[0].y != array2[1].y)
			{
				sizeDelta2.y = Mathf.Abs(array2[1].y - array2[0].y);
				v2.y = array2[0].y + (array2[1].y - array2[0].y) / 2f;
			}
			gameObject2.transform.localPosition = v2;
			gameObject2.GetComponent<RectTransform>().sizeDelta = sizeDelta2;
		}
	}

	// Token: 0x06000324 RID: 804 RVA: 0x0000F8A8 File Offset: 0x0000DCA8
	public void DeleteMap()
	{
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.name.Equals("MapNodeButton(Clone)") || transform.name.Equals("MapEdgeButton(Clone)"))
				{
					UnityEngine.Object.Destroy(transform.gameObject);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
	}

	// Token: 0x06000325 RID: 805 RVA: 0x0000F93C File Offset: 0x0000DD3C
	public void TestTravel()
	{
		if (this.goalNodeId == -1 && this.moveMode != TravelSimulator.TestMode.Auto)
		{
			this.setResultText("[Error] 目的地が設定されていません");
			return;
		}
		if (this.pathNodeSelect)
		{
			this.setResultText("[Error] 経由ノード選択中です。\n終了するには再度「経由」ボタンを押してください");
			return;
		}
		IEnumerator enumerator = base.transform.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				object obj = enumerator.Current;
				Transform transform = (Transform)obj;
				if (transform.name.Equals("MapNodeButton(Clone)"))
				{
					transform.GetComponent<MapNodeButton>().CngColorText(-1);
				}
			}
		}
		finally
		{
			IDisposable disposable;
			if ((disposable = (enumerator as IDisposable)) != null)
			{
				disposable.Dispose();
			}
		}
		if (this.moveMode == TravelSimulator.TestMode.Auto)
		{
			if (this.areaFixMode)
			{
				this.goalNodeId = base.GetComponent<TravelSimulator>().getGoal(this.nowArea);
			}
			else
			{
				this.goalNodeId = base.GetComponent<TravelSimulator>().getGoal();
			}
		}
		int start = base.GetComponent<TravelSimulator>().getStart(this.goalNodeId);
		AreaType areaType = SuperGameMaster.sDataBase.get_NodeDB_AreaType(start);
		this.travelResult = base.GetComponent<TravelSimulator>().TestTravel(this.moveMode, areaType, start, this.goalNodeId, this.pathNode);
		if (areaType != this.nowArea)
		{
			this.AreaCng(areaType);
		}
		this.pathNode = this.travelResult.pathNode;
		this.setResultText(this.travelResult);
		this.setTitleText();
		IEnumerator enumerator2 = base.transform.GetEnumerator();
		try
		{
			while (enumerator2.MoveNext())
			{
				Transform child = (Transform)enumerator2.Current;
				if (child.name.Equals("MapNodeButton(Clone)"))
				{
					if (this.travelResult.route.FindIndex((int node) => node.Equals(child.GetComponent<MapNodeButton>().getNodeId())) != -1)
					{
						child.GetComponent<MapNodeButton>().CngColorText(0);
					}
					if (this.pathNode.FindIndex((int node) => node.Equals(child.GetComponent<MapNodeButton>().getNodeId())) != -1)
					{
						child.GetComponent<MapNodeButton>().CngColorText(1);
					}
				}
			}
		}
		finally
		{
			IDisposable disposable2;
			if ((disposable2 = (enumerator2 as IDisposable)) != null)
			{
				disposable2.Dispose();
			}
		}
	}

	// Token: 0x06000326 RID: 806 RVA: 0x0000FB98 File Offset: 0x0000DF98
	public void PushPathNodeButton()
	{
		if (this.moveMode != TravelSimulator.TestMode.Manual)
		{
			this.setResultText("「手動設定」のみ、経由地点の設定が可能です。");
			return;
		}
		this.pathNodeSelect = !this.pathNodeSelect;
		if (this.pathNodeSelect)
		{
			this.pathNode = new List<int>();
			this.setResultText("...経由地点選択中...\n終了するには再度「経由」ボタンを押してください");
			IEnumerator enumerator = base.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					if (transform.name.Equals("MapNodeButton(Clone)"))
					{
						transform.GetComponent<MapNodeButton>().CngColorText(-1);
					}
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}
		else
		{
			this.setTitleText();
			this.setResultText("経由地点の入力が完了しました：" + this.pathNode.Count);
			IEnumerator enumerator2 = base.transform.GetEnumerator();
			try
			{
				while (enumerator2.MoveNext())
				{
					Transform child = (Transform)enumerator2.Current;
					if (child.name.Equals("MapNodeButton(Clone)") && this.pathNode.FindIndex((int node) => node.Equals(child.GetComponent<MapNodeButton>().getNodeId())) != -1)
					{
						child.GetComponent<MapNodeButton>().CngColorText(1);
					}
				}
			}
			finally
			{
				IDisposable disposable2;
				if ((disposable2 = (enumerator2 as IDisposable)) != null)
				{
					disposable2.Dispose();
				}
			}
		}
	}

	// Token: 0x06000327 RID: 807 RVA: 0x0000FD30 File Offset: 0x0000E130
	public void PushModeChangeButton()
	{
		if (this.pathNodeSelect)
		{
			this.setResultText("経路選択中のモード変更は不可能です。\n終了するには再度「経由」ボタンを押してください");
			return;
		}
		this.moveMode++;
		if (this.moveMode == TravelSimulator.TestMode.MODE_MAX)
		{
			this.moveMode = TravelSimulator.TestMode.Manual;
		}
		TravelSimulator.TestMode testMode = this.moveMode;
		if (testMode != TravelSimulator.TestMode.Manual)
		{
			if (testMode != TravelSimulator.TestMode.SemiAuto)
			{
				if (testMode == TravelSimulator.TestMode.Auto)
				{
					this.setResultText("「自動探索」モードに切り替わりました。\n現状の装備品から、目的地が決定されます。");
				}
			}
			else
			{
				this.setResultText("「半自動探索」モードに切り替わりました。\n目的地になるノードを選択して下さい。");
			}
		}
		else
		{
			this.setResultText("「手動設定」モードに切り替わりました。\n目的地になるノードと、必要な経由地点を選択して下さい。");
		}
		this.pathNode = new List<int>();
		this.setTitleText();
		TravelSimulator.TestMode testMode2 = this.moveMode;
		if (testMode2 != TravelSimulator.TestMode.Manual)
		{
			if (testMode2 != TravelSimulator.TestMode.SemiAuto)
			{
				if (testMode2 == TravelSimulator.TestMode.Auto)
				{
					this.cngModeBtnText.GetComponent<Text>().text = "自動散策";
				}
			}
			else
			{
				this.cngModeBtnText.GetComponent<Text>().text = "半自動散策";
			}
		}
		else
		{
			this.cngModeBtnText.GetComponent<Text>().text = "手動設定";
		}
		if (this.moveMode == TravelSimulator.TestMode.Manual)
		{
			this.pathBtnText.GetComponent<Text>().text = "経由";
		}
		else
		{
			this.pathBtnText.GetComponent<Text>().text = "-";
		}
		if (this.moveMode == TravelSimulator.TestMode.Auto)
		{
			this.areaFixMode = false;
			this.areaFixBtnText.GetComponent<Text>().text = "全域";
		}
		else
		{
			this.areaFixMode = true;
			this.areaFixBtnText.GetComponent<Text>().text = "-";
		}
	}

	// Token: 0x06000328 RID: 808 RVA: 0x0000FEC8 File Offset: 0x0000E2C8
	public void PushAreaFixButton()
	{
		if (this.moveMode != TravelSimulator.TestMode.Auto)
		{
			this.setResultText("「自動散策」モード以外では、エリアは固定です。");
			return;
		}
		this.areaFixMode = !this.areaFixMode;
		if (this.areaFixMode)
		{
			this.areaFixBtnText.GetComponent<Text>().text = "エリア\n固定";
		}
		else
		{
			this.areaFixBtnText.GetComponent<Text>().text = "全域";
		}
	}

	// Token: 0x06000329 RID: 809 RVA: 0x0000FF38 File Offset: 0x0000E338
	public void PushAreaCngButton()
	{
		if (this.pathNodeSelect)
		{
			this.setResultText("経路選択中のエリア変更は不可能です。\n終了するには再度「経由」ボタンを押してください");
			return;
		}
		this.goalNodeId = -1;
		this.pathNode = new List<int>();
		this.nowArea++;
		if (this.nowArea >= AreaType._AreaTypeMax)
		{
			this.nowArea = AreaType.East;
		}
		this.AreaCng(this.nowArea);
		this.setTitleText();
		this.setResultText("エリアを切り替えました：" + this.nowArea.ToString());
	}

	// Token: 0x0600032A RID: 810 RVA: 0x0000FFC2 File Offset: 0x0000E3C2
	public void AreaCng(AreaType cngArea)
	{
		this.nowArea = cngArea;
		this.DeleteMap();
		this.CreateMap(cngArea);
	}

	// Token: 0x0600032B RID: 811 RVA: 0x0000FFD8 File Offset: 0x0000E3D8
	public void setNode(int node)
	{
		if (!this.pathNodeSelect)
		{
			this.goalNodeId = node;
			this.setTitleText();
			this.getPrefTest();
		}
		else
		{
			this.pathNode.Add(node);
			this.setResultText(this.pathNode);
		}
	}

	// Token: 0x0600032C RID: 812 RVA: 0x00010018 File Offset: 0x0000E418
	public void setTitleText()
	{
		string text = string.Concat(new object[]
		{
			this.nowArea.ToString(),
			" / ",
			SuperGameMaster.sDataBase.get_NodeDB_AreaIndex(this.nowArea),
			" > ",
			this.goalNodeId
		});
		if (this.pathNode.Count != 0)
		{
			string text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				" \n経由：",
				this.pathNode.Count,
				" ["
			});
			foreach (int num in this.pathNode)
			{
				text = text + num.ToString() + " , ";
			}
			text = text.Remove(text.Length - 3, 3);
			text += "]";
		}
		this.MapTitleText.GetComponent<Text>().text = text;
	}

	// Token: 0x0600032D RID: 813 RVA: 0x00010150 File Offset: 0x0000E550
	public void setResultText(TravelSimulator.Travel travel)
	{
		string text = "移動ノード：" + travel.route.Count + " / ";
		if (travel.route.Count != 0 && travel.route[0] % 1000 != 0)
		{
			text += "（テレポート）";
		}
		for (int i = 0; i < travel.route.Count; i++)
		{
			text = text + travel.route[i].ToString() + " ";
			if (i == travel.route.Count - 1)
			{
				break;
			}
			if (travel.edgeType[i] != WayType.NONE)
			{
				WayType wayType = travel.edgeType[i];
				if (wayType != WayType.Mountain)
				{
					if (wayType != WayType.Sea)
					{
						if (wayType == WayType.Cave)
						{
							text += "<color=olive>";
						}
					}
					else
					{
						text += "<color=teal>";
					}
				}
				else
				{
					text += "<color=green>";
				}
				text += "> </color>";
			}
			else
			{
				text += "> ";
			}
		}
		string text2 = string.Empty;
		for (int j = 0; j < travel.route.Count; j++)
		{
			if (j == travel.route.Count - 1)
			{
				break;
			}
			switch (travel.edgeType[j])
			{
			case WayType.NONE:
				text2 += "<color=grey>";
				break;
			case WayType.Mountain:
				text2 += "<color=green>";
				break;
			case WayType.Sea:
				text2 += "<color=teal>";
				break;
			case WayType.Cave:
				text2 += "<color=olive>";
				break;
			}
			string text3 = text2;
			text2 = string.Concat(new object[]
			{
				text3,
				"<i>-",
				travel.edgeRoute[j],
				"- </i> "
			});
			text2 += "</color>";
		}
		this.MapResultText.GetComponent<Text>().text = string.Concat(new object[]
		{
			text,
			" [cost:",
			travel.cost,
			"]\n\n",
			text2
		});
	}

	// Token: 0x0600032E RID: 814 RVA: 0x000103D4 File Offset: 0x0000E7D4
	public void setResultText(List<int> pathNode)
	{
		string text = "経由：[";
		foreach (int num in pathNode)
		{
			text = text + num.ToString() + " , ";
		}
		text = text.Remove(text.Length - 3, 3);
		text += "]\n終了するには再度「経由」ボタンを押してください";
		this.MapResultText.GetComponent<Text>().text = text;
	}

	// Token: 0x0600032F RID: 815 RVA: 0x00010470 File Offset: 0x0000E870
	public void setResultText(string str)
	{
		this.MapResultText.GetComponent<Text>().text = str;
	}

	// Token: 0x06000330 RID: 816 RVA: 0x00010483 File Offset: 0x0000E883
	public void setResultText()
	{
		this.MapResultText.GetComponent<Text>().text = string.Empty;
	}

	// Token: 0x06000331 RID: 817 RVA: 0x0001049C File Offset: 0x0000E89C
	public void getItemTest()
	{
		if (this.pathNodeSelect)
		{
			this.setResultText("経路選択中のモードです。\n終了するには再度「経由」ボタンを押してください");
			return;
		}
		if (this.travelResult.route == null)
		{
			this.setResultText("移動データがありません。");
			return;
		}
		List<ItemListFormat> list = new List<ItemListFormat>();
		list = base.GetComponent<TravelSimulator>().getItem(this.travelResult.route);
		string text = "取得結果：route[";
		foreach (int num in this.travelResult.route)
		{
			text = text + num.ToString() + "-";
		}
		if (this.travelResult.route.Count != 0)
		{
			text = text.Remove(text.Length - 1, 1);
		}
		text += "] ⇒ item[";
		foreach (ItemListFormat itemListFormat in list)
		{
			text += itemListFormat.id.ToString();
			if (itemListFormat.stock != 1)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"(*",
					itemListFormat.stock,
					")"
				});
			}
			text += ",";
		}
		if (list.Count != 0)
		{
			text = text.Remove(text.Length - 1, 1);
		}
		text += "] （";
		foreach (ItemListFormat itemListFormat2 in list)
		{
			if (itemListFormat2.id < 10000)
			{
				if (itemListFormat2.id >= 0)
				{
					ItemDataFormat itemDataFormat = new ItemDataFormat();
					itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(itemListFormat2.id);
					text += itemDataFormat.name;
					if (itemListFormat2.stock != 1)
					{
						string text2 = text;
						text = string.Concat(new object[]
						{
							text2,
							"*",
							itemListFormat2.stock,
							string.Empty
						});
					}
				}
				else
				{
					if (itemListFormat2.id == -1)
					{
						text = text + "みつ葉*" + itemListFormat2.stock;
					}
					if (itemListFormat2.id == -2)
					{
						text = text + "福引き*" + itemListFormat2.stock;
					}
				}
			}
			else if (itemListFormat2.id < 30000)
			{
				int id = itemListFormat2.id - 10000;
				CollectionDataFormat collectionDataFormat = new CollectionDataFormat();
				collectionDataFormat = SuperGameMaster.sDataBase.get_CollectDB_forId(id);
				text = text + "【" + collectionDataFormat.name + "】";
			}
			text += ",";
		}
		if (list.Count != 0)
		{
			text = text.Remove(text.Length - 1, 1);
		}
		text += "）";
		this.setResultText(text);
	}

	// Token: 0x06000332 RID: 818 RVA: 0x0001082C File Offset: 0x0000EC2C
	public void getEvtTest()
	{
		if (this.pathNodeSelect)
		{
			this.EventResultText.GetComponent<Text>().text = "\n経路選択中のモードです。\n終了するには再度「経由」ボタンを押してください";
			return;
		}
		if (this.travelResult.route == null)
		{
			this.EventResultText.GetComponent<Text>().text = "\n旅行データがありません。";
			return;
		}
		this.eventResult = base.GetComponent<TravelSimulator>().getEvt(this.travelResult);
		string text = "\n経路：\n[";
		foreach (int num in this.travelResult.route)
		{
			text = text + num.ToString() + "-";
		}
		if (this.travelResult.route.Count != 0)
		{
			text = text.Remove(text.Length - 1, 1);
		}
		text += "]";
		string text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			" [cost:",
			this.travelResult.cost,
			"]\n\n"
		});
		text += "イベント：\n";
		int num2 = 0;
		string text3 = string.Empty;
		foreach (TravelSimulator.Event @event in this.eventResult)
		{
			num2 += @event.time;
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				num2.ToString(),
				"分(+",
				@event.time,
				")\u3000"
			});
			switch (@event.code)
			{
			case TravelSimulator.EventCode.START:
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"★ 出発（地点：",
					@event.val,
					")"
				});
				break;
			case TravelSimulator.EventCode.GOAL:
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"★ 目的地到着（地点：",
					@event.val,
					")"
				});
				if (text3 != string.Empty)
				{
					text = text + " [" + text3 + "]";
				}
				break;
			case TravelSimulator.EventCode.TimeUp:
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"× 時間切れ（目標地点：",
					@event.val,
					")"
				});
				if (text3 != string.Empty)
				{
					text = text + " [" + text3 + "]";
				}
				break;
			case TravelSimulator.EventCode.Arrival:
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"☆ 到着（地点：",
					@event.val,
					")"
				});
				break;
			case TravelSimulator.EventCode.Rest:
				text += "○ 休息開始";
				break;
			case TravelSimulator.EventCode.RestComplete:
				text += "○ 休息終了";
				break;
			case TravelSimulator.EventCode.Camp:
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"◆ イベント：野宿（地点：",
					@event.val,
					")"
				});
				break;
			case TravelSimulator.EventCode.Shortcut:
			{
				string str = text;
				string str2 = "◆ イベント：近道（";
				WayType val = (WayType)@event.val;
				text = str + str2 + val.ToString() + ")";
				break;
			}
			case TravelSimulator.EventCode.Encount:
			{
				string str3 = text;
				string str4 = "◆ イベント：遭遇（";
				WayType val2 = (WayType)@event.val;
				text = str3 + str4 + val2.ToString() + ")";
				break;
			}
			case TravelSimulator.EventCode.Picture_Normal:
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"■ 写真：通常（道：",
					@event.val,
					")"
				});
				if (text3 != string.Empty)
				{
					text = text + " [" + text3 + "]";
				}
				break;
			case TravelSimulator.EventCode.Picture_Tools:
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"■ 写真：道具（道：",
					@event.val,
					")"
				});
				if (text3 != string.Empty)
				{
					text = text + " [" + text3 + "]";
				}
				break;
			case TravelSimulator.EventCode.Picture_Unique:
				text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"■ 写真：ユニーク（道：",
					@event.val,
					")"
				});
				if (text3 != string.Empty)
				{
					text = text + " [" + text3 + "]";
				}
				break;
			case TravelSimulator.EventCode.Traveler:
				text3 = SuperGameMaster.sDataBase.get_PictureCharaDB_forId(@event.val).name;
				text = text + "▲ 道連れ（キャラ：" + text3 + ")";
				break;
			}
			text += "\n";
		}
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			"\n合計時間：",
			num2 / 60,
			"時間 ",
			num2 % 60,
			"分\n\n"
		});
		this.EventResultText.GetComponent<Text>().text = text;
	}

	// Token: 0x06000333 RID: 819 RVA: 0x00010DE4 File Offset: 0x0000F1E4
	public void getState()
	{
		if (this.pathNodeSelect)
		{
			this.setResultText("経路選択中のモードです。\n終了するには再度「経由」ボタンを押してください");
			return;
		}
		List<TravelSimulator.ItemEffect> list = new List<TravelSimulator.ItemEffect>();
		List<int> list2 = new List<int>(SuperGameMaster.GetBagList());
		foreach (int num in list2)
		{
			if (num != -1)
			{
				ItemDataFormat itemDataFormat = new ItemDataFormat();
				itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(num);
				for (int i = 0; i < itemDataFormat.effectType.Length; i++)
				{
					list.Add(new TravelSimulator.ItemEffect
					{
						type = itemDataFormat.effectType[i],
						elm = itemDataFormat.effectElm[i],
						value = itemDataFormat.effectValue[i]
					});
				}
			}
		}
		int num2 = 100;
		int num3 = 0;
		List<int> list3 = new List<int>();
		for (int j = 0; j < 5; j++)
		{
			list3.Add(0);
		}
		int num4 = 0;
		List<int> list4 = new List<int>();
		for (int k = 0; k < 5; k++)
		{
			list4.Add(0);
		}
		List<int> list5 = new List<int>();
		for (int l = 0; l < 4; l++)
		{
			list5.Add(0);
		}
		int num5 = 0;
		bool flag = false;
		foreach (TravelSimulator.ItemEffect itemEffect in list)
		{
			switch (itemEffect.type)
			{
			case EffectType.HP:
				if (itemEffect.elm == EffectElm.H_UP)
				{
					num2 += itemEffect.value;
				}
				if (itemEffect.elm == EffectElm.H_UP_RND)
				{
					num3 += itemEffect.value;
				}
				break;
			case EffectType.AREA_DECIDE:
			{
				int num6 = itemEffect.elm - EffectElm.A_EAST;
				if (itemEffect.elm == EffectElm.A_NEW_AREA)
				{
					num6 = 4;
				}
				if (itemEffect.value == -2)
				{
					list3[num6] = -2;
				}
				if (itemEffect.value == -1 && list3[num6] != -2)
				{
					list3[num6] = -1;
				}
				if (list3[num6] != -1 && list3[num6] != -2)
				{
					List<int> list6;
					int index;
					(list6 = list3)[index = num6] = list6[index] + itemEffect.value;
				}
				break;
			}
			case EffectType.ITEM_PERCENT:
				if (itemEffect.elm == EffectElm.I_UP)
				{
					num4 += itemEffect.value;
				}
				break;
			case EffectType.WAY_SPEED:
			{
				int num6 = itemEffect.elm - EffectElm.W_MOUNTAIN + 1;
				if (itemEffect.elm == EffectElm.W_NONE)
				{
					num6 = 0;
				}
				if (itemEffect.elm == EffectElm.W_ALL)
				{
					num6 = list4.Count - 1;
				}
				List<int> list6;
				int index2;
				(list6 = list4)[index2 = num6] = list6[index2] + itemEffect.value;
				break;
			}
			case EffectType.EVT_WAY:
				if (itemEffect.value > list5[itemEffect.elm - EffectElm.E_MOUNTAIN])
				{
					list5[itemEffect.elm - EffectElm.E_MOUNTAIN] = itemEffect.value;
				}
				break;
			case EffectType.FRIENDSHIP:
				if (itemEffect.value == -1)
				{
					num5 = -1;
				}
				if (num5 != -1)
				{
					num5 += num5;
				}
				break;
			case EffectType.TELEPORT:
				if (itemEffect.value == 1)
				{
					flag = true;
				}
				break;
			}
		}
		string text = "【装備：";
		foreach (int num7 in list2)
		{
			if (num7 == -1)
			{
				text += "(なし),";
			}
			else
			{
				ItemDataFormat itemDataFormat2 = new ItemDataFormat();
				itemDataFormat2 = SuperGameMaster.sDataBase.get_ItemDB_forId(num7);
				text = text + itemDataFormat2.name + ",";
			}
		}
		text = text.Remove(text.Length - 1, 1);
		text += "】\n";
		string text2;
		if (num3 == 0)
		{
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				" HP：",
				(int)((double)(100 * num2) * 0.01),
				"(",
				num2 - 100,
				"%) "
			});
		}
		else
		{
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				" HP：",
				(int)((double)(100 * num2) * 0.01),
				" ~ ",
				(double)(100 * (num2 + num3)) * 0.01,
				"(+",
				num2 - 100,
				" ~ ",
				num2 - 100 + num3,
				"%) "
			});
		}
		text += " ／ エリア出現率補正(+新)： ";
		for (int m = 0; m < list3.Count; m++)
		{
			if (list3[m] == -1)
			{
				text += "確定1 ";
			}
			else if (list3[m] == -2)
			{
				text += "確定2 ";
			}
			else
			{
				text = text + list3[m] + "% ";
			}
		}
		text2 = text;
		text = string.Concat(new object[]
		{
			text2,
			" ／ アイテム取得：逸品 + ",
			num4,
			"% 名物",
			60 + num4,
			" ( + ",
			num4,
			"%) "
		});
		text += " ／ 移動コスト(+All)： ";
		for (int n = 0; n < list4.Count; n++)
		{
			text = text + list4[n] + "% ";
		}
		text += " ／ グレード： ";
		for (int num8 = 0; num8 < list5.Count; num8++)
		{
			text = text + list5[num8] + ",";
		}
		text = text.Remove(text.Length - 1, 1);
		text += " ／ 遭遇率：（共通）";
		if (num5 == -1)
		{
			text += "確定 ";
		}
		else
		{
			text2 = text;
			text = string.Concat(new object[]
			{
				text2,
				num5,
				"％(",
				num5,
				"%)"
			});
		}
		text = text + " ／ 瞬間移動：" + flag.ToString().Substring(0, 1);
		this.setResultText(text);
	}

	// Token: 0x06000334 RID: 820 RVA: 0x00011578 File Offset: 0x0000F978
	public void getPrefTest()
	{
		if (this.pathNodeSelect)
		{
			return;
		}
		if (this.goalNodeId == -1)
		{
			this.setResultText("ノードが選択されていません");
			return;
		}
		NodeDataFormat nodeDataFormat = SuperGameMaster.sDataBase.get_NodeDB_forId(this.goalNodeId);
		NodeItemDataFormat nodeItemDataFormat = SuperGameMaster.sDataBase.get_NodeItemDB_forId(this.goalNodeId);
		object[] array = new object[6];
		array[0] = "ノード [";
		array[1] = nodeDataFormat.id;
		array[2] = "] Type：";
		int num = 3;
		NodeType type = nodeDataFormat.type;
		array[num] = type.ToString();
		array[4] = " / Path：";
		array[5] = nodeDataFormat.pathId;
		string text = string.Concat(array);
		text += " / ";
		text += "specialty：";
		string text2 = string.Empty;
		string text3 = "[";
		string text4 = "(";
		for (int i = 0; i < nodeItemDataFormat.specialtyId.Length; i++)
		{
			ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(nodeItemDataFormat.specialtyId[i]);
			text2 = text2 + itemDataFormat.id + ",";
			text3 = text3 + nodeItemDataFormat.specialtyPer[i] + "%,";
			text4 = text4 + itemDataFormat.name + ",";
		}
		if (nodeItemDataFormat.specialtyId.Length != 0)
		{
			text2 = text2.Remove(text2.Length - 1, 1);
			text3 = text3.Remove(text3.Length - 1, 1);
			text4 = text4.Remove(text4.Length - 1, 1);
		}
		text2 += " ";
		text3 += "] ";
		text4 += ") ";
		string text5 = text;
		text = string.Concat(new string[]
		{
			text5,
			text2,
			text3,
			text4,
			"\n"
		});
		NodePrefDataFormat nodePrefDataFormat = SuperGameMaster.sDataBase.get_NodePrefDB_forId(this.goalNodeId);
		if (nodePrefDataFormat != null)
		{
			text += "<color=orange>【県データ】";
			text += "ノード [";
			List<int> list_NodeDB_prefIds = SuperGameMaster.sDataBase.getList_NodeDB_prefIds(nodePrefDataFormat.id);
			foreach (int num2 in list_NodeDB_prefIds)
			{
				text = text + num2 + ",";
			}
			text = text.Remove(text.Length - 1, 1);
			text += "]";
			text += " / ";
			if (nodePrefDataFormat.collectionId != -1)
			{
				CollectionDataFormat collectionDataFormat = SuperGameMaster.sDataBase.get_CollectDB_forId(nodePrefDataFormat.collectionId);
				text5 = text;
				text = string.Concat(new object[]
				{
					text5,
					"collect：",
					collectionDataFormat.id,
					" (",
					collectionDataFormat.name,
					") / "
				});
			}
			else
			{
				text += "collect：(なし) / ";
			}
			text += "specialty：";
			text2 = string.Empty;
			text4 = "(";
			for (int j = 0; j < nodePrefDataFormat.specialtyId.Length; j++)
			{
				ItemDataFormat itemDataFormat2 = SuperGameMaster.sDataBase.get_ItemDB_forId(nodePrefDataFormat.specialtyId[j]);
				text2 = text2 + itemDataFormat2.id + ",";
				text4 = text4 + itemDataFormat2.name + ",";
			}
			if (nodeItemDataFormat.specialtyId.Length != 0)
			{
				text2 = text2.Remove(text2.Length - 1, 1);
				text4 = text4.Remove(text4.Length - 1, 1);
			}
			text2 += " ";
			text4 += ") ";
			text = text + text2 + text4 + "\n";
			text += "</color>";
		}
		this.setResultText(text);
	}

	// Token: 0x040001B8 RID: 440
	public AreaType nowArea;

	// Token: 0x040001B9 RID: 441
	public int goalNodeId;

	// Token: 0x040001BA RID: 442
	public TravelSimulator.Travel travelResult;

	// Token: 0x040001BB RID: 443
	public List<TravelSimulator.Event> eventResult;

	// Token: 0x040001BC RID: 444
	private List<int> pathNode;

	// Token: 0x040001BD RID: 445
	private bool pathNodeSelect;

	// Token: 0x040001BE RID: 446
	public TravelSimulator.TestMode moveMode;

	// Token: 0x040001BF RID: 447
	public bool areaFixMode;

	// Token: 0x040001C0 RID: 448
	public GameObject nodePrefab;

	// Token: 0x040001C1 RID: 449
	public GameObject edgePrefab;

	// Token: 0x040001C2 RID: 450
	[Space(10f)]
	public GameObject MapTitleText;

	// Token: 0x040001C3 RID: 451
	public GameObject MapResultText;

	// Token: 0x040001C4 RID: 452
	public GameObject EventResultText;

	// Token: 0x040001C5 RID: 453
	[Space(10f)]
	public GameObject cngModeBtnText;

	// Token: 0x040001C6 RID: 454
	public GameObject pathBtnText;

	// Token: 0x040001C7 RID: 455
	public GameObject areaFixBtnText;

	// Token: 0x040001C8 RID: 456
	public GameObject cngAreaBtnText;
}
