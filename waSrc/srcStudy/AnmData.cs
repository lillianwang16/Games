using System;
using System.IO;

// Token: 0x02000003 RID: 3
public class AnmData
{
	// Token: 0x06000015 RID: 21 RVA: 0x00002550 File Offset: 0x00000950
	public void Load(byte[] bytes)
	{
		Stream input = new MemoryStream(bytes);
		using (BinaryReader binaryReader = new BigEndianBinaryReader(input))
		{
			int num = (int)binaryReader.ReadByte();
			this.modules = new AnmModule[num];
			for (int i = 0; i < num; i++)
			{
				AnmModule anmModule = default(AnmModule);
				anmModule.x = (int)binaryReader.ReadByte();
				anmModule.y = (int)binaryReader.ReadByte();
				anmModule.w = (int)binaryReader.ReadByte();
				anmModule.h = (int)binaryReader.ReadByte();
				this.modules[i] = anmModule;
			}
			int num2 = (int)binaryReader.ReadByte();
			for (int j = 0; j < num2; j++)
			{
				int num3 = (int)binaryReader.ReadByte();
				int num4 = (int)binaryReader.ReadByte();
				int num5 = (int)binaryReader.ReadByte();
				if (num4 == 0)
				{
					AnmModule[] array = this.modules;
					int num6 = num3;
					array[num6].x = (array[num6].x | num5 << 8);
				}
				else if (num4 == 1)
				{
					AnmModule[] array2 = this.modules;
					int num7 = num3;
					array2[num7].y = (array2[num7].y | num5 << 8);
				}
				else if (num4 == 2)
				{
					AnmModule[] array3 = this.modules;
					int num8 = num3;
					array3[num8].w = (array3[num8].w | num5 << 8);
				}
				else if (num4 == 3)
				{
					AnmModule[] array4 = this.modules;
					int num9 = num3;
					array4[num9].h = (array4[num9].h | num5 << 8);
				}
			}
			int num10 = (int)binaryReader.ReadByte();
			this.frames = new AnmFrame[num10];
			for (int k = 0; k < num10; k++)
			{
				AnmFrame anmFrame = default(AnmFrame);
				anmFrame.spriteIndex = (int)binaryReader.ReadUInt16();
				anmFrame.spriteSize = (int)binaryReader.ReadByte();
				anmFrame.gx = (int)binaryReader.ReadInt16();
				anmFrame.gy = (int)binaryReader.ReadInt16();
				anmFrame.gr = (int)binaryReader.ReadInt16();
				anmFrame.gb = (int)binaryReader.ReadInt16();
				anmFrame.rx = (int)binaryReader.ReadInt16();
				anmFrame.ry = (int)binaryReader.ReadInt16();
				anmFrame.rr = (int)binaryReader.ReadInt16();
				anmFrame.rb = (int)binaryReader.ReadInt16();
				this.frames[k] = anmFrame;
			}
			int num11 = (int)binaryReader.ReadInt16();
			this.sprites = new AnmSprite[num11];
			for (int l = 0; l < num11; l++)
			{
				AnmSprite anmSprite = default(AnmSprite);
				anmSprite.module = (int)binaryReader.ReadByte();
				anmSprite.x = (int)binaryReader.ReadInt16();
				anmSprite.y = (int)binaryReader.ReadInt16();
				anmSprite.flip = (int)binaryReader.ReadByte();
				this.sprites[l] = anmSprite;
			}
			int num12 = (int)binaryReader.ReadByte();
			this.actions = new AnmAction[num12];
			for (int m = 0; m < num12; m++)
			{
				AnmAction anmAction = default(AnmAction);
				anmAction.sequenceIndex = (int)binaryReader.ReadInt16();
				anmAction.sequenceSize = (int)binaryReader.ReadChar();
				anmAction.ex1 = (int)binaryReader.ReadChar();
				anmAction.ex2 = (int)binaryReader.ReadChar();
				this.actions[m] = anmAction;
			}
			int num13 = (int)binaryReader.ReadUInt16();
			this.sequences = new AnmSequence[num13];
			int n = 0;
			while (n < num13)
			{
				int frame = (int)binaryReader.ReadByte();
				int num14 = (int)binaryReader.ReadByte();
				for (int num15 = 0; num15 < num14; num15++)
				{
					this.sequences[n].frame = frame;
					n++;
				}
			}
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x00002910 File Offset: 0x00000D10
	private void ParseTest(byte[] bytes)
	{
		Stream input = new MemoryStream(bytes);
		using (BinaryReader binaryReader = new BigEndianBinaryReader(input))
		{
			int num = (int)binaryReader.ReadByte();
			Util.pf("Module Total Count:{0}", new object[]
			{
				num
			});
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)binaryReader.ReadByte();
				int num3 = (int)binaryReader.ReadByte();
				int num4 = (int)binaryReader.ReadByte();
				int num5 = (int)binaryReader.ReadByte();
				Util.pf("Module {0} (x,y, w,h): {1},{2}, {3},{4}", new object[]
				{
					i,
					num2,
					num3,
					num4,
					num5
				});
			}
			int num6 = (int)binaryReader.ReadByte();
			Util.pf("Module Exception Total Count:{0}", new object[]
			{
				num6
			});
			for (int j = 0; j < num6; j++)
			{
				int num7 = (int)binaryReader.ReadByte();
				int num8 = (int)binaryReader.ReadByte();
				int num9 = (int)binaryReader.ReadByte();
				Util.pf("Module Exception {0} (mod,xywh,upper): {1},{2},{3}", new object[]
				{
					j,
					num7,
					num8,
					num9
				});
			}
			int num10 = (int)binaryReader.ReadByte();
			Util.pf("Frame Total Count:{0}", new object[]
			{
				num10
			});
			for (int k = 0; k < num10; k++)
			{
				int num11 = (int)binaryReader.ReadUInt16();
				int num12 = (int)binaryReader.ReadByte();
				int num13 = (int)binaryReader.ReadInt16();
				int num14 = (int)binaryReader.ReadInt16();
				int num15 = (int)binaryReader.ReadInt16();
				int num16 = (int)binaryReader.ReadInt16();
				int num17 = (int)binaryReader.ReadInt16();
				int num18 = (int)binaryReader.ReadInt16();
				int num19 = (int)binaryReader.ReadInt16();
				int num20 = (int)binaryReader.ReadInt16();
				Util.pf("frame {0}: {1},{2} g({3},{4},{5},{6}) r({7},{8},{9},{10})", new object[]
				{
					k,
					num11,
					num12,
					num13,
					num14,
					num15,
					num16,
					num17,
					num18,
					num19,
					num20
				});
			}
			int num21 = (int)binaryReader.ReadInt16();
			Util.pf("Sprite Total Count:{0}", new object[]
			{
				num21
			});
			for (int l = 0; l < num21; l++)
			{
				int num22 = (int)binaryReader.ReadByte();
				int num23 = (int)binaryReader.ReadInt16();
				int num24 = (int)binaryReader.ReadInt16();
				int num25 = (int)binaryReader.ReadByte();
				Util.pf("sprite {0}: mod:{1} ({2},{3}) {4}", new object[]
				{
					l,
					num22,
					num23,
					num24,
					num25
				});
			}
			int num26 = (int)binaryReader.ReadByte();
			Util.pf("Action Total Count:{0}", new object[]
			{
				num26
			});
			for (int m = 0; m < num26; m++)
			{
				int num27 = (int)binaryReader.ReadInt16();
				int num28 = (int)binaryReader.ReadChar();
				int num29 = (int)binaryReader.ReadChar();
				int num30 = (int)binaryReader.ReadChar();
				Util.pf("action {0}: startSeq:{1} seqCount:{2} ex({3},{4})", new object[]
				{
					m,
					num27,
					num28,
					num29,
					num30
				});
			}
			int num31 = (int)binaryReader.ReadUInt16();
			Util.pf("Sequence Total Count:{0}", new object[]
			{
				num31
			});
			int num32 = 0;
			do
			{
				int num33 = (int)binaryReader.ReadByte();
				int num34 = (int)binaryReader.ReadByte();
				Util.pf("Sequence time:{0} frame:{1} duration:{2}", new object[]
				{
					num32,
					num33,
					num34
				});
				num32 += num34;
			}
			while (num31 > num32);
		}
	}

	// Token: 0x04000010 RID: 16
	public AnmModule[] modules;

	// Token: 0x04000011 RID: 17
	public AnmSprite[] sprites;

	// Token: 0x04000012 RID: 18
	public AnmFrame[] frames;

	// Token: 0x04000013 RID: 19
	public AnmSequence[] sequences;

	// Token: 0x04000014 RID: 20
	public AnmAction[] actions;
}
