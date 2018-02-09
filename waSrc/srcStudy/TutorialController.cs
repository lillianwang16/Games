using System;
using System.Collections.Generic;
using TimerEvent;
using Tutorial;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020000F2 RID: 242
public class TutorialController : MonoBehaviour
{
	// Token: 0x0600074E RID: 1870 RVA: 0x00030C94 File Offset: 0x0002F094
	public void SetUpTutorial()
	{
		this.tutorialStep = SuperGameMaster.GetTutorialStep();
		this.seTime = 0f;
		if (this.TutorialComplete())
		{
			return;
		}
		switch (this.tutorialStep)
		{
		case Step.a0_MO_FrogTap:
			SuperGameMaster.SetStartScene(Scenes.MainOut);
			break;
		case Step.a1_MO_FrogName:
			SuperGameMaster.SetStartScene(Scenes.MainOut);
			break;
		case Step.a2_MO_GoHome:
			SuperGameMaster.SetStartScene(Scenes.MainOut);
			break;
		case Step.b0_MI_GoOut:
			SuperGameMaster.SetStartScene(Scenes.MainIn);
			break;
		case Step.c0_MO_GetStandby:
			SuperGameMaster.SetStartScene(Scenes.MainOut);
			break;
		case Step.c1_MO_GetClover:
			SuperGameMaster.SetStartScene(Scenes.MainOut);
			break;
		case Step.c2_MO_GoShop:
			SuperGameMaster.SetStartScene(Scenes.MainOut);
			break;
		case Step.d0_SH_BuyStandby:
			SuperGameMaster.SetStartScene(Scenes.Shop);
			break;
		case Step.d1_SH_BuyItem:
			SuperGameMaster.SetStartScene(Scenes.Shop);
			break;
		case Step.d2_SH_GoHome:
			SuperGameMaster.SetStartScene(Scenes.Shop);
			break;
		case Step.e0_MI_OpenBag:
			SuperGameMaster.SetStartScene(Scenes.MainIn);
			break;
		case Step.e1_MI_ReStart:
			this.tutorialStep++;
			SuperGameMaster.SetTutorialStep(this.tutorialStep);
			SuperGameMaster.SetStartScene(Scenes.MainOut);
			break;
		case Step.f0_MO_Standby:
			SuperGameMaster.SetStartScene(Scenes.MainOut);
			break;
		default:
			SuperGameMaster.SetStartScene(Scenes.MainOut);
			break;
		}
	}

	// Token: 0x0600074F RID: 1871 RVA: 0x00030DBC File Offset: 0x0002F1BC
	public bool TutorialComplete()
	{
		return this.tutorialStep == Step.Complete;
	}

	// Token: 0x06000750 RID: 1872 RVA: 0x00030DC8 File Offset: 0x0002F1C8
	public bool ClockOk()
	{
		return this.TutorialComplete() || this.tutorialStep >= Step.f0_MO_Standby;
	}

	// Token: 0x06000751 RID: 1873 RVA: 0x00030DE7 File Offset: 0x0002F1E7
	public bool BGMOk()
	{
		return this.TutorialComplete() || this.tutorialStep >= Step.b0_MI_GoOut;
	}

	// Token: 0x06000752 RID: 1874 RVA: 0x00030E08 File Offset: 0x0002F208
	public void StartTutorial(GameObject _Master)
	{
		if (this.TutorialComplete())
		{
			this.SetScene = Scenes.NONE;
			return;
		}
		this.SetScene = SuperGameMaster.GetNowScenes();
		this.Master = _Master;
		this.UI = this.Master.GetComponent<GameMaster>().UIMaster;
		this.Obj = this.Master.GetComponent<GameMaster>().ObjectMaster;
		this.CallTutorial();
	}

