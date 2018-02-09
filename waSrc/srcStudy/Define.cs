using System;
using System.Collections.Generic;
using Picture;
using Prize;
using UnityEngine;

// Token: 0x02000077 RID: 119
public static class Define
{
	// Token: 0x0400029B RID: 667
	public const float VERSION = 1.05f;

	// Token: 0x0400029C RID: 668
	public static readonly string BUILD = string.Empty;

	// Token: 0x0400029D RID: 669
	public const float BUILD_NUM = 0f;

	// Token: 0x0400029E RID: 670
	public const bool STOP_DEBUGLOG = true;

	// Token: 0x0400029F RID: 671
	public const bool AD_TEST = false;

	// Token: 0x040002A0 RID: 672
	public const bool SHORT_DEBUGLOG = true;

	// Token: 0x040002A1 RID: 673
	public const bool SHOW_PICTURE_DATE = false;

	// Token: 0x040002A2 RID: 674
	public const bool SHOW_PICTURE_NAME = false;

	// Token: 0x040002A3 RID: 675
	public static readonly string SaveName_Serialize = Application.persistentDataPath + "/GameData.sav";

	// Token: 0x040002A4 RID: 676
	public static readonly string SaveName_Binary = Application.persistentDataPath + "/Tabikaeru.sav";

	// Token: 0x040002A5 RID: 677
	public static readonly string SaveName_Binary_PicDir = Application.persistentDataPath + "/Picture";

	// Token: 0x040002A6 RID: 678
	public static readonly string SaveName_Binary_PicDir_SimplePath = "/Picture";

	// Token: 0x040002A7 RID: 679
	public static readonly Dictionary<SaveType, string> PicSaveDict = new Dictionary<SaveType, string>
	{
		{
			SaveType.INDEX,
			"/index.sav"
		},
		{
			SaveType.Album,
			"/album_"
		},
		{
			SaveType.Temp,
			"/tmp_"
		}
	};

	// Token: 0x040002A8 RID: 680
	public const int PathStr_ExByte = 10;

	// Token: 0x040002A9 RID: 681
	public const int BOOL = 1;

	// Token: 0x040002AA RID: 682
	public const int INT = 4;

	// Token: 0x040002AB RID: 683
	public const int LONG = 8;

	// Token: 0x040002AC RID: 684
	public const int DAYTIME_COUNT = 7;

	// Token: 0x040002AD RID: 685
	public const int STR_SHORT = 20;

	// Token: 0x040002AE RID: 686
	public const int STR_NORMAL = 40;

	// Token: 0x040002AF RID: 687
	public const int STR_LONG = 200;

	// Token: 0x040002B0 RID: 688
	public const int EVT_TIMER_FORMAT_EVTVAL = 100;

	// Token: 0x040002B1 RID: 689
	public static readonly string URL_Review_Android = "https://play.google.com/store/apps/details?id=jp.co.hit_point.tabikaeru";

	// Token: 0x040002B2 RID: 690
	public static readonly string URL_Tarm_Android = "http://hpmobile.jp/app/tabikaeru/tarms/tarm.html";

	// Token: 0x040002B3 RID: 691
	public static readonly string URL_UserPolicy_Android = "http://hpmobile.jp/app/tabikaeru/tarms/tokushou.html";

	// Token: 0x040002B4 RID: 692
	public static readonly string URL_PrivacyPolicy_Android = "http://hpmobile.jp/app/tabikaeru/tarms/privacypolicy.html";

	// Token: 0x040002B5 RID: 693
	public static readonly string URL_Review_iOS = "https://itunes.apple.com/jp/app/id1255032913?mt=8&action=write-review";

	// Token: 0x040002B6 RID: 694
	public static readonly string URL_Tarm_iOS = "http://hpmobile.jp/app/tabikaeru/tarms/tarm_ios.html";

	// Token: 0x040002B7 RID: 695
	public static readonly string URL_UserPolicy_iOS = "http://hpmobile.jp/app/tabikaeru/tarms/tokushou_ios.html";

	// Token: 0x040002B8 RID: 696
	public static readonly string URL_PrivacyPolicy_iOS = "http://hpmobile.jp/app/tabikaeru/tarms/privacypolicy_ios.html";

