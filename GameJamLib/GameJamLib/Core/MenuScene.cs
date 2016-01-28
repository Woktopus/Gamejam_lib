using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace GameJamLib.Core
{
    class MenuScene : Scene
    {
        Button play;

        public override void LoadContent(ContentManager Content, GraphicsDevice graph)
        {
            base.LoadContent(Content, graph);
            play = new Button(Content.Load<Texture2D>("Graphics/CodingMadeEasyPlatformer/Hero1"),graph);
            play.Position = new Vector2(350, 300);
        }

        public override void Update(GameTime gameTime)
        {

            if (play.isClicked)
            {
                SceneManager.Instance.AddScene(new PhysicsScene());
            }
            play.Update(gameTime);
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);


            play.Draw(spriteBatch);
        }
    }
}
