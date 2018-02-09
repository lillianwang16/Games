using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public class ObjectMaster_MainIn : ObjectMaster
{
	// Token: 0x06000503 RID: 1283 RVA: 0x0002228D File Offset: 0x0002068D
	public override void Object_Awake()
	{
	}

	// Token: 0x06000504 RID: 1284 RVA: 0x0002228F File Offset: 0x0002068F
	public override void Object_Start()
	{
		this.Frog.GetComponent<Frog>().CreateCheck();
	}

	// Token: 0x06000505 RID: 1285 RVA: 0x000222A1 File Offset: 0x000206A1
	public override void Object_Update()
	{
	}

	// Token: 0x06000506 RID: 1286 RVA: 0x000222A3 File Offset: 0x000206A3
	public override void Object_FixedUpdate()
	{
	}

	// Token: 0x06000507 RID: 1287 RVA: 0x000222A5 File Offset: 0x000206A5
	public override void Object_OnDisable()
	{
	}

	// Token: 0x06000508 RID: 1288 RVA: 0x000222A7 File Offset: 0x000206A7
	public override void Object_OnPouse()
	{
	}

	// Token: 0x06000509 RID: 1289 RVA: 0x000222A9 File Offset: 0x000206A9
	public override void Object_OnResume()
	{
	}

	// Token: 0x0600050A RID: 1290 RVA: 0x000222AB File Offset: 0x000206AB
	public override void Object_ApplicationQuit()
	{
	}

	// Token: 0x040004D0 RID: 1232
	[Space(10f)]
	public GameObject Frog;

	// Token: 0x040004D1 RID: 1233
	public GameObject Tutorial_Frog;
}
