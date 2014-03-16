using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Entities;

namespace SomethingLikeCrimsonland
{
    class GameEventsHandler
    {
        public delegate GameState GameActionHandler (KeyboardState keyboardState, MouseState mouseState, GameTime gameTime);
        public delegate GameState DrawHandler (SpriteBatch spriteBatch);
        public enum GameState
        {
            playing,
            gameTimeMenu,
            gameMenu
        }
        public GameState CurrentGameState { get; set; }
        private GameActionHandler _playingHandler;
        private GameActionHandler _gameTimeMenuHandler;
        private GameActionHandler _gameMenuHandler;
        private DrawHandler _playingDraw;
        private DrawHandler _gameTimeMenuDraw;
        private DrawHandler _gameMenuDraw;
        public GameEventsHandler(GameState state, GameActionHandler playing, GameActionHandler gameTimeMenu, GameActionHandler gameMenu, DrawHandler playingDraw, DrawHandler gameTimeMenuDraw, DrawHandler gameMenuDraw)
        {
            CurrentGameState = state;
            _playingHandler = playing;
            _gameTimeMenuHandler = gameTimeMenu;
            _gameMenuHandler = gameMenu;
            _playingDraw = playingDraw;
            _gameTimeMenuDraw = gameTimeMenuDraw;
            _gameMenuDraw = gameMenuDraw;
        }
        public void HandleGameInput(KeyboardState keyboardState, MouseState mouseState, GameTime gameTime)
        {
            switch(CurrentGameState)
            {
                case GameState.gameMenu: CurrentGameState = _gameMenuHandler(keyboardState,mouseState,gameTime); break;
                case GameState.gameTimeMenu: CurrentGameState = _gameTimeMenuHandler(keyboardState, mouseState, gameTime); break;
                case GameState.playing: CurrentGameState = _playingHandler(keyboardState, mouseState, gameTime); break;
            }
        }
        public void HandleGameDraw(SpriteBatch spriteBatch)
        {
            switch (CurrentGameState)
            {
                case GameState.gameMenu: CurrentGameState = _gameMenuDraw(spriteBatch); break;
                case GameState.gameTimeMenu: CurrentGameState = _gameTimeMenuDraw(spriteBatch); break;
                case GameState.playing: CurrentGameState = _playingDraw(spriteBatch); break;
            }
        }
    }
}
