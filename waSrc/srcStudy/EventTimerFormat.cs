using System;
using System.Collections.Generic;
using TimerEvent;

// Token: 0x02000090 RID: 144
[Serializable]
public class EventTimerFormat
{
	// Token: 0x0600040A RID: 1034 RVA: 0x0001B57C File Offset: 0x0001997C
	public EventTimerFormat(EventTimerFormat original)
	{
		this.id = original.id;
		this.timeSpanSec = original.timeSpanSec;
		this.activeTime = original.activeTime;
		this.evtType = original.evtType;
		this.evtId = original.evtId;
		this.evtValue = new List<int>(original.evtValue);
		this.addTime = new DateTime(original.addTime.Year, original.addTime.Month, original.addTime.Day, original.addTime.Hour, original.addTime.Minute, original.addTime.Second, original.addTime.Millisecond);
		this.trigger = original.trigger;
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x0001B640 File Offset: 0x00019A40
	public EventTimerFormat()
	{
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x0001B648 File Offset: 0x00019A48
	public int UseByte()
	{
		return ByteConverter.ConnectRead(this.ConvertBinary(true, null, true), 0, 4);
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x0001B65A File Offset: 0x00019A5A
	public byte[] ConvertBinary(bool write, byte[] sByte)
	{
		return this.ConvertBinary(write, sByte, false);
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x0001B668 File Offset: 0x00019A68
	public byte[] ConvertBinary(bool write, byte[] sByte, bool readSize)
	{
		int num = 0;
		int num2 = 0;
		List<byte[]> list = new List<byte[]>();
		int num3 = 4;
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
			ByteConverter.ConnectWrite(this.timeSpanSec, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.timeSpanSec = ByteConverter.ConnectRead(sByte, num2, num3);
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
			ByteConverter.ConnectWrite(this.activeTime, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.activeTime = ByteConverter.ConnectRead(sByte, num2, num3);
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
			ByteConverter.ConnectWrite((int)this.evtType, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.evtType = (TimerEvent.Type)ByteConverter.ConnectRead(sByte, num2, num3);
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
			ByteConverter.ConnectWrite(this.evtId, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.evtId = ByteConverter.ConnectRead(sByte, num2, num3);
			num2 += num3;
		}
		int num4;
		if (write)
		{
			if (readSize)
			{
				num4 = 100;
			}
			else
			{
				num4 = this.evtValue.Count;
			}
		}
		else
		{
			num4 = ByteConverter.ConnectRead(sByte, num2, 4);
		}
		int num5 = 4;
		int num6 = 4;
		num3 = 4 + 100 * num5;
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
				ByteConverter.ConnectWrite(this.evtValue[i], list[list.Count - 1], num6, num5);
				i++;
				num6 += num5;
			}
		}
		else
		{
			this.evtValue = new List<int>();
			int i = 0;
			while (i < num4)
			{
				this.evtValue.Add(ByteConverter.ConnectRead(sByte, num2 + num6, num5));
				i++;
				num6 += num5;
			}
			num2 += num3;
		}
		num4 = 7;
		num5 = 4;
		num6 = 4;
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
					ByteConverter.ConnectWrite(this.addTime.Year, list[list.Count - 1], num6, num5);
					break;
				case 1:
					ByteConverter.ConnectWrite(this.addTime.Month, list[list.Count - 1], num6, num5);
					break;
				case 2:
					ByteConverter.ConnectWrite(this.addTime.Day, list[list.Count - 1], num6, num5);
					break;
				case 3:
					ByteConverter.ConnectWrite(this.addTime.Hour, list[list.Count - 1], num6, num5);
					break;
				case 4:
					ByteConverter.ConnectWrite(this.addTime.Minute, list[list.Count - 1], num6, num5);
					break;
				case 5:
					ByteConverter.ConnectWrite(this.addTime.Second, list[list.Count - 1], num6, num5);
					break;
				case 6:
					ByteConverter.ConnectWrite(this.addTime.Millisecond, list[list.Count - 1], num6, num5);
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
			this.addTime = new DateTime(array[0], array[1], array[2], array[3], array[4], array[5], array[6]);
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
			ByteConverter.ConnectWrite(Convert.ToInt32(this.trigger), list[list.Count - 1], 0, num3);
		}
		else
		{
			this.trigger = Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num2, num3));
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

	// Token: 0x040003E4 RID: 996
	public int id;

	// Token: 0x040003E5 RID: 997
	public int timeSpanSec;

	// Token: 0x040003E6 RID: 998
	public int activeTime;

	// Token: 0x040003E7 RID: 999
	public TimerEvent.Type evtType;

	// Token: 0x040003E8 RID: 1000
	public int evtId;

	// Token: 0x040003E9 RID: 1001
	public List<int> evtValue;

	// Token: 0x040003EA RID: 1002
	public DateTime addTime;

	// Token: 0x040003EB RID: 1003
	public bool trigger;
}
