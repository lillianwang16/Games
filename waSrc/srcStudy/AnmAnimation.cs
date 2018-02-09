using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
public class AnmAnimation : MonoBehaviour
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000002 RID: 2 RVA: 0x00002063 File Offset: 0x00000463
	// (set) Token: 0x06000003 RID: 3 RVA: 0x0000206B File Offset: 0x0000046B
	public bool isPlaying { get; set; }

	// Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000474
	private void Awake()
	{
		this.Deactivate();
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000207C File Offset: 0x0000047C
	private void Start()
	{
	}

	// Token: 0x06000006 RID: 6 RVA: 0x0000207E File Offset: 0x0000047E
	private void Update()
	{
		this.AdvanceActionTime();
		this.UpdateAnmData();
	}

	// Token: 0x06000007 RID: 7 RVA: 0x0000208C File Offset: 0x0000048C
	private void UpdateAnmData()
	{
		if (this.loadedAnmName != this.anmName && this.LoadAnm(this.anmName))
		{
			this.loadedAnmName = this.anmName;
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000020C4 File Offset: 0x000004C4
	public void AdvanceActionTime()
	{
		if (this.currentAction < 0)
		{
			return;
		}
		if (this.currentActionTime < 0)
		{
			return;
		}
		if (this.isPlaying)
		{
			while (this.fpsTime > this.fpsClock)
			{
				this.currentActionTime++;
				this.fpsTime -= this.fpsClock;
			}
			this.fpsTime += Time.deltaTime;
		}
		this.SetActionTime(this.currentActionTime);
	}

	// Token: 0x06000009 RID: 9 RVA: 0x0000214C File Offset: 0x0000054C
	public void SetAction(int actionId)
	{
		this.SetAction(actionId, 0);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002156 File Offset: 0x00000556
	public void SetAction(int actionId, int frameTime)
	{
		this.currentAction = actionId;
		this.SetActionTime(frameTime);
		this.isPlaying = true;
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002170 File Offset: 0x00000570
	public int SetActionTime(int frameTime)
	{
		if (this.currentAction < 0)
		{
			return -1;
		}
		this.currentActionTime = frameTime;
		AnmAction anmAction = this.anmData.actions[this.currentAction];
		this.currentActionTime %= anmAction.sequenceSize;
		int sequence = anmAction.sequenceIndex + this.currentActionTime;
		this.SetSequence(sequence);
		return this.currentActionTime;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x000021E0 File Offset: 0x000005E0
	public void SetSequence(int seqId)
	{
		this.currentSequence = seqId;
		AnmSequence anmSequence = this.anmData.sequences[this.currentSequence];
		this.SetFrame(anmSequence.frame);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002220 File Offset: 0x00000620
	public void SetFrame(int frameId)
	{
		if (frameId < 0)
		{
			return;
		}
		this.DeactivateSprites();
		AnmFrame anmFrame = this.anmData.frames[frameId];
		for (int i = 0; i < anmFrame.spriteSize; i++)
		{
			int sprite = anmFrame.spriteIndex + i;
			this.SetSprite(sprite);
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x0000227B File Offset: 0x0000067B
	public void SetSprite(int spriteId)
	{
		this.sprites[spriteId].SetActive(true);
	}

	// Token: 0x0600000F RID: 15 RVA: 0x0000228C File Offset: 0x0000068C
	private void DeactivateSprites()
	{
		if (this.sprites == null)
		{
			return;
		}
		foreach (GameObject gameObject in this.sprites)
		{
			gameObject.SetActive(false);
		}
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000022CB File Offset: 0x000006CB
	private void Deactivate()
	{
		this.currentAction = -1;
		this.currentActionTime = -1;
		this.currentSequence = -1;
		this.currentFrame = -1;
		this.currentSprite = -1;
		this.isPlaying = false;
		this.DeactivateSprites();
	}

	// Token: 0x06000011 RID: 17 RVA: 0x00002300 File Offset: 0x00000700
	private bool LoadAnm(string anmPath)
	{
		byte[] array = Util.ReadBytesFromResource(anmPath, ".anm");
		if (array == null)
		{
			return false;
		}
		this.anmData = new AnmData();
		this.anmData.Load(array);
		this.CreateGameObjects(this.anmData);
		return true;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00002348 File Offset: 0x00000748
	private void CreateGameObjects(AnmData anmData)
	{
		Texture2D texture2D = Resources.Load<Texture2D>(this.pngName);
		Texture2D texture2D2 = new Texture2D(texture2D.width, texture2D.height, TextureFormat.RGB24, false);
		texture2D2 = texture2D;
		int width = texture2D2.width;
		int height = texture2D2.height;
		int num = anmData.modules.Length;
		this.modules = new Sprite[num];
		for (int i = 0; i < num; i++)
		{
			AnmModule anmModule = anmData.modules[i];
			int x = anmModule.x;
			int num2 = height - anmModule.y - anmModule.h;
			int w = anmModule.w;
			int h = anmModule.h;
			Sprite sprite = Sprite.Create(texture2D2, new Rect((float)x, (float)num2, (float)w, (float)h), new Vector2(0f, 1f), 100f);
			this.modules[i] = sprite;
		}
		num = anmData.sprites.Length;
		this.sprites = new GameObject[num];
		for (int j = 0; j < num; j++)
		{
			AnmSprite anmSprite = anmData.sprites[j];
			GameObject gameObject = new GameObject(string.Format("Sprite{0}", j));
			gameObject.AddComponent<SpriteRenderer>().sprite = this.modules[anmSprite.module];
			gameObject.transform.parent = base.gameObject.transform;
			float x2 = (float)anmSprite.x / 100f;
			float y = (float)(-(float)anmSprite.y) / 100f;
			float z = (float)(-(float)j) / 100f;
			gameObject.transform.position = new Vector3(x2, y, z);
			this.sprites[j] = gameObject;
		}
		this.Deactivate();
	}

	// Token: 0x06000013 RID: 19 RVA: 0x00002510 File Offset: 0x00000910
	public static GameObject CreateGameObject(string name, string anmPath, string pngPath)
	{
		GameObject gameObject = new GameObject(name);
		gameObject.AddComponent<AnmAnimation>();
		AnmAnimation component = gameObject.GetComponent<AnmAnimation>();
		component.pngName = pngPath;
		component.anmName = anmPath;
		component.UpdateAnmData();
		return gameObject;
	}

	// Token: 0x04000001 RID: 1
	public string pngName;

	// Token: 0x04000002 RID: 2
	public string anmName;

	// Token: 0x04000004 RID: 4
	private string loadedPngName;

	// Token: 0x04000005 RID: 5
	private string loadedAnmName;

	// Token: 0x04000006 RID: 6
	public int currentAction;

	// Token: 0x04000007 RID: 7
	public int currentActionTime;

	// Token: 0x04000008 RID: 8
	public int currentSequence;

	// Token: 0x04000009 RID: 9
	public int currentFrame;

	// Token: 0x0400000A RID: 10
	public int currentSprite;

	// Token: 0x0400000B RID: 11
	private AnmData anmData;

	// Token: 0x0400000C RID: 12
	private Sprite[] modules;

	// Token: 0x0400000D RID: 13
	private GameObject[] sprites;

	// Token: 0x0400000E RID: 14
	public readonly float fpsClock = 0.06666667f;

	// Token: 0x0400000F RID: 15
	public float fpsTime;
}
