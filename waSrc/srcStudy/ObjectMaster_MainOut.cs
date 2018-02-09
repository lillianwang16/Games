using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000C7 RID: 199
public class ObjectMaster_MainOut : ObjectMaster
{
	// Token: 0x0600050C RID: 1292 RVA: 0x000222D6 File Offset: 0x000206D6
	public override void Object_Awake()
	{
	}

	// Token: 0x0600050D RID: 1293 RVA: 0x000222D8 File Offset: 0x000206D8
	public override void Object_Start()
	{
		this.CloverFarm.GetComponent<CloverFarm>().checkCloverCreate();
		this.CloverFarm.GetComponent<CloverFarm>().SaveClover();
		this.S_FlickChecker = base.GetComponent<FlickCheaker>();
		if (SuperGameMaster.tutorial.TutorialComplete())
		{
			this.Table.GetComponent<CharaTable>().Init();
		}
		this.frontBackMain.transform.localPosition = new Vector3(-this.MainCamera.transform.localPosition.x / this.MainOut_width * (this.sideX * this.frontGround_width - this.sideX), this.frontBackMain.transform.localPosition.y, this.frontBackMain.transform.localPosition.z);
		foreach (GameObject gameObject in this.frontObjects)
		{
			gameObject.transform.localPosition = new Vector3(gameObject.transform.localPosition.x * this.frontGround_width, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
		}
	}

	// Token: 0x0600050E RID: 1294 RVA: 0x00022448 File Offset: 0x00020848
	public override void Object_Update()
	{
		this.ScrollAndCloverCheck();
		this.CloverFarm.GetComponent<CloverFarm>().CloverProc();
	}

	// Token: 0x0600050F RID: 1295 RVA: 0x00022460 File Offset: 0x00020860
	public override void Object_FixedUpdate()
	{
	}

	// Token: 0x06000510 RID: 1296 RVA: 0x00022462 File Offset: 0x00020862
	public override void Object_OnDisable()
	{
		this.SaveObject();
	}

	// Token: 0x06000511 RID: 1297 RVA: 0x0002246A File Offset: 0x0002086A
	public override void Object_OnPouse()
	{
		this.SaveObject();
	}

	// Token: 0x06000512 RID: 1298 RVA: 0x00022472 File Offset: 0x00020872
	public override void Object_OnResume()
	{
	}

	// Token: 0x06000513 RID: 1299 RVA: 0x00022474 File Offset: 0x00020874
	public override void Object_ApplicationQuit()
	{
		this.SaveObject();
	}

	// Token: 0x06000514 RID: 1300 RVA: 0x0002247C File Offset: 0x0002087C
	private void SaveObject()
	{
		this.CloverFarm.GetComponent<CloverFarm>().SaveClover();
		this.GameMaster.GetComponent<GameMaster>().OnSave();
	}

	// Token: 0x06000515 RID: 1301 RVA: 0x000224A0 File Offset: 0x000208A0
	public void ScrollAndCloverCheck()
	{
		base.GetComponent<FlickCheaker>().FlickUpdate();
		if (Input.GetMouseButton(0))
		{
			Vector2 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			if (this.CloverFarm.GetComponent<CloverFarm>().CheckHitClover(mousePoint))
			{
				this.hitFlickClover = true;
			}
			if (this.S_FlickChecker.nowFlickVector() != Vector2.zero)
			{
				Vector3 position = this.S_FlickChecker.nowFlickVector();
				this.flickMoveVector -= Camera.main.ScreenToWorldPoint(this.flickMoveBefore).x - Camera.main.ScreenToWorldPoint(position).x;
				this.flickMoveBefore = position;
			}
		}
		if (Input.GetMouseButtonDown(0))
		{
		}
		if (Input.GetMouseButtonUp(0))
		{
			this.flickMoveBefore = Vector3.zero;
			if (this.hitFlickClover)
			{
				this.flickMoveVector = 0f;
			}
			this.hitFlickClover = false;
		}
		if (!this.hitFlickClover)
		{
			this.FlickMove();
		}
	}

	// Token: 0x06000516 RID: 1302 RVA: 0x000225AF File Offset: 0x000209AF
	public void Call_FlickMove()
	{
		this.Call_FlickMove(Vector2.zero);
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x000225BC File Offset: 0x000209BC
	public void Call_FlickMove(Vector2 setVector)
	{
		if (setVector == Vector2.zero)
		{
			if (this.S_FlickChecker.nowFlickVector() != Vector2.zero)
			{
				Vector3 position = this.S_FlickChecker.nowFlickVector();
				this.flickMoveVector -= Camera.main.ScreenToWorldPoint(this.flickMoveBefore).x - Camera.main.ScreenToWorldPoint(position).x;
				this.flickMoveBefore = position;
			}
		}
		else
		{
			this.flickMoveVector -= setVector.x;
		}
		this.FlickMove();
	}

	// Token: 0x06000518 RID: 1304 RVA: 0x00022663 File Offset: 0x00020A63
	public void Call_FlickInit()
	{
		this.flickMoveBefore = Vector3.zero;
		this.flickMoveVector = 0f;
		this.hitFlickClover = false;
	}

	// Token: 0x06000519 RID: 1305 RVA: 0x00022684 File Offset: 0x00020A84
	public void FlickMove()
	{
		if (this.flickMoveVector != 0f)
		{
			Vector3 localPosition = this.MainCamera.transform.localPosition;
			localPosition.x -= this.flickMoveVector;
			if (Mathf.Abs(localPosition.x) > this.MainOut_width)
			{
				localPosition.x = Mathf.Sign(localPosition.x) * this.MainOut_width;
			}
			localPosition.z = -10f;
			this.MainCamera.transform.localPosition = localPosition;
			this.frontBackMain.transform.localPosition = new Vector3(-localPosition.x / this.MainOut_width * (this.sideX * this.frontGround_width - this.sideX), this.frontBackMain.transform.localPosition.y, this.frontBackMain.transform.localPosition.z);
			this.flickMoveVector = 0f;
			if ((double)Mathf.Abs(this.flickMoveVector) < 0.001)
			{
				this.flickMoveVector = 0f;
			}
		}
	}

	// Token: 0x040004D2 RID: 1234
	public GameObject CloverFarm;

	// Token: 0x040004D3 RID: 1235
	public GameObject Post;

	// Token: 0x040004D4 RID: 1236
	public GameObject Table;

	// Token: 0x040004D5 RID: 1237
	public GameObject Door;

	// Token: 0x040004D6 RID: 1238
	public GameObject Frog;

	// Token: 0x040004D7 RID: 1239
	[Space(10f)]
	public GameObject frontBackMain;

	// Token: 0x040004D8 RID: 1240
	public List<GameObject> frontObjects;

	// Token: 0x040004D9 RID: 1241
	public float frontGround_width = 1.2f;

	// Token: 0x040004DA RID: 1242
	[Space(10f)]
	public Camera MainCamera;

	// Token: 0x040004DB RID: 1243
	public float MainOut_width = 1.92f;

	// Token: 0x040004DC RID: 1244
	public float sideX = 5.76f;

	// Token: 0x040004DD RID: 1245
	[Space(10f)]
	public Vector3 flickMoveBefore;

	// Token: 0x040004DE RID: 1246
	public float flickMoveVector;

	// Token: 0x040004DF RID: 1247
	public bool hitFlickClover;

	// Token: 0x040004E0 RID: 1248
	private FlickCheaker S_FlickChecker;
}
