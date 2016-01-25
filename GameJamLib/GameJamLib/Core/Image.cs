using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Common;
using FarseerPhysics.Collision;
using FarseerPhysics;

namespace GameJamLib
{
	public class Image
	{
		protected Texture2D texture { get; set; }
		protected Rectangle sourceRect { get; set; }
		private Vector2 origin { get; set; }

		// Can be modified without fucking up everything
		public Color textureColor { get; set; }
		public float rotation { get; set; }
		public Vector2 scale { get; set; }
		public float alpha { get; set; }
		public Vector2 position { get; set; }

		public Rectangle SourceRect
		{
			get { return sourceRect; }
		}

		public void LoadContent(ContentManager content, string texturePath, Color textureColor, Vector2 position)
		{
			content = new ContentManager(content.ServiceProvider, "Content");

			if (texturePath != String.Empty)
			{
				texture = content.Load<Texture2D>(texturePath);
				sourceRect = new Rectangle(0, 0, texture.Width, texture.Height);
			}

			this.textureColor = textureColor;

			this.position = position;

			rotation = 0.0f;
			scale = new Vector2(1, 1);

			alpha = 1.0f;
		}

		public virtual void UnloadContent()
		{
			texture = null;
		}

		public virtual void Update(GameTime gameTime)
		{
		}

		public virtual void Draw(SpriteBatch spriteBatch)
		{
			if (texture != null)
			{
				origin = new Vector2(sourceRect.Width / 2, sourceRect.Height / 2);
				spriteBatch.Draw(texture, position, sourceRect, textureColor * alpha, rotation, origin, scale, SpriteEffects.None, 0.0f);
			}
		}

		public void ScaleToBody(Body body)
		{
			Transform t;
			body.GetTransform(out t);

			AABB aabb;
			aabb.LowerBound = new Vector2(Settings.MaxFloat, Settings.MaxFloat);
			aabb.UpperBound = new Vector2(-Settings.MaxFloat, -Settings.MaxFloat);

			AABB tempAABB;

			int fixtureCount = body.FixtureList.Count;
			for (int i = 0; i < fixtureCount; ++i)
			{
				body.FixtureList[i].Shape.ComputeAABB(out tempAABB, ref t, 0);
				aabb.Combine(ref tempAABB);
			}

			Vector2 scale = new Vector2(
				ConvertUnits.ToDisplayUnits(aabb.Width) / sourceRect.Width,
				ConvertUnits.ToDisplayUnits(aabb.Height) / sourceRect.Height
			);

			this.scale = scale;
		}
	}
}
