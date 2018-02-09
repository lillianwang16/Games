using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Picture;
using Tutorial;
using UnityEngine;

// Token: 0x020000AD RID: 173
[Serializable]
public class SaveDataFormat
{
	// Token: 0x06000441 RID: 1089 RVA: 0x0001CCB4 File Offset: 0x0001B0B4
	public SaveDataFormat(SaveDataFormat ori)
	{
		this.version = ori.version;
		this.supportID = ori.supportID;
		this.hoten = new List<bool>(ori.hoten);
		this.CloverPoint = ori.CloverPoint;
		this.ticket = ori.ticket;
		this.CloverList = new List<CloverDataFormat>(ori.CloverList);
		this.lastDateTime = new DateTime(ori.lastDateTime.Year, ori.lastDateTime.Month, ori.lastDateTime.Day, ori.lastDateTime.Hour, ori.lastDateTime.Minute, ori.lastDateTime.Second, ori.lastDateTime.Millisecond);
		this.MailList_nextId = ori.MailList_nextId;
		this.MailList = new List<MailEventFormat>(ori.MailList);
		this.ItemList = new List<ItemListFormat>(ori.ItemList);
		this.bagList = new List<int>(ori.bagList);
		this.deskList = new List<int>(ori.deskList);
		this.bagList_virtual = new List<int>(ori.bagList_virtual);
		this.deskList_virtual = new List<int>(ori.deskList_virtual);
		this.collectFlags = new List<bool>(ori.collectFlags);
		this.collectFailedCnt = new List<int>(ori.collectFailedCnt);
		this.specialtyFlags = new List<bool>(ori.specialtyFlags);
		this.albumPicture = new List<byte[]>(ori.albumPicture);
		this.albumPictureDate = new List<DateTime>(ori.albumPictureDate);
		this.tmpPictureId = new List<int>(ori.tmpPictureId);
		this.tmpPicture = new List<byte[]>(ori.tmpPicture);
		this.evtList_timer = new List<EventTimerFormat>(ori.evtList_timer);
		this.evtList_active = new List<EventTimerFormat>(ori.evtList_active);
		this.tutorialStep = ori.tutorialStep;
		this.firstFlag = new List<bool>(ori.firstFlag);
		this.frogName = ori.frogName;
		this.frogAchieveId = ori.frogAchieveId;
		this.achieveFlags = new List<bool>(ori.achieveFlags);
		this.frogMotion = ori.frogMotion;
		this.home = ori.home;
		this.drift = ori.drift;
		this.restTime = ori.restTime;
		this.lastTravelTime = ori.lastTravelTime;
		this.standby = ori.standby;
		this.standbyWait = ori.standbyWait;
		this.bgmVolume = ori.bgmVolume;
		this.seVolume = ori.seVolume;
		this.NoticeFlag = ori.NoticeFlag;
		this.gameFlags = new List<int>(ori.gameFlags);
		this.tmpRaffleResult = ori.tmpRaffleResult;
		this.version_start = ori.version_start;
		this.iapCallBackCnt = ori.iapCallBackCnt;
	}