	// Token: 0x040002B9 RID: 697
	public static readonly string AdMob_Android_AppID = "ca-app-pub-2904702161629737~3672811158";

	// Token: 0x040002BA RID: 698
	public static readonly string AdMob_Android_UnitID_Banner = "ca-app-pub-2904702161629737/2628398195";

	// Token: 0x040002BB RID: 699
	public static readonly string AdMob_Android_UnitID_Interstitial = "ca-app-pub-2904702161629737/3090357220";

	// Token: 0x040002BC RID: 700
	public static readonly string AdMob_iOS_AppID = "ca-app-pub-2904702161629737~1751911745";

	// Token: 0x040002BD RID: 701
	public static readonly string AdMob_iOS_UnitID_Banner = "ca-app-pub-2904702161629737/1781208629";

	// Token: 0x040002BE RID: 702
	public static readonly string AdMob_iOS_UnitID_Interstitial = "ca-app-pub-2904702161629737/8125748401";

	// Token: 0x040002BF RID: 703
	public static readonly string AdMob__TEST__Banner = "ca-app-pub-3940256099942544/6300978111";

	// Token: 0x040002C0 RID: 704
	public static readonly string AdMob__TEST__Interstitial = "ca-app-pub-3940256099942544/1033173712";

	// Token: 0x040002C1 RID: 705
	public const int CANVAS_SIZE_X = 640;

	// Token: 0x040002C2 RID: 706
	public const int CANVAS_SIZE_Y = 960;

	// Token: 0x040002C3 RID: 707
	public const int IAPListenerCount = 4;

	// Token: 0x040002C4 RID: 708
	public const int TIMESPAN_MAX = 2592000;

	// Token: 0x040002C5 RID: 709
	public const int LOCAL_NOTIFICATION_UNIQUE_ID = 72091216;

	// Token: 0x040002C6 RID: 710
	public const int BASE_HP = 100;

	// Token: 0x040002C7 RID: 711
	public const int REST_TIME = 180;

	// Token: 0x040002C8 RID: 712
	public const int TRAVEL_TIME_MIN = 60;

	// Token: 0x040002C9 RID: 713
	public const int DetourMax = 3;

	// Token: 0x040002CA RID: 714
	public const int BASE_PICTURE_PER = 70;

	// Token: 0x040002CB RID: 715
	public const int PICTURE_GETMAX = 4;

	// Token: 0x040002CC RID: 716
	public static readonly Dictionary<int, int> PICTURE_TOOLS_PLUSPER = new Dictionary<int, int>
	{
		{
			0,
			0
		},
		{
			1,
			15
		},
		{
			2,
			30
		},
		{
			3,
			50
		}
	};

	// Token: 0x040002CD RID: 717
	public const int FourLeafCloverID = 1000;

	// Token: 0x040002CE RID: 718
	public const int ItemIDBorder = 10000;

	// Token: 0x040002CF RID: 719
	public const int CollectionIDBorder = 30000;

	// Token: 0x040002D0 RID: 720
	public const int StartCloverPoint = 0;

	// Token: 0x040002D1 RID: 721
	public const float CloverGetTime = 0.4f;

	// Token: 0x040002D2 RID: 722
	public const float CloverDestroyTime = 0.6f;

	// Token: 0x040002D3 RID: 723
	public const int CloverMax = 999999;

	// Token: 0x040002D4 RID: 724
	public const int TicketMax = 999;

	// Token: 0x040002D5 RID: 725
	public const int HaveItemMax = 99;

	// Token: 0x040002D6 RID: 726
	public const int COLLECT_MAX = 100;

	// Token: 0x040002D7 RID: 727
	public const int SPECIALTY_MAX = 100;

	// Token: 0x040002D8 RID: 728
	public const int ACHIEVE_MAX = 100;

	// Token: 0x040002D9 RID: 729
	public const int ALBUM_MAX = 60;

	// Token: 0x040002DA RID: 730
	public const int SNAP_MAX = 30;

	// Token: 0x040002DB RID: 731
	public const float SNAP_SCALE_MIN = 1f;

	// Token: 0x040002DC RID: 732
	public const float SNAP_SCALE_MAX = 1.5f;

	// Token: 0x040002DD RID: 733
	public const float SNAP_SIZE_X = 240f;

	// Token: 0x040002DE RID: 734
	public const float SNAP_SIZE_Y = 160f;

