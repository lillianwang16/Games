using System;
using UnityEngine;

// Token: 0x020000D2 RID: 210
public class ObjectMaster : MonoBehaviour
{
	// Token: 0x060005AB RID: 1451 RVA: 0x000221F8 File Offset: 0x000205F8
	public virtual void Object_Awake()
	{
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x000221FA File Offset: 0x000205FA
	public virtual void Object_Start()
	{
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x000221FC File Offset: 0x000205FC
	public virtual void Object_Update()
	{
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x000221FE File Offset: 0x000205FE
	public virtual void Object_FixedUpdate()
	{
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x00022200 File Offset: 0x00020600
	public virtual void Object_OnDisable()
	{
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x00022202 File Offset: 0x00020602
	public virtual void Object_OnPouse()
	{
	}

	// Token: 0x060005B1 RID: 1457 RVA: 0x00022204 File Offset: 0x00020604
	public virtual void Object_OnResume()
	{
	}

	// Token: 0x060005B2 RID: 1458 RVA: 0x00022206 File Offset: 0x00020606
	public virtual void Object_ApplicationQuit()
	{
	}

	// Token: 0x060005B3 RID: 1459 RVA: 0x00022208 File Offset: 0x00020608
	public virtual void changeScene(Scenes _nextScene)
	{
		this.GameMaster.GetComponent<GameMaster>().ChangeSceneCall(_nextScene);
	}

	// Token: 0x060005B4 RID: 1460 RVA: 0x0002221B File Offset: 0x0002061B
	public virtual void OnSave()
	{
		this.GameMaster.GetComponent<GameMaster>().OnSave();
	}

	// Token: 0x060005B5 RID: 1461 RVA: 0x0002222D File Offset: 0x0002062D
	public virtual void Object_AwakeLoop()
	{
		this.Object_Awake();
	}

	// Token: 0x060005B6 RID: 1462 RVA: 0x00022235 File Offset: 0x00020635
	public virtual void Object_StartLoop()
	{
		this.Object_Start();
	}

	// Token: 0x060005B7 RID: 1463 RVA: 0x0002223D File Offset: 0x0002063D
	public virtual void Object_UpdateLoop()
	{
		this.Object_Update();
	}

	// Token: 0x060005B8 RID: 1464 RVA: 0x00022245 File Offset: 0x00020645
	public virtual void Object_FixedUpdateLoop()
	{
		this.Object_FixedUpdate();
	}

	// Token: 0x060005B9 RID: 1465 RVA: 0x0002224D File Offset: 0x0002064D
	public virtual void Object_OnDisableLoop()
	{
		this.Object_OnDisable();
	}

	// Token: 0x060005BA RID: 1466 RVA: 0x00022255 File Offset: 0x00020655
	public virtual void Object_OnPouseLoop()
	{
		this.Object_OnPouse();
	}

	// Token: 0x060005BB RID: 1467 RVA: 0x0002225D File Offset: 0x0002065D
	public virtual void Object_OnResumeLoop()
	{
		this.Object_OnResume();
	}

	// Token: 0x060005BC RID: 1468 RVA: 0x00022265 File Offset: 0x00020665
	public virtual void Object_ApplicationQuitLoop()
	{
		this.Object_ApplicationQuit();
	}

	// Token: 0x04000518 RID: 1304
	public GameObject GameMaster;
}
