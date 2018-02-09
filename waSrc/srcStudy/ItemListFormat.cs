using System;
using System.Collections.Generic;

// Token: 0x02000092 RID: 146
[Serializable]
public class ItemListFormat
{
	// Token: 0x06000411 RID: 1041 RVA: 0x0001BCB4 File Offset: 0x0001A0B4
	public ItemListFormat(ItemListFormat original)
	{
		this.id = original.id;
		this.stock = original.stock;
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x0001BCD4 File Offset: 0x0001A0D4
	public ItemListFormat()
	{
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x0001BCDC File Offset: 0x0001A0DC
	public int UseByte()
	{
		return ByteConverter.ConnectRead(this.ConvertBinary(true, null, true), 0, 4);
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x0001BCEE File Offset: 0x0001A0EE
	public byte[] ConvertBinary(bool write, byte[] sByte)
	{
		return this.ConvertBinary(write, sByte, false);
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x0001BCFC File Offset: 0x0001A0FC
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
			ByteConverter.ConnectWrite(this.stock, list[list.Count - 1], 0, num3);
		}
		else
		{
			this.stock = ByteConverter.ConnectRead(sByte, num2, num3);
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
			int num4 = 0;
			foreach (byte[] array in list)
			{
				num4 += array.Length;
			}
			sByte = new byte[num4];
			foreach (byte[] array2 in list)
			{
				Buffer.BlockCopy(array2, 0, sByte, num2, array2.Length);
				num2 += array2.Length;
			}
			return sByte;
		}
		return null;
	}

	// Token: 0x040003EE RID: 1006
	public int id;

	// Token: 0x040003EF RID: 1007
	public int stock;
}
