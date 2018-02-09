using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000DD RID: 221
public class GetServer : MonoBehaviour
{
	// Token: 0x06000604 RID: 1540 RVA: 0x00024364 File Offset: 0x00022764
	public IEnumerator Request(string url)
	{
		Debug.Log("[GetServer] リクエスト開始");
		WWW result = new WWW(url);
		float endTime = Time.realtimeSinceStartup + this.timeOutLimit;
		while (!result.isDone && Time.realtimeSinceStartup < endTime)
		{
			yield return 0;
		}
		this.isTimeOut = !result.isDone;
		this.www = result;
		if (this.isTimeOut)
		{
			Debug.Log("[GetServer] タイムアウト");
		}
		else if (this.www.error == null)
		{
			Debug.Log("[GetServer] 成功: " + this.www.text);
		}
		else
		{
			Debug.Log("[GetServer] エラー: " + this.www.error);
		}
		yield break;
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x00024386 File Offset: 0x00022786
	public bool checkConnectionError()
	{
		return this.www.error != null || this.isTimeOut;
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x000243A8 File Offset: 0x000227A8
	public string getErrorString()
	{
		if (this.isTimeOut)
		{
			return "接続がタイムアウトしました！ 時間を置いて、アプリを再起動してください。";
		}
		Debug.Log("接続エラー詳細：" + this.www.error);
		return "接続に失敗しました！ インターネットの接続設定を確認してください。";
	}

	// Token: 0x04000535 RID: 1333
	public float timeOutLimit = 5f;

	// Token: 0x04000536 RID: 1334
	public WWW www;

	// Token: 0x04000537 RID: 1335
	public bool isTimeOut;
}
