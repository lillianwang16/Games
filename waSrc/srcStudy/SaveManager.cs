using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Runtime.Serialization.Formatters.Binary;
using Picture;
using UnityEngine;

// Token: 0x020000E7 RID: 231
public class SaveManager : MonoBehaviour
{
	// Token: 0x0600064C RID: 1612 RVA: 0x00025CFB File Offset: 0x000240FB
	public void SaveData(SaveDataFormat sData)
	{
		this.Save_Binary(sData);
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x00025D04 File Offset: 0x00024104
	public SaveDataFormat LoadData()
	{
		return this.Load_Binary(false);
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x00025D10 File Offset: 0x00024110
	public IEnumerator CheckSerializeConverter()
	{
		if (File.Exists(Define.SaveName_Serialize))
		{
			Debug.Log("[SaveManager] serialize -> binary 変換を行ないます");
			SaveDataFormat save_Serialize = this.Load_Serialize(false);
			yield return null;
			this.Save_Binary(save_Serialize);
			yield return null;
			if (!Directory.Exists(Define.SaveName_Binary_PicDir))
			{
				Directory.CreateDirectory(Define.SaveName_Binary_PicDir);
				Debug.Log("[SaveManager] 写真ディレクトリ生成：" + Define.SaveName_Binary_PicDir);
			}
			for (int i = 0; i < save_Serialize.albumPicture.Count; i++)
			{
				this.Save_Picture(save_Serialize, SaveType.Album, i);
				if (i % 3 == 2)
				{
					yield return null;
				}
			}
			yield return null;
			for (int j = 0; j < save_Serialize.tmpPicture.Count; j++)
			{
				this.Save_Picture(save_Serialize, SaveType.Temp, j);
				if (j % 3 == 2)
				{
					yield return null;
				}
			}
			yield return null;
			this.CheckPictureDirectory();
			yield return null;
		}
		yield break;
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x00025D2C File Offset: 0x0002412C
	public IEnumerator CheckSerializeDelete()
	{
		if (File.Exists(Define.SaveName_Serialize))
		{
			Debug.Log("[SaveManager] serialize セーブデータを削除します");
			yield return File.Exists(Define.SaveName_Binary);
			File.Delete(Define.SaveName_Serialize);
			SuperGameMaster.LoadingProgress += 5f;
			yield return null;
			yield return !File.Exists(Define.SaveName_Serialize);
		}
		yield break;
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x00025D40 File Offset: 0x00024140
	public void Save_Binary(SaveDataFormat sData)
	{
		this.Save_Binary(sData, false);
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x00025D4A File Offset: 0x0002414A
	public void Save_Binary_BackUp(SaveDataFormat sData)
	{
		this.Save_Binary(sData, true);
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x00025D54 File Offset: 0x00024154
	public void Save_Binary(SaveDataFormat sData, bool back)
	{
		string text = Define.SaveName_Binary;
		if (back)
		{
			text += ".back";
		}
		byte[] array = sData.ConvertBinary_Main(true, null);
		File.WriteAllBytes(text, array);
		Debug.Log(string.Concat(new object[]
		{
			"[SaveManager] Binary セーブしました：",
			text,
			"[",
			array.Length,
			"]"
		}));
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x00025DC0 File Offset: 0x000241C0
	public SaveDataFormat Load_Binary()
	{
		return this.Load_Binary(false);
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x00025DC9 File Offset: 0x000241C9
	public SaveDataFormat Load_Binary_BackUp()
	{
		return this.Load_Binary(true);
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x00025DD4 File Offset: 0x000241D4
	public SaveDataFormat Load_Binary(bool back)
	{
		string text = Define.SaveName_Binary;
		if (back)
		{
			text += ".back";
		}
		SaveDataFormat saveDataFormat = new SaveDataFormat();
		saveDataFormat.initialize();
		try
		{
			byte[] array = new byte[0];
			array = File.ReadAllBytes(text);
			saveDataFormat.ConvertBinary_Main(false, array);
			this.Save_Binary_BackUp(new SaveDataFormat(saveDataFormat));
			Debug.Log(string.Concat(new object[]
			{
				"[SaveManager] Binary ロードしました：",
				text,
				"[",
				array.Length,
				"]"
			}));
		}
		catch (FileNotFoundException ex)
		{
			saveDataFormat.initialize();
			this.Save_Binary(new SaveDataFormat(saveDataFormat));
			this.Save_Binary_BackUp(new SaveDataFormat(saveDataFormat));
			Debug.Log("[SaveManager] Binary データを新規作成しました (FileNotFoundException)：" + text + " / " + ex.ToString());
		}
		catch (IsolatedStorageException ex2)
		{
			saveDataFormat.initialize();
			this.Save_Binary(new SaveDataFormat(saveDataFormat));
			this.Save_Binary_BackUp(new SaveDataFormat(saveDataFormat));
			Debug.Log("[SaveManager] Binary データを新規作成しました (IsolatedStorageException)：" + text + " / " + ex2.ToString());
		}
		catch (Exception ex3)
		{
			if (back)
			{
				Debug.LogError("[SaveManager] Binary バックアップからの読み込みに失敗しました：" + text + " / " + ex3.ToString());
				throw;
			}
			Debug.LogWarning("[SaveManager] Binary データの読み込みに失敗しました。バックアップから復元します。" + text + " / " + ex3.ToString());
			return this.Load_Binary_BackUp();
		}
		this.load_Binary_Version = saveDataFormat.version;
		saveDataFormat.versionFix();
		saveDataFormat.DataDiffCheck();
		return saveDataFormat;
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x00025F78 File Offset: 0x00024378
	public void CheckPictureDirectory()
	{
		if (!Directory.Exists(Define.SaveName_Binary_PicDir))
		{
			Directory.CreateDirectory(Define.SaveName_Binary_PicDir);
			Debug.Log("[SaveManager] 写真ディレクトリ生成：" + Define.SaveName_Binary_PicDir);
		}
		if (!File.Exists(Define.SaveName_Binary_PicDir + Define.PicSaveDict[SaveType.INDEX]))
		{
			this.album_Path = new List<string>();
			string[] files = Directory.GetFiles(Define.SaveName_Binary_PicDir, Define.PicSaveDict[SaveType.Album].Substring(1) + "*.sav");
			for (int i = 0; i < files.Length; i++)
			{
				int num = files[i].IndexOf(Define.SaveName_Binary_PicDir_SimplePath);
				if (num == -1)
				{
					break;
				}
				string item = files[i].Remove(0, num + Define.SaveName_Binary_PicDir_SimplePath.Length);
				this.album_Path.Add(item);
			}
			Debug.Log("[SaveManager] 写真ファイルを検出しました：" + this.album_Path.Count);
			files = Directory.GetFiles(Define.SaveName_Binary_PicDir, Define.PicSaveDict[SaveType.Temp].Substring(1) + "*.sav");
			for (int j = 0; j < files.Length; j++)
			{
				if (File.Exists(files[j]))
				{
					File.Delete(files[j]);
				}
				if (File.Exists(files[j] + ".back"))
				{
					File.Delete(files[j] + ".back");
				}
			}
			this.Update_PictureIndex();
		}
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x000260FC File Offset: 0x000244FC
	public void Update_PictureIndex()
	{
		string text = Define.SaveName_Binary_PicDir + Define.PicSaveDict[SaveType.INDEX];
		byte[] bytes = this.ConvertBinary_PictureIndex(true, null);
		File.WriteAllBytes(text, bytes);
		File.WriteAllBytes(text + ".back", bytes);
		Debug.Log("[SaveManager] 写真管理ファイルを更新：" + Define.SaveName_Binary_PicDir + Define.PicSaveDict[SaveType.INDEX]);
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0002615F File Offset: 0x0002455F
	public void Save_Picture(SaveDataFormat sData, SaveType sType, int chgIdx)
	{
		this.Save_Picture(sData, sType, chgIdx, false);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0002616B File Offset: 0x0002456B
	public void Save_Picture_Update(SaveDataFormat sData, SaveType sType, int chgIdx)
	{
		this.Save_Picture(sData, sType, chgIdx, true);
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x00026178 File Offset: 0x00024578
	public void Save_Picture(SaveDataFormat sData, SaveType sType, int chgIdx, bool upDate)
	{
		if (sType != SaveType.Album)
		{
			if (sType == SaveType.Temp)
			{
				string text = Define.PicSaveDict[SaveType.Temp] + DateTime.Now.ToString("yyyymmddhhmmssfff") + ".sav";
				string text2 = Define.SaveName_Binary_PicDir + text;
				byte[] array = sData.ConvertBinary_Picture(true, null, SaveType.Temp, chgIdx);
				if (!upDate)
				{
					this.tmp_Path.Add(text);
				}
				File.WriteAllBytes(Define.SaveName_Binary_PicDir + this.tmp_Path[chgIdx], array);
				File.WriteAllBytes(Define.SaveName_Binary_PicDir + this.tmp_Path[chgIdx] + ".back", array);
				Debug.Log(string.Concat(new object[]
				{
					"[SaveManager] 一時保存写真を保存しました： chgIdx = ",
					chgIdx,
					" [",
					array.Length,
					"] name = ",
					this.tmp_Path[chgIdx]
				}));
			}
		}
		else
		{
			string text3 = Define.PicSaveDict[SaveType.Album] + DateTime.Now.ToString("yyyymmddhhmmssfff") + ".sav";
			string text4 = Define.SaveName_Binary_PicDir + text3;
			byte[] array2 = sData.ConvertBinary_Picture(true, null, SaveType.Album, chgIdx);
			if (!upDate)
			{
				this.album_Path.Add(text3);
			}
			File.WriteAllBytes(Define.SaveName_Binary_PicDir + this.album_Path[chgIdx], array2);
			File.WriteAllBytes(Define.SaveName_Binary_PicDir + this.album_Path[chgIdx] + ".back", array2);
			Debug.Log(string.Concat(new object[]
			{
				"[SaveManager] アルバム写真を保存しました： chgIdx = ",
				chgIdx,
				"[",
				this.album_Path.Count,
				"] (",
				array2.Length,
				") name = ",
				this.album_Path[chgIdx]
			}));
		}
		this.Update_PictureIndex();
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x00026384 File Offset: 0x00024784
	public void Delete_Picture(SaveDataFormat sData, SaveType sType, int chgIdx)
	{
		if (sType != SaveType.Album)
		{
			if (sType == SaveType.Temp)
			{
				Debug.Log(string.Concat(new object[]
				{
					"[SaveManager] 写真を削除します： sType = ",
					sType,
					" / chgIdx = ",
					chgIdx,
					"[",
					this.tmp_Path.Count,
					"] name [",
					this.tmp_Path.Count,
					"] = ",
					this.tmp_Path[chgIdx]
				}));
				if (File.Exists(Define.SaveName_Binary_PicDir + this.tmp_Path[chgIdx]))
				{
					File.Delete(Define.SaveName_Binary_PicDir + this.tmp_Path[chgIdx]);
				}
				if (File.Exists(Define.SaveName_Binary_PicDir + this.tmp_Path[chgIdx] + ".back"))
				{
					File.Delete(Define.SaveName_Binary_PicDir + this.tmp_Path[chgIdx] + ".back");
				}
				this.tmp_Path.RemoveAt(chgIdx);
			}
		}
		else
		{
			Debug.Log(string.Concat(new object[]
			{
				"[SaveManager] 写真を削除します： sType = ",
				sType,
				" / chgIdx = ",
				chgIdx,
				"[",
				this.album_Path.Count,
				"] name[",
				this.album_Path.Count,
				"] = ",
				this.album_Path[chgIdx]
			}));
			if (File.Exists(Define.SaveName_Binary_PicDir + this.album_Path[chgIdx]))
			{
				File.Delete(Define.SaveName_Binary_PicDir + this.album_Path[chgIdx]);
			}
			if (File.Exists(Define.SaveName_Binary_PicDir + this.album_Path[chgIdx] + ".back"))
			{
				File.Delete(Define.SaveName_Binary_PicDir + this.album_Path[chgIdx] + ".back");
			}
			this.album_Path.RemoveAt(chgIdx);
		}
		this.Update_PictureIndex();
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x000265D0 File Offset: 0x000249D0
	public IEnumerator Load_Picture(SaveDataFormat lData)
	{
		this.CheckPictureDirectory();
		yield return null;
		yield return this.Load_Picture_Idx(lData);
		yield return this.Load_Picture_Album(lData);
		yield return this.Load_Picture_Tmp(lData);
		yield return null;
		yield break;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x000265F4 File Offset: 0x000249F4
	public IEnumerator Load_Picture_Idx(SaveDataFormat lData)
	{
		yield return this.Load_Picture_Idx(lData, false);
		yield break;
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x00026618 File Offset: 0x00024A18
	public IEnumerator Load_Picture_Idx_BackUp(SaveDataFormat lData)
	{
		yield return this.Load_Picture_Idx(lData, true);
		yield break;
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0002663C File Offset: 0x00024A3C
	public IEnumerator Load_Picture_Idx(SaveDataFormat lData, bool back)
	{
		string lName_idx = Define.SaveName_Binary_PicDir + Define.PicSaveDict[SaveType.INDEX];
		if (back)
		{
			lName_idx += ".back";
		}
		byte[] lByte_idx = new byte[0];
		bool backUpReload = false;
		bool reIndex = false;
		try
		{
			lByte_idx = File.ReadAllBytes(lName_idx);
			this.ConvertBinary_PictureIndex(false, lByte_idx);
			if (back)
			{
				this.Update_PictureIndex();
			}
			Debug.Log("[SaveManager] 写真インデックスを読み込みました：" + lByte_idx.Length);
		}
		catch (FileNotFoundException ex)
		{
			if (back)
			{
				Debug.LogError("[SaveManager] idx のバックアップからの読み込みに失敗しました (FileNotFoundException)：" + lName_idx + " / " + ex.ToString());
				reIndex = true;
			}
			else
			{
				Debug.LogWarning("[SaveManager] idx の読み込みに失敗したため、バックアップから復元します (FileNotFoundException)");
				backUpReload = true;
			}
		}
		catch (IsolatedStorageException ex2)
		{
			if (back)
			{
				Debug.LogError("[SaveManager] idx のバックアップからの読み込みに失敗しました (IsolatedStorageException)：" + lName_idx + " / " + ex2.ToString());
				reIndex = true;
			}
			else
			{
				Debug.LogWarning("[SaveManager] idx の読み込みに失敗したため、バックアップから復元します (IsolatedStorageException)");
				backUpReload = true;
			}
		}
		catch (Exception ex3)
		{
			if (back)
			{
				Debug.LogError("[SaveManager] idx のバックアップからの読み込みに失敗しました。写真インデックスを再生成します。" + lName_idx + " / " + ex3.ToString());
				reIndex = true;
			}
			else
			{
				Debug.LogWarning("[SaveManager] idx の読み込みに失敗したため、バックアップから復元します。" + lName_idx + " / " + ex3.ToString());
				backUpReload = true;
			}
		}
		if (reIndex)
		{
			if (File.Exists(lName_idx.Remove(lName_idx.Length - 5, 5)))
			{
				File.Delete(lName_idx.Remove(lName_idx.Length - 5, 5));
			}
			this.CheckPictureDirectory();
			yield return this.Load_Picture_Idx(lData);
		}
		if (backUpReload)
		{
			yield return this.Load_Picture_Idx_BackUp(lData);
		}
		yield return null;
		if (this.load_Binary_Version < 1.05f)
		{
			Debug.Log("[SaveManager] 管理ファイルのバージョン修正を行ないます： loadVersion = " + this.load_Binary_Version);
			for (int i = 0; i < this.album_Path.Count; i++)
			{
				int num = this.album_Path[i].IndexOf(Define.SaveName_Binary_PicDir_SimplePath);
				if (num != -1 && num != 0)
				{
					this.album_Path[i] = this.album_Path[i].Remove(0, num + Define.SaveName_Binary_PicDir_SimplePath.Length);
				}
			}
			for (int j = 0; j < this.tmp_Path.Count; j++)
			{
				int num2 = this.tmp_Path[j].IndexOf(Define.SaveName_Binary_PicDir_SimplePath);
				if (num2 != -1 && num2 != 0)
				{
					this.tmp_Path[j] = this.tmp_Path[j].Remove(0, num2 + Define.SaveName_Binary_PicDir_SimplePath.Length);
				}
			}
			this.Update_PictureIndex();
		}
		yield break;
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x00026668 File Offset: 0x00024A68
	public IEnumerator Load_Picture_Album(SaveDataFormat lData)
	{
		bool back = false;
		int i = 0;
		while (i < this.album_Path.Count)
		{
			string lName_album = Define.SaveName_Binary_PicDir + this.album_Path[i];
			if (back)
			{
				lName_album += ".back";
			}
			byte[] lByte_album = new byte[0];
			try
			{
				lByte_album = File.ReadAllBytes(lName_album);
				lData.ConvertBinary_Picture(false, lByte_album, SaveType.Album, i);
				if (back)
				{
					this.Save_Picture_Update(lData, SaveType.Album, i);
				}
			}
			catch (FileNotFoundException ex)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"[SaveManager] アルバム の読み込みに失敗しました (FileNotFoundException)：",
					i,
					" / ",
					lName_album,
					" / ",
					ex.ToString()
				}));
				this.Delete_Picture(lData, SaveType.Album, i);
				if (this.album_Path.Count < lData.albumPicture.Count)
				{
					lData.albumPicture.RemoveAt(i);
				}
				if (this.album_Path.Count < lData.albumPictureDate.Count)
				{
					lData.albumPictureDate.RemoveAt(i);
				}
				i--;
				back = false;
				goto IL_434;
			}
			catch (IsolatedStorageException ex2)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"[SaveManager] アルバム の読み込みに失敗しました (IsolatedStorageException)：",
					i,
					" / ",
					lName_album,
					" / ",
					ex2.ToString()
				}));
				this.Delete_Picture(lData, SaveType.Album, i);
				if (this.album_Path.Count < lData.albumPicture.Count)
				{
					lData.albumPicture.RemoveAt(i);
				}
				if (this.album_Path.Count < lData.albumPictureDate.Count)
				{
					lData.albumPictureDate.RemoveAt(i);
				}
				i--;
				back = false;
				goto IL_434;
			}
			catch (Exception ex3)
			{
				if (back)
				{
					Debug.LogError("[SaveManager] アルバム のバックアップからの読み込みに失敗しました。写真を削除します。：" + lName_album + " / " + ex3.ToString());
					this.Delete_Picture(lData, SaveType.Album, i);
					if (this.album_Path.Count < lData.albumPicture.Count)
					{
						lData.albumPicture.RemoveAt(i);
					}
					if (this.album_Path.Count < lData.albumPictureDate.Count)
					{
						lData.albumPictureDate.RemoveAt(i);
					}
					i--;
					back = false;
					goto IL_434;
				}
				Debug.LogWarning("[SaveManager] アルバム の読み込みに失敗したため、バックアップから復元します。" + lName_album + " / " + ex3.ToString());
				i--;
				back = true;
				goto IL_434;
			}
			goto IL_3DC;
			IL_434:
			i++;
			continue;
			IL_3DC:
			if (i % 2 == 1)
			{
				SuperGameMaster.LoadingProgress += 2f / (float)this.album_Path.Count * 5f;
				yield return null;
			}
			back = false;
			goto IL_434;
		}
		Debug.Log(string.Concat(new object[]
		{
			"[SaveManager] アルバムを読み込みました：",
			this.album_Path.Count,
			" / albumPicture.Count = ",
			lData.albumPicture.Count,
			" / albumPictureDate.Count = ",
			lData.albumPictureDate.Count
		}));
		yield return null;
		yield break;
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0002668C File Offset: 0x00024A8C
	public IEnumerator Load_Picture_Tmp(SaveDataFormat lData)
	{
		bool back = false;
		int i = 0;
		while (i < this.tmp_Path.Count)
		{
			string lName_tmp = Define.SaveName_Binary_PicDir + this.tmp_Path[i];
			if (back)
			{
				lName_tmp += ".back";
			}
			byte[] lByte_tmp = new byte[0];
			try
			{
				lByte_tmp = File.ReadAllBytes(lName_tmp);
				lData.ConvertBinary_Picture(false, lByte_tmp, SaveType.Temp, i);
				if (back)
				{
					this.Save_Picture_Update(lData, SaveType.Temp, i);
				}
			}
			catch (FileNotFoundException ex)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"[SaveManager] 一時保存写真 の読み込みに失敗しました (FileNotFoundException)：",
					i,
					" / ",
					lName_tmp,
					" / ",
					ex.ToString()
				}));
				this.Delete_Picture(lData, SaveType.Temp, i);
				if (this.tmp_Path.Count < lData.tmpPicture.Count)
				{
					lData.tmpPicture.RemoveAt(i);
				}
				if (this.tmp_Path.Count < lData.tmpPictureId.Count)
				{
					lData.tmpPictureId.RemoveAt(i);
				}
				i--;
				goto IL_41B;
			}
			catch (IsolatedStorageException ex2)
			{
				Debug.LogWarning(string.Concat(new object[]
				{
					"[SaveManager] 一時保存写真 の読み込みに失敗しました (IsolatedStorageException)：",
					i,
					" / ",
					lName_tmp,
					" / ",
					ex2.ToString()
				}));
				this.Delete_Picture(lData, SaveType.Temp, i);
				if (this.tmp_Path.Count < lData.tmpPicture.Count)
				{
					lData.tmpPicture.RemoveAt(i);
				}
				if (this.tmp_Path.Count < lData.tmpPictureId.Count)
				{
					lData.tmpPictureId.RemoveAt(i);
				}
				i--;
				goto IL_41B;
			}
			catch (Exception ex3)
			{
				if (!back)
				{
					Debug.LogWarning("[SaveManager] 一時保存写真 の読み込みに失敗したため、バックアップから復元します " + lName_tmp + " / " + ex3.ToString());
					i--;
					back = true;
					goto IL_41B;
				}
				Debug.LogError("[SaveManager] 一時保存写真 のバックアップからの読み込みに失敗しました。写真を削除します。：" + lName_tmp + " / " + ex3.ToString());
				this.Delete_Picture(lData, SaveType.Temp, i);
				if (this.tmp_Path.Count < lData.tmpPicture.Count)
				{
					lData.tmpPicture.RemoveAt(i);
				}
				if (this.tmp_Path.Count < lData.tmpPictureId.Count)
				{
					lData.tmpPictureId.RemoveAt(i);
				}
				i--;
			}
			goto IL_3C3;
			IL_41B:
			i++;
			continue;
			IL_3C3:
			if (i % 2 == 1)
			{
				SuperGameMaster.LoadingProgress += 2f / (float)this.tmp_Path.Count * 5f;
				yield return null;
			}
			back = false;
			goto IL_41B;
		}
		Debug.Log(string.Concat(new object[]
		{
			"[SaveManager] 一時保存写真 を読み込みました：",
			this.tmp_Path.Count,
			" / tmpPicture.Count = ",
			lData.tmpPicture.Count,
			" / tmpPictureId.Count = ",
			lData.tmpPictureId.Count
		}));
		yield return null;
		yield break;
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x000266B0 File Offset: 0x00024AB0
	public byte[] ConvertBinary_PictureIndex(bool write, byte[] sByte)
	{
		int num = 0;
		List<byte[]> list = new List<byte[]>();
		int num2;
		if (write)
		{
			num2 = this.album_Path.Count;
		}
		else
		{
			num2 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		int num3 = 4;
		if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(num2, list[list.Count - 1], 0, num3);
		}
		else
		{
			num += num3;
		}
		if (write)
		{
			list.Add(new byte[4]);
			ByteConverter.ConnectWrite(num2, list[list.Count - 1], 0, 4);
			for (int i = 0; i < num2; i++)
			{
				int num4 = this.album_Path[i].Length + 10;
				list.Add(new byte[4]);
				ByteConverter.ConnectWrite(num4, list[list.Count - 1], 0, 4);
				list.Add(new byte[num4]);
				ByteConverter.ConnectWriteString(this.album_Path[i], list[list.Count - 1], 0, num4);
			}
		}
		else
		{
			this.album_Path = new List<string>();
			num += 4;
			for (int i = 0; i < num2; i++)
			{
				int num5 = ByteConverter.ConnectRead(sByte, num, 4);
				num += 4;
				string item = ByteConverter.ConnectReadString(sByte, num, num5);
				this.album_Path.Add(item);
				num += num5;
			}
		}
		if (write)
		{
			num2 = this.tmp_Path.Count;
		}
		else
		{
			num2 = ByteConverter.ConnectRead(sByte, num, 4);
		}
		num3 = 4;
		if (write)
		{
			list.Add(new byte[num3]);
			ByteConverter.ConnectWrite(this.tmp_Path.Count, list[list.Count - 1], 0, num3);
		}
		else
		{
			num += num3;
		}
		if (write)
		{
			list.Add(new byte[4]);
			ByteConverter.ConnectWrite(num2, list[list.Count - 1], 0, 4);
			for (int i = 0; i < num2; i++)
			{
				int num6 = this.tmp_Path[i].Length + 10;
				list.Add(new byte[4]);
				ByteConverter.ConnectWrite(num6, list[list.Count - 1], 0, 4);
				list.Add(new byte[num6]);
				ByteConverter.ConnectWriteString(this.tmp_Path[i], list[list.Count - 1], 0, num6);
			}
		}
		else
		{
			this.tmp_Path = new List<string>();
			num += 4;
			for (int i = 0; i < num2; i++)
			{
				int num7 = ByteConverter.ConnectRead(sByte, num, 4);
				num += 4;
				string item2 = ByteConverter.ConnectReadString(sByte, num, num7);
				this.tmp_Path.Add(item2);
				num += num7;
			}
		}
		if (write)
		{
			int num8 = 0;
			foreach (byte[] array in list)
			{
				num8 += array.Length;
			}
			sByte = new byte[num8];
			foreach (byte[] array2 in list)
			{
				Buffer.BlockCopy(array2, 0, sByte, num, array2.Length);
				num += array2.Length;
			}
			return sByte;
		}
		return null;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x00026A58 File Offset: 0x00024E58
	public void Save_Serialize(SaveDataFormat sData)
	{
		this.Save_Serialize(sData, false);
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x00026A62 File Offset: 0x00024E62
	public void Save_Serialize_BackUp(SaveDataFormat sData)
	{
		this.Save_Serialize(sData, true);
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x00026A6C File Offset: 0x00024E6C
	public void Save_Serialize(SaveDataFormat sData, bool back)
	{
		string text = Define.SaveName_Serialize;
		if (back)
		{
			text += ".back";
		}
		using (FileStream fileStream = new FileStream(text, FileMode.Create, FileAccess.Write))
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(fileStream, sData);
		}
		Debug.Log("[SaveManager] Serialize セーブしました：" + text);
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x00026ADC File Offset: 0x00024EDC
	public SaveDataFormat Load_Serialize()
	{
		return this.Load_Serialize(false);
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x00026AE5 File Offset: 0x00024EE5
	public SaveDataFormat Load_Serialize_BackUp()
	{
		return this.Load_Serialize(true);
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x00026AF0 File Offset: 0x00024EF0
	public SaveDataFormat Load_Serialize(bool back)
	{
		string text = Define.SaveName_Serialize;
		if (back)
		{
			text += ".back";
		}
		SaveDataFormat saveDataFormat = new SaveDataFormat();
		try
		{
			Debug.Log("[SaveManager] Serialize ロード開始：" + text);
			using (FileStream fileStream = new FileStream(text, FileMode.Open, FileAccess.Read))
			{
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				saveDataFormat = (binaryFormatter.Deserialize(fileStream) as SaveDataFormat);
			}
			Debug.Log("[SaveManager] Serialize ロードしました：" + text);
			if (saveDataFormat != null)
			{
				this.Save_Serialize(new SaveDataFormat(saveDataFormat), true);
			}
		}
		catch (FileNotFoundException ex)
		{
			saveDataFormat.initialize();
			this.Save_Serialize(new SaveDataFormat(saveDataFormat));
			this.Save_Serialize_BackUp(new SaveDataFormat(saveDataFormat));
			Debug.Log("[SaveManager] Serialize データを新規作成しました (FileNotFoundException)：" + text + " / " + ex.ToString());
		}
		catch (IsolatedStorageException ex2)
		{
			saveDataFormat.initialize();
			this.Save_Serialize(new SaveDataFormat(saveDataFormat));
			this.Save_Serialize_BackUp(new SaveDataFormat(saveDataFormat));
			Debug.Log("[SaveManager] Serialize データを新規作成しました (IsolatedStorageException)：" + text + " / " + ex2.ToString());
		}
		catch (Exception ex3)
		{
			if (text.Remove(0, text.Length - 5) == ".back")
			{
				Debug.LogError("[SaveManager] Serialize バックアップからの読み込みに失敗しました：" + text + " / " + ex3.ToString());
				throw;
			}
			Debug.LogWarning("[SaveManager] Serialize データの読み込みに失敗しました。バックアップから復元します。：" + text + " / " + ex3.ToString());
			return this.Load_Serialize_BackUp();
		}
		saveDataFormat.DataDiffCheck();
		return saveDataFormat;
	}

	// Token: 0x04000561 RID: 1377
	private float load_Binary_Version;

	// Token: 0x04000562 RID: 1378
	public List<string> album_Path;

	// Token: 0x04000563 RID: 1379
	public List<string> tmp_Path;
}