	// Token: 0x06000753 RID: 1875 RVA: 0x00030E6C File Offset: 0x0002F26C
	public void CallTutorial()
	{
		Scenes setScene = this.SetScene;
		if (setScene != Scenes.MainOut)
		{
			if (setScene != Scenes.MainIn)
			{
				if (setScene == Scenes.Shop)
				{
					UIMaster_Shop UI_Cmp = this.UI.GetComponent<UIMaster_Shop>();
					if (!this.ClockOk())
					{
						UI_Cmp.freezeObject(true);
						UI_Cmp.blockUI(true, new Color(0f, 0f, 0f, 0f));
						UI_Cmp.BackFunc_Stop(true);
					}
					switch (this.tutorialStep)
					{
					case Step.d0_SH_BuyStandby:
					{
						HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
						help.OpenPanel("はじめにお手頃な\n「えびづるのスコーン」を\n買ってみましょう");
						help.ResetOnClick_Screen();
						help.SetOnClick_Screen(delegate
						{
							help.ClosePanel();
						});
						help.SetOnClick_Screen(delegate
						{
							UI_Cmp.TutorialUBlock();
						});
						help.SetOnClick_Screen(delegate
						{
							this.StepTutorial(false);
						});
						break;
					}
					case Step.d1_SH_BuyItem:
						UI_Cmp.TutorialUBlock();
						UI_Cmp.blockUI(false);
						break;
					case Step.d2_SH_GoHome:
					{
						HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
						help.OpenPanel("早速、旅のもちものを\nしたくしてあげましょう");
						help.ResetOnClick_Screen();
						help.SetOnClick_Screen(delegate
						{
							help.ClosePanel();
						});
						help.SetOnClick_Screen(delegate
						{
							UI_Cmp.blockUI(false);
						});
						help.SetOnClick_Screen(delegate
						{
							UI_Cmp.TutorialUBlock();
						});
						help.SetOnClick_Screen(delegate
						{
							UI_Cmp.DisplayPanel.GetComponent<FlickCheaker>().stopFlick(false);
						});
						help.SetOnClick_Screen(delegate
						{
							UI_Cmp.MoveCursorUI.SetActive(true);
						});
						help.SetOnClick_Screen(delegate
						{
							UI_Cmp.MoveUI.GetComponent<MovePanel>().InBtn.GetComponent<Button>().onClick.AddListener(delegate
							{
								this.StepTutorial(true);
							});
						});
						break;
					}
					}
				}
			}
			else
			{
				UIMaster_MainIn UI_Cmp = this.UI.GetComponent<UIMaster_MainIn>();
				ObjectMaster_MainIn component = this.Obj.GetComponent<ObjectMaster_MainIn>();
				if (!this.ClockOk())
				{
					UI_Cmp.freezeObject(true);
					UI_Cmp.blockUI(true, new Color(0f, 0f, 0f, 0f));
					UI_Cmp.BackFunc_Stop(true);
				}
				Step step = this.tutorialStep;
				if (step != Step.e0_MI_OpenBag)
				{
					if (step != Step.e1_MI_ReStart)
					{
						if (step == Step.b0_MI_GoOut)
						{
							if (!component.Tutorial_Frog.activeSelf)
							{
								component.Tutorial_Frog.SetActive(true);
								component.Tutorial_Frog.GetComponent<AnmAnimationObj>().SetUpAniAnimation();
							}
							UI_Cmp.TutorialUBlockAll();
							if (!SuperGameMaster.audioMgr.isPlayingBGM() && this.BGMOk())
							{
								SuperGameMaster.audioMgr.PlayBGM(Define.BGMDict["BGM_Default"]);
							}
							HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
							help.OpenPanel(string.Empty);
							help.ResetOnClick_Screen();
							help.SetOnClick_Screen(delegate
							{
								help.ClosePanel();
							});
							help.SetOnClick_Screen(delegate
							{
								help.ActionStock_Next();
							});
							help.ActionStock_New("旅に出かけるしたくをしています\n" + SuperGameMaster.GetFrogName() + "の旅のしたくを\n少しだけ手伝ってあげましょう");
							help.ActionStock_Add(delegate
							{
								help.ClosePanel();
							});
							help.ActionStock_Add(delegate
							{
								help.ActionStock_Next();
							});
							help.ActionStock_New("旅のしたくでは\n<color=#61a8c7><b>おべんとう</b></color>、<color=#61a8c7><b>おまもり</b></color>、<color=#61a8c7><b>どうぐ</b></color>\nの3種類を用意することができます\n\n今回は旅に欠かすことのできない\n<color=#61a8c7><b>おべんとう</b></color>をしたくしてあげましょう");
							help.ActionStock_Add(delegate
							{
								help.ClosePanel();
							});
							help.ActionStock_Add(delegate
							{
								help.ActionStock_Next();
							});
							help.ActionStock_New("旅のもちものは、<color=#61a8c7><b>みつ葉のクローバー</b></color>を\n使って、おみせで買うことができます\nまずは、<color=#61a8c7><b>みつ葉のクローバー</b></color>を収穫しに\nにわさきの畑まで行ってみましょう");
							help.ActionStock_Add(delegate
							{
								help.ClosePanel();
							});
							help.ActionStock_Add(delegate
							{
								UI_Cmp.blockUI(false);
							});
							help.ActionStock_Add(delegate
							{
								UI_Cmp.TutorialUBlock();
							});
							help.ActionStock_Add(delegate
							{
								UI_Cmp.MoveCursorUI.SetActive(true);
							});
							help.ActionStock_Add(delegate
							{
								UI_Cmp.MoveUI.GetComponent<MovePanel>().OutBtn.GetComponent<Button>().onClick.AddListener(delegate
								{
									this.StepTutorial(true);
								});
							});
							this.seTime = 0f;
						}
					}
					else
					{
						if (!component.Tutorial_Frog.activeSelf)
						{
							component.Tutorial_Frog.SetActive(true);
							component.Tutorial_Frog.GetComponent<AnmAnimationObj>().SetUpAniAnimation();
						}
						HelpPanel component2 = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
						component2.OpenPanel(string.Concat(new string[]
						{
							"旅のしたくが完了しました\n",
							SuperGameMaster.GetFrogName(),
							"が旅立つまで待ちましょう\n※今回はチュートリアルなので\n再起動で",
							SuperGameMaster.GetFrogName(),
							"は旅立ちます"
						}));
						component2.ResetOnClick_Screen();
					}
				}
				else
				{
					if (!component.Tutorial_Frog.activeSelf)
					{
						component.Tutorial_Frog.SetActive(true);
						component.Tutorial_Frog.GetComponent<AnmAnimationObj>().SetUpAniAnimation();
					}
					UI_Cmp.BagCompleteCursor.SetActive(true);
					UI_Cmp.BagCompleteCursor.GetComponentInChildren<Image>().enabled = false;
					UI_Cmp.TutorialUBlock();
					UI_Cmp.blockUI(false);
					UI_Cmp.BagMarkUI.SetActive(true);
					Button component3 = UI_Cmp.BagDeskUI.GetComponent<BagDeskPanels>().BagOpenUI.GetComponent<Button>();
					component3.onClick.AddListener(delegate
					{
						UI_Cmp.BagDeskUI.GetComponent<BagDeskPanels>().BagPanelUI.GetComponent<BagPanel>().ChangeButton.SetActive(false);
					});
					component3.onClick.AddListener(delegate
					{
						UI_Cmp.BagDeskUI.GetComponent<BagDeskPanels>().DeskPanelUI.GetComponent<BagPanel>().ChangeButton.SetActive(false);
					});
					component3.onClick.AddListener(delegate
					{
						UI_Cmp.BagMarkUI.SetActive(false);
					});
					component3.onClick.AddListener(delegate
					{
						UI_Cmp.BagDeskUI.GetComponent<BagDeskPanels>().BagPanelUI.GetComponent<BagPanel>().CloseBtn.SetActive(false);
					});
					HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
					component3.onClick.AddListener(delegate
					{
						help.OpenPanel("おべんとうに「えびづるのスコーン」\nおまもりに「よつ葉」\nを選んで<color=#61a8c7><b>かんりょう</b></color>ボタンを\n押してください");
					});
					component3.onClick.AddListener(delegate
					{
						help.ResetOnClick_Screen();
					});
					component3.onClick.AddListener(delegate
					{
						help.SetOnClick_Screen(delegate
						{
							help.ClosePanel();
						});
					});
				}
			}
		}
		else
		{
			UIMaster_MainOut UI_Cmp = this.UI.GetComponent<UIMaster_MainOut>();
			ObjectMaster_MainOut component4 = this.Obj.GetComponent<ObjectMaster_MainOut>();
			if (component4.Post.activeSelf && !this.ClockOk())
			{
				component4.Post.SetActive(false);
			}
			if (!this.ClockOk())
			{
				UI_Cmp.freezeObject(true);
				UI_Cmp.blockUI(true, new Color(0f, 0f, 0f, 0f));
				UI_Cmp.BackFunc_Stop(true);
			}
			Step step2 = this.tutorialStep;
			switch (step2 + 1)
			{
			case Step.a0_MO_FrogTap:
			{
				UI_Cmp.TitleUI.SetActive(true);
				Vector3 position = Camera.main.transform.position;
				Camera.main.transform.position = new Vector3(-Mathf.Abs(position.x), position.y, position.z);
				UI_Cmp.TutorialUBlock();
				this.seTime = 0f;
				break;
			}
			case Step.a1_MO_FrogName:
			{
				component4.Call_FlickInit();
				Vector3 position2 = Camera.main.transform.position;
				Camera.main.transform.position = new Vector3(-Mathf.Abs(position2.x), position2.y, position2.z);
				component4.Frog.SetActive(true);
				component4.Frog.GetComponent<AnmAnimationObj>().SetUpAniAnimation();
				UI_Cmp.TutorialUBlock();
				if (this.seTime != 0f)
				{
					this.seTime = this.seTime % 5f - 5f;
				}
				this.seTime2 = 0f;
				break;
			}
			case Step.a2_MO_GoHome:
			{
				component4.Frog.SetActive(true);
				UI_Cmp.FrogCursorUI.SetActive(false);
				this.seTime = 0f;
				HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
				help.OpenPanel("かえるがいます\n名前をつけてあげましょう");
				help.ResetOnClick_Screen();
				help.SetOnClick_Screen(delegate
				{
					help.ClosePanel();
				});
				help.SetOnClick_Screen(delegate
				{
					UI_Cmp.blockUI(true, new Color(0f, 0f, 0f, 0f));
				});
				help.SetOnClick_Screen(delegate
				{
					this.seTime = -1E-06f;
				});
				help.SetOnClick_Screen(delegate
				{
					UI_Cmp.FrogCursorUI.SetActive(false);
				});
				break;
			}
			case Step.b0_MI_GoOut:
				UI_Cmp.TutorialUBlock();
				this.seTime = 0f;
				break;
			case Step.c1_MO_GetClover:
				UI_Cmp.CloverCursorUI.SetActive(true);
				UI_Cmp.TutorialUBlock();
				break;
			case Step.c2_MO_GoShop:
				UI_Cmp.CloverCursorUI.SetActive(true);
				UI_Cmp.CloverCursorUI.GetComponentInChildren<Image>().enabled = false;
				UI_Cmp.TutorialUBlock();
				break;
			case Step.d0_SH_BuyStandby:
			{
				HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
				help.OpenPanel("<color=#61a8c7><b>よつ葉のクローバー</b></color>を見つけました\n畑では、みつ葉のクローバー以外にも\n稀によつ葉のクローバーを収穫します\nよつ葉のクローバーは旅の<color=#61a8c7><b>おまもり</b></color>に\nしたくすることができます");
				help.ResetOnClick_Screen();
				help.SetOnClick_Screen(delegate
				{
					help.ClosePanel();
				});
				help.SetOnClick_Screen(delegate
				{
					help.ActionStock_Next();
				});
				help.ActionStock_New("折角ですので、<color=#61a8c7><b>おまもり</b></color>も\nしたくしてあげましょう");
				help.ActionStock_Add(delegate
				{
					help.ClosePanel();
				});
				help.ActionStock_Add(delegate
				{
					help.ActionStock_Next();
				});
				help.ActionStock_New("みつ葉のクローバーを収穫し終えました\nでは、みつ葉のクローバーを使って\n<color=#61a8c7><b>おみせ</b></color>で買い物をしてみましょう");
				help.ActionStock_Add(delegate
				{
					help.ClosePanel();
				});
				help.ActionStock_Add(delegate
				{
					UI_Cmp.blockUI(false);
				});
				help.ActionStock_Add(delegate
				{
					UI_Cmp.TutorialUBlock();
				});
				help.ActionStock_Add(delegate
				{
					UI_Cmp.MoveCursorUI.transform.localPosition = new Vector2(UI_Cmp.MoveCursorUI.transform.localPosition.x, UI_Cmp.MoveCursorUI.transform.localPosition.y + 94f);
				});
				help.ActionStock_Add(delegate
				{
					UI_Cmp.MoveCursorUI.SetActive(true);
				});
				help.ActionStock_Add(delegate
				{
					UI_Cmp.MoveUI.GetComponent<MovePanel>().ShopBtn.GetComponent<Button>().onClick.AddListener(delegate
					{
						this.StepTutorial(true);
					});
				});
				break;
			}
			}
		}
	}