	// Token: 0x040002DF RID: 735
	public const float ALBUM_SIZE_X = 500f;

	// Token: 0x040002E0 RID: 736
	public const float ALBUM_SIZE_Y = 350f;

	// Token: 0x040002E1 RID: 737
	public const float TwitterTime = 2f;

	// Token: 0x040002E2 RID: 738
	public const int RAFFEL_NEEDTICKETS = 5;

	// Token: 0x040002E3 RID: 739
	public static readonly Dictionary<int, int> COLLECT_PER = new Dictionary<int, int>
	{
		{
			0,
			15
		},
		{
			1,
			30
		},
		{
			2,
			50
		},
		{
			3,
			100
		}
	};

	// Token: 0x040002E4 RID: 740
	public const int SPECIALTY_PER = 60;

	// Token: 0x040002E5 RID: 741
	public const int TRAVEL_ITEM_GETMAX = 10;

	// Token: 0x040002E6 RID: 742
	public const int MAIL_MAX = 100;

	// Token: 0x040002E7 RID: 743
	public const int FROG_RESTTIME = 21600;

	// Token: 0x040002E8 RID: 744
	public const int FROG_RESTTIME_MAX = 43200;

	// Token: 0x040002E9 RID: 745
	public const int FROG_STANDBY_WAIT_MIN = 300;

	// Token: 0x040002EA RID: 746
	public const int FROG_DRIFTRETURNTIME = 300;

	// Token: 0x040002EB RID: 747
	public const int FROG_DRIFTRETURNTIME_MAX = 1800;

	// Token: 0x040002EC RID: 748
	public const int FRIEND_VISIT_RNDPER = 10;

	// Token: 0x040002ED RID: 749
	public const int FRIEND_VISIT_RNDSEC = 1800;

	// Token: 0x040002EE RID: 750
	public const int FRIEND_VISIT_COOL = 21600;

	// Token: 0x040002EF RID: 751
	public const int FRIEND_VISIT_ACTCOUNT_MIN = 6;

	// Token: 0x040002F0 RID: 752
	public const int FRIEND_VISIT_ACTCOUNT_MAX = 8;

	// Token: 0x040002F1 RID: 753
	public const int FRIEND_RNDPOS_MAX = 3;

	// Token: 0x040002F2 RID: 754
	public static readonly float[] FRIEND_ITEM_DEBUFF = new float[]
	{
		0.6f,
		0.75f,
		0.9f
	};

	// Token: 0x040002F3 RID: 755
	public const int Leaflet_Per = 100;

	// Token: 0x040002F4 RID: 756
	public const int TutorialShopID = 0;

	// Token: 0x040002F5 RID: 757
	public const int TutorialBuyItemID = 0;

	// Token: 0x040002F6 RID: 758
	public const int TutorialGoalNodeId = 1;

	// Token: 0x040002F7 RID: 759
	public const int TutorialTravelResetID = 3009;

	// Token: 0x040002F8 RID: 760
	public const int SHOP_TICKET_PER = 15;

	// Token: 0x040002F9 RID: 761
	public static readonly Dictionary<Define.Gift, int> FRIEND_GIFTPER_NORMAL = new Dictionary<Define.Gift, int>
	{
		{
			Define.Gift.Clover,
			80
		},
		{
			Define.Gift.FourClover,
			18
		},
		{
			Define.Gift.Ticket,
			2
		},
		{
			Define.Gift.MAX,
			100
		}
	};

	// Token: 0x040002FA RID: 762
	public static readonly Dictionary<Define.Gift, int> FRIEND_GIFTPER_RARE = new Dictionary<Define.Gift, int>
	{
		{
			Define.Gift.Clover,
			20
		},
		{
			Define.Gift.FourClover,
			50
		},
		{
			Define.Gift.Ticket,
			30
		},
		{
			Define.Gift.MAX,
			100
		}
	};

	// Token: 0x040002FB RID: 763
	public static readonly Dictionary<Define.Gift, int> FRIEND_GIFTFIX = new Dictionary<Define.Gift, int>
	{
		{
			Define.Gift.Clover,
			0
		},
		{
			Define.Gift.FourClover,
			1
		},
		{
			Define.Gift.Ticket,
			1
		}
	};

