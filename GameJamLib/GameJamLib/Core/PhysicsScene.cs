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
using FarseerPhysics.DebugView;
using FarseerPhysics.Collision.Shapes;
using System.Collections.Generic;

namespace GameJamLib.Core
{
	class PhysicsScene : Scene
	{
		protected World world;

		protected Vector2 gravity;

		protected DebugViewXNA debugView;
		protected Matrix projection;

		Body player;
		Animation playerAnimation;

		List<Body> platforms;
		List<Image> platformsImages;

		public PhysicsScene()
		{
			world = null;

			gravity = new Vector2(0, 10);

			playerAnimation = new Animation();

			platforms = new List<Body>();
			platformsImages = new List<Image>();
		}

		public override void LoadContent(ContentManager content, GraphicsDevice graph)
		{
			base.LoadContent(content, graph);

			Settings.UseFPECollisionCategories = true;

			ConvertUnits.SetDisplayUnitToSimUnitRatio(32f);

			if (world == null)
			{
				world = new World(Vector2.Zero);
			}
			else
			{
				world.Clear();
			}

			world.Gravity = gravity;

			if (debugView == null)
			{
				debugView = new DebugViewXNA(world);

				debugView.LoadContent(graph, content);
			}
			
			projection = Matrix.CreateOrthographicOffCenter(
				0f, ConvertUnits.ToSimUnits(graph.Viewport.Width),
				ConvertUnits.ToSimUnits(graph.Viewport.Height), 0f,
				0f, 1f
			);

			///////////////////////////////////////////////////////////////////////////////
			player = BodyFactory.CreateCapsule(world, 0.5f, 0.8f, 1f);

			player.CollisionCategories = Category.Cat2;
			player.CollidesWith = Category.Cat1;

			player.BodyType = BodyType.Dynamic;
			player.Position = new Vector2(10, 3);
			player.FixedRotation = true;

			playerAnimation.LoadContent(
				content,
				"Graphics/CodingMadeEasyPlatformer/Hero1", Color.White,
				ConvertUnits.ToDisplayUnits(player.Position),
				200, new Vector2(3, 4)
			);

			playerAnimation.ScaleToBody(player);

			playerAnimation.isActive = true;

			for (int i = 0; i < 5; ++i)
			{
				Body platform = BodyFactory.CreateRectangle(world, 2f, 2f, 1f);

				platform.CollisionCategories = Category.Cat1;
				platform.CollidesWith = Category.Cat2;

				platform.BodyType = BodyType.Static;
				platform.Position = new Vector2(2 + (i * 3), 10);

				Image platformImage = new Image();
				platformImage.LoadContent(
					 content,
					 "Graphics/minecraft", Color.White,
					 ConvertUnits.ToDisplayUnits(platform.Position)
				 );

				platformImage.ScaleToBody(platform);

				platforms.Add(platform);
				platformsImages.Add(platformImage);
			}
		}

		public override void UnloadContent()
		{
			base.UnloadContent();
		}

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			var input = ServiceHelper.Get<InputManagerService>().Keyboard.GetState();

			Vector2 jump = new Vector2(0, -3);
			Vector2 movement = new Vector2(3, 0);

			if (input.IsKeyDown(Keys.Up))
			{
				player.ApplyLinearImpulse(jump);
			}
			
			if (input.IsKeyDown(Keys.Right))
			{
				player.ApplyLinearImpulse(movement);
			}
			
			if (input.IsKeyDown(Keys.Left))
			{
				player.ApplyLinearImpulse(-movement);
			}

			if (input.IsKeyUp(Keys.Left) && input.IsKeyUp(Keys.Right))
			{
				player.LinearVelocity = new Vector2(player.LinearVelocity.X * 0.92f, player.LinearVelocity.Y);
			}
			else
			{
				player.LinearVelocity = new Vector2(MathHelper.Clamp(player.LinearVelocity.X, -10, 10), player.LinearVelocity.Y);
			}

			playerAnimation.position = ConvertUnits.ToDisplayUnits(player.Position);
			playerAnimation.Update(gameTime);

			// variable time step but never less then 30 Hz
			world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
		}

		public override void Draw(SpriteBatch spriteBatch)
		{
			debugView.RenderDebugData(ref projection);

			int platformCount = platforms.Count;
			for (int i = 0; i < platformCount; ++i)
			{
				platformsImages[i].Draw(spriteBatch);
			}

			playerAnimation.Draw(spriteBatch);
		}
	}
}
