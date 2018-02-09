using System;
using System.Collections;
using System.Collections.Generic;
using Picture;
using TimerEvent;
using UnityEngine;

// Token: 0x020000E5 RID: 229
public class PictureCreator : MonoBehaviour
{
	// Token: 0x0600063F RID: 1599 RVA: 0x00024BE8 File Offset: 0x00022FE8
	public IEnumerator Proc()
	{
		this.evtId = -1;
		this.picDate = new DateTime(1970, 1, 1);
		if (SuperGameMaster.evtMgr.search_ActEvtIndex_forType(TimerEvent.Type.Picture) != -1)
		{
			bool cng = false;
			using (List<EventTimerFormat>.Enumerator enumerator = SuperGameMaster.evtMgr.get_ActEvtList_forType(TimerEvent.Type.Picture).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					EventTimerFormat evt = enumerator.Current;
					int setId = evt.evtId;
					int charaId = evt.evtValue[0];
					if (SuperGameMaster.saveData.tmpPictureId.FindIndex((int rec) => rec.Equals(evt.id)) != -1)
					{
						SuperGameMaster.LoadingProgress += 2f;
						if (SuperGameMaster.LoadingProgress > 100f)
						{
							SuperGameMaster.LoadingProgress = 100f;
						}
					}
					else
					{
						Texture2D tex = SuperGameMaster.picture.Create(setId, charaId);
						byte[] pngData = tex.EncodeToPNG();
						SuperGameMaster.saveData.tmpPictureId.Add(evt.id);
						SuperGameMaster.saveData.tmpPicture.Add(pngData);
						SuperGameMaster.saveMgr.Save_Picture(SuperGameMaster.saveData, SaveType.Temp, SuperGameMaster.saveData.tmpPicture.Count - 1);
						cng = true;
						SuperGameMaster.LoadingProgress += 2f;
						if (SuperGameMaster.LoadingProgress > 100f)
						{
							SuperGameMaster.LoadingProgress = 100f;
						}
						yield return null;
					}
				}
			}
			if (cng)
			{
				SuperGameMaster.SaveData();
			}
		}
		else
		{
			if (SuperGameMaster.saveData.tmpPicture.Count > 0)
			{
				SuperGameMaster.saveData.tmpPicture = new List<byte[]>();
				Debug.Log("[PictureCreator] tmpPicture に余分なデータが含まれてたため、削除しました");
			}
			if (SuperGameMaster.saveData.tmpPictureId.Count > 0)
			{
				SuperGameMaster.saveData.tmpPictureId = new List<int>();
				Debug.Log("[PictureCreator] tmpPictureId に余分なデータが含まれてたため、削除しました");
			}
		}
		yield break;
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x00024C04 File Offset: 0x00023004
	public void ChangePictureData(int _evtId, DateTime _dateTime)
	{
		this.evtId = _evtId;
		this.picDate = new DateTime(_dateTime.Year, _dateTime.Month, _dateTime.Day, _dateTime.Hour, _dateTime.Minute, _dateTime.Second, _dateTime.Millisecond);
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x00024C54 File Offset: 0x00023054
	public int GetChangePictureData()
	{
		return this.evtId;
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x00024C5C File Offset: 0x0002305C
	public DateTime GetChangePictureData_DateTime()
	{
		return new DateTime(this.picDate.Year, this.picDate.Month, this.picDate.Day, this.picDate.Hour, this.picDate.Minute, this.picDate.Second, this.picDate.Millisecond);
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x00024CBB File Offset: 0x000230BB
	public Texture2D Create(int setId, int charaId)
	{
		return this.PictureCreate(setId, charaId, false);
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x00024CC8 File Offset: 0x000230C8
	public Texture2D Create(int setId, int charaId, int RandomSeed)
	{
		return this.PictureCreate(setId, charaId, false);
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x00024CE0 File Offset: 0x000230E0
	public Texture2D PictureCreate(int setId, int charaId, bool rndFix)
	{
		Texture2D texture2D = new Texture2D(500, 350, TextureFormat.RGB24, false);
		List<List<Texture2D>> list = new List<List<Texture2D>>();
		List<List<Texture2D>> list2 = new List<List<Texture2D>>();
		Texture2D texture2D2 = null;
		Texture2D texture2D3 = null;
		PictureDataFormat pictureDataFormat = SuperGameMaster.sDataBase.get_PictureDB_forId(setId);
		if (rndFix)
		{
			pictureDataFormat.view = Vector2.zero;
		}
		List<PictureBackDataFormat> list3 = new List<PictureBackDataFormat>();
		foreach (string text in pictureDataFormat.backImage)
		{
			if (text == string.Empty)
			{
				list3.Add(new PictureBackDataFormat());
			}
			else
			{
				if (!pictureDataFormat.randomSet)
				{
					list3.Add(SuperGameMaster.sDataBase.get_PictureBackDB_forName(text));
				}
				else
				{
					int num = SuperGameMaster.sDataBase.search_PictureRandomDBIndex_forSet(text);
					if (num != -1)
					{
						PictureRandomDataFormat pictureRandomDataFormat = SuperGameMaster.sDataBase.get_PictureRandomDB(num);
						string name = pictureRandomDataFormat.setName[UnityEngine.Random.Range(0, pictureRandomDataFormat.setName.Length)];
						list3.Add(SuperGameMaster.sDataBase.get_PictureBackDB_forName(name));
					}
					else
					{
						list3.Add(SuperGameMaster.sDataBase.get_PictureBackDB_forName(text));
					}
				}
				if (rndFix)
				{
					list3[list3.Count - 1].rndPos = new int[4];
				}
			}
		}
		List<PictureBackDataFormat> list4 = new List<PictureBackDataFormat>();
		foreach (string text2 in pictureDataFormat.frontImage)
		{
			if (text2 == string.Empty)
			{
				list4.Add(new PictureBackDataFormat());
			}
			else
			{
				if (!pictureDataFormat.randomSet)
				{
					list4.Add(SuperGameMaster.sDataBase.get_PictureBackDB_forName(text2));
				}
				else
				{
					int num2 = SuperGameMaster.sDataBase.search_PictureRandomDBIndex_forSet(text2);
					if (num2 != -1)
					{
						PictureRandomDataFormat pictureRandomDataFormat2 = SuperGameMaster.sDataBase.get_PictureRandomDB(num2);
						string name2 = pictureRandomDataFormat2.setName[UnityEngine.Random.Range(0, pictureRandomDataFormat2.setName.Length)];
						list4.Add(SuperGameMaster.sDataBase.get_PictureBackDB_forName(name2));
					}
					else
					{
						list4.Add(SuperGameMaster.sDataBase.get_PictureBackDB_forName(text2));
					}
				}
				if (rndFix)
				{
					list4[list4.Count - 1].rndPos = new int[4];
				}
			}
		}
		PictureCharaDataFormat pictureCharaDataFormat = SuperGameMaster.sDataBase.get_PictureCharaDB_forId(-1);
		PictureCharaDataFormat pictureCharaDataFormat2 = null;
		if (charaId != -1)
		{
			pictureCharaDataFormat2 = SuperGameMaster.sDataBase.get_PictureCharaDB_forId(charaId);
		}
		foreach (PictureBackDataFormat pictureBackDataFormat in list3)
		{
			List<Texture2D> list5 = new List<Texture2D>();
			if (pictureBackDataFormat.path == null)
			{
				list5.Add(null);
			}
			else
			{
				foreach (string path2 in pictureBackDataFormat.path)
				{
					list5.Add(Resources.Load(path2) as Texture2D);
				}
			}
			list.Add(list5);
		}
		foreach (PictureBackDataFormat pictureBackDataFormat2 in list4)
		{
			List<Texture2D> list6 = new List<Texture2D>();
			if (pictureBackDataFormat2.path == null)
			{
				list6.Add(null);
			}
			else
			{
				foreach (string path4 in pictureBackDataFormat2.path)
				{
					list6.Add(Resources.Load(path4) as Texture2D);
				}
			}
			list2.Add(list6);
		}
		bool flag = false;
		if (charaId == -1)
		{
			flag = true;
		}
		else if (pictureDataFormat.travelerPose[charaId] == string.Empty)
		{
			flag = true;
		}
		string text3 = string.Empty;
		if (flag)
		{
			text3 = pictureDataFormat.frogPose_s;
		}
		else
		{
			text3 = pictureDataFormat.frogPose;
		}
		int num3;
		if (!pictureDataFormat.randomSet)
		{
			num3 = SuperGameMaster.sDataBase.search_PictureCharaDB_posePathIndex_forPoseName(text3);
		}
		else
		{
			int num4 = SuperGameMaster.sDataBase.search_PictureRandomDBIndex_forSet(text3);
			if (num4 != -1)
			{
				PictureRandomDataFormat pictureRandomDataFormat3 = SuperGameMaster.sDataBase.get_PictureRandomDB(num4);
				string poseName = pictureRandomDataFormat3.setName[UnityEngine.Random.Range(0, pictureRandomDataFormat3.setName.Length)];
				num3 = SuperGameMaster.sDataBase.search_PictureCharaDB_posePathIndex_forPoseName(poseName);
			}
			else
			{
				num3 = -1;
			}
		}
		if (num3 != -1)
		{
			texture2D2 = (Resources.Load(pictureCharaDataFormat.posePath[num3]) as Texture2D);
		}
		num3 = -1;
		if (charaId != -1)
		{
			if (!pictureDataFormat.randomSet)
			{
				num3 = SuperGameMaster.sDataBase.search_PictureCharaDB_posePathIndex_forPoseName(pictureDataFormat.travelerPose[charaId]);
			}
			else
			{
				int num5 = SuperGameMaster.sDataBase.search_PictureRandomDBIndex_forSet(pictureDataFormat.travelerPose[charaId]);
				if (num5 != -1)
				{
					PictureRandomDataFormat pictureRandomDataFormat4 = SuperGameMaster.sDataBase.get_PictureRandomDB(num5);
					string poseName2 = pictureRandomDataFormat4.setName[UnityEngine.Random.Range(0, pictureRandomDataFormat4.setName.Length)];
					num3 = SuperGameMaster.sDataBase.search_PictureCharaDB_posePathIndex_forPoseName(poseName2);
				}
				else
				{
					num3 = -1;
				}
			}
		}
		if (num3 != -1 && pictureCharaDataFormat2 != null)
		{
			texture2D3 = (Resources.Load(pictureCharaDataFormat2.posePath[num3]) as Texture2D);
		}
		this.photo_frame = (Resources.Load("Picture/photo_frame") as Texture2D);
		if (pictureDataFormat.view != Vector2.zero)
		{
			int num6 = UnityEngine.Random.Range(0, (int)pictureDataFormat.view.x + 1);
			if (UnityEngine.Random.Range(0, 2) == 1)
			{
				num6 *= -1;
			}
			int num7 = UnityEngine.Random.Range(0, (int)pictureDataFormat.view.y + 1);
			if (UnityEngine.Random.Range(0, 2) == 1)
			{
				num7 *= -1;
			}
			pictureDataFormat.view = new Vector2((float)num6, (float)num7);
		}
		for (int m = 0; m < list.Count; m++)
		{
			if (list[m] != null)
			{
				this.SetTexture(texture2D, list[m], this.GetPivotPos(texture2D, list[m], list3[m].fixPos + pictureDataFormat.view, PictureCreator.Pivot.Center, list3[m].rndPos));
			}
		}
		if (charaId != -1)
		{
			if (pictureDataFormat.travelerPose[charaId] != string.Empty)
			{
				if (!pictureDataFormat.priority)
				{
					this.SetTexture(texture2D, texture2D2, this.GetPivotPos(texture2D, texture2D2, pictureDataFormat.frogPos + pictureDataFormat.view, PictureCreator.Pivot.Center));
					this.SetTexture(texture2D, texture2D3, this.GetPivotPos(texture2D, texture2D3, pictureDataFormat.travelerPos[charaId] + pictureDataFormat.view, PictureCreator.Pivot.Center));
				}
				else
				{
					this.SetTexture(texture2D, texture2D3, this.GetPivotPos(texture2D, texture2D3, pictureDataFormat.travelerPos[charaId] + pictureDataFormat.view, PictureCreator.Pivot.Center));
					this.SetTexture(texture2D, texture2D2, this.GetPivotPos(texture2D, texture2D2, pictureDataFormat.frogPos + pictureDataFormat.view, PictureCreator.Pivot.Center));
				}
			}
			else if (pictureDataFormat.frogPose_s != string.Empty)
			{
				this.SetTexture(texture2D, texture2D2, this.GetPivotPos(texture2D, texture2D2, pictureDataFormat.frogPos_s + pictureDataFormat.view, PictureCreator.Pivot.Center));
			}
			else
			{
				this.SetTexture(texture2D, texture2D2, this.GetPivotPos(texture2D, texture2D2, pictureDataFormat.frogPos + pictureDataFormat.view, PictureCreator.Pivot.Center));
			}
		}
		else if (pictureDataFormat.frogPose_s != string.Empty)
		{
			this.SetTexture(texture2D, texture2D2, this.GetPivotPos(texture2D, texture2D2, pictureDataFormat.frogPos_s + pictureDataFormat.view, PictureCreator.Pivot.Center));
		}
		else
		{
			this.SetTexture(texture2D, texture2D2, this.GetPivotPos(texture2D, texture2D2, pictureDataFormat.frogPos + pictureDataFormat.view, PictureCreator.Pivot.Center));
		}
		for (int n = 0; n < list2.Count; n++)
		{
			if (list2[n] != null)
			{
				this.SetTexture(texture2D, list2[n], this.GetPivotPos(texture2D, list2[n], list4[n].fixPos + pictureDataFormat.view, PictureCreator.Pivot.Center, list4[n].rndPos));
			}
		}
		this.SetTexture(texture2D, this.photo_frame, new Vector2(texture2D.texelSize.x / 2f, texture2D.texelSize.y / 2f));
		Debug.Log(string.Concat(new object[]
		{
			"[PictureCreator] 写真データ取得：setID = ",
			setId,
			" / charaId = ",
			charaId
		}));
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06000646 RID: 1606 RVA: 0x000255CC File Offset: 0x000239CC
	public void SetTexture(Texture2D baseTex, List<Texture2D> addTexs, List<Vector2> fixPos)
	{
		for (int i = 0; i < addTexs.Count; i++)
		{
			this.SetTexture(baseTex, addTexs[i], fixPos[i]);
		}
	}

	// Token: 0x06000647 RID: 1607 RVA: 0x00025608 File Offset: 0x00023A08
	public void SetTexture(Texture2D baseTex, Texture2D addTex, Vector2 fixPos)
	{
		if (addTex == null)
		{
			Debug.Log("[PictureCreator] 空のテクスチャを編集ししようとしました！");
			return;
		}
		Vector2 vector = default(Vector2);
		Color[] pixels = addTex.GetPixels();
		int num = -1;
		for (int i = 0; i < addTex.height; i++)
		{
			for (int j = 0; j < addTex.width; j++)
			{
				num++;
				Color color = pixels[num];
				vector = new Vector2((float)j + fixPos.x, (float)i + fixPos.y);
				if (vector.x >= 0f)
				{
					if (vector.y >= 0f)
					{
						if (vector.x < (float)baseTex.width)
						{
							if (vector.y < (float)baseTex.height)
							{
								if (color.a != 0f)
								{
									if (color.a != 1f)
									{
										Color pixel = baseTex.GetPixel((int)vector.x, (int)vector.y);
										color = color.a * color + (1f - color.a) * pixel;
									}
									baseTex.SetPixel((int)vector.x, (int)vector.y, color);
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06000648 RID: 1608 RVA: 0x0002577C File Offset: 0x00023B7C
	public List<Vector2> GetPivotPos(Texture2D baseTex, List<Texture2D> foucusTex, Vector2 fixPos, PictureCreator.Pivot pivot, int[] rndPos)
	{
		List<Vector2> list = new List<Vector2>();
		if (foucusTex[0] == null)
		{
			list.Add(Vector2.zero);
			return list;
		}
		if (rndPos.Length == 4)
		{
			int num = UnityEngine.Random.Range(-rndPos[2], rndPos[3]);
			int num2 = UnityEngine.Random.Range(-rndPos[1], rndPos[0]);
			fixPos.y += (float)num2;
			fixPos.x += (float)num;
		}
		foreach (Texture2D foucusTex2 in foucusTex)
		{
			list.Add(this.GetPivotPos(baseTex, foucusTex2, fixPos, pivot));
		}
		return list;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0002584C File Offset: 0x00023C4C
	public Vector2 GetPivotPos(Texture2D baseTex, Texture2D foucusTex, Vector2 fixPos, PictureCreator.Pivot pivot, int[] rndPos)
	{
		if (foucusTex == null)
		{
			return Vector2.zero;
		}
		if (rndPos.Length == 4)
		{
			int num = UnityEngine.Random.Range(-rndPos[2], rndPos[3]);
			int num2 = UnityEngine.Random.Range(-rndPos[1], rndPos[0]);
			fixPos.y += (float)num2;
			fixPos.x += (float)num;
		}
		return this.GetPivotPos(baseTex, foucusTex, fixPos, pivot);
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x000258C0 File Offset: 0x00023CC0
	public Vector2 GetPivotPos(Texture2D baseTex, Texture2D foucusTex, Vector2 fixPos, PictureCreator.Pivot pivot)
	{
		if (foucusTex == null)
		{
			return Vector2.zero;
		}
		Vector2 zero = Vector2.zero;
		if (pivot == PictureCreator.Pivot.Up || pivot == PictureCreator.Pivot.Center || pivot == PictureCreator.Pivot.Down)
		{
			zero.x = (float)(baseTex.width / 2 - foucusTex.width / 2);
		}
		if (pivot == PictureCreator.Pivot.Right)
		{
			zero.x = (float)(baseTex.width - foucusTex.width);
		}
		if (pivot == PictureCreator.Pivot.Left || pivot == PictureCreator.Pivot.Center || pivot == PictureCreator.Pivot.Right)
		{
			zero.y = (float)(baseTex.height / 2 - foucusTex.height / 2);
		}
		if (pivot == PictureCreator.Pivot.Up)
		{
			zero.y = (float)(baseTex.height - foucusTex.height);
		}
		return zero + fixPos;
	}

	// Token: 0x04000556 RID: 1366
	[SerializeField]
	private Texture2D photo_frame;

	// Token: 0x04000557 RID: 1367
	[SerializeField]
	public int evtId = -1;

	// Token: 0x04000558 RID: 1368
	[SerializeField]
	public DateTime picDate;

	// Token: 0x020000E6 RID: 230
	public enum Pivot
	{
		// Token: 0x0400055A RID: 1370
		NONE = -1,
		// Token: 0x0400055B RID: 1371
		Default,
		// Token: 0x0400055C RID: 1372
		Center,
		// Token: 0x0400055D RID: 1373
		Up,
		// Token: 0x0400055E RID: 1374
		Down,
		// Token: 0x0400055F RID: 1375
		Left,
		// Token: 0x04000560 RID: 1376
		Right
	}
}
