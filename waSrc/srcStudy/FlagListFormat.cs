using System;

// Token: 0x02000091 RID: 145
[Serializable]
public class FlagListFormat
{
	// Token: 0x0600040F RID: 1039 RVA: 0x0001BC8C File Offset: 0x0001A08C
	public FlagListFormat(FlagListFormat original)
	{
		this.id = original.id;
		this.val = original.val;
	}

	// Token: 0x06000410 RID: 1040 RVA: 0x0001BCAC File Offset: 0x0001A0AC
	public FlagListFormat()
	{
	}

	// Token: 0x040003EC RID: 1004
	public int id;

	// Token: 0x040003ED RID: 1005
	public int val;
}
