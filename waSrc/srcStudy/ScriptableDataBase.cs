using System;
using System.Collections.Generic;
using Node;
using Picture;
using UnityEngine;

// Token: 0x020000E8 RID: 232
public class ScriptableDataBase : MonoBehaviour
{
	// Token: 0x0600066A RID: 1642 RVA: 0x000281EB File Offset: 0x000265EB
	public void SetUpDataBase()
	{
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x000281ED File Offset: 0x000265ED
	public MailEventFormat get_MailEvtDB(int index)
	{
		return new MailEventFormat(this.MailEvtDB.data[index]);
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x00028205 File Offset: 0x00026605
	public int count_MailEvtDB()
	{
		return this.MailEvtDB.data.Count;
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x00028218 File Offset: 0x00026618
	public MailEventFormat get_MailEvtDB_forId(int id)
	{
		int num = this.search_MailEvtDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_MailEvtDB(num);
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x00028240 File Offset: 0x00026640
	public int search_MailEvtDBIndex_forId(int id)
	{
		return this.MailEvtDB.data.FindIndex((MailEventFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x00028276 File Offset: 0x00026676
	public ShopDataFormat get_ShopDB(int index)
	{
		return new ShopDataFormat(this.ShopDB.data[index]);
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0002828E File Offset: 0x0002668E
	public int count_ShopDB()
	{
		return this.ShopDB.data.Count;
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x000282A0 File Offset: 0x000266A0
	public ShopDataFormat get_ShopDB_forId(int id)
	{
		int num = this.search_ShopDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_ShopDB(num);
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x000282C8 File Offset: 0x000266C8
	public int search_ShopDBIndex_forId(int id)
	{
		return this.ShopDB.data.FindIndex((ShopDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x000282FE File Offset: 0x000266FE
	public CollectionDataFormat get_CollectDB(int index)
	{
		return new CollectionDataFormat(this.CollectDB.data[index]);
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x00028316 File Offset: 0x00026716
	public int count_CollectDB()
	{
		return this.CollectDB.data.Count;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x00028328 File Offset: 0x00026728
	public CollectionDataFormat get_CollectDB_forId(int id)
	{
		int num = this.search_CollectDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_CollectDB(num);
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x00028350 File Offset: 0x00026750
	public int search_CollectDBIndex_forId(int id)
	{
		return this.CollectDB.data.FindIndex((CollectionDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x00028386 File Offset: 0x00026786
	public int get_CollectDB_lastIndexId()
	{
		return this.CollectDB.data[this.CollectDB.data.Count - 1].id;
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x000283AF File Offset: 0x000267AF
	public SpecialtyDataFormat get_SpecialtyDB(int index)
	{
		return new SpecialtyDataFormat(this.SpecialtyDB.data[index]);
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x000283C7 File Offset: 0x000267C7
	public int count_SpecialtyDB()
	{
		return this.SpecialtyDB.data.Count;
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x000283DC File Offset: 0x000267DC
	public SpecialtyDataFormat get_SpecialtyDB_forId(int id)
	{
		int num = this.search_SpecialtyDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_SpecialtyDB(num);
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x00028404 File Offset: 0x00026804
	public int search_SpecialtyDBIndex_forId(int id)
	{
		return this.SpecialtyDB.data.FindIndex((SpecialtyDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0002843C File Offset: 0x0002683C
	public int search_SpecialtyDBIndex_forItemId(int itemId)
	{
		return this.SpecialtyDB.data.FindIndex((SpecialtyDataFormat rec) => rec.itemId.Equals(itemId));
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x00028472 File Offset: 0x00026872
	public int get_SpecialtyDB_lastIndexId()
	{
		return this.SpecialtyDB.data[this.SpecialtyDB.data.Count - 1].id;
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0002849B File Offset: 0x0002689B
	public CharacterDataFormat get_CharaDB(int index)
	{
		return new CharacterDataFormat(this.CharaDB.data[index]);
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x000284B3 File Offset: 0x000268B3
	public int count_CharaDB()
	{
		return this.CharaDB.data.Count;
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x000284C8 File Offset: 0x000268C8
	public CharacterDataFormat get_CharaDB_forId(int id)
	{
		int num = this.search_CharaDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_CharaDB(num);
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x000284F0 File Offset: 0x000268F0
	public int search_CharaDBIndex_forId(int id)
	{
		return this.CharaDB.data.FindIndex((CharacterDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x00028528 File Offset: 0x00026928
	public int get_CharaDB_rowItemId_index(int itemId)
	{
		return this.CharaDB.rowItemId.FindIndex((int rec) => rec.Equals(itemId));
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0002855E File Offset: 0x0002695E
	public int get_CharaDB_lastIndexId()
	{
		return this.CharaDB.data[this.CharaDB.data.Count - 1].id;
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x00028587 File Offset: 0x00026987
	public PrizeDataFormat get_PrizeDB(int index)
	{
		return new PrizeDataFormat(this.PrizeDB.data[index]);
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0002859F File Offset: 0x0002699F
	public int count_PrizeDB()
	{
		return this.PrizeDB.data.Count;
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x000285B4 File Offset: 0x000269B4
	public PrizeDataFormat get_PrizeDB_forId(int id)
	{
		int num = this.search_PrizeDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_PrizeDB(num);
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x000285DC File Offset: 0x000269DC
	public int search_PrizeDBIndex_forId(int id)
	{
		return this.PrizeDB.data.FindIndex((PrizeDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x00028612 File Offset: 0x00026A12
	public ItemDataFormat get_ItemDB(int index)
	{
		return new ItemDataFormat(this.ItemDB.data[index]);
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0002862A File Offset: 0x00026A2A
	public int count_ItemDB()
	{
		return this.ItemDB.data.Count;
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0002863C File Offset: 0x00026A3C
	public ItemDataFormat get_ItemDB_forId(int id)
	{
		int num = this.search_ItemDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_ItemDB(num);
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x00028664 File Offset: 0x00026A64
	public int search_ItemDBIndex_forId(int id)
	{
		return this.ItemDB.data.FindIndex((ItemDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0002869A File Offset: 0x00026A9A
	public AchieveDataFormat get_AchieveDB(int index)
	{
		return new AchieveDataFormat(this.AchieveDB.data[index]);
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x000286B2 File Offset: 0x00026AB2
	public int count_AchieveDB()
	{
		return this.AchieveDB.data.Count;
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x000286C4 File Offset: 0x00026AC4
	public AchieveDataFormat get_AchieveDB_forId(int id)
	{
		int num = this.search_AchieveDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_AchieveDB(num);
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x000286EC File Offset: 0x00026AEC
	public int search_AchieveDBIndex_forId(int id)
	{
		return this.AchieveDB.data.FindIndex((AchieveDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x00028722 File Offset: 0x00026B22
	public int get_AchieveDB_lastIndexId()
	{
		return this.AchieveDB.data[this.AchieveDB.data.Count - 1].id;
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0002874B File Offset: 0x00026B4B
	public NodeDataFormat get_NodeDB(int index)
	{
		return new NodeDataFormat(this.NodeDB.data[index]);
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x00028763 File Offset: 0x00026B63
	public int count_NodeDB()
	{
		return this.NodeDB.data.Count;
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x00028778 File Offset: 0x00026B78
	public NodeDataFormat get_NodeDB_forId(int id)
	{
		int num = this.search_NodeDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_NodeDB(num);
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x000287A0 File Offset: 0x00026BA0
	public int search_NodeDBIndex_forId(int id)
	{
		return this.NodeDB.data.FindIndex((NodeDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x000287D6 File Offset: 0x00026BD6
	public int get_NodeDB_AreaIndex(AreaType area)
	{
		if (area < AreaType.East)
		{
			return -1;
		}
		if (area >= (AreaType)this.NodeDB.areaIndex.Count)
		{
			return 9999000;
		}
		return this.NodeDB.areaIndex[(int)area];
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x00028810 File Offset: 0x00026C10
	public int get_NodeDB_AreaIndex(int id)
	{
		if (id < 0)
		{
			return -1;
		}
		for (int i = this.NodeDB.areaIndex.Count - 1; i >= 0; i--)
		{
			if (this.NodeDB.areaIndex[i] <= id)
			{
				return this.NodeDB.areaIndex[i];
			}
		}
		return -1;
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x00028874 File Offset: 0x00026C74
	public AreaType get_NodeDB_AreaType(int id)
	{
		if (id < 0)
		{
			return AreaType.NONE;
		}
		for (int i = this.NodeDB.areaIndex.Count - 1; i >= 0; i--)
		{
			if (this.NodeDB.areaIndex[i] <= id)
			{
				return (AreaType)i;
			}
		}
		return AreaType.NONE;
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x000288C8 File Offset: 0x00026CC8
	public List<int> getList_NodeDB_prefIds(int prefId)
	{
		List<NodeDataFormat> list = new List<NodeDataFormat>();
		list.AddRange(this.NodeDB.data.FindAll((NodeDataFormat data) => data.pathId == prefId));
		List<int> list2 = new List<int>();
		foreach (NodeDataFormat nodeDataFormat in list)
		{
			list2.Add(nodeDataFormat.id);
		}
		return list2;
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x00028964 File Offset: 0x00026D64
	public NodeConnectDataFormat get_NodeConnectDB(int index)
	{
		return new NodeConnectDataFormat(this.NConnectDB.data[index]);
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x0002897C File Offset: 0x00026D7C
	public int count_NodeConnectDB()
	{
		return this.NConnectDB.data.Count;
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x00028990 File Offset: 0x00026D90
	public NodeConnectDataFormat get_NodeConnectDB_forId(int id)
	{
		int num = this.search_NodeConnectDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_NodeConnectDB(num);
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x000289B8 File Offset: 0x00026DB8
	public int search_NodeConnectDBIndex_forId(int id)
	{
		return this.NConnectDB.data.FindIndex((NodeConnectDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x000289EE File Offset: 0x00026DEE
	public NodeEdgeDataFormat get_NodeEdgeDB(int index)
	{
		return new NodeEdgeDataFormat(this.NEdgeDB.data[index]);
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x00028A06 File Offset: 0x00026E06
	public int count_NodeEdgeDB()
	{
		return this.NEdgeDB.data.Count;
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x00028A18 File Offset: 0x00026E18
	public NodeEdgeDataFormat get_NodeEdgeDB_forId(int id)
	{
		int num = this.search_NodeEdgeDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_NodeEdgeDB(num);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x00028A40 File Offset: 0x00026E40
	public int search_NodeEdgeDBIndex_forId(int id)
	{
		return this.NEdgeDB.data.FindIndex((NodeEdgeDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x00028A76 File Offset: 0x00026E76
	public NodeGoalDataFormat get_NodeGoalDB(int index)
	{
		return new NodeGoalDataFormat(this.NGoalDB.data[index]);
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x00028A8E File Offset: 0x00026E8E
	public int count_NodeGoalDB()
	{
		return this.NGoalDB.data.Count;
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00028AA0 File Offset: 0x00026EA0
	public NodeGoalDataFormat get_NodeGoalDB_forId(int id)
	{
		int num = this.search_NodeGoalDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_NodeGoalDB(num);
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x00028AC8 File Offset: 0x00026EC8
	public int search_NodeGoalDBIndex_forId(int id)
	{
		return this.NGoalDB.data.FindIndex((NodeGoalDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x00028B00 File Offset: 0x00026F00
	public List<int> getList_NodeGoalItemPer(int itemId)
	{
		int num = this.NGoalDB.rowItemId.FindIndex((int rec) => rec.Equals(itemId));
		if (num == -1)
		{
			return null;
		}
		List<int> list = new List<int>();
		foreach (NodeGoalDataFormat nodeGoalDataFormat in SuperGameMaster.sDataBase.NGoalDB.data)
		{
			list.Add(nodeGoalDataFormat.itemPer[num]);
		}
		return new List<int>(list);
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x00028BAC File Offset: 0x00026FAC
	public List<int> getList_NodeGoalIdList()
	{
		List<int> list = new List<int>();
		foreach (NodeGoalDataFormat nodeGoalDataFormat in SuperGameMaster.sDataBase.NGoalDB.data)
		{
			list.Add(nodeGoalDataFormat.id);
		}
		return new List<int>(list);
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x00028C24 File Offset: 0x00027024
	public NodeItemDataFormat get_NodeItemDB(int index)
	{
		return new NodeItemDataFormat(this.NItemDB.data[index]);
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00028C3C File Offset: 0x0002703C
	public int count_NodeItemDB()
	{
		return this.NItemDB.data.Count;
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00028C50 File Offset: 0x00027050
	public NodeItemDataFormat get_NodeItemDB_forId(int id)
	{
		int num = this.search_NodeItemDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_NodeItemDB(num);
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x00028C78 File Offset: 0x00027078
	public int search_NodeItemDBIndex_forId(int id)
	{
		return this.NItemDB.data.FindIndex((NodeItemDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00028CAE File Offset: 0x000270AE
	public NodePrefDataFormat get_NodePrefDB(int index)
	{
		return new NodePrefDataFormat(this.NPrefDB.data[index]);
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x00028CC6 File Offset: 0x000270C6
	public int count_NodePrefDB()
	{
		return this.NPrefDB.data.Count;
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x00028CD8 File Offset: 0x000270D8
	public NodePrefDataFormat get_NodePrefDB_forId(int id)
	{
		int num = this.search_NodePrefDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_NodePrefDB(num);
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x00028D00 File Offset: 0x00027100
	public int search_NodePrefDBIndex_forId(int id)
	{
		return this.NPrefDB.data.FindIndex((NodePrefDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00028D36 File Offset: 0x00027136
	public PictureDataFormat get_PictureDB(int index)
	{
		return new PictureDataFormat(this.PictureDB.data[index]);
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x00028D4E File Offset: 0x0002714E
	public int count_PictureDB()
	{
		return this.PictureDB.data.Count;
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x00028D60 File Offset: 0x00027160
	public PictureDataFormat get_PictureDB_forId(int id)
	{
		int num = this.search_PictureDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_PictureDB(num);
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x00028D88 File Offset: 0x00027188
	public int search_PictureDBIndex_forId(int id)
	{
		return this.PictureDB.data.FindIndex((PictureDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x00028DC0 File Offset: 0x000271C0
	public int search_PictureDBIndex_forSetType(Picture.Type setType)
	{
		return this.PictureDB.data.FindIndex((PictureDataFormat rec) => rec.type.Equals(setType));
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x00028DF8 File Offset: 0x000271F8
	public int search_PictureDBIndex_forName(string name)
	{
		return this.PictureDB.data.FindIndex((PictureDataFormat rec) => rec.name.Equals(name));
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x00028E2E File Offset: 0x0002722E
	public PictureBackDataFormat get_PictureBackDB(int index)
	{
		return new PictureBackDataFormat(this.PBackDB.data[index]);
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00028E46 File Offset: 0x00027246
	public int count_PictureBackDB()
	{
		return this.PBackDB.data.Count;
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x00028E58 File Offset: 0x00027258
	public PictureBackDataFormat get_PictureBackDB_forId(int id)
	{
		int num = this.search_PictureBackDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_PictureBackDB(num);
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x00028E80 File Offset: 0x00027280
	public int search_PictureBackDBIndex_forId(int id)
	{
		return this.PBackDB.data.FindIndex((PictureBackDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00028EB8 File Offset: 0x000272B8
	public PictureBackDataFormat get_PictureBackDB_forName(string name)
	{
		int num = this.search_PictureBackDBIndex_forName(name);
		if (num == -1)
		{
			return null;
		}
		return this.get_PictureBackDB(num);
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00028EE0 File Offset: 0x000272E0
	public int search_PictureBackDBIndex_forName(string name)
	{
		return this.PBackDB.data.FindIndex((PictureBackDataFormat rec) => rec.name.Equals(name));
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00028F16 File Offset: 0x00027316
	public PictureCharaDataFormat get_PictureCharaDB(int index)
	{
		return new PictureCharaDataFormat(this.PCharaDB.data[index]);
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00028F2E File Offset: 0x0002732E
	public int count_PictureCharaDB()
	{
		return this.PCharaDB.data.Count;
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x00028F40 File Offset: 0x00027340
	public PictureCharaDataFormat get_PictureCharaDB_forId(int id)
	{
		int num = this.search_PictureCharaDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_PictureCharaDB(num);
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x00028F68 File Offset: 0x00027368
	public int search_PictureCharaDBIndex_forId(int id)
	{
		return this.PCharaDB.data.FindIndex((PictureCharaDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x00028FA0 File Offset: 0x000273A0
	public int search_PictureCharaDB_posePathIndex_forPoseName(string poseName)
	{
		if (poseName == string.Empty)
		{
			return -1;
		}
		return this.PCharaDB.poseNameList.FindIndex((string rec) => rec.Equals(poseName));
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00028FF0 File Offset: 0x000273F0
	public int search_PictureCharaDBIndex_forName(string name)
	{
		return this.PCharaDB.data.FindIndex((PictureCharaDataFormat rec) => rec.name.Equals(name));
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x00029026 File Offset: 0x00027426
	public PictureTagDataFormat get_PictureTagDB(int index)
	{
		return new PictureTagDataFormat(this.PTagDB.data[index]);
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x0002903E File Offset: 0x0002743E
	public int count_PictureTagDB()
	{
		return this.PTagDB.data.Count;
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x00029050 File Offset: 0x00027450
	public PictureTagDataFormat get_PictureTagDB_forId(int id)
	{
		int num = this.search_PictureTagDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_PictureTagDB(num);
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x00029078 File Offset: 0x00027478
	public int search_PictureTagDBIndex_forId(int id)
	{
		return this.PCharaDB.data.FindIndex((PictureCharaDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x000290B0 File Offset: 0x000274B0
	public PictureTagDataFormat get_PictureTagDB_forTag(string tag)
	{
		int num = this.search_PictureTagDBIndex_forTag(tag);
		if (num == -1)
		{
			return null;
		}
		return this.get_PictureTagDB(num);
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x000290D8 File Offset: 0x000274D8
	public int search_PictureTagDBIndex_forTag(string tag)
	{
		if (tag == string.Empty)
		{
			return -1;
		}
		return this.PTagDB.data.FindIndex((PictureTagDataFormat rec) => rec.tag.Equals(tag));
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x00029125 File Offset: 0x00027525
	public PictureRandomDataFormat get_PictureRandomDB(int index)
	{
		return new PictureRandomDataFormat(this.PRandomDB.data[index]);
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0002913D File Offset: 0x0002753D
	public int count_PictureRandomDB()
	{
		return this.PRandomDB.data.Count;
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x00029150 File Offset: 0x00027550
	public PictureRandomDataFormat get_PictureRandomDB_forId(int id)
	{
		int num = this.search_PictureRandomDBIndex_forId(id);
		if (num == -1)
		{
			return null;
		}
		return this.get_PictureRandomDB(num);
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x00029178 File Offset: 0x00027578
	public int search_PictureRandomDBIndex_forId(int id)
	{
		return this.PCharaDB.data.FindIndex((PictureCharaDataFormat rec) => rec.id.Equals(id));
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x000291B0 File Offset: 0x000275B0
	public PictureRandomDataFormat get_PictureRandomDB_forRandom(string set)
	{
		int num = this.search_PictureRandomDBIndex_forSet(set);
		if (num == -1)
		{
			return null;
		}
		return this.get_PictureRandomDB(num);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x000291D8 File Offset: 0x000275D8
	public int search_PictureRandomDBIndex_forSet(string set)
	{
		if (set == string.Empty)
		{
			return -1;
		}
		return this.PRandomDB.data.FindIndex((PictureRandomDataFormat rec) => rec.set.Equals(set));
	}

	// Token: 0x04000564 RID: 1380
	public MailEventDataBase MailEvtDB;

	// Token: 0x04000565 RID: 1381
	public ShopDataBase ShopDB;

	// Token: 0x04000566 RID: 1382
	public CollectionDataBase CollectDB;

	// Token: 0x04000567 RID: 1383
	public SpecialtyDataBase SpecialtyDB;

	// Token: 0x04000568 RID: 1384
	public CharacterDataBase CharaDB;

	// Token: 0x04000569 RID: 1385
	public PrizeDataBase PrizeDB;

	// Token: 0x0400056A RID: 1386
	public ItemDataBase ItemDB;

	// Token: 0x0400056B RID: 1387
	public AchieveDataBase AchieveDB;

	// Token: 0x0400056C RID: 1388
	[Space(10f)]
	public NodeDataBase NodeDB;

	// Token: 0x0400056D RID: 1389
	public NodeConnectDataBase NConnectDB;

	// Token: 0x0400056E RID: 1390
	public NodeEdgeDataBase NEdgeDB;

	// Token: 0x0400056F RID: 1391
	public NodeGoalDataBase NGoalDB;

	// Token: 0x04000570 RID: 1392
	public NodeItemDataBase NItemDB;

	// Token: 0x04000571 RID: 1393
	public NodePrefDataBase NPrefDB;

	// Token: 0x04000572 RID: 1394
	[Space(10f)]
	public PictureDataBase PictureDB;

	// Token: 0x04000573 RID: 1395
	public PictureBackDataBase PBackDB;

	// Token: 0x04000574 RID: 1396
	public PictureCharaDataBase PCharaDB;

	// Token: 0x04000575 RID: 1397
	public PictureTagDataBase PTagDB;

	// Token: 0x04000576 RID: 1398
	public PictureRandomDataBase PRandomDB;
}
