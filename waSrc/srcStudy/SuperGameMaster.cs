using System;
using System.Collections;
using System.Collections.Generic;
using Flag;
using Item;
using Picture;
using Support;
using Tutorial;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Token: 0x020000EA RID: 234
public class SuperGameMaster : MonoBehaviour
{
	// Token: 0x060006CE RID: 1742 RVA: 0x00029554 File Offset: 0x00027954
	private void Awake()
	{
		Debug.logger.logEnabled = false;
		if (!SuperGameMaster.create_SuperMaster)
		{
			UnityEngine.Object.DontDestroyOnLoad(this);
			SuperGameMaster.create_SuperMaster = true;
			Application.targetFrameRate = 60;
			SceneManager.activeSceneChanged += this.OnActiveSceneChanged;
			SceneManager.sceneLoaded += this.OnSceneLoaded;
			SceneManager.sceneUnloaded += this.OnSceneUnloaded;
			SuperGameMaster.saveMgr = base.GetComponent<SaveManager>();
			SuperGameMaster.sDataBase = base.GetComponent<ScriptableDataBase>();
			SuperGameMaster.evtMgr = base.GetComponent<EventTimerManager>();
			SuperGameMaster.travel = base.GetComponent<TravelSimulator>();
			SuperGameMaster.picture = base.GetComponent<PictureCreator>();
			SuperGameMaster.audioMgr = base.GetComponent<AudioManager>();
			SuperGameMaster.tutorial = base.GetComponent<TutorialController>();
			SuperGameMaster.admobMgr = base.GetComponent<AdMobManager>();
			SuperGameMaster.audioMgr.Init();
			SuperGameMaster.nowLoading = false;
			base.StartCoroutine(this.initLoading());
		}
		else
		{
			UnityEngine.Object.Destroy(base.gameObject);
		}
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00029644 File Offset: 0x00027A44
	private void Update()
	{
		Scenes nowScene = SuperGameMaster.NowScene;
		if (nowScene != Scenes._Reload)
		{
			if (nowScene == Scenes.InitScene)
			{
				if (!SuperGameMaster.nowLoading)
				{
					SuperGameMaster.setNextScene(SuperGameMaster.StartScene);
				}
			}
		}
		else
		{
			SuperGameMaster.NextScene = Scenes.InitScene;
			SuperGameMaster.nowLoading = false;
			base.StartCoroutine(this.initLoading());
		}
		if (SuperGameMaster.NextScene != Scenes.NONE)
		{
			Scenes nextScene = SuperGameMaster.NextScene;
			switch (nextScene + 1)
			{
			case Scenes.InitScene:
				goto IL_113;
			case Scenes._Reload:
				SceneManager.LoadSceneAsync("InitScene");
				goto IL_113;
			case Scenes._Prev:
			case Scenes.MainOut:
				goto IL_113;
			case Scenes.MainIn:
				SceneManager.LoadSceneAsync("MainOut");
				goto IL_113;
			case Scenes.Shop:
				SceneManager.LoadSceneAsync("MainIn");
				goto IL_113;
			case Scenes.Raffle:
				SceneManager.LoadSceneAsync("Shop");
				goto IL_113;
			case Scenes.Present:
				SceneManager.LoadSceneAsync("Raffle");
				goto IL_113;
			case Scenes.Album:
				SceneManager.LoadSceneAsync("Present");
				goto IL_113;
			case Scenes.Snap:
				break;
			case (Scenes)10:
				break;
			default:
				goto IL_113;
			}
			SceneManager.LoadSceneAsync("Album");
			IL_113:
			SuperGameMaster.NowScene = SuperGameMaster.NextScene;
			SuperGameMaster.NextScene = Scenes.NONE;
		}
		SuperGameMaster.GameTimer += Time.deltaTime;
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00029784 File Offset: 0x00027B84
	private void OnGUI()
	{
		Scenes nowScene = SuperGameMaster.NowScene;
		if (nowScene == Scenes.InitScene)
		{
			if (this.LoadingSlider == null)
			{
				this.LoadingSlider = GameObject.Find("LoadingSlider");
			}
			if (this.LoadingSlider != null)
			{
				this.LoadingSlider.GetComponent<Slider>().value = SuperGameMaster.LoadingProgress;
			}
			if (this.Version == null)
			{
				this.Version = GameObject.Find("VersionText");
				if (this.Version != null)
				{
					int num = Convert.ToInt32(1050f);
					this.Version.GetComponent<Text>().text = string.Concat(new object[]
					{
						"Ver.",
						num / 1000,
						".",
						num / 100 % 10,
						".",
						num / 10 % 10
					});
					if (num % 10 != 0)
					{
						Text component = this.Version.GetComponent<Text>();
						component.text = component.text + "." + num % 10;
					}
					if (global::Define.BUILD != string.Empty)
					{
						Text component2 = this.Version.GetComponent<Text>();
						string text = component2.text;
						component2.text = string.Concat(new object[]
						{
							text,
							"(",
							global::Define.BUILD,
							0f,
							")"
						});
					}
				}
			}
		}
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x0002991C File Offset: 0x00027D1C
	private void OnActiveSceneChanged(Scene i_preChangedScene, Scene i_postChangedScene)
	{
		Debug.LogFormat("[SuperGameMaster] Scene を 切り替えました： preChangedScene:{0} postChangedScene:{1}", new object[]
		{
			i_preChangedScene.name,
			i_postChangedScene.name
		});
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x00029942 File Offset: 0x00027D42
	private void OnSceneLoaded(Scene i_loadedScene, LoadSceneMode i_mode)
	{
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x00029944 File Offset: 0x00027D44
	private void OnSceneUnloaded(Scene i_unloaded)
	{
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00029948 File Offset: 0x00027D48
	private IEnumerator initLoading()
	{
		SuperGameMaster.nowLoading = true;
		SuperGameMaster.LoadingProgress = 0f;
		this.init();
		SuperGameMaster.LoadingProgress = 5f;
		yield return null;
		yield return SuperGameMaster.saveMgr.CheckSerializeConverter();
		SuperGameMaster.LoadingProgress = 20f;
		yield return null;
		SuperGameMaster.LoadData();
		SuperGameMaster.LoadingProgress = 25f;
		yield return SuperGameMaster.saveMgr.Load_Picture(SuperGameMaster.saveData);
		SuperGameMaster.LoadingProgress = 40f;
		yield return null;
		SuperGameMaster.tutorial.SetUpTutorial();
		SuperGameMaster.LoadingProgress = 45f;
		yield return null;
		if (!SuperGameMaster.timeError && SuperGameMaster.tutorial.ClockOk())
		{
			SuperGameMaster.MathTime_Clover((int)SuperGameMaster.LastTime_SpanSec());
			SuperGameMaster.LoadingProgress = 50f;
			yield return null;
			SuperGameMaster.evtMgr.Proc((int)SuperGameMaster.LastTime_SpanSec());
			SuperGameMaster.LoadingProgress = 55f;
			yield return null;
			SuperGameMaster.travel.Proc((int)SuperGameMaster.LastTime_SpanSec());
			SuperGameMaster.LoadingProgress = 60f;
			yield return null;
			yield return SuperGameMaster.picture.Proc();
		}
		SuperGameMaster.LoadingProgress = 90f;
		yield return null;
		SuperGameMaster.audioMgr.AudioInit();
		SuperGameMaster.LoadingProgress = 95f;
		yield return null;
		yield return SuperGameMaster.saveMgr.CheckSerializeDelete();
		SuperGameMaster.LoadingProgress = 100f;
		SuperGameMaster.nowLoading = false;
		yield break;
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00029963 File Offset: 0x00027D63
	public void init()
	{
		SuperGameMaster.GameTimer = 0f;
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00029970 File Offset: 0x00027D70
	public static void LoadData()
	{
		SuperGameMaster.saveData = SuperGameMaster.saveMgr.LoadData();
		SuperGameMaster.deviceTime = DateTime.Now;
		if (SuperGameMaster.saveData.lastDateTime == new DateTime(1970, 1, 1))
		{
			SuperGameMaster.saveData.lastDateTime = new DateTime(SuperGameMaster.deviceTime.Year, SuperGameMaster.deviceTime.Month, SuperGameMaster.deviceTime.Day, SuperGameMaster.deviceTime.Hour, SuperGameMaster.deviceTime.Minute, SuperGameMaster.deviceTime.Second, SuperGameMaster.deviceTime.Millisecond);
		}
		if (SuperGameMaster.saveData.lastDateTime <= SuperGameMaster.deviceTime)
		{
			SuperGameMaster.timeError = false;
			SuperGameMaster.timeErrorString = string.Empty;
		}
		else
		{
			SuperGameMaster.timeError = true;
			string str = string.Concat(new object[]
			{
				SuperGameMaster.saveData.lastDateTime.Year,
				"/",
				SuperGameMaster.saveData.lastDateTime.Month,
				"/",
				SuperGameMaster.saveData.lastDateTime.Day,
				" ",
				SuperGameMaster.saveData.lastDateTime.ToShortTimeString()
			});
			SuperGameMaster.timeErrorString = "前回から時計が戻っています\n次回更新は\n[" + str + "]\n以後です";
		}
		Debug.Log(string.Concat(new object[]
		{
			"[SuperGameMaster] ☆ 最終プレイ時間：",
			SuperGameMaster.saveData.lastDateTime.ToString(),
			" ／ 時間差：",
			SuperGameMaster.LastTime_SpanSec(),
			"秒)"
		}));
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00029B1C File Offset: 0x00027F1C
	public static void SaveData()
	{
		if (!SuperGameMaster.timeError)
		{
			SuperGameMaster.saveData.lastDateTime = new DateTime(SuperGameMaster.deviceTime.Year, SuperGameMaster.deviceTime.Month, SuperGameMaster.deviceTime.Day, SuperGameMaster.deviceTime.Hour, SuperGameMaster.deviceTime.Minute, SuperGameMaster.deviceTime.Second, SuperGameMaster.deviceTime.Millisecond);
		}
		else
		{
			Debug.Log("[SuperGameMaster] 時間が巻き戻っているため、時間の保存はしませんでした。");
		}
		SuperGameMaster.saveMgr.SaveData(new SaveDataFormat(SuperGameMaster.saveData));
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00029BAC File Offset: 0x00027FAC
	public static float LastTime_SpanSec()
	{
		if (SuperGameMaster.timeError)
		{
			return 0f;
		}
		return Mathf.Clamp((float)(SuperGameMaster.deviceTime - SuperGameMaster.saveData.lastDateTime).TotalSeconds, 0f, 2592000f);
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x00029BF7 File Offset: 0x00027FF7
	public static float getGameTimer()
	{
		return SuperGameMaster.GameTimer;
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00029C00 File Offset: 0x00028000
	public static DateTime GetLastDateTime()
	{
		return new DateTime(SuperGameMaster.saveData.lastDateTime.Year, SuperGameMaster.saveData.lastDateTime.Month, SuperGameMaster.saveData.lastDateTime.Day, SuperGameMaster.saveData.lastDateTime.Hour, SuperGameMaster.saveData.lastDateTime.Minute, SuperGameMaster.saveData.lastDateTime.Second, SuperGameMaster.saveData.lastDateTime.Millisecond);
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00029C7B File Offset: 0x0002807B
	public static Scenes GetNowScenes()
	{
		return SuperGameMaster.NowScene;
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x00029C84 File Offset: 0x00028084
	public static void setNextScene(Scenes _NextScene)
	{
		SuperGameMaster.NextScene = _NextScene;
		if (_NextScene == Scenes._Reload)
		{
			SuperGameMaster.NowScene = Scenes._Reload;
			SuperGameMaster.NextScene = Scenes.NONE;
			SuperGameMaster.PrevScene = SuperGameMaster.StartScene;
		}
		else if (_NextScene == Scenes._Prev)
		{
			SuperGameMaster.NextScene = SuperGameMaster.PrevScene;
			if (SuperGameMaster.NextScene == Scenes.InitScene)
			{
				SuperGameMaster.NextScene = SuperGameMaster.DefaultScene;
			}
		}
		else
		{
			SuperGameMaster.PrevScene = SuperGameMaster.NowScene;
		}
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x00029CED File Offset: 0x000280ED
	public static void SetStartScene(Scenes setScene)
	{
		SuperGameMaster.StartScene = setScene;
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x00029CF5 File Offset: 0x000280F5
	public static bool GetIAPCallBackCntEnable()
	{
		return SuperGameMaster.saveData.iapCallBackCnt > 0;
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x00029D04 File Offset: 0x00028104
	public static void IAPCallBackCntReset()
	{
		SuperGameMaster.saveData.iapCallBackCnt = 4;
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x00029D11 File Offset: 0x00028111
	public static void IAPCallBackCntUse()
	{
		SuperGameMaster.saveData.iapCallBackCnt--;
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x00029D28 File Offset: 0x00028128
	public static void MathTime_Clover(int addTimer)
	{
		int num = int.MaxValue;
		foreach (CloverDataFormat cloverDataFormat in SuperGameMaster.saveData.CloverList)
		{
			if (cloverDataFormat.timeSpanSec > 0)
			{
				cloverDataFormat.timeSpanSec -= addTimer;
				if (cloverDataFormat.timeSpanSec <= 0)
				{
					cloverDataFormat.newFlag = true;
					cloverDataFormat.timeSpanSec--;
				}
				if (num > cloverDataFormat.timeSpanSec && cloverDataFormat.timeSpanSec > 0)
				{
					num = cloverDataFormat.timeSpanSec;
				}
			}
		}
		if (num != 2147483647)
		{
			Debug.Log(string.Concat(new object[]
			{
				"[SuperGameMaster] クローバー時間進捗：",
				addTimer,
				" / 次回生成まで：",
				num / 3600,
				"時間 ",
				num % 3600 / 60,
				"分 ",
				num % 60,
				"秒"
			}));
		}
		else
		{
			Debug.Log("[SuperGameMaster] クローバー時間進捗：" + addTimer + " / 生成予定なし");
		}
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x00029E7C File Offset: 0x0002827C
	public static List<CloverDataFormat> GetCloverList()
	{
		List<CloverDataFormat> list = new List<CloverDataFormat>();
		foreach (CloverDataFormat original in SuperGameMaster.saveData.CloverList)
		{
			list.Add(new CloverDataFormat(original));
		}
		return new List<CloverDataFormat>(list);
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00029EF0 File Offset: 0x000282F0
	public static void SaveCloverList(List<CloverDataFormat> cloverList)
	{
		List<CloverDataFormat> list = new List<CloverDataFormat>();
		foreach (CloverDataFormat original in cloverList)
		{
			list.Add(new CloverDataFormat(original));
		}
		SuperGameMaster.saveData.CloverList = list;
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x00029F60 File Offset: 0x00028360
	public static void getCloverPoint(int num)
	{
		SuperGameMaster.saveData.CloverPoint += num;
		if (num > 0)
		{
			SuperGameMaster.set_FlagAdd(Flag.Type.CLOVER_NUM, num);
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00029F82 File Offset: 0x00028382
	public static int CloverPointStock()
	{
		return SuperGameMaster.saveData.CloverPoint;
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x00029F8E File Offset: 0x0002838E
	public static int TicketStock()
	{
		return SuperGameMaster.saveData.ticket;
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x00029F9C File Offset: 0x0002839C
	public static void GetTicket(int getTicket)
	{
		SuperGameMaster.saveData.ticket += getTicket;
		if (SuperGameMaster.saveData.ticket > 999)
		{
			SuperGameMaster.saveData.ticket = 999;
			Debug.Log("[SuperGameMaster] チケットの数が上限に達しました：stock = " + SuperGameMaster.saveData.ticket);
		}
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x00029FFC File Offset: 0x000283FC
	public static int GetTmpRaffleResult()
	{
		return SuperGameMaster.saveData.tmpRaffleResult;
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0002A008 File Offset: 0x00028408
	public static void SetTmpRaffleResult(int _val)
	{
		SuperGameMaster.saveData.tmpRaffleResult = _val;
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0002A015 File Offset: 0x00028415
	public static void SetFrogState(string name, int achieveId)
	{
		SuperGameMaster.saveData.frogName = name;
		SuperGameMaster.saveData.frogAchieveId = achieveId;
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0002A02D File Offset: 0x0002842D
	public static string GetFrogName()
	{
		return SuperGameMaster.saveData.frogName;
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0002A039 File Offset: 0x00028439
	public static int GetAchieveId()
	{
		return SuperGameMaster.saveData.frogAchieveId;
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0002A045 File Offset: 0x00028445
	public static int GetFrogMotion()
	{
		return SuperGameMaster.saveData.frogMotion;
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0002A051 File Offset: 0x00028451
	public static void SetFrogMotion(int motionId)
	{
		SuperGameMaster.saveData.frogMotion = motionId;
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0002A05E File Offset: 0x0002845E
	public static bool GetHome()
	{
		return SuperGameMaster.saveData.home;
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0002A06A File Offset: 0x0002846A
	public static bool GetNoticeFlag()
	{
		return SuperGameMaster.saveData.NoticeFlag;
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x0002A076 File Offset: 0x00028476
	public static void SetNoticeFlag(bool flag)
	{
		SuperGameMaster.saveData.NoticeFlag = flag;
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0002A084 File Offset: 0x00028484
	public static void GetItem(int itemId, int itemStock)
	{
		if (itemId < 10000)
		{
			ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(itemId);
			if (itemDataFormat == null)
			{
				Debug.Log(string.Concat(new object[]
				{
					"[SuperGameMaster] 不明なアイテムを取得しました：ID = ",
					itemId,
					" / stock + ",
					itemStock
				}));
				return;
			}
			int num = SuperGameMaster.saveData.ItemList.FindIndex((ItemListFormat item) => item.id.Equals(itemId));
			if (num == -1)
			{
				ItemListFormat itemListFormat = new ItemListFormat();
				itemListFormat.id = itemId;
				itemListFormat.stock = itemStock;
				SuperGameMaster.saveData.ItemList.Add(itemListFormat);
				SuperGameMaster.saveData.ItemList.Sort((ItemListFormat x, ItemListFormat y) => x.id - y.id);
				if (itemDataFormat.type == Item.Type.Specialty)
				{
					int num2 = SuperGameMaster.sDataBase.search_SpecialtyDBIndex_forItemId(itemId);
					if (num2 == -1)
					{
						Debug.Log("[SuperGameMaster] めいぶつ取得失敗：ID = " + itemId);
						return;
					}
					SpecialtyDataFormat specialtyDataFormat = SuperGameMaster.sDataBase.get_SpecialtyDB(num2);
					if (!SuperGameMaster.saveData.specialtyFlags[specialtyDataFormat.id])
					{
						SuperGameMaster.saveData.specialtyFlags[specialtyDataFormat.id] = true;
						SuperGameMaster.set_FlagAdd(Flag.Type.SPECIALTY_NUM, 1);
						for (int i = 0; i < itemDataFormat.effectType.Length; i++)
						{
							if (itemDataFormat.effectElm[i] == EffectElm.FL_VEGETABLE)
							{
								SuperGameMaster.set_FlagAdd(Flag.Type.VEGETABLE_NUM, 1);
							}
							if (itemDataFormat.effectElm[i] == EffectElm.FL_FRUITS)
							{
								SuperGameMaster.set_FlagAdd(Flag.Type.FRUITS_NUM, 1);
							}
							if (itemDataFormat.effectElm[i] == EffectElm.FL_NORMAL)
							{
								SuperGameMaster.set_FlagAdd(Flag.Type.NORMALFOODS_NUM, 1);
							}
						}
						Debug.Log(string.Concat(new object[]
						{
							"[SuperGameMaster] めいぶつ取得：ID = ",
							num2,
							" ( itemId = ",
							itemId,
							")"
						}));
					}
				}
				num = SuperGameMaster.saveData.ItemList.FindIndex((ItemListFormat item) => item.id.Equals(itemId));
				Debug.Log(string.Concat(new object[]
				{
					"[SuperGameMaster] 新規アイテム入手・ソートしました：ID = ",
					itemId,
					" / stock = ",
					itemStock
				}));
			}
			else
			{
				SuperGameMaster.saveData.ItemList[num].stock += itemStock;
				Debug.Log(string.Concat(new object[]
				{
					"[SuperGameMaster] アイテム入手しました：ID = ",
					itemId,
					" / stock + ",
					itemStock,
					"(",
					SuperGameMaster.saveData.ItemList[num].stock,
					")"
				}));
			}
			if (SuperGameMaster.saveData.ItemList[num].stock > 99)
			{
				SuperGameMaster.saveData.ItemList[num].stock = 99;
				Debug.Log(string.Concat(new object[]
				{
					"[SuperGameMaster] アイテムの数が上限に達しました：ID = ",
					itemId,
					" / stock = ",
					itemStock
				}));
			}
			for (int j = 0; j < itemDataFormat.effectType.Length; j++)
			{
				if (itemDataFormat.effectElm[j] == EffectElm.FL_MANJU && SuperGameMaster.saveData.ItemList[num].stock > SuperGameMaster.get_Flag(Flag.Type.MANJU_NUM))
				{
					SuperGameMaster.set_Flag(Flag.Type.MANJU_NUM, SuperGameMaster.saveData.ItemList[num].stock);
				}
			}
		}
		else if (itemId < 30000)
		{
			itemId -= 10000;
			if (!SuperGameMaster.FindCollection(itemId))
			{
				SuperGameMaster.saveData.collectFlags[itemId] = true;
				SuperGameMaster.set_FlagAdd(Flag.Type.COLLECT_NUM, 1);
				Debug.Log(string.Concat(new object[]
				{
					"[SuperGameMaster] いっぴん取得(+",
					10000,
					")：ID = ",
					itemId
				}));
			}
		}
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0002A4E8 File Offset: 0x000288E8
	public static bool UseItem(int itemId, int stock)
	{
		int num = SuperGameMaster.saveData.ItemList.FindIndex((ItemListFormat item) => item.id.Equals(itemId));
		if (num == -1)
		{
			Debug.Log("[SuperGameMaster] 所持していないアイテムを使用しようとしました！：ID = " + itemId);
			return false;
		}
		if (SuperGameMaster.saveData.ItemList[num].stock <= 0)
		{
			Debug.Log("[SuperGameMaster] ストックが 0 の アイテムを使用しようとしました！：ID = " + itemId);
			return false;
		}
		ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(itemId);
		if (itemDataFormat == null)
		{
			Debug.Log("[SuperGameMaster] 不明なアイテムを使用しようとしました！：ID = " + itemId);
			return false;
		}
		if (itemDataFormat.spend)
		{
			SuperGameMaster.saveData.ItemList[num].stock -= stock;
			Debug.Log(string.Concat(new object[]
			{
				"[SuperGameMaster] 消費アイテムを使いました：ID = ",
				itemId,
				" / stock = ",
				SuperGameMaster.saveData.ItemList[num].stock
			}));
			if (SuperGameMaster.saveData.ItemList[num].stock <= 0)
			{
				SuperGameMaster.saveData.ItemList.RemoveAt(num);
				SuperGameMaster.saveData.ItemList.Sort((ItemListFormat x, ItemListFormat y) => x.id - y.id);
			}
		}
		else
		{
			Debug.Log("[SuperGameMaster] 非消費アイテムを使いました：ID = " + itemId);
		}
		return true;
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x0002A698 File Offset: 0x00028A98
	public static int FindItemStock(int itemId)
	{
		int num = SuperGameMaster.saveData.ItemList.FindIndex((ItemListFormat item) => item.id.Equals(itemId));
		int result = 0;
		if (num != -1)
		{
			result = SuperGameMaster.saveData.ItemList[num].stock;
		}
		return result;
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0002A6F0 File Offset: 0x00028AF0
	public static List<int> GetBagList()
	{
		List<int> list = new List<int>();
		foreach (int item in SuperGameMaster.saveData.bagList)
		{
			list.Add(item);
		}
		return new List<int>(list);
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0002A75C File Offset: 0x00028B5C
	public static List<int> GetDeskList()
	{
		List<int> list = new List<int>();
		foreach (int item in SuperGameMaster.saveData.deskList)
		{
			list.Add(item);
		}
		return new List<int>(list);
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0002A7C8 File Offset: 0x00028BC8
	public static void UseBagItem()
	{
		bool flag = false;
		for (int i = 0; i < SuperGameMaster.saveData.bagList.Count; i++)
		{
			if (SuperGameMaster.saveData.bagList[i] != -1)
			{
				ItemDataFormat itemDataFormat = SuperGameMaster.sDataBase.get_ItemDB_forId(SuperGameMaster.saveData.bagList[i]);
				for (int j = 0; j < itemDataFormat.effectType.Length; j++)
				{
					if (itemDataFormat.effectElm[j] == EffectElm.FL_CONPEITO)
					{
						flag = true;
					}
				}
				SuperGameMaster.UseItem(SuperGameMaster.saveData.bagList[i], 1);
				SuperGameMaster.saveData.bagList[i] = -1;
			}
		}
		if (flag)
		{
			SuperGameMaster.set_FlagAdd(Flag.Type.CONPEITO_COMBO, 1);
			if (SuperGameMaster.get_Flag(Flag.Type.CONPEITO_COMBO) > SuperGameMaster.get_Flag(Flag.Type.CONPEITO_COMBOMAX))
			{
				SuperGameMaster.set_Flag(Flag.Type.CONPEITO_COMBOMAX, SuperGameMaster.get_Flag(Flag.Type.CONPEITO_COMBO));
			}
		}
		else
		{
			SuperGameMaster.set_Flag(Flag.Type.CONPEITO_COMBO, 0);
		}
		Debug.Log("[SuperGameMaster] かばんのアイテムを空にしました : " + flag);
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0002A8D0 File Offset: 0x00028CD0
	public static void SaveBagList(List<int> saveList)
	{
		List<int> list = new List<int>();
		foreach (int item in saveList)
		{
			list.Add(item);
		}
		if (SuperGameMaster.saveData.bagList.Count == 4)
		{
			SuperGameMaster.saveData.bagList = list;
		}
		else
		{
			Debug.Log("[SuperGameMaster] かばんのアイテムの数に違いがあるため、保存出来ませんでした");
		}
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0002A95C File Offset: 0x00028D5C
	public static void SaveDeskList(List<int> saveList)
	{
		List<int> list = new List<int>();
		foreach (int item in saveList)
		{
			list.Add(item);
		}
		if (SuperGameMaster.saveData.deskList.Count == 8)
		{
			SuperGameMaster.saveData.deskList = list;
		}
		else
		{
			Debug.Log("[SuperGameMaster] つくえのアイテムの数に違いがあるため、保存出来ませんでした");
		}
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0002A9E8 File Offset: 0x00028DE8
	public static List<int> GetBagList_virtual()
	{
		List<int> list = new List<int>();
		foreach (int item in SuperGameMaster.saveData.bagList_virtual)
		{
			list.Add(item);
		}
		return new List<int>(list);
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0002AA54 File Offset: 0x00028E54
	public static List<int> GetDeskList_virtual()
	{
		List<int> list = new List<int>();
		foreach (int item in SuperGameMaster.saveData.deskList_virtual)
		{
			list.Add(item);
		}
		return new List<int>(list);
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0002AAC0 File Offset: 0x00028EC0
	public static void SaveBagList_virtual(List<int> saveList)
	{
		List<int> list = new List<int>();
		foreach (int item in saveList)
		{
			list.Add(item);
		}
		if (list.Count == 4)
		{
			SuperGameMaster.saveData.bagList_virtual = list;
		}
		else
		{
			Debug.Log("[SuperGameMaster] かばんのアイテムの数に違いがあるため、保存出来ませんでした");
		}
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0002AB44 File Offset: 0x00028F44
	public static void SaveDeskList_virtual(List<int> saveList)
	{
		List<int> list = new List<int>();
		foreach (int item in saveList)
		{
			list.Add(item);
		}
		if (list.Count == 8)
		{
			SuperGameMaster.saveData.deskList_virtual = list;
		}
		else
		{
			Debug.Log("[SuperGameMaster] つくえのアイテムの数に違いがあるため、保存出来ませんでした");
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0002ABC8 File Offset: 0x00028FC8
	public static void ResetSave_BagDeskList_virtual()
	{
		SuperGameMaster.saveData.bagList_virtual = new List<int>();
		SuperGameMaster.saveData.deskList_virtual = new List<int>();
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0002ABE8 File Offset: 0x00028FE8
	public static bool GetStandby()
	{
		return SuperGameMaster.saveData.standby;
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0002ABF4 File Offset: 0x00028FF4
	public static void SetStandby(bool flag)
	{
		SuperGameMaster.saveData.standby = flag;
		if (flag && (float)SuperGameMaster.saveData.standbyWait - SuperGameMaster.getGameTimer() < 300f)
		{
			SuperGameMaster.saveData.standbyWait = 300 + (int)SuperGameMaster.getGameTimer();
		}
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0002AC43 File Offset: 0x00029043
	public static void SetLastTravelTime(int setTime)
	{
		SuperGameMaster.saveData.lastTravelTime = setTime;
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0002AC50 File Offset: 0x00029050
	public static void SetRestTime(int setTime)
	{
		SuperGameMaster.saveData.restTime = setTime;
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0002AC5D File Offset: 0x0002905D
	public static int GetRestTime()
	{
		return SuperGameMaster.saveData.restTime;
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0002AC69 File Offset: 0x00029069
	public static void SetStandbyWait(int setTime)
	{
		SuperGameMaster.saveData.standbyWait = setTime;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0002AC76 File Offset: 0x00029076
	public static int GetStandbyWait()
	{
		return SuperGameMaster.saveData.standbyWait;
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x0002AC84 File Offset: 0x00029084
	public static bool FindCollection(int collectionId)
	{
		if (collectionId < 0)
		{
			return false;
		}
		int id = SuperGameMaster.sDataBase.get_CollectDB_forId(collectionId).id;
		return SuperGameMaster.saveData.collectFlags[id];
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0002ACBC File Offset: 0x000290BC
	public static int Count_CollectionFlag()
	{
		int num = 0;
		foreach (bool flag in SuperGameMaster.saveData.collectFlags)
		{
			if (flag)
			{
				num++;
			}
		}
		return num;
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0002AD24 File Offset: 0x00029124
	public static int CollectionFailedCount(int collectionId)
	{
		if (collectionId < 0)
		{
			return 0;
		}
		int id = SuperGameMaster.sDataBase.get_CollectDB_forId(collectionId).id;
		int num = SuperGameMaster.saveData.collectFailedCnt[id];
		if (num >= global::Define.COLLECT_PER.Count - 1)
		{
			num = global::Define.COLLECT_PER.Count - 1;
		}
		return num;
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0002AD7C File Offset: 0x0002917C
	public static void Add_CollectionFailedCount(int collectionId)
	{
		if (collectionId < 0)
		{
			return;
		}
		int id = SuperGameMaster.sDataBase.get_CollectDB_forId(collectionId).id;
		List<int> collectFailedCnt;
		int index;
		(collectFailedCnt = SuperGameMaster.saveData.collectFailedCnt)[index = id] = collectFailedCnt[index] + 1;
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0002ADC0 File Offset: 0x000291C0
	public static bool FindSpecialty(int specialtyId)
	{
		int num = SuperGameMaster.sDataBase.search_SpecialtyDBIndex_forItemId(specialtyId);
		if (num == -1)
		{
			return false;
		}
		int id = SuperGameMaster.sDataBase.get_SpecialtyDB(num).id;
		return SuperGameMaster.saveData.specialtyFlags[id];
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0002AE03 File Offset: 0x00029203
	public static bool CheckAchieveFlag(int achiId)
	{
		return SuperGameMaster.saveData.achieveFlags[achiId];
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0002AE15 File Offset: 0x00029215
	public static void Set_GetAchieve(int achiId)
	{
		SuperGameMaster.saveData.achieveFlags[achiId] = true;
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0002AE28 File Offset: 0x00029228
	public static List<int> get_FlagList()
	{
		List<int> list = new List<int>();
		foreach (int item in SuperGameMaster.saveData.gameFlags)
		{
			list.Add(item);
		}
		return new List<int>(list);
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0002AE94 File Offset: 0x00029294
	public static int get_Flag(Flag.Type flagType)
	{
		return SuperGameMaster.saveData.gameFlags[(int)flagType];
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x0002AEA8 File Offset: 0x000292A8
	public static void set_FlagAdd(Flag.Type flagType, int addValue)
	{
		List<int> gameFlags;
		(gameFlags = SuperGameMaster.saveData.gameFlags)[(int)flagType] = gameFlags[(int)flagType] + addValue;
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x0002AED2 File Offset: 0x000292D2
	public static void set_Flag(Flag.Type flagType, int value)
	{
		SuperGameMaster.saveData.gameFlags[(int)flagType] = value;
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x0002AEE8 File Offset: 0x000292E8
	public static List<byte[]> GetPictureByteList(bool album)
	{
		List<byte[]> list = new List<byte[]>();
		if (album)
		{
			foreach (byte[] item in SuperGameMaster.saveData.albumPicture)
			{
				list.Add(item);
			}
			Debug.Log("[SuperGameMaster] アルバムデータを取得： Count = " + list.Count);
		}
		return list;
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x0002AF70 File Offset: 0x00029370
	public static List<Texture2D> GetPictureList(bool album, TextureFormat texQuality)
	{
		List<Texture2D> list = new List<Texture2D>();
		if (album)
		{
			foreach (byte[] array in SuperGameMaster.saveData.albumPicture)
			{
				byte[] data = array;
				Texture2D texture2D = new Texture2D(500, 350, texQuality, false);
				texture2D.LoadImage(data);
				list.Add(texture2D);
			}
			Debug.Log("[SuperGameMaster] アルバム画像を取得： Count = " + list.Count);
		}
		return list;
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x0002B01C File Offset: 0x0002941C
	public static List<DateTime> GetPictureList_DateTime(bool album)
	{
		List<DateTime> list = new List<DateTime>();
		if (album)
		{
			foreach (DateTime dateTime in SuperGameMaster.saveData.albumPictureDate)
			{
				DateTime item = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
				list.Add(item);
			}
			Debug.Log("[SuperGameMaster] アルバム時間リストを取得： Count = " + list.Count);
		}
		return list;
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0002B0E0 File Offset: 0x000294E0
	public static bool SavePictureList(bool album, Texture2D tex, DateTime dateTime)
	{
		if (album)
		{
			if (SuperGameMaster.saveData.albumPicture.Count >= 60)
			{
				return false;
			}
			byte[] item = tex.EncodeToPNG();
			int count = SuperGameMaster.saveData.albumPicture.Count;
			SuperGameMaster.saveData.albumPicture.Add(item);
			SuperGameMaster.saveData.albumPictureDate.Add(new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond));
			SuperGameMaster.saveMgr.Save_Picture(SuperGameMaster.saveData, SaveType.Album, count);
			Debug.Log(string.Concat(new object[]
			{
				"[SuperGameMaster] アルバムに画像を追加： ",
				dateTime.ToString(),
				"Count = ",
				SuperGameMaster.saveData.albumPicture.Count,
				" / ",
				SuperGameMaster.saveData.albumPictureDate.Count
			}));
		}
		return true;
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0002B1F4 File Offset: 0x000295F4
	public static bool DeletePictureList(bool album, int idx)
	{
		if (album)
		{
			if (SuperGameMaster.saveData.albumPicture.Count - 1 < idx)
			{
				return false;
			}
			SuperGameMaster.saveData.albumPicture.RemoveAt(idx);
			SuperGameMaster.saveData.albumPictureDate.RemoveAt(idx);
			SuperGameMaster.saveMgr.Delete_Picture(SuperGameMaster.saveData, SaveType.Album, idx);
			Debug.Log(string.Concat(new object[]
			{
				"[SuperGameMaster] アルバムの画像を削除： idx = ",
				idx,
				" / Count = ",
				SuperGameMaster.saveData.albumPicture.Count,
				" / ",
				SuperGameMaster.saveData.albumPictureDate.Count
			}));
		}
		return true;
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x0002B2B6 File Offset: 0x000296B6
	public static int GetPictureListCount(bool album)
	{
		return SuperGameMaster.saveData.albumPicture.Count;
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x0002B2C8 File Offset: 0x000296C8
	public static Texture2D GetTmpPicture(int actId, TextureFormat texQuality)
	{
		List<Texture2D> list = new List<Texture2D>();
		int num = SuperGameMaster.saveData.tmpPictureId.FindIndex((int rec) => rec.Equals(actId));
		if (num == -1)
		{
			Debug.Log("[SuperGameMaster] 一時保存画像が取得できませんでした： actId = " + actId);
			return new Texture2D(500, 350, texQuality, false);
		}
		byte[] data = SuperGameMaster.saveData.tmpPicture[num];
		Texture2D texture2D = new Texture2D(500, 350, texQuality, false);
		texture2D.LoadImage(data);
		Debug.Log(string.Concat(new object[]
		{
			"[SuperGameMaster] 一時保存画像を取得：actId = ",
			actId,
			" / tmpIdx = ",
			num
		}));
		return UnityEngine.Object.Instantiate<Texture2D>(texture2D);
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x0002B3A4 File Offset: 0x000297A4
	public static bool DeleteTmpPicture(int actId)
	{
		int num = SuperGameMaster.saveData.tmpPictureId.FindIndex((int rec) => rec.Equals(actId));
		if (num == -1)
		{
			Debug.Log("[SuperGameMaster] 一時保存画像が削除できませんでした： actId = " + actId);
			return false;
		}
		SuperGameMaster.saveData.tmpPictureId.RemoveAt(num);
		SuperGameMaster.saveData.tmpPicture.RemoveAt(num);
		SuperGameMaster.saveMgr.Delete_Picture(SuperGameMaster.saveData, SaveType.Temp, num);
		Debug.Log(string.Concat(new object[]
		{
			"[SuperGameMaster] 一時保存画像を削除： actId = ",
			actId,
			" / tmpIdx = ",
			num,
			" / Count = ",
			SuperGameMaster.saveData.tmpPicture.Count
		}));
		return true;
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x0002B486 File Offset: 0x00029886
	public static Step GetTutorialStep()
	{
		return SuperGameMaster.saveData.tutorialStep;
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x0002B492 File Offset: 0x00029892
	public static void SetTutorialStep(Step step)
	{
		SuperGameMaster.saveData.tutorialStep = step;
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x0002B49F File Offset: 0x0002989F
	public static bool GetFirstFlag(Flag flagIndex)
	{
		return SuperGameMaster.saveData.firstFlag[(int)flagIndex];
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x0002B4B1 File Offset: 0x000298B1
	public static void SetFirstFlag(Flag flagIndex)
	{
		SuperGameMaster.saveData.firstFlag[(int)flagIndex] = true;
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x0002B4C4 File Offset: 0x000298C4
	public static void SendLocalNotification(string message, int timeSec, int unique_id)
	{
		if (!SuperGameMaster.saveData.NoticeFlag)
		{
			return;
		}
		SuperGameMaster.m_plugin = new AndroidJavaObject("jp.co.hit_point.unitynative.localNotification", new object[0]);
		if (SuperGameMaster.m_plugin != null)
		{
			string text = "旅かえる";
			SuperGameMaster.m_plugin.Call("sendNotification", new object[]
			{
				text,
				message,
				message,
				(long)timeSec,
				unique_id
			});
			Debug.Log(string.Concat(new object[]
			{
				"[SuperGameMaster] ローカル通知を設定しました！：（unique_id：",
				unique_id,
				") / ",
				timeSec / 3600,
				"h ",
				timeSec % 3600 / 60,
				"m ",
				timeSec % 60,
				"s（",
				timeSec,
				")"
			}));
		}
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0002B5BE File Offset: 0x000299BE
	public static void SetNotification(string message, int delayTime, int badgeNumber = -1)
	{
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x0002B5C0 File Offset: 0x000299C0
	public static void CancelLocalNotification(int unique_id)
	{
		SuperGameMaster.m_plugin = new AndroidJavaObject("jp.co.hit_point.unitynative.localNotification", new object[0]);
		if (SuperGameMaster.m_plugin != null)
		{
			SuperGameMaster.m_plugin.Call("cancelNotification", new object[]
			{
				unique_id
			});
			Debug.Log("[SuperGameMaster] ローカル通知をキャンセルしました！ unique_id：" + unique_id + ")");
		}
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x0002B624 File Offset: 0x00029A24
	public static void iOS_AgreeNotifications()
	{
		Debug.Log("[SuperGameMaster] Android / iOS 通知の許可を求めるタイミングです");
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x0002B630 File Offset: 0x00029A30
	public static bool Android_CheckDoze()
	{
		SuperGameMaster.m_plugin = new AndroidJavaObject("jp.co.hit_point.unitynative.localNotification", new object[0]);
		if (SuperGameMaster.m_plugin != null)
		{
			bool flag = SuperGameMaster.m_plugin.CallStatic<bool>("DozeCheck", new object[0]);
			Debug.Log("[SuperGameMaster] Android / Doze除外フラグ：" + flag);
			return flag;
		}
		Debug.Log("[SuperGameMaster] Android / unityNative の読み込みに失敗しました");
		return true;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x0002B694 File Offset: 0x00029A94
	public static int Android_CheckApiLevel()
	{
		SuperGameMaster.m_plugin = new AndroidJavaClass("android.os.Build$VERSION");
		return SuperGameMaster.m_plugin.GetStatic<int>("SDK_INT");
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x0002B6C4 File Offset: 0x00029AC4
	public static string setSupportID(int sid)
	{
		if (sid <= 0)
		{
			int num;
			for (;;)
			{
				num = UnityEngine.Random.Range(0, 1000000000);
				if (num > 0)
				{
					if (num != SuperGameMaster.saveData.supportID)
					{
						break;
					}
				}
			}
			SuperGameMaster.saveData.supportID = num;
			SuperGameMaster.SaveData();
			return SuperGameMaster.setSupportID(num);
		}
		char[] uid = (sid + 1000000000).ToString().Remove(0, 1).ToCharArray();
		char[] value = SuperGameMaster.uidToXid(uid);
		return new string(value);
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x0002B750 File Offset: 0x00029B50
	public static int getSupportID()
	{
		return SuperGameMaster.saveData.supportID;
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x0002B75C File Offset: 0x00029B5C
	public static char[] uidToXid(char[] uid)
	{
		int num = int.Parse(new string(uid));
		int num2 = num & 31;
		num ^= Support.Define.uidSeed[num2];
		char[] array = new char[7];
		for (int i = 0; i < 6; i++)
		{
			array[i] = (char)(num >> i * 5 & 31);
		}
		array[6] = (char)num2;
		char[] array2 = "I".ToCharArray();
		char[] value = new char[512];
		for (int i = 0; i < 7; i++)
		{
			char c = Support.Define.XXcode[(int)array[Support.Define.hozonJun[i]]];
			value = array2;
			array2 = (new string(value) + c).ToCharArray();
		}
		return array2;
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x0002B80C File Offset: 0x00029C0C
	public static char[] xidToUid(char[] xid)
	{
		char[] array = new char[7];
		if (xid == null || xid[0] == '\0')
		{
			return null;
		}
		for (int i = 0; i < 7; i++)
		{
			char c = xid[i + 1];
			int num = -1;
			for (int j = 0; j < Support.Define.XXcode.Length; j++)
			{
				if (c == Support.Define.XXcode[j])
				{
					num = j;
					break;
				}
			}
			if (num == -1)
			{
				return null;
			}
			array[Support.Define.hozonJun[i]] = (char)num;
		}
		int num2 = 0;
		for (int i = 0; i < 6; i++)
		{
			num2 |= (int)((int)array[i] << (i * 5 & 31));
		}
		num2 ^= Support.Define.uidSeed[(int)array[6]];
		if ((num2 & 31) != (int)array[6])
		{
			return null;
		}
		return (num2 + 1000000000).ToString().Remove(0, 1).ToCharArray();
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x0002B8FC File Offset: 0x00029CFC
	public static int checkHoten(string _str)
	{
		char[] array = SuperGameMaster.xidToUid(_str.ToCharArray());
		if (array == null)
		{
			return -1;
		}
		int index = int.Parse(array[0].ToString());
		int num = int.Parse(array[1].ToString());
		char[] value = new char[]
		{
			array[2],
			array[3]
		};
		int num2 = int.Parse(new string(value));
		string text = new string(array).Remove(0, 4);
		int num3 = int.Parse(text);
		Debug.Log(string.Concat(new object[]
		{
			"mid = ",
			new string(array),
			" / midStr = ",
			text,
			" / supportID = ",
			SuperGameMaster.saveData.supportID,
			" / sid = ",
			num3
		}));
		if (SuperGameMaster.saveData.supportID % 100000 != num3)
		{
			return -2;
		}
		if (SuperGameMaster.saveData.hoten[index])
		{
			return -3;
		}
		if (num == 0)
		{
			SuperGameMaster.getCloverPoint(num2 * 100);
			SuperGameMaster.saveData.hoten[index] = true;
			SuperGameMaster.SaveData();
			return num2 * 100;
		}
		if (num != 1)
		{
			return -9;
		}
		SuperGameMaster.saveData.lastDateTime = new DateTime(SuperGameMaster.deviceTime.Year, SuperGameMaster.deviceTime.Month, SuperGameMaster.deviceTime.Day, SuperGameMaster.deviceTime.Hour, SuperGameMaster.deviceTime.Minute, SuperGameMaster.deviceTime.Second, SuperGameMaster.deviceTime.Millisecond);
		SuperGameMaster.saveData.hoten[index] = true;
		SuperGameMaster.setNextScene(Scenes._Reload);
		SuperGameMaster.SaveData();
		return int.MaxValue;
	}

	// Token: 0x04000583 RID: 1411
	private static bool create_SuperMaster;

	// Token: 0x04000584 RID: 1412
	public static Scenes NowScene;

	// Token: 0x04000585 RID: 1413
	public static Scenes NextScene = Scenes.NONE;

	// Token: 0x04000586 RID: 1414
	public static Scenes PrevScene = Scenes.MainOut;

	// Token: 0x04000587 RID: 1415
	public static Scenes StartScene = Scenes.MainOut;

	// Token: 0x04000588 RID: 1416
	public static Scenes DefaultScene = Scenes.MainOut;

	// Token: 0x04000589 RID: 1417
	public static float GameTimer;

	// Token: 0x0400058A RID: 1418
	public GameObject LoadingSlider;

	// Token: 0x0400058B RID: 1419
	public GameObject Version;

	// Token: 0x0400058C RID: 1420
	public static bool nowLoading;

	// Token: 0x0400058D RID: 1421
	public static float LoadingProgress;

	// Token: 0x0400058E RID: 1422
	public static SaveDataFormat saveData;

	// Token: 0x0400058F RID: 1423
	public static SaveManager saveMgr;

	// Token: 0x04000590 RID: 1424
	public static ScriptableDataBase sDataBase;

	// Token: 0x04000591 RID: 1425
	public static EventTimerManager evtMgr;

	// Token: 0x04000592 RID: 1426
	public static TravelSimulator travel;

	// Token: 0x04000593 RID: 1427
	public static PictureCreator picture;

	// Token: 0x04000594 RID: 1428
	public static AudioManager audioMgr;

	// Token: 0x04000595 RID: 1429
	public static TutorialController tutorial;

	// Token: 0x04000596 RID: 1430
	public static AdMobManager admobMgr;

	// Token: 0x04000597 RID: 1431
	public static AndroidJavaObject m_plugin;

	// Token: 0x04000598 RID: 1432
	public static DateTime deviceTime;

	// Token: 0x04000599 RID: 1433
	public static bool timeError;

	// Token: 0x0400059A RID: 1434
	public static string timeErrorString;
}
