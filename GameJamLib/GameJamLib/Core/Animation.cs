﻿using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamLib
{
	public class Animation : Image
	{
		private int frameCounter { get; set; }
		private int frameDuration { get; set; }
		private Vector2 frames { get; set; }
		private Vector2 currentFrame { get; set; }

		// Can be modified without fucking up everything
		public bool isActive { get; set; }

		private int FrameWidth
		{
			get { return texture.Width / (int)frames.X; }
		}

		private int FrameHeight
		{
			get { return texture.Height / (int)frames.Y; }
		}

		public bool SelectAnimation(int index)
		{
			currentFrame = new Vector2(currentFrame.X, index);

			if (currentFrame.Y * FrameHeight >= texture.Height)
			{
				currentFrame = new Vector2(currentFrame.X, 0);

				return false;
			}

			return true;
		}

		public void SetSpeed(int milliseconds)
		{
			if (milliseconds > 0)
			{
				frameDuration = milliseconds;
			}
		}

		public void LoadContent(ContentManager content, string texturePath, Color textureColor, string fontPath, Color fontColor, string text, Vector2 position,
			int frameDuration, Vector2 frames)
		{
			base.LoadContent(content, texturePath, textureColor, fontPath, fontColor, text, position);

			frameCounter = 0;
			this.frameDuration = frameDuration;

			this.frames = frames;
			currentFrame = new Vector2(0, 0);

			sourceRect = new Rectangle((int)currentFrame.X * FrameWidth, (int)currentFrame.Y * FrameHeight, FrameWidth, FrameHeight);

			isActive = false;
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
		}

		public override void Update(GameTime gameTime)
		{
			if (isActive)
			{
				frameCounter += (int)gameTime.ElapsedGameTime.Milliseconds;

				if (frameCounter >= frameDuration)
				{
					frameCounter = 0;
					currentFrame = new Vector2(currentFrame.X + 1, currentFrame.Y);

					if (currentFrame.X * FrameWidth >= texture.Width)
					{
						currentFrame = new Vector2(0, currentFrame.Y);
					}
				}
			}
			else
			{
				frameCounter = 0;
				currentFrame = new Vector2(0, currentFrame.Y);
			}

			sourceRect = new Rectangle((int)currentFrame.X * FrameWidth, (int)currentFrame.Y * FrameHeight, FrameWidth, FrameHeight);
		}
	}
}
