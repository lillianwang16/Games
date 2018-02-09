using System;
using UnityEngine;
using UnityEngine.SceneManagement;

// Token: 0x020000D1 RID: 209
public class GameMaster : MonoBehaviour
{
	// Token: 0x0600058A RID: 1418 RVA: 0x0002152D File Offset: 0x0001F92D
	public virtual void Master_Awake()
	{
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x0002152F File Offset: 0x0001F92F
	public virtual void Master_Awake_After()
	{
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00021531 File Offset: 0x0001F931
	public virtual void Master_Start()
	{
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00021533 File Offset: 0x0001F933
	public virtual void Master_Start_After()
	{
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00021535 File Offset: 0x0001F935
	public virtual void Master_Update()
	{
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00021537 File Offset: 0x0001F937
	public virtual void Master_Update_After()
	{
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00021539 File Offset: 0x0001F939
	public virtual void Master_FixedUpdate()
	{
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x0002153B File Offset: 0x0001F93B
	public virtual void Master_FixedUpdate_After()
	{
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0002153D File Offset: 0x0001F93D
	public virtual void Master_Disable()
	{
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x0002153F File Offset: 0x0001F93F
	public virtual void Master_Disable_After()
	{
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x00021541 File Offset: 0x0001F941
	public virtual void Master_OnPouse()
	{
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x00021543 File Offset: 0x0001F943
	public virtual void Master_OnPouse_After()
	{
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x00021545 File Offset: 0x0001F945
	public virtual void Master_OnResume()
	{
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x00021547 File Offset: 0x0001F947
	public virtual void Master_OnResume_After()
	{
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x00021549 File Offset: 0x0001F949
	public virtual void Master_ApplicationQuit()
	{
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0002154B File Offset: 0x0001F94B
	public virtual void Master_ApplicationQuit_After()
	{
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x0002154D File Offset: 0x0001F94D
	public virtual void ChangeSceneCall(Scenes _nextScene)
	{
		this.ChangeSceneUpdate(_nextScene);
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00021556 File Offset: 0x0001F956
	public virtual void ChangeScene(Scenes _nextScene)
	{
		this.nextScene = _nextScene;
		this.Disable();
		SuperGameMaster.setNextScene(this.nextScene);
		this.nowSceneChanging = true;
		this.callSceneChange = false;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x0002157E File Offset: 0x0001F97E
	public virtual void ChangeSceneUpdate(Scenes _nextScene)
	{
		if (this.nowSceneChanging)
		{
			Debug.Log("[GameMaster] シーン呼び出し中に ChangeScene が呼ばれました！");
			return;
		}
		this.nextScene = _nextScene;
		this.callSceneChange = true;
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x000215A4 File Offset: 0x0001F9A4
	public virtual void StartThisScene(Scenes _Scene)
	{
		SuperGameMaster.StartScene = _Scene;
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x000215AC File Offset: 0x0001F9AC
	public virtual void OnSave()
	{
		this.onSave = true;
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x000215B5 File Offset: 0x0001F9B5
	public virtual void NotSave()
	{
		this.onSave = false;
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x000215C0 File Offset: 0x0001F9C0
	public virtual void Awake()
	{
		this.Master_Awake();
		this.UIMaster.GetComponent<UIMaster>().UI_AwakeLoop();
		this.ObjectMaster.GetComponent<ObjectMaster>().Object_AwakeLoop();
		this.Master_Awake_After();
		if (!GameObject.Find("SuperGameMaster"))
		{
			SceneManager.LoadScene("InitScene");
			this.nowSceneChanging = true;
		}
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0002161E File Offset: 0x0001FA1E
	public virtual void Start()
	{
		this.Master_Start();
		this.UIMaster.GetComponent<UIMaster>().UI_StartLoop();
		this.ObjectMaster.GetComponent<ObjectMaster>().Object_StartLoop();
		this.Master_Start_After();
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0002164C File Offset: 0x0001FA4C
	public virtual void Update()
	{
		this.Master_Update();
		this.UIMaster.GetComponent<UIMaster>().UI_UpdateLoop();
		if (!this.UIMaster.GetComponent<UIMaster>().get_ObjectFreeze_Update())
		{
			this.ObjectMaster.GetComponent<ObjectMaster>().Object_UpdateLoop();
		}
		this.Master_Update_After();
		if (this.callSceneChange)
		{
			this.ChangeScene(this.nextScene);
		}
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x000216B4 File Offset: 0x0001FAB4
	public virtual void FixedUpdate()
	{
		this.Master_FixedUpdate();
		this.UIMaster.GetComponent<UIMaster>().UI_FixedUpdateLoop();
		if (!this.UIMaster.GetComponent<UIMaster>().get_ObjectFreeze_Update())
		{
			this.ObjectMaster.GetComponent<ObjectMaster>().Object_FixedUpdateLoop();
		}
		this.Master_FixedUpdate_After();
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x00021704 File Offset: 0x0001FB04
	public virtual void Disable()
	{
		this.Master_Disable();
		this.UIMaster.GetComponent<UIMaster>().UI_OnDisableLoop();
		this.ObjectMaster.GetComponent<ObjectMaster>().Object_OnDisableLoop();
		this.Master_Disable_After();
		if (this.onSave)
		{
			SuperGameMaster.SaveData();
		}
		this.onSave = false;
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x00021754 File Offset: 0x0001FB54
	public virtual void OnPouse()
	{
		this.Master_OnPouse();
		this.UIMaster.GetComponent<UIMaster>().UI_OnPouseLoop();
		this.ObjectMaster.GetComponent<ObjectMaster>().Object_OnPouseLoop();
		this.Master_OnPouse_After();
		if (this.onSave)
		{
			SuperGameMaster.SaveData();
		}
		this.onSave = false;
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x000217A4 File Offset: 0x0001FBA4
	public virtual void OnResume()
	{
		this.Master_OnResume();
		this.UIMaster.GetComponent<UIMaster>().UI_OnResumeLoop();
		this.ObjectMaster.GetComponent<ObjectMaster>().Object_OnResumeLoop();
		this.Master_OnResume_After();
		if (this.onSave)
		{
			SuperGameMaster.SaveData();
		}
		this.onSave = false;
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x000217F4 File Offset: 0x0001FBF4
	public virtual void ApplicationQuit()
	{
		this.Master_ApplicationQuit();
		this.UIMaster.GetComponent<UIMaster>().UI_ApplicationQuitLoop();
		this.ObjectMaster.GetComponent<ObjectMaster>().Object_ApplicationQuitLoop();
		this.Master_ApplicationQuit_After();
		if (this.onSave)
		{
			SuperGameMaster.SaveData();
		}
		this.onSave = false;
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x00021844 File Offset: 0x0001FC44
	public virtual void OnApplicationQuit()
	{
		if (!SuperGameMaster.nowLoading)
		{
			this.ApplicationQuit();
		}
		Debug.Log("[GameMaster] ゲームが終了しました");
	}

	// Token: 0x060005A9 RID: 1449 RVA: 0x00021860 File Offset: 0x0001FC60
	public virtual void OnApplicationPause(bool pauseStatus)
	{
		if (GameObject.Find("SuperGameMaster"))
		{
			if (pauseStatus)
			{
				if (!SuperGameMaster.nowLoading)
				{
					this.OnPouse();
				}
				Debug.Log("[GameMaster] ゲームが中断しました");
			}
			else
			{
				if (!SuperGameMaster.nowLoading)
				{
					this.OnResume();
				}
				Debug.Log("[GameMaster] ゲームが復帰しました");
			}
		}
	}

	// Token: 0x04000512 RID: 1298
	public GameObject UIMaster;

	// Token: 0x04000513 RID: 1299
	public GameObject ObjectMaster;

	// Token: 0x04000514 RID: 1300
	protected Scenes nextScene;

	// Token: 0x04000515 RID: 1301
	protected bool nowSceneChanging;

	// Token: 0x04000516 RID: 1302
	protected bool callSceneChange;

	// Token: 0x04000517 RID: 1303
	protected bool onSave;
}
