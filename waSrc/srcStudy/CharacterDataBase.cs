using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000097 RID: 151
[Serializable]
public class CharacterDataBase : ScriptableObject
{
	// Token: 0x04000407 RID: 1031
	public List<int> rowItemId;

	// Token: 0x04000408 RID: 1032
	public List<CharacterDataFormat> data;
}
