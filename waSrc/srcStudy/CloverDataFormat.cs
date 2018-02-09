using System;
using System.Collections.Generic;

// Token: 0x02000076 RID: 118
[Serializable]
public class CloverDataFormat
{
	// Token: 0x06000403 RID: 1027 RVA: 0x0001A644 File Offset: 0x00018A44
	public CloverDataFormat(CloverDataFormat original)
	{
		this.x = original.x;
		this.y = original.y;
		this.element = original.element;
		this.spriteNum = original.spriteNum;
		this.point = original.point;
		this.lastHarvest = new DateTime(original.lastHarvest.Year, original.lastHarvest.Month, original.lastHarvest.Day, original.lastHarvest.Hour, original.lastHarvest.Minute, original.lastHarvest.Second, original.lastHarvest.Millisecond);
		this.timeSpanSec = original.timeSpanSec;
		this.newFlag = original.newFlag;
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x0001A703 File Offset: 0x00018B03
	public CloverDataFormat()
	{
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x0001A70B File Offset: 0x00018B0B
	public int UseByte()
	{
		return ByteConverter.ConnectRead(this.ConvertBinary(true, null, true), 0, 4);
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x0001A71D File Offset: 0x00018B1D
	public byte[] ConvertBinary(bool write, byte[] sByte)
	{
		return this.ConvertBinary(write, sByte, false);
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x0001A728 File Offset: 0x00018B28
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
			ByteConverter.ConnectWrite(Convert.ToInt32(this.x * 10000f), list[list.Count - 1], 0, num3);
		}
		else
		{
			this.x = (float)ByteConverter.ConnectRead(sByte, num2, num3) / 10000f;
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
			ByteConverter.ConnectWrite(Convert.ToInt32(this.y * 10000f), list[list.Count - 1], 0, num3);
		}
		else
		{
			this.y = (float)ByteConverter.ConnectRead(sByte, num2, num3) / 10000f;
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
			ByteConverter.ConnectWrite(this.element, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.element = ByteConverter.ConnectRead(sByte, num2, num3);
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
			ByteConverter.ConnectWrite(this.spriteNum, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.spriteNum = ByteConverter.ConnectRead(sByte, num2, num3);
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
			ByteConverter.ConnectWrite(this.point, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.point = ByteConverter.ConnectRead(sByte, num2, num3);
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
					ByteConverter.ConnectWrite(this.lastHarvest.Year, list[list.Count - 1], num6, num5);
					break;
				case 1:
					ByteConverter.ConnectWrite(this.lastHarvest.Month, list[list.Count - 1], num6, num5);
					break;
				case 2:
					ByteConverter.ConnectWrite(this.lastHarvest.Day, list[list.Count - 1], num6, num5);
					break;
				case 3:
					ByteConverter.ConnectWrite(this.lastHarvest.Hour, list[list.Count - 1], num6, num5);
					break;
				case 4:
					ByteConverter.ConnectWrite(this.lastHarvest.Minute, list[list.Count - 1], num6, num5);
					break;
				case 5:
					ByteConverter.ConnectWrite(this.lastHarvest.Second, list[list.Count - 1], num6, num5);
					break;
				case 6:
					ByteConverter.ConnectWrite(this.lastHarvest.Millisecond, list[list.Count - 1], num6, num5);
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
			this.lastHarvest = new DateTime(array[0], array[1], array[2], array[3], array[4], array[5], array[6]);
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
		num3 = 1;
		if (readSize)
		{
			num += num3;
		}
		else if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(Convert.ToInt32(this.newFlag), list[list.Count - 1], 0, num3);
		}
		else
		{
			this.newFlag = Convert.ToBoolean(ByteConverter.ConnectRead(sByte, num2, num3));
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

	// Token: 0x04000293 RID: 659
	public float x;

	// Token: 0x04000294 RID: 660
	public float y;

	// Token: 0x04000295 RID: 661
	public int element;

	// Token: 0x04000296 RID: 662
	public int spriteNum;

	// Token: 0x04000297 RID: 663
	public int point;

	// Token: 0x04000298 RID: 664
	public DateTime lastHarvest;

	// Token: 0x04000299 RID: 665
	public int timeSpanSec;

	// Token: 0x0400029A RID: 666
	public bool newFlag;
}
