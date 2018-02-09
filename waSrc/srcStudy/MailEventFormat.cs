using System;
using System.Collections.Generic;
using Mail;
using UnityEngine;

// Token: 0x0200009F RID: 159
[Serializable]
public class MailEventFormat
{
	// Token: 0x06000426 RID: 1062 RVA: 0x0001C0C4 File Offset: 0x0001A4C4
	public MailEventFormat(MailEventFormat original)
	{
		this.title = original.title;
		this.message = original.message;
		this.id = original.id;
		this.senderCharaId = original.senderCharaId;
		this.mailEvt = original.mailEvt;
		this.CloverPoint = original.CloverPoint;
		this.ticket = original.ticket;
		this.itemId = original.itemId;
		this.itemStock = original.itemStock;
		this.mailId = original.mailId;
		this.date = new DateTime(original.date.Year, original.date.Month, original.date.Day, original.date.Hour, original.date.Minute, original.date.Second, original.date.Millisecond);
		this.opened = original.opened;
		this.protect = original.protect;
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x0001C1E6 File Offset: 0x0001A5E6
	public MailEventFormat()
	{
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0001C218 File Offset: 0x0001A618
	public void NewMail()
	{
		MailEventFormat mailEventFormat = new MailEventFormat();
		mailEventFormat.title = string.Empty;
		mailEventFormat.message = string.Empty;
		mailEventFormat.id = -1;
		mailEventFormat.senderCharaId = -1;
		mailEventFormat.mailEvt = EvtId.NONE;
		mailEventFormat.CloverPoint = 0;
		mailEventFormat.ticket = 0;
		mailEventFormat.itemId = -1;
		mailEventFormat.itemStock = 0;
		mailEventFormat.mailId = -1;
		mailEventFormat.date = new DateTime(1970, 1, 1);
		mailEventFormat.opened = false;
		mailEventFormat.protect = false;
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x0001C299 File Offset: 0x0001A699
	public int UseByte()
	{
		return ByteConverter.ConnectRead(this.ConvertBinary(true, null, true), 0, 4);
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x0001C2AB File Offset: 0x0001A6AB
	public byte[] ConvertBinary(bool write, byte[] sByte)
	{
		return this.ConvertBinary(write, sByte, false);
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x0001C2B8 File Offset: 0x0001A6B8
	public byte[] ConvertBinary(bool write, byte[] sByte, bool readSize)
	{
		int num = 0;
		int num2 = 0;
		List<byte[]> list = new List<byte[]>();
		int num3 = 40;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWriteString(this.title, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.title = ByteConverter.ConnectReadString(sByte, num2, num3);
			num2 += num3;
		}
		num3 = 40;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWriteString(this.message, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.message = ByteConverter.ConnectReadString(sByte, num2, num3);
			num2 += num3;
		}
		num3 = 4;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(this.id, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.id = ByteConverter.ConnectRead(sByte, num2, num3);
			num2 += num3;
		}
		num3 = 4;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(this.senderCharaId, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.senderCharaId = ByteConverter.ConnectRead(sByte, num2, num3);
			num2 += num3;
		}
		num3 = 4;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite((int)this.mailEvt, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.mailEvt = (EvtId)ByteConverter.ConnectRead(sByte, num2, num3);
			num2 += num3;
		}
		num3 = 4;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(this.CloverPoint, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.CloverPoint = ByteConverter.ConnectRead(sByte, num2, num3);
			num2 += num3;
		}
		num3 = 4;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(this.ticket, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.ticket = ByteConverter.ConnectRead(sByte, num2, num3);
			num2 += num3;
		}
		num3 = 4;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(this.itemId, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.itemId = ByteConverter.ConnectRead(sByte, num2, num3);
			num2 += num3;
		}
		num3 = 4;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(this.itemStock, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.itemStock = ByteConverter.ConnectRead(sByte, num2, num3);
			num2 += num3;
		}
		num3 = 4;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(this.mailId, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.mailId = ByteConverter.ConnectRead(sByte, num2, num3);
			num2 += num3;
		}
		int num4 = 7;
		int num5 = 4;
		int num6 = 4;
		num3 = 4 + num4 * num5;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(num4, list[list.Count - 1], 0, 4);
			int i = 0;
			while (i < num4)
			{
				switch (i)
				{
				case 0:
					ByteConverter.ConnectWrite(this.date.Year, list[list.Count - 1], num6, num5);
					break;
				case 1:
					ByteConverter.ConnectWrite(this.date.Month, list[list.Count - 1], num6, num5);
					break;
				case 2:
					ByteConverter.ConnectWrite(this.date.Day, list[list.Count - 1], num6, num5);
					break;
				case 3:
					ByteConverter.ConnectWrite(this.date.Hour, list[list.Count - 1], num6, num5);
					break;
				case 4:
					ByteConverter.ConnectWrite(this.date.Minute, list[list.Count - 1], num6, num5);
					break;
				case 5:
					ByteConverter.ConnectWrite(this.date.Second, list[list.Count - 1], num6, num5);
					break;
				case 6:
					ByteConverter.ConnectWrite(this.date.Millisecond, list[list.Count - 1], num6, num5);
					break;
				}
				i++;
				num6 += num5;
			}
		}
		else
		{
			int[] array = new int[7];
			int i = 0;
			while (i < num4)
			{
				array[i] = ByteConverter.ConnectRead(sByte, num2 + num6, num5);
				i++;
				num6 += num5;
			}
			this.date = new DateTime(array[0], array[1], array[2], array[3], array[4], array[5], array[6]);
			num2 += num3;
		}
		num3 = 1;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(Convert.ToInt32(this.opened), list[list.Count - 1], 0, num3);
		}
		else
		{
			this.opened = Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num2, num3));
			num2 += num3;
		}
		num3 = 1;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(Convert.ToInt32(this.protect), list[list.Count - 1], 0, num3);
		}
		else
		{
			this.protect = Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num2, num3));
			num2 += num3;
		}
		if (readSize)
		{
			sByte = new byte[4];
			ByteConverter.ConnectWrite(num, sByte, 0, 4);
			return sByte;
		}
		if (write)
		{
			int num7 = 0;
			foreach (byte[] array2 in list)
			{
				num7 += array2.Length;
			}
			sByte = new byte[num7];
			foreach (byte[] array3 in list)
			{
				Buffer.BlockCopy(array3, 0, sByte, num2, array3.Length);
				num2 += array3.Length;
			}
			return sByte;
		}
		return null;
	}

	// Token: 0x04000419 RID: 1049
	[Tooltip("メールタイトル")]
	public string title;

	// Token: 0x0400041A RID: 1050
	[Tooltip("メール本文")]
	[Multiline(3)]
	public string message;

	// Token: 0x0400041B RID: 1051
	[Tooltip("メール識別ＩＤ")]
	[Space(10f)]
	public int id;

	// Token: 0x0400041C RID: 1052
	[Tooltip("差出人キャラＩＤ")]
	public int senderCharaId = -1;

	// Token: 0x0400041D RID: 1053
	[Tooltip("メールイベント")]
	public EvtId mailEvt = EvtId.NONE;

	// Token: 0x0400041E RID: 1054
	[Tooltip("添付するクローバーの数")]
	[Space(10f)]
	public int CloverPoint;

	// Token: 0x0400041F RID: 1055
	[Tooltip("添付する福引券の数")]
	public int ticket;

	// Token: 0x04000420 RID: 1056
	[Tooltip("添付するアイテムのＩＤ（添付なし = -1）")]
	[Space(10f)]
	public int itemId = -1;

	// Token: 0x04000421 RID: 1057
	[Tooltip("添付するアイテムの数")]
	public int itemStock;

	// Token: 0x04000422 RID: 1058
	[HideInInspector]
	public int mailId;

	// Token: 0x04000423 RID: 1059
	[HideInInspector]
	public DateTime date = new DateTime(1970, 1, 1);

	// Token: 0x04000424 RID: 1060
	[HideInInspector]
	public bool opened;

	// Token: 0x04000425 RID: 1061
	[HideInInspector]
	public bool protect;
}
