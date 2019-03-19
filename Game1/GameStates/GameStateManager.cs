using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Game1.GameStates
{

    public class GameStateManager
    {
        private ContentManager _content;

        public static GameStateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameStateManager();
                }
                return _instance;
            }
        }

        public void SetContent(ContentManager content)
        {
            _content = content;
        }

        public void AddScreen(GameState screen)
        {
            try
            {
                _screens.Push(screen);

                _screens.Peek().Initialize();
                if(_content != null)
                {
                    _screens.Peek().LoadContent(_content);
                }
            }
            catch(Exception ex)
            {
                //Log the exception
            }
        }

        public void RemoveScreen()
        {
            if(_screens.Count > 0)
            {
                try
                {
                    var screen = _screens.Peek();
                    _screens.Pop();
                }
                catch(Exception ex)
                {
                    // Log the exception
                }
            }
        }

        public void ClearScreens()
        {
            while(_screens.Count > 0)
            {
                _screens.Pop();
            }
        }

        public void ChangeScreen(GameState screen)
        {
            try
            {
                ClearScreens();
                AddScreen(screen);
            }
            catch(Exception ex)
            {
                // Log the exception
            }
        }

        public void Update(GameTime gameTime)
        {
            try
            {
                if(_screens.Count > 0)
                {
                    _screens.Peek().Update(gameTime);
                }
            }
            catch(Exception ex)
            {
                // Log the exception
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            try
            {
                if(_screens.Count > 0)
                {
                    _screens.Peek().Draw(spriteBatch);
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        public void UnloadContent()
        {
            foreach(GameState state in _screens)
            {
                state.UnloadContent();
            }
        }

        private static GameStateManager _instance;

        private Stack<GameState> _screens = new Stack<GameState>();

    }
}
