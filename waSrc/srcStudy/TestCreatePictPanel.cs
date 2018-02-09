using System;
using Picture;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000068 RID: 104
public class TestCreatePictPanel : MonoBehaviour
{
	// Token: 0x060003A3 RID: 931 RVA: 0x00015D44 File Offset: 0x00014144
	public void OpenPanel()
	{
		base.gameObject.SetActive(true);
		base.transform.parent.GetComponentInParent<UIMaster>().freezeObject(true);
		base.transform.parent.GetComponentInParent<UIMaster>().blockUI(true, new Color(0f, 0f, 0f, 0.3f));
		this.setType = Picture.Type.Normal;
		this.charaId = -1;
		this.setId = SuperGameMaster.sDataBase.get_PictureDB(0).id;
		this.rndfix = true;
		this.RandomButtonText.GetComponent<Text>().text = "Fix";
		this.ResetTitleText();
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00015DE8 File Offset: 0x000141E8
	public void ClosePanel()
	{
		base.gameObject.SetActive(false);
		base.transform.parent.GetComponentInParent<UIMaster>().freezeObject(false);
		base.transform.parent.GetComponentInParent<UIMaster>().blockUI(false, new Color(0f, 0f, 0f, 0f));
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00015E48 File Offset: 0x00014248
	public void PushCreate()
	{
		this.createImg = base.GetComponent<PictureCreator>().PictureCreate(this.setId, this.charaId, this.rndfix);
		this.createImgSprite = Sprite.Create(this.createImg, new Rect(0f, 0f, 500f, 350f), Vector2.zero);
		this.TextureImages.GetComponent<Image>().sprite = this.createImgSprite;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00015EC0 File Offset: 0x000142C0
	public void ResetTitleText()
	{
		string name = SuperGameMaster.sDataBase.get_PictureCharaDB_forId(this.charaId).name;
		this.TitleText.GetComponent<Text>().text = string.Concat(new object[]
		{
			this.setType.ToString(),
			"：",
			this.setId,
			"\n(chara：[",
			this.charaId,
			"] ",
			name,
			" )"
		});
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00015F52 File Offset: 0x00014352
	public void Push_Chara()
	{
		this.charaId++;
		if (this.charaId >= SuperGameMaster.sDataBase.count_PictureCharaDB() - 1)
		{
			this.charaId = -1;
		}
		this.ResetTitleText();
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00015F88 File Offset: 0x00014388
	public void Push_Rnd()
	{
		this.rndfix = !this.rndfix;
		if (this.rndfix)
		{
			this.RandomButtonText.GetComponent<Text>().text = "Fix";
		}
		else
		{
			this.RandomButtonText.GetComponent<Text>().text = "Rnd";
		}
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00015FE0 File Offset: 0x000143E0
	public void Push_Type()
	{
		this.setType++;
		if (this.setType == Picture.Type._TypeMax)
		{
			this.setType = Picture.Type.Normal;
		}
		int index = SuperGameMaster.sDataBase.search_PictureDBIndex_forSetType(this.setType);
		this.setId = SuperGameMaster.sDataBase.get_PictureDB(index).id;
		this.ResetTitleText();
	}

	// Token: 0x060003AA RID: 938 RVA: 0x0001603C File Offset: 0x0001443C
	public void Push_Next()
	{
		int num = SuperGameMaster.sDataBase.search_PictureDBIndex_forId(this.setId);
		if (num == SuperGameMaster.sDataBase.count_PictureDB() - 1)
		{
			num = -1;
		}
		PictureDataFormat pictureDataFormat = SuperGameMaster.sDataBase.get_PictureDB(num + 1);
		if (pictureDataFormat == null)
		{
			num = SuperGameMaster.sDataBase.search_PictureDBIndex_forSetType(this.setType);
			this.setId = SuperGameMaster.sDataBase.get_PictureDB(num).id;
		}
		else if (this.setType != pictureDataFormat.type)
		{
			num = SuperGameMaster.sDataBase.search_PictureDBIndex_forSetType(this.setType);
			this.setId = SuperGameMaster.sDataBase.get_PictureDB(num).id;
		}
		else
		{
			this.setId = pictureDataFormat.id;
		}
		this.ResetTitleText();
	}

	// Token: 0x060003AB RID: 939 RVA: 0x000160FD File Offset: 0x000144FD
	public void PushSave()
	{
		if (SuperGameMaster.GetPictureListCount(true) < 60)
		{
			SuperGameMaster.SavePictureList(true, this.createImg, SuperGameMaster.GetLastDateTime());
			SuperGameMaster.SaveData();
		}
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00016123 File Offset: 0x00014523
	public void PushPosTest()
	{
		this.PosTestPanel.SetActive(!this.PosTestPanel.activeSelf);
	}

	// Token: 0x0400023F RID: 575
	public GameObject PosTestPanel;

	// Token: 0x04000240 RID: 576
	public GameObject TextureImages;

	// Token: 0x04000241 RID: 577
	public Texture2D createImg;

	// Token: 0x04000242 RID: 578
	public Sprite createImgSprite;

	// Token: 0x04000243 RID: 579
	public GameObject TitleText;

	// Token: 0x04000244 RID: 580
	public GameObject RandomButtonText;

	// Token: 0x04000245 RID: 581
	private Picture.Type setType;

	// Token: 0x04000246 RID: 582
	private int setId;

	// Token: 0x04000247 RID: 583
	private int charaId;

	// Token: 0x04000248 RID: 584
	private bool rndfix = true;
}
