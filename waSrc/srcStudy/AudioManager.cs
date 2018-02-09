using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000031 RID: 49
public class AudioManager : MonoBehaviour
{
	// Token: 0x060001FE RID: 510 RVA: 0x00006BE4 File Offset: 0x00004FE4
	public void Init()
	{
		this.bgmDict = new Dictionary<string, int>();
		this.seDict = new Dictionary<string, int>();
		int num = 0;
		foreach (AudioClip audioClip in this.BGMList)
		{
			this.bgmDict.Add(audioClip.name, num);
			num++;
		}
		num = 0;
		foreach (AudioClip audioClip2 in this.SEList)
		{
			this.seDict.Add(audioClip2.name, num);
			num++;
		}
	}

	// Token: 0x060001FF RID: 511 RVA: 0x00006CC4 File Offset: 0x000050C4
	public void Get_MainCamera_SeAudioSouce()
	{
		this.seSource = Camera.main.gameObject.GetComponent<AudioSource>();
	}

	// Token: 0x06000200 RID: 512 RVA: 0x00006CDC File Offset: 0x000050DC
	public void PlayBGM(string bgmName)
	{
		if (!this.bgmDict.ContainsKey(bgmName))
		{
			Debug.Log("[AudioManager] BGMが見つかりませんでした：" + bgmName);
		}
		this.StopBGM();
		this.bgmSource.clip = this.BGMList[this.bgmDict[bgmName]];
		this.bgmSource.Play();
	}

	// Token: 0x06000201 RID: 513 RVA: 0x00006D40 File Offset: 0x00005140
	public void PlaySE(string seName)
	{
		if (!this.seDict.ContainsKey(seName))
		{
			Debug.Log("[AudioManager] SEが見つかりませんでした：" + seName);
		}
		this.seSource.PlayOneShot(this.SEList[this.seDict[seName]]);
	}

	// Token: 0x06000202 RID: 514 RVA: 0x00006D90 File Offset: 0x00005190
	public void StopBGM()
	{
		this.bgmSource.Stop();
		this.bgmSource.clip = null;
	}

	// Token: 0x06000203 RID: 515 RVA: 0x00006DA9 File Offset: 0x000051A9
	public void StopSE()
	{
		this.seSource.Stop();
		this.seSource.clip = null;
	}

	// Token: 0x06000204 RID: 516 RVA: 0x00006DC2 File Offset: 0x000051C2
	public bool isPlayingBGM()
	{
		return this.bgmSource.isPlaying;
	}

	// Token: 0x06000205 RID: 517 RVA: 0x00006DCF File Offset: 0x000051CF
	public int GetBgmVolume()
	{
		return this.bgmVolume;
	}

	// Token: 0x06000206 RID: 518 RVA: 0x00006DD7 File Offset: 0x000051D7
	public void SetBgmVolume(int vol)
	{
		this.bgmVolume = vol;
		this.Mixer.SetFloat("BGM_Volume", this.Cng_db(vol));
	}

	// Token: 0x06000207 RID: 519 RVA: 0x00006DF8 File Offset: 0x000051F8
	public int GetSeVolume()
	{
		return this.seVolume;
	}

	// Token: 0x06000208 RID: 520 RVA: 0x00006E00 File Offset: 0x00005200
	public void SetSeVolume(int vol)
	{
		this.seVolume = vol;
		this.Mixer.SetFloat("SE_Volume", this.Cng_db(vol));
	}

	// Token: 0x06000209 RID: 521 RVA: 0x00006E24 File Offset: 0x00005224
	public float Cng_db(int volume)
	{
		float f = Mathf.Clamp((float)volume / 100f, 0.0001f, 1f);
		float value = 20f * Mathf.Log10(f);
		return Mathf.Clamp(value, -80f, 0f);
	}

	// Token: 0x0600020A RID: 522 RVA: 0x00006E68 File Offset: 0x00005268
	public void AudioInit()
	{
		this.SetBgmVolume(SuperGameMaster.saveData.bgmVolume);
		this.SetSeVolume(SuperGameMaster.saveData.seVolume);
		if (!this.isPlayingBGM() && SuperGameMaster.tutorial.BGMOk())
		{
			this.PlayBGM(Define.BGMDict["BGM_Default"]);
		}
	}

	// Token: 0x0600020B RID: 523 RVA: 0x00006EC4 File Offset: 0x000052C4
	public void SaveVolume()
	{
		SuperGameMaster.saveData.bgmVolume = this.bgmVolume;
		SuperGameMaster.saveData.seVolume = this.seVolume;
	}

	// Token: 0x040000C4 RID: 196
	public AudioMixer Mixer;

	// Token: 0x040000C5 RID: 197
	public AudioMixerGroup MixerBgmGroup;

	// Token: 0x040000C6 RID: 198
	public AudioMixerGroup MixerSeGroup;

	// Token: 0x040000C7 RID: 199
	[Space(10f)]
	public AudioSource bgmSource;

	// Token: 0x040000C8 RID: 200
	public AudioSource seSource;

	// Token: 0x040000C9 RID: 201
	private Dictionary<string, int> bgmDict;

	// Token: 0x040000CA RID: 202
	private Dictionary<string, int> seDict;

	// Token: 0x040000CB RID: 203
	private int bgmVolume;

	// Token: 0x040000CC RID: 204
	private int seVolume;

	// Token: 0x040000CD RID: 205
	public const float dbMax = 0f;

	// Token: 0x040000CE RID: 206
	public const float dbMin = -80f;

	// Token: 0x040000CF RID: 207
	[Space(10f)]
	public List<AudioClip> BGMList;

	// Token: 0x040000D0 RID: 208
	public List<AudioClip> SEList;
}
