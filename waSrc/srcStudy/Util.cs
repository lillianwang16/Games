using System;
using System.IO;
using UnityEngine;

// Token: 0x0200000A RID: 10
public class Util
{
	// Token: 0x0600001E RID: 30 RVA: 0x00002E28 File Offset: 0x00001228
	public static byte[] ReadBytesFromResource(string name, string extension)
	{
		if (!name.EndsWith(extension))
		{
			name += extension;
		}
		TextAsset textAsset = Resources.Load<TextAsset>(name);
		if (textAsset == null)
		{
			return null;
		}
		return textAsset.bytes;
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00002E65 File Offset: 0x00001265
	public static string CreatePngPathFromAssets(string path)
	{
		return Util.CreateAssetPath(path, ".png");
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00002E72 File Offset: 0x00001272
	public static string CreateAnmPathFromAssets(string path)
	{
		return Util.CreateAssetPath(path, ".anm");
	}

	// Token: 0x06000021 RID: 33 RVA: 0x00002E7F File Offset: 0x0000127F
	public static string CreateAssetPath(string path, string extension)
	{
		if (!path.EndsWith(extension))
		{
			path += extension;
		}
		return Path.Combine(Application.dataPath, path);
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00002EA4 File Offset: 0x000012A4
	public static string CombinePaths(string first, params string[] others)
	{
		string text = first;
		foreach (string path in others)
		{
			text = Path.Combine(text, path);
		}
		return text;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00002ED6 File Offset: 0x000012D6
	public static void pf(string format, params object[] args)
	{
		Util.p(string.Format(format, args));
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00002EE4 File Offset: 0x000012E4
	public static void p(object o)
	{
		Debug.Log(o.ToString());
	}
}