	// Token: 0x040002FC RID: 764
	public const int FRIEND_GIFTBOUNUS_CLOVER = 20;

	// Token: 0x040002FD RID: 765
	public const int FRIEND_GIFTBOUNUS_TICKET = 1;

	// Token: 0x040002FE RID: 766
	public const int FRIEND_GIFTBOUNUS_TICKET_MAX = 3;

	// Token: 0x040002FF RID: 767
	public const int BAGITEMS = 4;

	// Token: 0x04000300 RID: 768
	public const int DESKITEMS = 8;

	// Token: 0x04000301 RID: 769
	public const int SIZE_X_MIN = 160;

	// Token: 0x04000302 RID: 770
	public const int SIZE_X_MAX = 320;

	// Token: 0x04000303 RID: 771
	public static int PRIZE_WHITE_ID = 0;

	// Token: 0x04000304 RID: 772
	public static readonly Dictionary<Rank, int> PrizeBalls = new Dictionary<Rank, int>
	{
		{
			Rank.White,
			60
		},
		{
			Rank.Blue,
			27
		},
		{
			Rank.Green,
			9
		},
		{
			Rank.Red,
			3
		},
		{
			Rank.Gold,
			1
		},
		{
			Rank.RankMax,
			100
		}
	};

	// Token: 0x04000305 RID: 773
	public static readonly Dictionary<Rank, string> PrizeBallName = new Dictionary<Rank, string>
	{
		{
			Rank.White,
			"白玉"
		},
		{
			Rank.Blue,
			"青玉"
		},
		{
			Rank.Green,
			"緑玉"
		},
		{
			Rank.Red,
			"赤玉"
		},
		{
			Rank.Gold,
			"黄玉"
		}
	};

	// Token: 0x04000306 RID: 774
	public static readonly Dictionary<Rank, int> PrizeClover = new Dictionary<Rank, int>
	{
		{
			Rank.White,
			10
		},
		{
			Rank.Blue,
			30
		},
		{
			Rank.Green,
			100
		},
		{
			Rank.Red,
			350
		},
		{
			Rank.Gold,
			1000
		}
	};

	// Token: 0x04000307 RID: 775
	public static readonly int FrogPatternMax = 3;

	// Token: 0x04000308 RID: 776
	public static readonly Dictionary<int, List<string>> Frogpattern = new Dictionary<int, List<string>>
	{
		{
			0,
			new List<string>
			{
				"eat",
				"eat",
				"eat",
				"eat",
				"doku",
				"doku",
				"doku",
				"doku",
				"doku_s",
				"doku_s",
				"make",
				"make",
				"make"
			}
		},
		{
			1,
			new List<string>
			{
				"eat",
				"eat",
				"doku",
				"doku",
				"doku",
				"doku",
				"doku_s",
				"doku_s",
				"write",
				"write",
				"write",
				"write",
				"write"
			}
		},
		{
			2,
			new List<string>
			{
				"write",
				"write",
				"write",
				"write",
				"eat",
				"eat",
				"make",
				"make",
				"make",
				"make",
				"write",
				"write",
				"write"
			}
		}
	};

	// Token: 0x04000309 RID: 777
	public static readonly Dictionary<string, int> FrogMotionNum = new Dictionary<string, int>
	{
		{
			"doku",
			0
		},
		{
			"doku_s",
			1
		},
		{
			"write",
			2
		},
		{
			"make",
			3
		},
		{
			"eat",
			4
		}
	};

	// Token: 0x0400030A RID: 778
	public static readonly Dictionary<int, string> FrogMotionName = new Dictionary<int, string>
	{
		{
			0,
			"AniAnimation/MainIn/dokusyo_ie"
		},
		{
			1,
			"AniAnimation/MainIn/inemuri_ie"
		},
		{
			2,
			"AniAnimation/MainIn/hikki_ie"
		},
		{
			3,
			"AniAnimation/MainIn/sagyou_ie"
		},
		{
			4,
			"AniAnimation/MainIn/syokuzi_ie"
		}
	};

	// Token: 0x0400030B RID: 779
	public static readonly Dictionary<int, Vector2> FrogMotionPos = new Dictionary<int, Vector2>
	{
		{
			0,
			new Vector2(0.78f, 3.12f)
		},
		{
			1,
			new Vector2(0.78f, 3.12f)
		},
		{
			2,
			new Vector2(-0.97f, 2.89f)
		},
		{
			3,
			new Vector2(2.03f, -1.82f)
		},
		{
			4,
			new Vector2(2.03f, -1.82f)
		}
	};

