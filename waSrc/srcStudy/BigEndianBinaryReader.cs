using System;
using System.IO;

// Token: 0x02000009 RID: 9
internal class BigEndianBinaryReader : BinaryReader
{
	// Token: 0x06000017 RID: 23 RVA: 0x00002D34 File Offset: 0x00001134
	public BigEndianBinaryReader(Stream input) : base(input)
	{
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00002D40 File Offset: 0x00001140
	public override short ReadInt16()
	{
		byte[] array = this.ReadBytes(2);
		return (short)((int)array[1] + ((int)array[0] << 8));
	}

	// Token: 0x06000019 RID: 25 RVA: 0x00002D60 File Offset: 0x00001160
	public override ushort ReadUInt16()
	{
		byte[] array = this.ReadBytes(2);
		return (ushort)((int)array[1] + ((int)array[0] << 8));
	}

	// Token: 0x0600001A RID: 26 RVA: 0x00002D80 File Offset: 0x00001180
	public override int ReadInt32()
	{
		byte[] array = this.ReadBytes(4);
		return (int)array[3] + ((int)array[2] << 8) + ((int)array[1] << 16) + ((int)array[0] << 24);
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00002DAC File Offset: 0x000011AC
	public override uint ReadUInt32()
	{
		byte[] array = this.ReadBytes(4);
		return (uint)((int)array[3] + ((int)array[2] << 8) + ((int)array[1] << 16) + ((int)array[0] << 24));
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00002DD8 File Offset: 0x000011D8
	public override long ReadInt64()
	{
		byte[] array = this.ReadBytes(8);
		return (long)((int)array[7] + ((int)array[6] << 8) + ((int)array[5] << 16) + ((int)array[4] << 24) + ((int)array[3] << 0) + ((int)array[2] << 8) + ((int)array[1] << 16) + ((int)array[0] << 24));
	}
}
