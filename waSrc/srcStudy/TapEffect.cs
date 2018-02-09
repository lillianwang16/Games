using System;
using UnityEngine;

// Token: 0x0200003D RID: 61
public class TapEffect : MonoBehaviour
{
	// Token: 0x06000258 RID: 600 RVA: 0x00007BA0 File Offset: 0x00005FA0
	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 position = Input.mousePosition;
			position = Camera.main.ScreenToWorldPoint(position);
			position.z = this.effectPos_Z;
			base.transform.position = position;
			base.GetComponent<ParticleSystem>().Emit(1);
		}
	}

	// Token: 0x0400010D RID: 269
	public float effectPos_Z;
}
