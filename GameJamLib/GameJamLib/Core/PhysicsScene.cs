using System;

using GameJamLib.Utils.Services;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics;
using FarseerPhysics.Collision;
using FarseerPhysics.Common;

namespace GameJamLib.Core
{
	class PhysicsScene : Scene
	{
		protected World world;

		protected Vector2 gravity;

		Body dynamicRectangle;
		Animation dynamicRectangleAnimation;

		Body staticRectangle;
		Image staticRectangleImage;

		public PhysicsScene()
		{
			world = null;

			gravity = new Vector2(0, 10);

			dynamicRectangleAnimation = new Animation();

			staticRectangleImage = new Image();
		}

		public override void LoadContent(ContentManager Content, GraphicsDevice graph)
		{
			base.LoadContent(Content, graph);

			if (world == null)
			{
				world = new World(Vector2.Zero);
			}
			else
			{
				world.Clear();
			}

			world.Gravity = gravity;

			ConvertUnits.SetDisplayUnitToSimUnitRatio(32f);

			dynamicRectangle = BodyFactory.CreateRectangle(world, 1f, 1f, 0.5f);
			dynamicRectangle.BodyType = BodyType.Dynamic;
			dynamicRectangle.Position = new Vector2(10, 3);
			dynamicRectangle.FixedRotation = true;

			dynamicRectangleAnimation.LoadContent(
				Content,
				"Graphics/CodingMadeEasyPlatformer/Hero1", Color.White,
				"", Color.White, "",
				ConvertUnits.ToDisplayUnits(dynamicRectangle.Position),
				200, new Vector2(3, 4)
			);

			Transform dynamicRectangleTransform;
			dynamicRectangle.GetTransform(out dynamicRectangleTransform);

			AABB dynamicRectangleAABB;
			dynamicRectangle.FixtureList[0].Shape.ComputeAABB(out dynamicRectangleAABB, ref dynamicRectangleTransform, 0);

			Vector2 dynamicRectangleAnimationScale = new Vector2(
				ConvertUnits.ToDisplayUnits(dynamicRectangleAABB.Width) / dynamicRectangleAnimation.SourceRect.Width,
				ConvertUnits.ToDisplayUnits(dynamicRectangleAABB.Height) / dynamicRectangleAnimation.SourceRect.Height
			);

			dynamicRectangleAnimation.scale = dynamicRectangleAnimationScale;

			dynamicRectangleAnimation.isActive = true;

			staticRectangle = BodyFactory.CreateRectangle(world, 10f, 1f, 1f);
			staticRectangle.BodyType = BodyType.Static;
			staticRectangle.Position = new Vector2(10, 10);

			staticRectangleImage.LoadContent(
				Content,
				"Graphics/minecraft", Color.White,
				"", Color.White, "",
				ConvertUnits.ToDisplayUnits(staticRectangle.Position)
			);

			Transform staticRectangleTransform;
			staticRectangle.GetTransform(out staticRectangleTransform);

			AABB staticRectangleAABB;
			staticRectangle.FixtureList[0].Shape.ComputeAABB(out staticRectangleAABB, ref staticRectangleTransform, 0);

			Vector2 staticRectangleImageScale = new Vector2(
				ConvertUnits.ToDisplayUnits(staticRectangleAABB.Width) / staticRectangleImage.SourceRect.Width,
				ConvertUnits.ToDisplayUnits(staticRectangleAABB.Height) / staticRectangleImage.SourceRect.Height
			);

			staticRectangleImage.scale = staticRectangleImageScale;
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var input = ServiceHelper.Get<InputManagerService>().Keyboard.GetState();

			Vector2 rightForce = new Vector2(5, 0);
			Vector2 leftForce = new Vector2(-5, 0);

			if (input.IsKeyDown(Keys.Right))
			{
				dynamicRectangle.ApplyForce(ref rightForce);
			}

			if (input.IsKeyDown(Keys.Left))
			{
				dynamicRectangle.ApplyForce(ref leftForce);
			}

			dynamicRectangleAnimation.position = ConvertUnits.ToDisplayUnits(dynamicRectangle.Position);
			dynamicRectangleAnimation.Update(gameTime);

			// variable time step but never less then 30 Hz
			world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			staticRectangleImage.Draw(spriteBatch);
			dynamicRectangleAnimation.Draw(spriteBatch);
		}
	}
}
