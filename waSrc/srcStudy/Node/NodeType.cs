using System;

namespace Node
{
	// Token: 0x0200007B RID: 123
	[Serializable]
	public enum NodeType
	{
		// Token: 0x0400032A RID: 810
		Start = -1,
		// Token: 0x0400032B RID: 811
		NONE,
		// Token: 0x0400032C RID: 812
		Goal,
		// Token: 0x0400032D RID: 813
		Path,
		// Token: 0x0400032E RID: 814
		Detour,
		// Token: 0x0400032F RID: 815
		_NodeTypeMax
	}
}
