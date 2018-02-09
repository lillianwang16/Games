using System;
using System.Text;
using UnityEngine;

// Token: 0x020000D8 RID: 216
public class ByteConverter : MonoBehaviour
{
	// Token: 0x060005E4 RID: 1508 RVA: 0x00023A44 File Offset: 0x00021E44
	public static int ConnectRead(byte[] buffer, int pos, int readByte)
	{
		int num = 0;
		for (int i = 0; i < readByte; i++)
		{
			num <<= 8;
			num += Mathf.Abs((int)(buffer[pos + i] & byte.MaxValue));
		}
		return num;
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x00023A80 File Offset: 0x00021E80
	public static long ConnectReadL(byte[] buffer, int pos, int readByte)
	{
		long num = 0L;
		for (int i = 0; i < readByte; i++)
		{
			num <<= 8;
			num += (long)Mathf.Abs((int)(buffer[pos + i] & byte.MaxValue));
		}
		return num;
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x00023ABC File Offset: 0x00021EBC
	public static string ConnectReadString(byte[] buffer, int pos, int maxLength)
	{
		int num = ByteConverter.ConnectRead(buffer, pos, 2);
		if (num <= 0)
		{
			return null;
		}
		if (num > maxLength)
		{
			return null;
		}
		byte[] array = new byte[num];
		Array.Copy(buffer, pos + 2, array, 0, num);
		ByteConverter.lastReadStringLength = num;
		return Encoding.UTF8.GetString(array);
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x00023B08 File Offset: 0x00021F08
	public static string ConnectReadStringRt(byte[] buffer, int pos)
	{
		string result = null;
		try
		{
			int num = pos;
			while (Mathf.Abs((int)(buffer[pos++] & 255)) != 10)
			{
			}
			result = Encoding.UTF8.GetString(buffer, num, pos - num - 1);
			ByteConverter.lastReadStringLength = pos - num;
		}
		catch (UnityException ex)
		{
		}
		return result;
	}

	// Token: 0x060005E8 RID: 1512 RVA: 0x00023B70 File Offset: 0x00021F70
	public static int ConnectWrite(int src, byte[] buffer, int pos, int writeByte)
	{
		for (int i = 0; i < writeByte; i++)
		{
			int num = src >> (writeByte - (i + 1)) * 8 & 255;
			buffer[pos + i] = (byte)num;
		}
		return writeByte;
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00023BAC File Offset: 0x00021FAC
	public static int ConnectWriteL(long src, byte[] buffer, int pos, int writeByte)
	{
		for (int i = 0; i < writeByte; i++)
		{
			int num = (int)(src >> (writeByte - (i + 1)) * 8 & 255L);
			buffer[pos + i] = (byte)num;
		}
		return writeByte;
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x00023BEC File Offset: 0x00021FEC
	public static int ConnectWriteString(string str, byte[] buffer, int pos)
	{
		if (str == null || str == string.Empty)
		{
			ByteConverter.ConnectWrite(0, buffer, pos, 2);
			return 2;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(str);
		ByteConverter.ConnectWrite(bytes.Length, buffer, pos, 2);
		Array.Copy(bytes, 0, buffer, pos + 2, bytes.Length);
		return bytes.Length + 2;
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x00023C48 File Offset: 0x00022048
	public static int ConnectWriteString(string str, byte[] buffer, int pos, int limit)
	{
		if (str == null || str == string.Empty)
		{
			ByteConverter.ConnectWrite(0, buffer, pos, 2);
			return 2;
		}
		byte[] bytes = Encoding.UTF8.GetBytes(str);
		int num = bytes.Length;
		if (num + 2 > limit)
		{
			num = limit - 2;
		}
		ByteConverter.ConnectWrite(num, buffer, pos, 2);
		Array.Copy(bytes, 0, buffer, pos + 2, num);
		return num + 2;
	}

	// Token: 0x0400051D RID: 1309
	public static int lastReadStringLength;
}