	// Token: 0x06000754 RID: 1876 RVA: 0x00031A3C File Offset: 0x0002FE3C
	public void UpdateTutorial()
	{
		if (this.TutorialComplete())
		{
			return;
		}
		if (this.SetScene == Scenes.NONE)
		{
			return;
		}
		Scenes setScene = this.SetScene;
		if (setScene != Scenes.MainOut)
		{
			if (setScene != Scenes.MainIn)
			{
				if (setScene == Scenes.Shop)
				{
					UIMaster_Shop UI_Cmp = this.UI.GetComponent<UIMaster_Shop>();
					switch (this.tutorialStep)
					{
					case Step.d1_SH_BuyItem:
					{
						int selectShopIndex = UI_Cmp.DisplayPanel.GetComponent<DisplayPanel>().GetSelectShopIndex();
						if (selectShopIndex != -1 && !UI_Cmp.HelpUI.activeSelf && selectShopIndex != 0)
						{
							UI_Cmp.DisplayPanel.GetComponent<DisplayPanel>().unsetCursor();
							UI_Cmp.DisplayPanel.GetComponent<DisplayPanel>().ResetSelectShopIndex();
							UI_Cmp.DisplayPanel.GetComponent<DisplayPanel>().SetInfoPanelData(-1, Vector3.zero);
							UI_Cmp.DisplayPanel.GetComponent<FlickCheaker>().stopFlick(true);
							UI_Cmp.blockUI(true, new Color(0f, 0f, 0f, 0f));
							HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
							help.OpenPanel("はじめにお手頃な\n「えびづるのスコーン」を\n買ってみましょう");
							help.ResetOnClick_Screen();
							help.SetOnClick_Screen(delegate
							{
								help.ClosePanel();
							});
							help.SetOnClick_Screen(delegate
							{
								UI_Cmp.blockUI(false);
							});
							help.SetOnClick_Screen(delegate
							{
								UI_Cmp.DisplayPanel.GetComponent<FlickCheaker>().stopFlick(false);
							});
						}
						if (!UI_Cmp.BackFunc_GetStopFlag())
						{
							UI_Cmp.BackFunc_Stop(true);
						}
						if (SuperGameMaster.FindItemStock(0) >= 1)
						{
							this.StepTutorial(true);
						}
						break;
					}
					case Step.d2_SH_GoHome:
					{
						int selectShopIndex2 = UI_Cmp.DisplayPanel.GetComponent<DisplayPanel>().GetSelectShopIndex();
						if (selectShopIndex2 != -1 && !UI_Cmp.HelpUI.activeSelf)
						{
							UI_Cmp.DisplayPanel.GetComponent<DisplayPanel>().unsetCursor();
							UI_Cmp.DisplayPanel.GetComponent<DisplayPanel>().ResetSelectShopIndex();
							UI_Cmp.DisplayPanel.GetComponent<DisplayPanel>().SetInfoPanelData(-1, Vector3.zero);
						}
						break;
					}
					}
				}
			}
			else
			{
				UIMaster_MainIn component = this.UI.GetComponent<UIMaster_MainIn>();
				ObjectMaster_MainIn component2 = this.Obj.GetComponent<ObjectMaster_MainIn>();
				if (component2.Frog.activeSelf)
				{
					component2.Frog.SetActive(false);
				}
				if (component.BagDeskUI.GetComponent<BagDeskPanels>().EmptyIcon.activeSelf)
				{
					component.BagDeskUI.GetComponent<BagDeskPanels>().EmptyIcon.SetActive(false);
				}
				Step step = this.tutorialStep;
				if (step != Step.e0_MI_OpenBag)
				{
					if (step != Step.e1_MI_ReStart)
					{
						if (step == Step.b0_MI_GoOut)
						{
							this.seTime += Time.deltaTime;
							if (this.seTime > 2f && this.seTime - Time.deltaTime < 2f)
							{
								HelpPanel component3 = component.HelpUI.GetComponent<HelpPanel>();
								if (!component3.HelpWindow.activeSelf)
								{
									component3.ClosePanel();
									component3.ActionStock_Next();
								}
							}
						}
					}
				}
				else
				{
					if (component2.Frog.activeSelf)
					{
						component2.Frog.SetActive(false);
					}
					if (component.BagDeskUI.activeSelf)
					{
						component.BagDeskUI.GetComponent<FlickCheaker>().stopFlick(true);
						if (!component.BagDeskUI.GetComponent<BagDeskPanels>().BagPanelUI.GetComponent<BagPanel>().ItemView.activeSelf)
						{
							if (!SuperGameMaster.GetStandby())
							{
								List<int> tmpListAll = component.BagDeskUI.GetComponent<BagDeskPanels>().Get_tmpListAll();
								if (tmpListAll[0] == 0 && tmpListAll[1] == 1000)
								{
									component.BagCompleteCursor.GetComponentInChildren<Image>().enabled = true;
								}
								else
								{
									component.BagCompleteCursor.GetComponentInChildren<Image>().enabled = false;
								}
							}
							else
							{
								component.BagCompleteCursor.GetComponentInChildren<Image>().enabled = false;
							}
						}
						else
						{
							component.BagCompleteCursor.GetComponentInChildren<Image>().enabled = false;
						}
					}
					else if (!component.BagMarkUI.activeSelf && !component.HelpUI.activeSelf)
					{
						component.BagCompleteCursor.GetComponentInChildren<Image>().enabled = false;
						List<int> bagList = SuperGameMaster.GetBagList();
						if (bagList[0] == 0 && bagList[1] == 1000 && SuperGameMaster.GetStandby())
						{
							SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.GoTravel);
							SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.BackHome);
							SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Picture);
							SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Drift);
							SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Return);
							SuperGameMaster.travel.GoTravel(536870911, 1);
							int index = SuperGameMaster.evtMgr.search_TimerEvtIndex_forType(TimerEvent.Type.BackHome);
							EventTimerFormat eventTimerFormat = SuperGameMaster.evtMgr.get_TimerEvt(index);
							List<int> list = new List<int>(eventTimerFormat.evtValue);
							if (list.Count >= 6 && list[4] >= 10000)
							{
								list[4] = 3009;
							}
							SuperGameMaster.evtMgr.set_TimerEvt_forId(eventTimerFormat.id, list);
							SuperGameMaster.evtMgr.SetTime_TimerEvt(TimerEvent.Type.GoTravel, 0, 536870911);
							SuperGameMaster.SetStandby(true);
							SuperGameMaster.SetLastTravelTime(536870911);
							SuperGameMaster.SetStandbyWait(0);
							component.BagCompleteCursor.SetActive(false);
							this.StepTutorial(true);
						}
						else
						{
							SuperGameMaster.saveData.lastTravelTime = SuperGameMaster.saveData.restTime;
							SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.GoTravel);
							SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.BackHome);
							SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Picture);
							SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Drift);
							SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Return);
							component.blockUI(false);
							component.TutorialUBlock();
							component.BagMarkUI.SetActive(true);
						}
					}
				}
			}
		}
		else
		{
			UIMaster_MainOut UI_Cmp = this.UI.GetComponent<UIMaster_MainOut>();
			ObjectMaster_MainOut component4 = this.Obj.GetComponent<ObjectMaster_MainOut>();
			Step step2 = this.tutorialStep;
			switch (step2 + 1)
			{
			case Step.a0_MO_FrogTap:
				this.seTime += Time.deltaTime;
				if (this.seTime % 5f < (this.seTime - Time.deltaTime) % 5f || this.seTime - Time.deltaTime == 0f)
				{
					SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Sparrow"]);
				}
				if (Input.GetKeyDown(KeyCode.Escape) && !UI_Cmp.ConfilmUI.activeSelf)
				{
					if (!UI_Cmp.Title_StartUI.activeSelf)
					{
						if (UI_Cmp.WebViewUI.activeSelf)
						{
							UI_Cmp.WebViewUI.GetComponent<WebViewPanel>().ClosePanel();
						}
						else
						{
							UI_Cmp.Title_StartUI.SetActive(true);
							UI_Cmp.Title_PolicyUI.SetActive(false);
						}
						SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Cancel"]);
					}
					else
					{
						ConfilmPanel confilm = UI_Cmp.ConfilmUI.GetComponent<ConfilmPanel>();
						confilm.OpenPanel_YesNo("アプリケーションを終了しますか？");
						confilm.ResetOnClick_Yes();
						confilm.SetOnClick_Yes(delegate
						{
							confilm.ClosePanel();
						});
						confilm.SetOnClick_Yes(delegate
						{
							SuperGameMaster.SaveData();
						});
						confilm.SetOnClick_Yes(delegate
						{
							Application.Quit();
						});
						confilm.ResetOnClick_No();
						confilm.SetOnClick_No(delegate
						{
							confilm.ClosePanel();
						});
					}
				}
				break;
			case Step.a1_MO_FrogName:
				this.seTime += Time.deltaTime;
				if (this.seTime > 0f && this.seTime - Time.deltaTime < 0f)
				{
					this.seTime = 0f;
				}
				this.seTime2 += Time.deltaTime;
				if (this.seTime2 > 4f)
				{
					component4.Call_FlickMove(new Vector2(Mathf.Clamp((1.92f - Camera.main.transform.localPosition.x) / 60f, 1E-05f, 0.1f), 0f));
				}
				if (this.seTime2 > 7f)
				{
					if (Input.GetMouseButtonDown(0))
					{
						Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
						if (component4.Frog.GetComponent<Collider2D>().OverlapPoint(point))
						{
							this.StepTutorial(false);
							UI_Cmp.FrogCursorUI.SetActive(false);
						}
					}
					else if (this.seTime2 > 9f)
					{
						this.StepTutorial(false);
						UI_Cmp.FrogCursorUI.SetActive(false);
					}
				}
				if (this.seTime2 >= 2f && this.seTime2 - Time.deltaTime < 2f)
				{
					SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Grassy"]);
				}
				if (this.seTime2 % 3f < (this.seTime2 - Time.deltaTime) % 3f && this.seTime2 > 7f)
				{
					SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Frog"]);
				}
				if (this.seTime % 5f < (this.seTime - Time.deltaTime) % 5f || this.seTime - Time.deltaTime == 0f)
				{
					SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Sparrow"]);
				}
				break;
			case Step.a2_MO_GoHome:
				if (this.seTime != 0f)
				{
					this.seTime += Time.deltaTime;
					UI_Cmp.blockUI(true, new Color(0f, 0f, 0f, this.seTime));
					if (this.seTime > 1f)
					{
						UI_Cmp.blockUI(true, new Color(0f, 0f, 0f, 1f));
						this.seTime = 0f;
						UI_Cmp.FrogNameUI.GetComponent<FrogNamePanel>().OpenNamePanel();
						component4.Frog.SetActive(false);
					}
				}
				break;
			case Step.b0_MI_GoOut:
				this.seTime += Time.deltaTime;
				if (this.seTime >= 0.5f && this.seTime - Time.deltaTime < 0.5f)
				{
					SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Frog"]);
				}
				if (this.seTime <= 1f)
				{
					UI_Cmp.blockUI(true, new Color(0f, 0f, 0f, 1f - this.seTime));
				}
				if (this.seTime > 1f && this.seTime - Time.deltaTime < 1f)
				{
					UI_Cmp.blockUI(true, new Color(0f, 0f, 0f, 0f));
					HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
					help.OpenPanel(string.Empty);
					help.ResetOnClick_Screen();
					help.SetOnClick_Screen(delegate
					{
						help.ClosePanel();
					});
					help.SetOnClick_Screen(delegate
					{
						help.ActionStock_Next();
					});
					help.ActionStock_New("おうちのなかに入っていきました\nなかをのぞいてみましょう");
					help.ActionStock_Add(delegate
					{
						help.ClosePanel();
					});
					help.ActionStock_Add(delegate
					{
						UI_Cmp.blockUI(false);
					});
					help.ActionStock_Add(delegate
					{
						UI_Cmp.TutorialUBlock();
					});
					help.ActionStock_Add(delegate
					{
						UI_Cmp.MoveCursorUI.SetActive(true);
					});
					help.ActionStock_Add(delegate
					{
						UI_Cmp.MoveUI.GetComponent<MovePanel>().InBtn.GetComponent<Button>().onClick.AddListener(delegate
						{
							this.StepTutorial(true);
						});
					});
					help.ActionStock_Add(delegate
					{
						UI_Cmp.MoveUI.GetComponent<MovePanel>().InBtn.GetComponent<Button>().onClick.AddListener(delegate
						{
							UI_Cmp.MoveUI.GetComponent<MovePanel>().InBtn.GetComponent<Button>().enabled = true;
						});
					});
					help.ActionStock_Add(delegate
					{
						UI_Cmp.MoveUI.GetComponent<MovePanel>().InBtn.GetComponent<Button>().onClick.AddListener(delegate
						{
							SuperGameMaster.audioMgr.StopSE();
						});
					});
					help.ActionStock_Add(delegate
					{
						UI_Cmp.MoveUI.GetComponent<MovePanel>().InBtn.GetComponent<Button>().onClick.AddListener(delegate
						{
							SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Knock"]);
						});
					});
					help.ActionStock_Add(delegate
					{
						UI_Cmp.MoveUI.GetComponent<MovePanel>().InBtn.GetComponent<Button>().onClick.AddListener(delegate
						{
							this.seTime = 0f;
						});
					});
				}
				if (this.seTime > 3f && this.seTime - Time.deltaTime < 3f)
				{
					HelpPanel component5 = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
					if (!component5.HelpWindow.activeSelf)
					{
						component5.ClosePanel();
						component5.ActionStock_Next();
					}
				}
				break;
			case Step.c0_MO_GetStandby:
				if (this.seTime == 0f)
				{
					UI_Cmp.setFadeOut(99999f);
					UI_Cmp.FadeUI.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
				}
				this.seTime += Time.deltaTime;
				if (this.seTime >= 1f && this.seTime - Time.deltaTime < 1f)
				{
					UI_Cmp.setFadeOut(0.25f);
					SuperGameMaster.audioMgr.PlaySE(Define.SEDict["SE_Move"]);
				}
				break;
			case Step.c1_MO_GetClover:
				if (!UI_Cmp.HelpUI.activeSelf)
				{
					if (Input.GetMouseButtonUp(0))
					{
						component4.Call_FlickInit();
					}
					this.Obj.GetComponent<FlickCheaker>().FlickUpdate();
					component4.Call_FlickMove();
				}
				if (Camera.main.transform.position.x <= -1.9f && UI_Cmp.CloverCursorUI.activeSelf)
				{
					UI_Cmp.CloverCursorUI.SetActive(false);
					HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
					help.OpenPanel("スライド操作でみつ葉のクローバーを\n収穫しましょう");
					help.ResetOnClick_Screen();
					help.SetOnClick_Screen(delegate
					{
						help.ClosePanel();
					});
					help.SetOnClick_Screen(delegate
					{
						this.StepTutorial(false);
					});
				}
				break;
			case Step.c2_MO_GoShop:
				if (Input.GetMouseButton(0))
				{
					if (Camera.main.transform.position.x > 0f)
					{
						UI_Cmp.CloverCursorUI.GetComponentInChildren<Image>().enabled = true;
					}
					else
					{
						UI_Cmp.CloverCursorUI.GetComponentInChildren<Image>().enabled = false;
					}
				}
				if (!UI_Cmp.HelpUI.activeSelf && !UI_Cmp.ConfilmUI.activeSelf)
				{
					component4.ScrollAndCloverCheck();
					component4.CloverFarm.GetComponent<CloverFarm>().CloverProc();
					if (SuperGameMaster.GetFirstFlag(Flag.TUTORIAL_CLOVER) && SuperGameMaster.GetFirstFlag(Flag.TUTORIAL_FOURLEAF) && SuperGameMaster.FindItemStock(1000) + SuperGameMaster.CloverPointStock() >= SuperGameMaster.GetCloverList().Count)
					{
						if (SuperGameMaster.FindItemStock(1000) > 10)
						{
							SuperGameMaster.getCloverPoint(10);
						}
						this.StepTutorial(true);
					}
				}
				if (!UI_Cmp.HelpUI.activeSelf && !UI_Cmp.ConfilmUI.activeSelf && !SuperGameMaster.GetFirstFlag(Flag.TUTORIAL_CLOVER) && SuperGameMaster.CloverPointStock() >= 1)
				{
					UI_Cmp.CloverMarkUI.SetActive(true);
					HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
					help.OpenPanel("収穫したみつ葉のクローバーは\nこちらで確認することができます");
					help.ResetOnClick_Screen();
					help.SetOnClick_Screen(delegate
					{
						help.ClosePanel();
					});
					help.SetOnClick_Screen(delegate
					{
						UI_Cmp.CloverMarkUI.SetActive(false);
					});
					help.SetOnClick_Screen(delegate
					{
						help.ActionStock_Next();
					});
					help.ActionStock_New("畑のクローバーを全て収穫しましょう");
					help.ActionStock_Add(delegate
					{
						help.ClosePanel();
					});
					SuperGameMaster.SetFirstFlag(Flag.TUTORIAL_CLOVER);
				}
				if (!UI_Cmp.HelpUI.activeSelf && !UI_Cmp.ConfilmUI.activeSelf && !SuperGameMaster.GetFirstFlag(Flag.TUTORIAL_FOURLEAF) && SuperGameMaster.FindItemStock(1000) >= 1)
				{
					ConfilmPanel confilm = UI_Cmp.ConfilmUI.GetComponent<ConfilmPanel>();
					confilm.OpenPanel(string.Empty);
					confilm.AddContents(UnityEngine.Object.Instantiate<GameObject>(component4.CloverFarm.GetComponent<CloverFarm>().AddConfirm_pref));
					confilm.ResetOnClick_Screen();
					confilm.SetOnClick_Screen(delegate
					{
						confilm.ClosePanel();
					});
					confilm.SetOnClick_Screen(delegate
					{
						UI_Cmp.BackFunc_Stop(true);
					});
					SuperGameMaster.SetFirstFlag(Flag.TUTORIAL_FOURLEAF);
				}
				break;
			case Step._StepMax:
				if (!UI_Cmp.ResultUI.activeSelf && !UI_Cmp.HelpUI.activeSelf && !UI_Cmp.ConfilmUI.activeSelf && !SuperGameMaster.tutorial.TutorialComplete())
				{
					HelpPanel help = UI_Cmp.HelpUI.GetComponent<HelpPanel>();
					ConfilmPanel confilm = UI_Cmp.ConfilmUI.GetComponent<ConfilmPanel>();
					help.OpenPanel(string.Concat(new string[]
					{
						string.Empty,
						SuperGameMaster.GetFrogName(),
						"は旅立ちました\n",
						SuperGameMaster.GetFrogName(),
						"が帰ってくるには\nしばらく時間がかかります\n今回のしたくなら数時間ほどで\n帰ってきそうです"
					}));
					help.ResetOnClick_Screen();
					help.SetOnClick_Screen(delegate
					{
						help.ClosePanel();
					});
					help.SetOnClick_Screen(delegate
					{
						help.ActionStock_Next();
					});
					help.ActionStock_New("※ヘルプで通知をONに設定しておくと\n" + SuperGameMaster.GetFrogName() + "が帰ってきたときに\n通知が入ります");
					help.ActionStock_Add(delegate
					{
						help.ClosePanel();
					});
					help.ActionStock_Add(delegate
					{
						help.ActionStock_Next();
					});
					help.ActionStock_New(SuperGameMaster.GetFrogName() + "は旅のしたくがなくても\n自由にでかけていきますが\n今回のように旅のしたくを\n手伝ってあげていると\n旅の様子をうつした<color=#61a8c7><b>写真</b></color>や\n手に入れた各地の<color=#61a8c7><b>おみやげ</b></color>を\nプレゼントしてくれます");
					help.ActionStock_Add(delegate
					{
						help.ClosePanel();
					});
					help.ActionStock_Add(delegate
					{
						help.ActionStock_Next();
					});
					help.ActionStock_New(SuperGameMaster.GetFrogName() + "が出かけている間に\n次の旅に備えて\nみつ葉のクローバーを収穫して\nもちものを用意しておきましょう");
					help.ActionStock_Add(delegate
					{
						help.ClosePanel();
					});
					help.ActionStock_Add(delegate
					{
						help.ActionStock_Next();
					});
					help.ActionStock_New("チュートリアルはここまでです\n" + SuperGameMaster.GetFrogName() + "との\n旅をお楽しみください");
					help.ActionStock_Add(delegate
					{
						help.ClosePanel();
					});
					help.ActionStock_Add(delegate
					{
						confilm.OpenPanel(string.Empty);
					});
					help.ActionStock_Add(delegate
					{
						confilm.AddContents(UnityEngine.Object.Instantiate<GameObject>(UI_Cmp.AddConfirm_pref));
					});
					help.ActionStock_Add(delegate
					{
						confilm.ResetOnClick_Screen();
					});
					help.ActionStock_Add(delegate
					{
						confilm.SetOnClick_Screen(delegate
						{
							confilm.ClosePanel();
						});
					});
					help.ActionStock_Add(delegate
					{
						confilm.SetOnClick_Screen(delegate
						{
							UI_Cmp.blockUI(false);
						});
					});
					help.ActionStock_Add(delegate
					{
						confilm.SetOnClick_Screen(delegate
						{
							UI_Cmp.freezeObject(false);
						});
					});
					help.ActionStock_Add(delegate
					{
						confilm.SetOnClick_Screen(delegate
						{
							UI_Cmp.BackFunc_Stop(false);
						});
					});
					help.ActionStock_Add(delegate
					{
						confilm.SetOnClick_Screen(delegate
						{
							this.FinishTutorial();
						});
					});
					SuperGameMaster.evtMgr.delete_Act_Timer_EvtList_forType(TimerEvent.Type.Drift);
				}
				break;
			}
		}
	}

	// Token: 0x06000755 RID: 1877 RVA: 0x00032FFC File Offset: 0x000313FC
	public void StepTutorial(bool save)
	{
		this.tutorialStep++;
		if (save)
		{
			SuperGameMaster.SetTutorialStep(this.tutorialStep);
			SuperGameMaster.SaveData();
		}
		this.CallTutorial();
	}

	// Token: 0x06000756 RID: 1878 RVA: 0x00033028 File Offset: 0x00031428
	public void FinishTutorial()
	{
		this.tutorialStep = Step.Complete;
		SuperGameMaster.SetTutorialStep(this.tutorialStep);
		MailScrollView component = this.UI.GetComponent<UIMaster_MainOut>().MailUI.GetComponent<MailScrollView>();
		component.CreateMailEvt(SuperGameMaster.sDataBase.get_MailEvtDB(0));
		component.CreateMailEvt(SuperGameMaster.sDataBase.get_MailEvtDB(1));
		component.PostObj.GetComponent<Post>().DispNew(true);
		this.Obj.GetComponent<ObjectMaster_MainOut>().Table.GetComponent<CharaTable>().CheckFriendCreate();
		int index = SuperGameMaster.evtMgr.search_TimerEvtIndex_forType(TimerEvent.Type.BackHome);
		EventTimerFormat eventTimerFormat = SuperGameMaster.evtMgr.get_TimerEvt(index);
		int num = eventTimerFormat.evtValue[1];
		num += (int)SuperGameMaster.getGameTimer();
		SuperGameMaster.evtMgr.SetTime_TimerEvt(TimerEvent.Type.BackHome, num, -1);
		SuperGameMaster.evtMgr.SetTime_TimerEvt(TimerEvent.Type.Picture, num, -1);
		SuperGameMaster.SetLastTravelTime(num);
		SuperGameMaster.SaveData();
	}

	// Token: 0x06000757 RID: 1879 RVA: 0x000330FE File Offset: 0x000314FE
	public void InHomeTutorial(bool save)
	{
		this.tutorialStep++;
		if (save)
		{
			SuperGameMaster.SetTutorialStep(this.tutorialStep);
			SuperGameMaster.SaveData();
		}
		this.CallTutorial();
	}

	// Token: 0x040005CE RID: 1486
	public Step tutorialStep = Step.NONE;

	// Token: 0x040005CF RID: 1487
	public Scenes SetScene;

	// Token: 0x040005D0 RID: 1488
	public GameObject Master;

	// Token: 0x040005D1 RID: 1489
	public GameObject UI;

	// Token: 0x040005D2 RID: 1490
	public GameObject Obj;

	// Token: 0x040005D3 RID: 1491
	private float seTime;

	// Token: 0x040005D4 RID: 1492
	private float seTime2;
}
