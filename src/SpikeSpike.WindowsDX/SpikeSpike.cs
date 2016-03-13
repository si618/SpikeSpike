using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Shared amongst projects
// ReSharper disable once CheckNamespace

namespace SpikeSpike
{
    public class SpikeSpike : Game
    {
        private readonly GraphicsDeviceManager _graphics;
        private FontRenderer _fontRenderer;
        private GameState _gameState;
        private KeyboardState _lastKeyState;
        private SpriteBatch _spriteBatch;
        private SpriteTexture _spriteTexture;
        private Texture2D _texture;
        private IEnumerable<Track> _tracks;

        public SpikeSpike()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 600
            };
            Content.RootDirectory = "Content";
            Window.Title = "Spike Spike";
        }

        /// <summary>LoadContent called once per game.</summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            _texture = new Texture2D(_graphics.GraphicsDevice, 1, 1);
            _texture.SetData(new[] {Color.White});

            using (var spikeStream = File.OpenRead("Spike.png"))
            {
                var spikeTexture = Texture2D.FromStream(_graphics.GraphicsDevice, spikeStream);
                var spikeTextureData = new[]
                {
                    new Color(Color.Transparent, spikeTexture.Width * spikeTexture.Height)
                };
                spikeTexture.GetData(spikeTextureData);
                _spriteTexture = new SpriteTexture
                {
                    Texture = spikeTexture,
                    TextureData = spikeTextureData,
                    SpriteWidth = spikeTexture.Width / 8,
                    NumSprites = 8
                };
                using (var fontTextureStream = File.OpenRead("GameFontImage.png"))
                {
                    var fontTexture = Texture2D.FromStream(_graphics.GraphicsDevice,
                        fontTextureStream);
                    var fontFile = FontLoader.Load("GameFont.fnt");
                    _fontRenderer = new FontRenderer(fontFile, fontTexture);
                }
            }
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world, checking for collisions,
        ///     gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            var currentKeyState = Keyboard.GetState();
            var key = currentKeyState.GetPressedKeys().LastOrDefault();
            if (key != Keys.None)
            {
                UpdateGame(key, gameTime);
            }
            _lastKeyState = currentKeyState;

            base.Update(gameTime);
        }

        private void UpdateGame(Keys key, GameTime gameTime)
        {
            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (_gameState)
            {
                case GameState.MainMenu:
                    Action<int> startGame = numTracks =>
                    {
                        _tracks = Track.CreateTacks(_graphics.GraphicsDevice.Viewport.Bounds,
                            _spriteTexture, numTracks);
                        _gameState = GameState.Game;
                    };
                    if (key == Keys.D1) startGame(1);
                    else if (key == Keys.D2) startGame(2);
                    else if (key == Keys.D3) startGame(3);
                    else if (key == Keys.D4) startGame(4);
                    else if (key == Keys.D5) startGame(5);
                    break;
                case GameState.Game:
                    if (key == Keys.P)
                    {
                        _gameState = GameState.GamePaused;
                    }
                    else
                    {
                        var deltaTime = gameTime.ElapsedGameTime.TotalMilliseconds;
                        foreach (var track in _tracks)
                        {
                            track.Update((float)deltaTime, key, track.Bounds);
                            if (track.HasCollision())
                            {
                                _gameState = GameState.GameOver;
                            }
                        }
                    }
                    break;
                case GameState.GameOver:
                    if (key == Keys.Space)
                    {
                        _gameState = GameState.MainMenu;
                    }
                    break;
                case GameState.GamePaused:
                    if (key == Keys.P)
                    {
                        _gameState = GameState.Game;
                    }
                    break;
            }
        }

        /// <summary>This is called when the game should draw itself.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.Black);

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied);

            var avoidedObstacles = _tracks?.Select(track => track.AvoidedObstacles).Count() ?? 0;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (_gameState)
            {
                case GameState.MainMenu:
                    _fontRenderer.DrawText(_spriteBatch, 100, 50, "Choose a difficulty.");
                    _fontRenderer.DrawText(_spriteBatch, 100, 150, "1 - Even Spork could handle it");
                    _fontRenderer.DrawText(_spriteBatch, 100, 200, "2 - Not nice");
                    _fontRenderer.DrawText(_spriteBatch, 100, 250, "3 - Game On");
                    _fontRenderer.DrawText(_spriteBatch, 100, 300, "4 - I don't get paid enough for this");
                    _fontRenderer.DrawText(_spriteBatch, 100, 350, "5 - Tell him he's dreamin'");
                    break;
                case GameState.Game:
                case GameState.GamePaused:
                    foreach (var track in _tracks)
                    {
                        track.Draw(_spriteBatch, _texture, _fontRenderer);
                        _fontRenderer.DrawText(_spriteBatch,
                            _graphics.GraphicsDevice.Viewport.Bounds.Right - 60, 30,
                            avoidedObstacles.ToString());
                    }
                    break;
                case GameState.GameOver:
                    foreach (var track in _tracks)
                    {
                        track.Draw(_spriteBatch, _texture, _fontRenderer);
                    }
                    _fontRenderer.DrawText(_spriteBatch,
                        _graphics.GraphicsDevice.Viewport.Bounds.Right - 60, 30,
                        avoidedObstacles.ToString());
                    _fontRenderer.DrawText(_spriteBatch, 100, 100, "Game Over!");
                    _fontRenderer.DrawText(_spriteBatch, 100, 150, "Press Space to continue.");
                    break;
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}