using System;
using System.Collections.Generic;
using GoogleMobileAds.Api.Mediation;

namespace GoogleMobileAds.Api
{
	// Token: 0x02000010 RID: 16
	public class AdRequest
	{
		// Token: 0x06000044 RID: 68 RVA: 0x000031F0 File Offset: 0x000015F0
		private AdRequest(AdRequest.Builder builder)
		{
			this.TestDevices = new List<string>(builder.TestDevices);
			this.Keywords = new HashSet<string>(builder.Keywords);
			this.Birthday = builder.Birthday;
			this.Gender = builder.Gender;
			this.TagForChildDirectedTreatment = builder.ChildDirectedTreatmentTag;
			this.Extras = new Dictionary<string, string>(builder.Extras);
			this.MediationExtras = builder.MediationExtras;
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00003266 File Offset: 0x00001666
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000326E File Offset: 0x0000166E
		public List<string> TestDevices { get; private set; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00003277 File Offset: 0x00001677
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000327F File Offset: 0x0000167F
		public HashSet<string> Keywords { get; private set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00003288 File Offset: 0x00001688
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00003290 File Offset: 0x00001690
		public DateTime? Birthday { get; private set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00003299 File Offset: 0x00001699
		// (set) Token: 0x0600004C RID: 76 RVA: 0x000032A1 File Offset: 0x000016A1
		public Gender? Gender { get; private set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600004D RID: 77 RVA: 0x000032AA File Offset: 0x000016AA
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000032B2 File Offset: 0x000016B2
		public bool? TagForChildDirectedTreatment { get; private set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000032BB File Offset: 0x000016BB
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000032C3 File Offset: 0x000016C3
		public Dictionary<string, string> Extras { get; private set; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000032CC File Offset: 0x000016CC
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000032D4 File Offset: 0x000016D4
		public List<MediationExtras> MediationExtras { get; private set; }

		// Token: 0x04000042 RID: 66
		public const string Version = "3.9.0";

		// Token: 0x04000043 RID: 67
		public const string TestDeviceSimulator = "SIMULATOR";

		// Token: 0x02000011 RID: 17
		public class Builder
		{
			// Token: 0x06000053 RID: 83 RVA: 0x000032E0 File Offset: 0x000016E0
			public Builder()
			{
				this.TestDevices = new List<string>();
				this.Keywords = new HashSet<string>();
				this.Birthday = null;
				this.Gender = null;
				this.ChildDirectedTreatmentTag = null;
				this.Extras = new Dictionary<string, string>();
				this.MediationExtras = new List<MediationExtras>();
			}

			// Token: 0x17000012 RID: 18
			// (get) Token: 0x06000054 RID: 84 RVA: 0x0000334C File Offset: 0x0000174C
			// (set) Token: 0x06000055 RID: 85 RVA: 0x00003354 File Offset: 0x00001754
			internal List<string> TestDevices { get; private set; }

			// Token: 0x17000013 RID: 19
			// (get) Token: 0x06000056 RID: 86 RVA: 0x0000335D File Offset: 0x0000175D
			// (set) Token: 0x06000057 RID: 87 RVA: 0x00003365 File Offset: 0x00001765
			internal HashSet<string> Keywords { get; private set; }

			// Token: 0x17000014 RID: 20
			// (get) Token: 0x06000058 RID: 88 RVA: 0x0000336E File Offset: 0x0000176E
			// (set) Token: 0x06000059 RID: 89 RVA: 0x00003376 File Offset: 0x00001776
			internal DateTime? Birthday { get; private set; }

			// Token: 0x17000015 RID: 21
			// (get) Token: 0x0600005A RID: 90 RVA: 0x0000337F File Offset: 0x0000177F
			// (set) Token: 0x0600005B RID: 91 RVA: 0x00003387 File Offset: 0x00001787
			internal Gender? Gender { get; private set; }

			// Token: 0x17000016 RID: 22
			// (get) Token: 0x0600005C RID: 92 RVA: 0x00003390 File Offset: 0x00001790
			// (set) Token: 0x0600005D RID: 93 RVA: 0x00003398 File Offset: 0x00001798
			internal bool? ChildDirectedTreatmentTag { get; private set; }

			// Token: 0x17000017 RID: 23
			// (get) Token: 0x0600005E RID: 94 RVA: 0x000033A1 File Offset: 0x000017A1
			// (set) Token: 0x0600005F RID: 95 RVA: 0x000033A9 File Offset: 0x000017A9
			internal Dictionary<string, string> Extras { get; private set; }

			// Token: 0x17000018 RID: 24
			// (get) Token: 0x06000060 RID: 96 RVA: 0x000033B2 File Offset: 0x000017B2
			// (set) Token: 0x06000061 RID: 97 RVA: 0x000033BA File Offset: 0x000017BA
			internal List<MediationExtras> MediationExtras { get; private set; }

			// Token: 0x06000062 RID: 98 RVA: 0x000033C3 File Offset: 0x000017C3
			public AdRequest.Builder AddKeyword(string keyword)
			{
				this.Keywords.Add(keyword);
				return this;
			}

			// Token: 0x06000063 RID: 99 RVA: 0x000033D3 File Offset: 0x000017D3
			public AdRequest.Builder AddTestDevice(string deviceId)
			{
				this.TestDevices.Add(deviceId);
				return this;
			}

			// Token: 0x06000064 RID: 100 RVA: 0x000033E2 File Offset: 0x000017E2
			public AdRequest Build()
			{
				return new AdRequest(this);
			}

			// Token: 0x06000065 RID: 101 RVA: 0x000033EA File Offset: 0x000017EA
			public AdRequest.Builder SetBirthday(DateTime birthday)
			{
				this.Birthday = new DateTime?(birthday);
				return this;
			}

			// Token: 0x06000066 RID: 102 RVA: 0x000033F9 File Offset: 0x000017F9
			public AdRequest.Builder SetGender(Gender gender)
			{
				this.Gender = new Gender?(gender);
				return this;
			}

			// Token: 0x06000067 RID: 103 RVA: 0x00003408 File Offset: 0x00001808
			public AdRequest.Builder AddMediationExtras(MediationExtras extras)
			{
				this.MediationExtras.Add(extras);
				return this;
			}

			// Token: 0x06000068 RID: 104 RVA: 0x00003417 File Offset: 0x00001817
			public AdRequest.Builder TagForChildDirectedTreatment(bool tagForChildDirectedTreatment)
			{
				this.ChildDirectedTreatmentTag = new bool?(tagForChildDirectedTreatment);
				return this;
			}

			// Token: 0x06000069 RID: 105 RVA: 0x00003426 File Offset: 0x00001826
			public AdRequest.Builder AddExtra(string key, string value)
			{
				this.Extras.Add(key, value);
				return this;
			}
		}
	}
}
