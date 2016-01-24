using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamLib
{
	public class Image
	{
		private ContentManager content { get; set; }
		protected Texture2D texture { get; set; }
		private SpriteFont font { get; set; }
		protected Rectangle sourceRect { get; set; }
		private Vector2 origin { get; set; }

		// Can be modified without fucking up everything
		public string text { get; set; }
		public Color textureColor { get; set; }
		public Color fontColor { get; set; }
		public float rotation { get; set; }
		public Vector2 scale { get; set; }
		public float alpha { get; set; }
		public Vector2 position { get; set; }

		public Rectangle SourceRect
		{
			get { return sourceRect; }
		}

		public void LoadContent(ContentManager content, string texturePath, Color textureColor, string fontPath, Color fontColor, string text, Vector2 position)
		{
			content = new ContentManager(content.ServiceProvider, "Content");

			if (texturePath != String.Empty)
			{
				texture = content.Load<Texture2D>(texturePath);
				sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
			}
			if (fontPath != String.Empty)
			{
				font = this.content.Load<SpriteFont>(fontPath);
			}

			this.textureColor = textureColor;
			this.fontColor = fontColor;

			this.text = text;

			this.position = position;

			rotation = 0.0f;
			scale = new Vector2(1, 1);

			alpha = 1.0f;
		}

		public virtual void UnloadContent()
		{
			texture = null;
			font = null;
		}

		public virtual void Update(GameTime gameTime)
		{
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (texture != null)
			{
				origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
				spriteBatch.Draw(texture, position /*+ (origin * scale)*/, sourceRect, textureColor * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
			}
			if (font != null)
			{
				origin = new Vector2(font.MeasureString(text).X / 2, font.MeasureString(text).Y / 2);
				spriteBatch.DrawString(font, text, position /*+ (origin * scale)*/, fontColor * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
			}
		}
	}
}
