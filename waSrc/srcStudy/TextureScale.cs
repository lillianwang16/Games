using System;
using System.Threading;
using UnityEngine;

// Token: 0x020000E1 RID: 225
public class TextureScale
{
	// Token: 0x0600060E RID: 1550 RVA: 0x0002473A File Offset: 0x00022B3A
	public static void Point(Texture2D tex, int newWidth, int newHeight)
	{
		TextureScale.ThreadedScale(tex, newWidth, newHeight, false);
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x00024745 File Offset: 0x00022B45
	public static void Bilinear(Texture2D tex, int newWidth, int newHeight)
	{
		TextureScale.ThreadedScale(tex, newWidth, newHeight, true);
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x00024750 File Offset: 0x00022B50
	private static void ThreadedScale(Texture2D tex, int newWidth, int newHeight, bool useBilinear)
	{
		TextureScale.texColors = tex.GetPixels();
		TextureScale.newColors = new Color[newWidth * newHeight];
		if (useBilinear)
		{
			TextureScale.ratioX = 1f / ((float)newWidth / (float)(tex.width - 1));
			TextureScale.ratioY = 1f / ((float)newHeight / (float)(tex.height - 1));
		}
		else
		{
			TextureScale.ratioX = (float)tex.width / (float)newWidth;
			TextureScale.ratioY = (float)tex.height / (float)newHeight;
		}
		TextureScale.w = tex.width;
		TextureScale.w2 = newWidth;
		int num = Mathf.Min(SystemInfo.processorCount, newHeight);
		int num2 = newHeight / num;
		TextureScale.finishCount = 0;
		if (TextureScale.mutex == null)
		{
			TextureScale.mutex = new Mutex(false);
		}
		if (num > 1)
		{
			int i;
			TextureScale.ThreadData threadData;
			for (i = 0; i < num - 1; i++)
			{
				threadData = new TextureScale.ThreadData(num2 * i, num2 * (i + 1));
				ParameterizedThreadStart start = (!useBilinear) ? new ParameterizedThreadStart(TextureScale.PointScale) : new ParameterizedThreadStart(TextureScale.BilinearScale);
				Thread thread = new Thread(start);
				thread.Start(threadData);
			}
			threadData = new TextureScale.ThreadData(num2 * i, newHeight);
			if (useBilinear)
			{
				TextureScale.BilinearScale(threadData);
			}
			else
			{
				TextureScale.PointScale(threadData);
			}
			while (TextureScale.finishCount < num)
			{
				Thread.Sleep(1);
			}
		}
		else
		{
			TextureScale.ThreadData obj = new TextureScale.ThreadData(0, newHeight);
			if (useBilinear)
			{
				TextureScale.BilinearScale(obj);
			}
			else
			{
				TextureScale.PointScale(obj);
			}
		}
		tex.Resize(newWidth, newHeight);
		tex.SetPixels(TextureScale.newColors);
		tex.Apply();
		TextureScale.texColors = null;
		TextureScale.newColors = null;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x000248F0 File Offset: 0x00022CF0
	public static void BilinearScale(object obj)
	{
		TextureScale.ThreadData threadData = (TextureScale.ThreadData)obj;
		for (int i = threadData.start; i < threadData.end; i++)
		{
			int num = (int)Mathf.Floor((float)i * TextureScale.ratioY);
			int num2 = num * TextureScale.w;
			int num3 = (num + 1) * TextureScale.w;
			int num4 = i * TextureScale.w2;
			for (int j = 0; j < TextureScale.w2; j++)
			{
				int num5 = (int)Mathf.Floor((float)j * TextureScale.ratioX);
				float value = (float)j * TextureScale.ratioX - (float)num5;
				TextureScale.newColors[num4 + j] = TextureScale.ColorLerpUnclamped(TextureScale.ColorLerpUnclamped(TextureScale.texColors[num2 + num5], TextureScale.texColors[num2 + num5 + 1], value), TextureScale.ColorLerpUnclamped(TextureScale.texColors[num3 + num5], TextureScale.texColors[num3 + num5 + 1], value), (float)i * TextureScale.ratioY - (float)num);
			}
		}
		TextureScale.mutex.WaitOne();
		TextureScale.finishCount++;
		TextureScale.mutex.ReleaseMutex();
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x00024A2C File Offset: 0x00022E2C
	public static void PointScale(object obj)
	{
		TextureScale.ThreadData threadData = (TextureScale.ThreadData)obj;
		for (int i = threadData.start; i < threadData.end; i++)
		{
			int num = (int)(TextureScale.ratioY * (float)i) * TextureScale.w;
			int num2 = i * TextureScale.w2;
			for (int j = 0; j < TextureScale.w2; j++)
			{
				TextureScale.newColors[num2 + j] = TextureScale.texColors[(int)((float)num + TextureScale.ratioX * (float)j)];
			}
		}
		TextureScale.mutex.WaitOne();
		TextureScale.finishCount++;
		TextureScale.mutex.ReleaseMutex();
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x00024AE0 File Offset: 0x00022EE0
	private static Color ColorLerpUnclamped(Color c1, Color c2, float value)
	{
		return new Color(c1.r + (c2.r - c1.r) * value, c1.g + (c2.g - c1.g) * value, c1.b + (c2.b - c1.b) * value, c1.a + (c2.a - c1.a) * value);
	}

	// Token: 0x04000540 RID: 1344
	private static Color[] texColors;

	// Token: 0x04000541 RID: 1345
	private static Color[] newColors;

	// Token: 0x04000542 RID: 1346
	private static int w;

	// Token: 0x04000543 RID: 1347
	private static float ratioX;

	// Token: 0x04000544 RID: 1348
	private static float ratioY;

	// Token: 0x04000545 RID: 1349
	private static int w2;

	// Token: 0x04000546 RID: 1350
	private static int finishCount;

	// Token: 0x04000547 RID: 1351
	private static Mutex mutex;

	// Token: 0x020000E2 RID: 226
	public class ThreadData
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x00024B56 File Offset: 0x00022F56
		public ThreadData(int s, int e)
		{
			this.start = s;
			this.end = e;
		}

		// Token: 0x04000548 RID: 1352
		public int start;

		// Token: 0x04000549 RID: 1353
		public int end;
	}
}
