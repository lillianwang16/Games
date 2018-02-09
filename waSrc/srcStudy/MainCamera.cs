using System;
using UnityEngine;

// Token: 0x020000E4 RID: 228
public class MainCamera : MonoBehaviour
{
	// Token: 0x0600063D RID: 1597 RVA: 0x00024B74 File Offset: 0x00022F74
	private void Awake()
	{
		float num = (float)Screen.width / 640f / ((float)Screen.height / 960f);
		float orthographicSize = Camera.main.orthographicSize;
		if (num <= 1f)
		{
			if (num < 1f)
			{
				float num2 = 1f / num;
				Camera.main.orthographicSize = orthographicSize * num2;
			}
		}
	}
}