	// Token: 0x0400030C RID: 780
	public static readonly Dictionary<int, Vector2> FrogHitOffset = new Dictionary<int, Vector2>
	{
		{
			0,
			new Vector2(0f, 0.35f)
		},
		{
			1,
			new Vector2(0f, 0.35f)
		},
		{
			2,
			new Vector2(0.05f, 0.35f)
		},
		{
			3,
			new Vector2(-0.1f, 0.3f)
		},
		{
			4,
			new Vector2(-0.25f, 0.3f)
		}
	};

	// Token: 0x0400030D RID: 781
	public static readonly Dictionary<int, Vector2> FrogHitSize = new Dictionary<int, Vector2>
	{
		{
			0,
			new Vector2(1.05f, 1.15f)
		},
		{
			1,
			new Vector2(1.05f, 1.15f)
		},
		{
			2,
			new Vector2(1.2f, 1f)
		},
		{
			3,
			new Vector2(1.1f, 1.2f)
		},
		{
			4,
			new Vector2(1.3f, 1.2f)
		}
	};

	// Token: 0x0400030E RID: 782
	public static readonly Vector2 PICTURE_NOSET_POS = new Vector2(999f, 999f);

	// Token: 0x0400030F RID: 783
	public static readonly Dictionary<string, string> BGMDict = new Dictionary<string, string>
	{
		{
			"BGM_Default",
			"bgm01"
		}
	};

	// Token: 0x04000310 RID: 784
	public static readonly Dictionary<string, string> SEDict = new Dictionary<string, string>
	{
		{
			"SE_Enter",
			"se01"
		},
		{
			"SE_Cancel",
			"se02"
		},
		{
			"SE_Cursor",
			"se03"
		},
		{
			"SE_Popup",
			"se04"
		},
		{
			"SE_Move",
			"se05"
		},
		{
			"SE_Clover",
			"se06"
		},
		{
			"SE_PageNext",
			"se07"
		},
		{
			"SE_Buy",
			"se08"
		},
		{
			"SE_Camera",
			"se09"
		},
		{
			"SE_Raffle",
			"se10"
		},
		{
			"SE_RaffleResult",
			"se11"
		},
		{
			"SE_Frog",
			"se12"
		},
		{
			"SE_Knock",
			"se079_Homes"
		},
		{
			"SE_Sparrow",
			"se080_Homes"
		},
		{
			"SE_Grassy",
			"se115_Hyakki"
		}
	};

	// Token: 0x02000078 RID: 120
	public enum Gift
	{
		// Token: 0x04000312 RID: 786
		NONE = -1,
		// Token: 0x04000313 RID: 787
		Clover,
		// Token: 0x04000314 RID: 788
		FourClover,
		// Token: 0x04000315 RID: 789
		Ticket,
		// Token: 0x04000316 RID: 790
		MAX
	}

	// Token: 0x02000079 RID: 121
	public enum BagItem
	{
		// Token: 0x04000318 RID: 792
		NONE = -1,
		// Token: 0x04000319 RID: 793
		LunchBox,
		// Token: 0x0400031A RID: 794
		Amulet,
		// Token: 0x0400031B RID: 795
		Tool_1,
		// Token: 0x0400031C RID: 796
		Tool_2,
		// Token: 0x0400031D RID: 797
		MAX
	}

	// Token: 0x0200007A RID: 122
	public enum DeskItem
	{
		// Token: 0x0400031F RID: 799
		NONE = -1,
		// Token: 0x04000320 RID: 800
		LunchBox_1,
		// Token: 0x04000321 RID: 801
		LunchBox_2,
		// Token: 0x04000322 RID: 802
		Amulet_1,
		// Token: 0x04000323 RID: 803
		Amulet_2,
		// Token: 0x04000324 RID: 804
		Tool_1,
		// Token: 0x04000325 RID: 805
		Tool_2,
		// Token: 0x04000326 RID: 806
		Tool_3,
		// Token: 0x04000327 RID: 807
		Tool_4,
		// Token: 0x04000328 RID: 808
		MAX
	}
}