	// Token: 0x06000442 RID: 1090 RVA: 0x0001CF6F File Offset: 0x0001B36F
	public SaveDataFormat()
	{
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x0001CF78 File Offset: 0x0001B378
	public void initialize()
	{
		this.version = 1.05f;
		this.supportID = 0;
		this.hoten = new List<bool>();
		this.CloverPoint = 0;
		this.ticket = 0;
		this.CloverList = new List<CloverDataFormat>();
		this.lastDateTime = new DateTime(1970, 1, 1);
		this.MailList_nextId = 0;
		this.MailList = new List<MailEventFormat>();
		this.ItemList = new List<ItemListFormat>();
		this.bagList = new List<int>();
		this.deskList = new List<int>();
		this.bagList_virtual = new List<int>();
		this.deskList_virtual = new List<int>();
		this.collectFlags = new List<bool>();
		this.collectFailedCnt = new List<int>();
		this.specialtyFlags = new List<bool>();
		this.albumPicture = new List<byte[]>();
		this.albumPictureDate = new List<DateTime>();
		this.tmpPictureId = new List<int>();
		this.tmpPicture = new List<byte[]>();
		this.evtList_timer = new List<EventTimerFormat>();
		this.evtList_active = new List<EventTimerFormat>();
		this.tutorialStep = Step.NONE;
		this.firstFlag = new List<bool>();
		this.frogName = string.Empty;
		this.frogAchieveId = 0;
		this.achieveFlags = new List<bool>();
		this.frogMotion = -1;
		this.home = true;
		this.drift = false;
		this.restTime = 1800;
		this.lastTravelTime = this.restTime * 2;
		this.standby = false;
		this.standbyWait = 0;
		this.bgmVolume = 50;
		this.seVolume = 50;
		this.NoticeFlag = true;
		this.gameFlags = new List<int>();
		this.tmpRaffleResult = -1;
		this.version_start = 1.05f;
		this.iapCallBackCnt = 0;
	}

	// Token: 0x06000444 RID: 1092 RVA: 0x0001D11F File Offset: 0x0001B51F
	[OnDeserializing]
	public void setRegionDefault(StreamingContext sc)
	{
		this.initialize();
	}

	// Token: 0x06000445 RID: 1093 RVA: 0x0001D127 File Offset: 0x0001B527
	[OnDeserialized]
	public void endLoadRegion(StreamingContext sc)
	{
		this.versionFix();
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x0001D130 File Offset: 0x0001B530
	public void versionFix()
	{
		Debug.Log("[SaveDataFormat] version：" + this.version);
		if (this.version < 1.05f)
		{
			if (this.version == 1f)
			{
				this.version_start = 1f;
			}
			Debug.Log(string.Concat(new object[]
			{
				"[SaveDataFormat] バージョンが更新されました：",
				this.version,
				"->",
				1.05f,
				" (start：",
				this.version_start,
				")"
			}));
			this.version = 1.05f;
		}
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x0001D1E8 File Offset: 0x0001B5E8
	public bool DataDiffCheck()
	{
		bool flag = false;
		while (this.bagList.Count != 4)
		{
			if (this.bagList.Count > 4)
			{
				this.bagList.RemoveAt(this.bagList.Count - 1);
			}
			if (this.bagList.Count < 4)
			{
				this.bagList.Add(-1);
			}
			flag = true;
		}
		if (flag)
		{
			Debug.Log("[saveDataFormat] セーブデータのサイズが修正されました：[bag:" + this.bagList.Count + "] ");
			flag = false;
		}
		while (this.deskList.Count != 8)
		{
			if (this.deskList.Count > 8)
			{
				this.deskList.RemoveAt(this.deskList.Count - 1);
			}
			if (this.deskList.Count < 8)
			{
				this.deskList.Add(-1);
			}
			flag = true;
		}
		if (flag)
		{
			Debug.Log("[saveDataFormat] セーブデータのサイズが修正されました：[desk: " + this.deskList.Count + "] ");
			flag = false;
		}
		int num = 100;
		while (this.collectFlags.Count != num)
		{
			if (this.collectFlags.Count > num)
			{
				this.collectFlags.RemoveAt(this.collectFlags.Count - 1);
			}
			if (this.collectFlags.Count < num)
			{
				this.collectFlags.Add(false);
			}
			flag = true;
		}
		if (flag)
		{
			Debug.Log("[saveDataFormat] セーブデータのサイズが修正されました：[collectFlags:" + this.collectFlags.Count + "] ");
			flag = false;
		}
		int num2 = 100;
		while (this.collectFailedCnt.Count != num2)
		{
			if (this.collectFailedCnt.Count > num2)
			{
				this.collectFailedCnt.RemoveAt(this.collectFailedCnt.Count - 1);
			}
			if (this.collectFailedCnt.Count < num2)
			{
				this.collectFailedCnt.Add(0);
			}
			flag = true;
		}
		if (flag)
		{
			Debug.Log("[saveDataFormat] セーブデータのサイズが修正されました：[collectFailedCnt:" + this.collectFailedCnt.Count + "] ");
			flag = false;
		}
		int num3 = 100;
		while (this.specialtyFlags.Count != num3)
		{
			if (this.specialtyFlags.Count > num3)
			{
				this.specialtyFlags.RemoveAt(this.specialtyFlags.Count - 1);
			}
			if (this.specialtyFlags.Count < num3)
			{
				this.specialtyFlags.Add(false);
			}
			flag = true;
		}
		if (flag)
		{
			Debug.Log("[saveDataFormat] セーブデータのサイズが修正されました：[specialtyFlags:" + this.specialtyFlags.Count + "] ");
			flag = false;
		}
		int num4 = 100;
		while (this.achieveFlags.Count != num4)
		{
			if (this.achieveFlags.Count > num4)
			{
				this.achieveFlags.RemoveAt(this.achieveFlags.Count - 1);
			}
			if (this.achieveFlags.Count < num4)
			{
				this.achieveFlags.Add(false);
			}
			flag = true;
		}
		if (flag)
		{
			Debug.Log("[saveDataFormat] セーブデータのサイズが修正されました：[achieveFlags:" + this.achieveFlags.Count + "] ");
			flag = false;
		}
		int num5 = 13;
		while (this.gameFlags.Count != num5)
		{
			if (this.gameFlags.Count > num4)
			{
				this.gameFlags.RemoveAt(this.gameFlags.Count - 1);
			}
			if (this.gameFlags.Count < num4)
			{
				this.gameFlags.Add(0);
			}
			flag = true;
		}
		if (flag)
		{
			Debug.Log("[saveDataFormat] セーブデータのサイズが修正されました：[gameFlags:" + this.gameFlags.Count + "] ");
			flag = false;
		}
		int num6 = 7;
		while (this.firstFlag.Count != num6)
		{
			if (this.firstFlag.Count > num6)
			{
				this.firstFlag.RemoveAt(this.firstFlag.Count - 1);
			}
			if (this.firstFlag.Count < num6)
			{
				this.firstFlag.Add(false);
			}
			flag = true;
		}
		if (flag)
		{
			Debug.Log("[saveDataFormat] セーブデータのサイズが修正されました：[firstFlag:" + this.firstFlag.Count + "] ");
			flag = false;
		}
		int num7 = 10;
		while (this.hoten.Count != num7)
		{
			if (this.hoten.Count > num7)
			{
				this.hoten.RemoveAt(this.hoten.Count - 1);
			}
			if (this.hoten.Count < num7)
			{
				this.hoten.Add(false);
			}
			flag = true;
		}
		if (flag)
		{
			Debug.Log("[saveDataFormat] セーブデータのサイズが修正されました：[hotenFlag:" + this.hoten.Count + "] ");
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x0001D6F8 File Offset: 0x0001BAF8
	public byte[] ConvertBinary_Main(bool write, byte[] sByte)
	{
		int num = 0;
		List<byte[]> list = new List<byte[]>();
		int num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(Convert.ToInt32(this.version * 10000f), list[list.Count - 1], 0, num2);
		}
		else
		{
			this.version = (float)ByteConverter.ConnectRead(sByte, num, num2) / 10000f;
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.supportID, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.supportID = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		int num3;
		if (write)
		{
			num3 = this.hoten.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		int num4 = 1;
		int num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(Convert.ToInt32(this.hoten[i]), list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.hoten.Add(Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num + num5, num4)));
				i++;
				num5 += num4;
			}
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.CloverPoint, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.CloverPoint = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.ticket, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.ticket = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		if (write)
		{
			num3 = this.CloverList.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = new CloverDataFormat().UseByte();
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				Buffer.BlockCopy(this.CloverList[i].ConvertBinary(true, null), 0, list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				byte[] array = new byte[num4];
				Buffer.BlockCopy(sByte, num + num5, array, 0, num4);
				this.CloverList.Add(new CloverDataFormat());
				this.CloverList[this.CloverList.Count - 1].ConvertBinary(false, array);
				i++;
				num5 += num4;
			}
			num += num2;
		}
		num3 = 7;
		num4 = 4;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				switch (i)
				{
				case 0:
					ByteConverter.ConnectWrite(this.lastDateTime.Year, list[list.Count - 1], num5, num4);
					break;
				case 1:
					ByteConverter.ConnectWrite(this.lastDateTime.Month, list[list.Count - 1], num5, num4);
					break;
				case 2:
					ByteConverter.ConnectWrite(this.lastDateTime.Day, list[list.Count - 1], num5, num4);
					break;
				case 3:
					ByteConverter.ConnectWrite(this.lastDateTime.Hour, list[list.Count - 1], num5, num4);
					break;
				case 4:
					ByteConverter.ConnectWrite(this.lastDateTime.Minute, list[list.Count - 1], num5, num4);
					break;
				case 5:
					ByteConverter.ConnectWrite(this.lastDateTime.Second, list[list.Count - 1], num5, num4);
					break;
				case 6:
					ByteConverter.ConnectWrite(this.lastDateTime.Millisecond, list[list.Count - 1], num5, num4);
					break;
				}
				i++;
				num5 += num4;
			}
		}
		else
		{
			int[] array2 = new int[7];
			int i = 0;
			while (i < num3)
			{
				array2[i] = ByteConverter.ConnectRead(sByte, num + num5, num4);
				i++;
				num5 += num4;
			}
			this.lastDateTime = new DateTime(array2[0], array2[1], array2[2], array2[3], array2[4], array2[5], array2[6]);
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.MailList_nextId, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.MailList_nextId = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		if (write)
		{
			num3 = this.MailList.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = new MailEventFormat().UseByte();
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				Buffer.BlockCopy(this.MailList[i].ConvertBinary(true, null), 0, list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				byte[] array3 = new byte[num4];
				Buffer.BlockCopy(sByte, num + num5, array3, 0, num4);
				this.MailList.Add(new MailEventFormat());
				this.MailList[this.MailList.Count - 1].ConvertBinary(false, array3);
				i++;
				num5 += num4;
			}
			num += num2;
		}
		if (write)
		{
			num3 = this.ItemList.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = new ItemListFormat().UseByte();
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				Buffer.BlockCopy(this.ItemList[i].ConvertBinary(true, null), 0, list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				byte[] array4 = new byte[num4];
				Buffer.BlockCopy(sByte, num + num5, array4, 0, num4);
				this.ItemList.Add(new ItemListFormat());
				this.ItemList[this.ItemList.Count - 1].ConvertBinary(false, array4);
				i++;
				num5 += num4;
			}
			num += num2;
		}
		if (write)
		{
			num3 = this.bagList.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = 4;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(this.bagList[i], list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.bagList.Add(ByteConverter.ConnectRead(sByte, num + num5, num4));
				i++;
				num5 += num4;
			}
			num += num2;
		}
		if (write)
		{
			num3 = this.deskList.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = 4;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(this.deskList[i], list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.deskList.Add(ByteConverter.ConnectRead(sByte, num + num5, num4));
				i++;
				num5 += num4;
			}
			num += num2;
		}
		if (write)
		{
			num3 = this.bagList_virtual.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = 4;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(this.bagList_virtual[i], list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.bagList_virtual.Add(ByteConverter.ConnectRead(sByte, num + num5, num4));
				i++;
				num5 += num4;
			}
			num += num2;
		}
		if (write)
		{
			num3 = this.deskList_virtual.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = 4;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(this.deskList_virtual[i], list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.deskList_virtual.Add(ByteConverter.ConnectRead(sByte, num + num5, num4));
				i++;
				num5 += num4;
			}
			num += num2;
		}
		if (write)
		{
			num3 = this.collectFlags.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = 1;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(Convert.ToInt32(this.collectFlags[i]), list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.collectFlags.Add(Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num + num5, num4)));
				i++;
				num5 += num4;
			}
			num += num2;
		}
		if (write)
		{
			num3 = this.collectFailedCnt.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = 4;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(this.collectFailedCnt[i], list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.collectFailedCnt.Add(ByteConverter.ConnectRead(sByte, num + num5, num4));
				if (this.collectFailedCnt[i] != 0)
				{
					Debug.LogWarning(string.Concat(new object[]
					{
						"collectFailedCnt[",
						i,
						"] ",
						this.collectFailedCnt[i]
					}));
				}
				i++;
				num5 += num4;
			}
			num += num2;
		}
		if (write)
		{
			num3 = this.specialtyFlags.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = 1;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(Convert.ToInt32(this.specialtyFlags[i]), list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.specialtyFlags.Add(Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num + num5, num4)));
				i++;
				num5 += num4;
			}
			num += num2;
		}
		if (write)
		{
			num3 = this.evtList_timer.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = new EventTimerFormat().UseByte();
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				Buffer.BlockCopy(this.evtList_timer[i].ConvertBinary(true, null), 0, list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				byte[] array5 = new byte[num4];
				Buffer.BlockCopy(sByte, num + num5, array5, 0, num4);
				this.evtList_timer.Add(new EventTimerFormat());
				this.evtList_timer[this.evtList_timer.Count - 1].ConvertBinary(false, array5);
				i++;
				num5 += num4;
			}
			num += num2;
		}
		if (write)
		{
			num3 = this.evtList_active.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = new EventTimerFormat().UseByte();
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				Buffer.BlockCopy(this.evtList_active[i].ConvertBinary(true, null), 0, list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				byte[] array6 = new byte[num4];
				Buffer.BlockCopy(sByte, num + num5, array6, 0, num4);
				this.evtList_active.Add(new EventTimerFormat());
				this.evtList_active[this.evtList_active.Count - 1].ConvertBinary(false, array6);
				i++;
				num5 += num4;
			}
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite((int)this.tutorialStep, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.tutorialStep = (Step)ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		if (write)
		{
			num3 = this.firstFlag.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = 1;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(Convert.ToInt32(this.firstFlag[i]), list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.firstFlag.Add(Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num + num5, num4)));
				i++;
				num5 += num4;
			}
			num += num2;
		}
		num2 = 20;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWriteString(this.frogName, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.frogName = ByteConverter.ConnectReadString(sByte, num, num2);
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.frogAchieveId, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.frogAchieveId = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		if (write)
		{
			num3 = this.achieveFlags.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = 1;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(Convert.ToInt32(this.achieveFlags[i]), list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.achieveFlags.Add(Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num + num5, num4)));
				i++;
				num5 += num4;
			}
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.frogMotion, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.frogMotion = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		num2 = 1;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(Convert.ToInt32(this.home), list[list.Count - 1], 0, num2);
		}
		else
		{
			this.home = Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num, num2));
			num += num2;
		}
		num2 = 1;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(Convert.ToInt32(this.drift), list[list.Count - 1], 0, num2);
		}
		else
		{
			this.drift = Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num, num2));
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.restTime, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.restTime = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.lastTravelTime, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.lastTravelTime = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		num2 = 1;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(Convert.ToInt32(this.standby), list[list.Count - 1], 0, num2);
		}
		else
		{
			this.standby = Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num, num2));
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.standbyWait, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.standbyWait = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.bgmVolume, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.bgmVolume = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.seVolume, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.seVolume = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		num2 = 1;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(Convert.ToInt32(this.NoticeFlag), list[list.Count - 1], 0, num2);
		}
		else
		{
			this.NoticeFlag = Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num, num2));
			num += num2;
		}
		if (write)
		{
			num3 = this.gameFlags.Count;
		}
		else
		{
			num3 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num4 = 4;
		num5 = 4;
		num2 = 4 + num3 * num4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num3)
			{
				ByteConverter.ConnectWrite(this.gameFlags[i], list[list.Count - 1], num5, num4);
				i++;
				num5 += num4;
			}
		}
		else
		{
			int i = 0;
			while (i < num3)
			{
				this.gameFlags.Add(ByteConverter.ConnectRead(sByte, num + num5, num4));
				i++;
				num5 += num4;
			}
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.tmpRaffleResult, list[list.Count - 1], 0, num2);
		}
		else
		{
			this.tmpRaffleResult = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(Convert.ToInt32(this.version_start * 10000f), list[list.Count - 1], 0, num2);
		}
		else
		{
			this.version_start = (float)ByteConverter.ConnectRead(sByte, num, num2) / 10000f;
			num += num2;
		}
		num2 = 4;
		if (write)
		{
			list.Add(new byte[num2]);
			ByteConverter.ConnectWrite(this.iapCallBackCnt, list[list.Count - 1], 0, num2);
		}
		else if (this.version >= 1.05f)
		{
			this.iapCallBackCnt = ByteConverter.ConnectRead(sByte, num, num2);
			num += num2;
		}
		if (!write && this.version < 1.05f)
		{
			this.supportID = UnityEngine.Random.Range(0, 1000000000);
			this.hoten[0] = true;
		}
		if (write)
		{
			int num6 = 0;
			foreach (byte[] array7 in list)
			{
				num6 += array7.Length;
			}
			sByte = new byte[num6];
			foreach (byte[] array8 in list)
			{
				Buffer.BlockCopy(array8, 0, sByte, num, array8.Length);
				num += array8.Length;
			}
			return sByte;
		}
		return null;
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x0001EF60 File Offset: 0x0001D360
	public byte[] ConvertBinary_Picture(bool write, byte[] sByte, SaveType sType, int chgIdx)
	{
		int num = 0;
		List<byte[]> list = new List<byte[]>();
		if (sType != SaveType.Album)
		{
			if (sType == SaveType.Temp)
			{
				int num2 = 4;
				if (write)
				{
					list.Add(new byte[num2]);
					ByteConverter.ConnectWrite(this.tmpPictureId[chgIdx], list[list.Count - 1], 0, num2);
				}
				else
				{
					while (this.tmpPictureId.Count - 1 < chgIdx)
					{
						this.tmpPictureId.Add(0);
					}
					this.tmpPictureId[chgIdx] = ByteConverter.ConnectRead(sByte, num, num2);
					num += num2;
				}
				if (write)
				{
					int num3 = this.tmpPicture[chgIdx].Length;
					list.Add(new byte[4]);
					ByteConverter.ConnectWrite(num3, list[list.Count - 1], 0, 4);
					list.Add(new byte[num3]);
					Buffer.BlockCopy(this.tmpPicture[chgIdx], 0, list[list.Count - 1], 0, num3);
				}
				else
				{
					int num4 = ByteConverter.ConnectRead(sByte, num, 4);
					while (this.tmpPicture.Count - 1 < chgIdx)
					{
						this.tmpPicture.Add(new byte[0]);
					}
					num += 4;
					this.tmpPicture[chgIdx] = new byte[num4];
					Buffer.BlockCopy(sByte, num, this.tmpPicture[chgIdx], 0, num4);
					num += num4;
				}
			}
		}
		else
		{
			if (write)
			{
				int num5 = this.albumPicture[chgIdx].Length;
				list.Add(new byte[4]);
				ByteConverter.ConnectWrite(num5, list[list.Count - 1], 0, 4);
				list.Add(new byte[num5]);
				Buffer.BlockCopy(this.albumPicture[chgIdx], 0, list[list.Count - 1], 0, num5);
			}
			else
			{
				int num6 = ByteConverter.ConnectRead(sByte, num, 4);
				while (this.albumPicture.Count - 1 < chgIdx)
				{
					this.albumPicture.Add(new byte[0]);
				}
				num += 4;
				this.albumPicture[chgIdx] = new byte[num6];
				Buffer.BlockCopy(sByte, num, this.albumPicture[chgIdx], 0, num6);
				num += num6;
			}
			int num7 = 7;
			int num8 = 4;
			int num2 = 4 + num7 * num8;
			if (write)
			{
				int num9 = 4;
				list.Add(new byte[num2]);
				ByteConverter.ConnectWrite(num7, list[list.Count - 1], 0, 4);
				int i = 0;
				while (i < num7)
				{
					switch (i)
					{
					case 0:
						ByteConverter.ConnectWrite(this.albumPictureDate[chgIdx].Year, list[list.Count - 1], num9, num8);
						break;
					case 1:
						ByteConverter.ConnectWrite(this.albumPictureDate[chgIdx].Month, list[list.Count - 1], num9, num8);
						break;
					case 2:
						ByteConverter.ConnectWrite(this.albumPictureDate[chgIdx].Day, list[list.Count - 1], num9, num8);
						break;
					case 3:
						ByteConverter.ConnectWrite(this.albumPictureDate[chgIdx].Hour, list[list.Count - 1], num9, num8);
						break;
					case 4:
						ByteConverter.ConnectWrite(this.albumPictureDate[chgIdx].Minute, list[list.Count - 1], num9, num8);
						break;
					case 5:
						ByteConverter.ConnectWrite(this.albumPictureDate[chgIdx].Second, list[list.Count - 1], num9, num8);
						break;
					case 6:
						ByteConverter.ConnectWrite(this.albumPictureDate[chgIdx].Millisecond, list[list.Count - 1], num9, num8);
						break;
					}
					i++;
					num9 += num8;
				}
			}
			else
			{
				int num9 = 4;
				int[] array = new int[7];
				int i = 0;
				while (i < num7)
				{
					array[i] = ByteConverter.ConnectRead(sByte, num + num9, num8);
					i++;
					num9 += num8;
				}
				while (this.albumPictureDate.Count - 1 < chgIdx)
				{
					this.albumPictureDate.Add(default(DateTime));
				}
				this.albumPictureDate[chgIdx] = new DateTime(array[0], array[1], array[2], array[3], array[4], array[5], array[6]);
				num += num2;
			}
		}
		if (write)
		{
			int num10 = 0;
			foreach (byte[] array2 in list)
			{
				num10 += array2.Length;
			}
			sByte = new byte[num10];
			foreach (byte[] array3 in list)
			{
				Buffer.BlockCopy(array3, 0, sByte, num, array3.Length);
				num += array3.Length;
			}
			return sByte;
		}
		return null;
	}

	// Token: 0x04000456 RID: 1110
	public static readonly string SavePath = Application.persistentDataPath + "/";

	// Token: 0x04000457 RID: 1111
	public float version;

	// Token: 0x04000458 RID: 1112
	public int supportID;

	// Token: 0x04000459 RID: 1113
	public List<bool> hoten;

	// Token: 0x0400045A RID: 1114
	public int CloverPoint;

	// Token: 0x0400045B RID: 1115
	public int ticket;

	// Token: 0x0400045C RID: 1116
	public List<CloverDataFormat> CloverList;

	// Token: 0x0400045D RID: 1117
	public DateTime lastDateTime;

	// Token: 0x0400045E RID: 1118
	public int MailList_nextId;

	// Token: 0x0400045F RID: 1119
	public List<MailEventFormat> MailList;

	// Token: 0x04000460 RID: 1120
	public List<ItemListFormat> ItemList;

	// Token: 0x04000461 RID: 1121
	public List<int> bagList;

	// Token: 0x04000462 RID: 1122
	public List<int> deskList;

	// Token: 0x04000463 RID: 1123
	public List<int> bagList_virtual;

	// Token: 0x04000464 RID: 1124
	public List<int> deskList_virtual;

	// Token: 0x04000465 RID: 1125
	public List<bool> collectFlags;

	// Token: 0x04000466 RID: 1126
	public List<int> collectFailedCnt;

	// Token: 0x04000467 RID: 1127
	public List<bool> specialtyFlags;

	// Token: 0x04000468 RID: 1128
	public List<byte[]> albumPicture;

	// Token: 0x04000469 RID: 1129
	public List<DateTime> albumPictureDate;

	// Token: 0x0400046A RID: 1130
	public List<int> tmpPictureId;

	// Token: 0x0400046B RID: 1131
	public List<byte[]> tmpPicture;

	// Token: 0x0400046C RID: 1132
	public List<EventTimerFormat> evtList_timer;

	// Token: 0x0400046D RID: 1133
	public List<EventTimerFormat> evtList_active;

	// Token: 0x0400046E RID: 1134
	public Step tutorialStep;

	// Token: 0x0400046F RID: 1135
	public List<bool> firstFlag;

	// Token: 0x04000470 RID: 1136
	public string frogName;

	// Token: 0x04000471 RID: 1137
	public int frogAchieveId;

	// Token: 0x04000472 RID: 1138
	public List<bool> achieveFlags;

	// Token: 0x04000473 RID: 1139
	public int frogMotion;

	// Token: 0x04000474 RID: 1140
	public bool home;

	// Token: 0x04000475 RID: 1141
	public bool drift;

	// Token: 0x04000476 RID: 1142
	public int restTime;

	// Token: 0x04000477 RID: 1143
	public int lastTravelTime;

	// Token: 0x04000478 RID: 1144
	public bool standby;

	// Token: 0x04000479 RID: 1145
	public int standbyWait;

	// Token: 0x0400047A RID: 1146
	public int bgmVolume;

	// Token: 0x0400047B RID: 1147
	public int seVolume;

	// Token: 0x0400047C RID: 1148
	public bool NoticeFlag;

	// Token: 0x0400047D RID: 1149
	public List<int> gameFlags;

	// Token: 0x0400047E RID: 1150
	[OptionalField(VersionAdded = 2)]
	public int tmpRaffleResult;

	// Token: 0x0400047F RID: 1151
	[OptionalField(VersionAdded = 2)]
	public float version_start;

	// Token: 0x04000480 RID: 1152
	[OptionalField(VersionAdded = 3)]
	public int iapCallBackCnt;
}
