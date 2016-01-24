using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using GameJamLib.Utils.Services;

using Microsoft.Xna.Framework.Graphics;

namespace GameJamLib.Core
{
    class Scene
    {
        protected ContentManager content;
        public GraphicsDevice graphicDevice;


        public virtual void LoadContent(ContentManager Content, GraphicsDevice graph)
        {
            this.graphicDevice = graph;
            content = new ContentManager(Content.ServiceProvider, "Content");
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
