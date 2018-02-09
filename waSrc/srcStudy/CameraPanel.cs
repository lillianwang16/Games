using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200004A RID: 74
public class CameraPanel : MonoBehaviour
{
	// Token: 0x060002A2 RID: 674 RVA: 0x0000A6E4 File Offset: 0x00008AE4
	public void BackFunc()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.PushClose();
		});
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0000A710 File Offset: 0x00008B10
	public void BackFunc_2()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.Result_No();
		});
	}

	// Token: 0x060002A4 RID: 676 RVA: 0x0000A73C File Offset: 0x00008B3C
	public void BackFunc_3()
	{
		UIMaster componentInParent = base.GetComponentInParent<UIMaster>();
		componentInParent.BackFunc_Reset();
		componentInParent.BackFunc_Set(delegate
		{
			this.CloseCamera();
		});
	}

	// Token: 0x060002A5 RID: 677 RVA: 0x0000A768 File Offset: 0x00008B68
	public void OpenCamera()
	{
		if (SuperGameMaster.GetPictureListCount(false) >= 30)
		{
			base.GetComponentInParent<UIMaster>().freezeObject(true);
			base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
			ConfilmPanel confilm = this.ConfilmUI.GetComponent<ConfilmPanel>();
			confilm.OpenPanel_YesNo("写真がいっぱいです。\nさつえいに移動しますか？");
			confilm.ResetOnClick_Yes();
			confilm.SetOnClick_Yes(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.GetComponentInParent<UIMaster>().freezeAll(true);
			});
			confilm.SetOnClick_Yes(delegate
			{
				this.GetComponentInParent<UIMaster>().changeScene(Scenes.Snap);
			});
			confilm.ResetOnClick_No();
			confilm.SetOnClick_No(delegate
			{
				confilm.ClosePanel();
			});
			confilm.SetOnClick_No(delegate
			{
				this.GetComponentInParent<UIMaster>().freezeObject(false);
			});
			confilm.SetOnClick_No(delegate
			{
				this.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
			});
			return;
		}
		base.gameObject.SetActive(true);
		base.GetComponentInParent<UIMaster>().freezeObject(true);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0f));
		this.SnapPanel.SetActive(true);
		this.CameraResultPanel.SetActive(false);
		this.ScreenRect = base.transform.GetComponent<RectTransform>().rect;
		this.cameraPhase = CameraPanel.Phase.Camera;
		this.cameraMode = CameraPanel.Mode.NONE;
		this.modeTimer = 0;
		this.lastTap = 7;
		this.backDist = 0f;
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
		Debug.Log("[CameraPanel] カメラ起動");
		this.BackFunc();
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0000A940 File Offset: 0x00008D40
	public void PushClose()
	{
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		this.CloseCamera();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		Scenes nowScenes = SuperGameMaster.GetNowScenes();
		if (nowScenes != Scenes.MainOut)
		{
			if (nowScenes == Scenes.MainIn)
			{
				base.GetComponentInParent<UIMaster_MainIn>().BackFunc();
			}
		}
		else
		{
			base.GetComponentInParent<UIMaster_MainOut>().BackFunc();
		}
	}

	// Token: 0x060002A7 RID: 679 RVA: 0x0000A9D8 File Offset: 0x00008DD8
	public void CloseCamera()
	{
		base.GetComponentInParent<UIMaster>().freezeObject(false);
		base.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
		base.gameObject.SetActive(false);
		this.SnapPanel.SetActive(false);
		this.CameraResultPanel.SetActive(false);
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
		Scenes nowScenes = SuperGameMaster.GetNowScenes();
		if (nowScenes != Scenes.MainOut)
		{
			if (nowScenes == Scenes.MainIn)
			{
				base.GetComponentInParent<UIMaster_MainIn>().BackFunc();
			}
		}
		else
		{
			base.GetComponentInParent<UIMaster_MainOut>().BackFunc();
		}
	}

	// Token: 0x060002A8 RID: 680 RVA: 0x0000AA8C File Offset: 0x00008E8C
	public void ControllCamera()
	{
		CameraPanel.Phase phase = this.cameraPhase;
		if (phase == CameraPanel.Phase.Camera)
		{
			this.CameraPhase();
		}
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0000AAB8 File Offset: 0x00008EB8
	public void Result_Yes()
	{
		SuperGameMaster.SavePictureList(false, this.saveTex, SuperGameMaster.GetLastDateTime());
		base.GetComponentInParent<UIMaster>().OnSave();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
		this.CameraResult_YesNo.SetActive(false);
		this.CameraResult_Saved.SetActive(true);
		this.BackFunc_3();
	}

	// Token: 0x060002AA RID: 682 RVA: 0x0000AB19 File Offset: 0x00008F19
	public void Result_No()
	{
		this.OpenCamera();
		SuperGameMaster.audioMgr.StopSE();
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
	}

	// Token: 0x060002AB RID: 683 RVA: 0x0000AB44 File Offset: 0x00008F44
	public void CameraPhase()
	{
		if (SuperGameMaster.NowScene == Scenes.MainOut)
		{
			this.ObjectMaster.GetComponent<FlickCheaker>().FlickUpdate();
		}
		int touchCount = Input.touchCount;
		if (touchCount != 1)
		{
			if (touchCount != 2)
			{
				this.modeTimer = 0;
				if (this.cameraMode != CameraPanel.Mode.NONE)
				{
					this.cameraMode = CameraPanel.Mode.NONE;
				}
			}
			else if (this.cameraMode == CameraPanel.Mode.NONE || this.cameraMode == CameraPanel.Mode.Zoom)
			{
				Touch[] array = new Touch[2];
				Vector2[] array2 = new Vector2[2];
				for (int i = 0; i < 2; i++)
				{
					array[i] = Input.GetTouch(i);
					array2[i] = array[i].position;
					float num;
					if (this.ScreenRect.height / 3f > this.ScreenRect.width / 2f)
					{
						num = (float)Screen.width / 640f;
					}
					else
					{
						num = (float)Screen.height / 960f;
					}
					array2[i].x = array[i].position.x / num - this.ScreenRect.width / 2f;
					array2[i].y = array[i].position.y / num - this.ScreenRect.height / 2f;
				}
				CameraPanel.Mode mode = this.cameraMode;
				if (mode != CameraPanel.Mode.NONE)
				{
					if (mode == CameraPanel.Mode.Zoom)
					{
						float num2 = Vector2.Distance(array2[0], array2[1]);
						float d = 1f + (num2 - this.backDist) / 500f;
						this.backDist = num2;
						Vector3 vector = this.MaskCube.transform.localScale;
						vector *= d;
						vector.z = 1f;
						float num3 = vector.x / 240f;
						if (num3 < 1f)
						{
							vector = new Vector3(240f, 160f, 1f);
						}
						if (num3 > 1.5f)
						{
							vector = new Vector3(360f, 240f, 1f);
						}
						this.MaskCube.transform.localScale = vector;
						Vector3 localPosition = this.MaskCube.transform.localPosition;
						if (this.MaskCube.transform.localPosition.x < -this.ScreenRect.width / 2f + this.MaskCube.transform.localScale.x / 2f)
						{
							localPosition.x = -this.ScreenRect.width / 2f + this.MaskCube.transform.localScale.x / 2f;
						}
						if (this.MaskCube.transform.localPosition.y < -this.ScreenRect.height / 2f + this.MaskCube.transform.localScale.y / 2f)
						{
							localPosition.y = -this.ScreenRect.height / 2f + this.MaskCube.transform.localScale.y / 2f;
						}
						if (this.MaskCube.transform.localPosition.x > this.ScreenRect.width / 2f - this.MaskCube.transform.localScale.x / 2f)
						{
							localPosition.x = this.ScreenRect.width / 2f - this.MaskCube.transform.localScale.x / 2f;
						}
						if (this.MaskCube.transform.localPosition.y > this.ScreenRect.height / 2f - this.MaskCube.transform.localScale.y / 2f)
						{
							localPosition.y = this.ScreenRect.height / 2f - this.MaskCube.transform.localScale.y / 2f;
						}
						this.MaskCube.transform.localPosition = localPosition;
					}
				}
				else
				{
					this.cameraMode = CameraPanel.Mode.Zoom;
					this.backDist = Vector2.Distance(array2[0], array2[1]);
				}
				this.modeTimer++;
			}
		}
		else if (this.cameraMode == CameraPanel.Mode.NONE || this.cameraMode == CameraPanel.Mode.Move || this.cameraMode == CameraPanel.Mode.Scroll)
		{
			Touch touch = Input.GetTouch(0);
			Vector2 position = touch.position;
			float num4;
			if (this.ScreenRect.height / 3f > this.ScreenRect.width / 2f)
			{
				num4 = (float)Screen.width / 640f;
			}
			else
			{
				num4 = (float)Screen.height / 960f;
			}
			position.x = touch.position.x / num4 - this.ScreenRect.width / 2f;
			position.y = touch.position.y / num4 - this.ScreenRect.height / 2f;
			CameraPanel.Mode mode2 = this.cameraMode;
			if (mode2 != CameraPanel.Mode.NONE)
			{
				if (mode2 != CameraPanel.Mode.Move)
				{
					if (mode2 == CameraPanel.Mode.Scroll)
					{
						this.ObjectMaster.GetComponent<ObjectMaster_MainOut>().Call_FlickMove();
					}
				}
				else
				{
					this.MaskCube.transform.localPosition = position + this.cameraCenter_diff;
					Vector3 localPosition2 = this.MaskCube.transform.localPosition;
					if (this.MaskCube.transform.localPosition.x < -this.ScreenRect.width / 2f + this.MaskCube.transform.localScale.x / 2f)
					{
						localPosition2.x = -this.ScreenRect.width / 2f + this.MaskCube.transform.localScale.x / 2f;
					}
					if (this.MaskCube.transform.localPosition.y < -this.ScreenRect.height / 2f + this.MaskCube.transform.localScale.y / 2f)
					{
						localPosition2.y = -this.ScreenRect.height / 2f + this.MaskCube.transform.localScale.y / 2f;
					}
					if (this.MaskCube.transform.localPosition.x > this.ScreenRect.width / 2f - this.MaskCube.transform.localScale.x / 2f)
					{
						localPosition2.x = this.ScreenRect.width / 2f - this.MaskCube.transform.localScale.x / 2f;
					}
					if (this.MaskCube.transform.localPosition.y > this.ScreenRect.height / 2f - this.MaskCube.transform.localScale.y / 2f)
					{
						localPosition2.y = this.ScreenRect.height / 2f - this.MaskCube.transform.localScale.y / 2f;
					}
					if (SuperGameMaster.NowScene == Scenes.MainOut && localPosition2.x != this.MaskCube.transform.localPosition.x)
					{
						float num5 = localPosition2.x - this.MaskCube.transform.localPosition.x;
						float num6;
						if (this.ScreenRect.height / 3f > this.ScreenRect.width / 2f)
						{
							num6 = (float)Screen.width / 640f;
						}
						else
						{
							num6 = (float)Screen.height / 960f;
						}
						num5 *= num6;
						this.ObjectMaster.GetComponent<ObjectMaster_MainOut>().Call_FlickMove(new Vector2(-num5 / 5000f, 0f));
					}
					this.MaskCube.transform.localPosition = localPosition2;
				}
			}
			else
			{
				if (this.modeTimer >= 3)
				{
					if (Mathf.Abs(position.x - this.MaskCube.transform.localPosition.x) < this.MaskCube.transform.localScale.x / 2f && Mathf.Abs(position.y - this.MaskCube.transform.localPosition.y) < this.MaskCube.transform.localScale.y / 2f)
					{
						this.cameraMode = CameraPanel.Mode.Move;
						this.cameraCenter_diff.x = this.MaskCube.transform.localPosition.x - position.x;
						this.cameraCenter_diff.y = this.MaskCube.transform.localPosition.y - position.y;
						if (SuperGameMaster.NowScene == Scenes.MainOut)
						{
							this.ObjectMaster.GetComponent<ObjectMaster_MainOut>().Call_FlickInit();
						}
					}
					else if (SuperGameMaster.NowScene == Scenes.MainIn)
					{
						this.cameraMode = CameraPanel.Mode.NoAct;
					}
					else
					{
						this.ObjectMaster.GetComponent<ObjectMaster_MainOut>().Call_FlickInit();
						this.cameraMode = CameraPanel.Mode.Scroll;
					}
				}
				if (touch.phase == TouchPhase.Began && this.lastTap <= 6)
				{
					this.MaskCube.transform.localPosition = position;
					this.lastTap = 7;
					this.cameraMode = CameraPanel.Mode.Move;
					this.cameraCenter_diff = Vector2.zero;
					if (SuperGameMaster.NowScene == Scenes.MainOut)
					{
						this.ObjectMaster.GetComponent<ObjectMaster_MainOut>().Call_FlickInit();
					}
				}
			}
			this.lastTap = -1;
			this.modeTimer++;
		}
		this.lastTap++;
	}

	// Token: 0x060002AC RID: 684 RVA: 0x0000B678 File Offset: 0x00009A78
	public void ShotInit()
	{
		this.cameraPhase = CameraPanel.Phase.Shot;
		foreach (GameObject gameObject in this.DesableList)
		{
			gameObject.SetActive(false);
		}
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Camera"]);
		base.StartCoroutine("ReadScreenshot");
	}

	// Token: 0x060002AD RID: 685 RVA: 0x0000B700 File Offset: 0x00009B00
	public void FinishScreenShot()
	{
		foreach (GameObject gameObject in this.DesableList)
		{
			gameObject.SetActive(true);
		}
		this.SnapPanel.SetActive(false);
		this.CameraResultPanel.SetActive(true);
		this.CameraResult_YesNo.SetActive(true);
		this.CameraResult_Saved.SetActive(false);
		base.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.PanelImage.GetComponent<Image>().sprite = Sprite.Create(this.saveTex, new Rect(0f, 0f, (float)this.saveTex.width, (float)this.saveTex.height), Vector2.zero);
		this.PanelImage.GetComponent<RectTransform>().sizeDelta = new Vector2((float)this.saveTex.width, (float)this.saveTex.height);
		Debug.Log(this.saveTex.width + ", " + this.saveTex.height);
		this.cameraPhase = CameraPanel.Phase.Result;
		this.BackFunc_2();
	}

	// Token: 0x060002AE RID: 686 RVA: 0x0000B864 File Offset: 0x00009C64
	private IEnumerator ReadScreenshot()
	{
		yield return new WaitForEndOfFrame();
		Rect saveRect = default(Rect);
		float num;
		if (this.ScreenRect.height / 3f > this.ScreenRect.width / 2f)
		{
			num = (float)Screen.width / 640f;
		}
		else
		{
			num = (float)Screen.height / 960f;
		}
		saveRect.width = this.MaskCube.transform.localScale.x * num;
		saveRect.height = this.MaskCube.transform.localScale.y * num;
		saveRect.x = this.MaskCube.transform.localPosition.x * num - saveRect.width / 2f + (float)(Screen.width / 2);
		saveRect.y = this.MaskCube.transform.localPosition.y * num - saveRect.height / 2f + (float)(Screen.height / 2);
		Debug.Log(string.Concat(new object[]
		{
			"[CameraPanel] 写真保存：saveRect：",
			saveRect,
			" / (ReSizeRect) MaskCube：",
			this.MaskCube.transform.localPosition,
			",",
			this.MaskCube.transform.localScale
		}));
		this.saveTex = new Texture2D((int)saveRect.width, (int)saveRect.height, TextureFormat.RGB24, false);
		this.saveTex.ReadPixels(saveRect, 0, 0);
		this.saveTex.Apply();
		yield return new WaitForEndOfFrame();
		TextureScale.Bilinear(this.saveTex, (int)this.MaskCube.transform.localScale.x, (int)this.MaskCube.transform.localScale.y);
		this.FinishScreenShot();
		yield break;
	}

	// Token: 0x060002AF RID: 687 RVA: 0x0000B880 File Offset: 0x00009C80
	public void PushTwitter()
	{
		SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Enter"]);
		Scenes nowScenes = SuperGameMaster.GetNowScenes();
		if (nowScenes != Scenes.MainOut)
		{
			if (nowScenes == Scenes.MainIn)
			{
				base.GetComponentInParent<UIMaster_MainIn>().GameMaster.GetComponent<GameMaster_MainIn>().SetReloadTimer(2f);
			}
		}
		else
		{
			base.GetComponentInParent<UIMaster_MainOut>().GameMaster.GetComponent<GameMaster_MainOut>().SetReloadTimer(2f);
		}
	}

	// Token: 0x04000149 RID: 329
	public GameObject ConfilmUI;

	// Token: 0x0400014A RID: 330
	public GameObject ObjectMaster;

	// Token: 0x0400014B RID: 331
	public GameObject SnapPanel;

	// Token: 0x0400014C RID: 332
	public GameObject MaskCube;

	// Token: 0x0400014D RID: 333
	public GameObject CameraResultPanel;

	// Token: 0x0400014E RID: 334
	public GameObject CameraResult_YesNo;

	// Token: 0x0400014F RID: 335
	public GameObject CameraResult_Saved;

	// Token: 0x04000150 RID: 336
	public GameObject PanelImage;

	// Token: 0x04000151 RID: 337
	public GameObject PanelText;

	// Token: 0x04000152 RID: 338
	public List<GameObject> DesableList;

	// Token: 0x04000153 RID: 339
	public CameraPanel.Phase cameraPhase;

	// Token: 0x04000154 RID: 340
	public CameraPanel.Mode cameraMode;

	// Token: 0x04000155 RID: 341
	private int modeTimer;

	// Token: 0x04000156 RID: 342
	private int lastTap;

	// Token: 0x04000157 RID: 343
	private const int TapCheckWait = 3;

	// Token: 0x04000158 RID: 344
	private const int TapWait = 6;

	// Token: 0x04000159 RID: 345
	public Vector2 cameraCenter_diff;

	// Token: 0x0400015A RID: 346
	private Rect ScreenRect;

	// Token: 0x0400015B RID: 347
	private float backDist;

	// Token: 0x0400015C RID: 348
	public Texture2D saveTex;

	// Token: 0x0200004B RID: 75
	public enum Phase
	{
		// Token: 0x0400015E RID: 350
		Camera,
		// Token: 0x0400015F RID: 351
		Shot,
		// Token: 0x04000160 RID: 352
		Result
	}

	// Token: 0x0200004C RID: 76
	public enum Mode
	{
		// Token: 0x04000162 RID: 354
		NONE = -1,
		// Token: 0x04000163 RID: 355
		Move,
		// Token: 0x04000164 RID: 356
		Scroll,
		// Token: 0x04000165 RID: 357
		Zoom,
		// Token: 0x04000166 RID: 358
		NoAct
	}
}
