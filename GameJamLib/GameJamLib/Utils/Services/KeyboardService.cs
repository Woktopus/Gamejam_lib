using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameJamLib.Utils.Services
{
    /// <summary>
    /// dans Game methode Initialize() ajouter 
    /// this.Components.Ad(new KeyboardService(this));
    /// et dans Game.Update() ajouter
    /// this.Services.GetService(typeof(KeyboardService)).Update(gameTime);
    /// </summary>
    public class KeyboardService : GameComponent
    {
        KeyboardState KBState;

        public KeyboardService(Game game) 
            : base(game)
        {
            ServiceHelper.Add<KeyboardService>(this);
        }

        public bool IsKeyDown(Keys key) 
        {
            return KBState.IsKeyDown(key);
        }

        public override void Update(GameTime gameTime)
        {
            KBState = Keyboard.GetState();
            base.Update(gameTime);
        }

    }
}
