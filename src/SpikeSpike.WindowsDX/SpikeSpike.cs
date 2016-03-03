using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Shared amongst projects
// ReSharper disable once CheckNamespace

namespace SpikeSpike
{
    public class SpikeSpike : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public SpikeSpike()
        {
            _graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = 800,
                PreferredBackBufferHeight = 600
            };
            Content.RootDirectory = "Content";
            Window.Title = "Spike Spike";
            _spriteBatch = new SpriteBatch(_graphics.GraphicsDevice);
            /*
            type MakeSantaJumpGame() as this =
                let mutable spriteBatch = Unchecked.defaultof<SpriteBatch>
                let mutable texture = Unchecked.defaultof<Texture2D>
                let mutable spriteTexture = Unchecked.defaultof<SpriteTexture>
                let mutable fontRenderer = Unchecked.defaultof<FontRendering.FontRenderer>

                let mutable tracks = []
                let mutable gameState = MainMenu
                let mutable lastKeyState = KeyboardState()
            */
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            /*
            override this.LoadContent() =
                spriteBatch <- new SpriteBatch(this.GraphicsDevice)

                texture <- new Texture2D(this.GraphicsDevice, 1, 1)
                texture.SetData([| Color.White |])

                use santaStream = System.IO.File.OpenRead("Santa.png")
                let santaTexture = Texture2D.FromStream(this.GraphicsDevice, santaStream)
                let santaTextureData = Array.create<Color> (santaTexture.Width * santaTexture.Height) Color.Transparent
                santaTexture.GetData(santaTextureData)
                spriteTexture <- { texture = santaTexture;
                                   textureData = santaTextureData;
                                   spriteWidth = santaTexture.Width / 8;
                                   numSprites = 8 }

                use fontTextureStream = System.IO.File.OpenRead("GameFont_0.png")
                let fontTexture = Texture2D.FromStream(this.GraphicsDevice, fontTextureStream)
                let fontFile = FontRendering.FontLoader.Load("GameFont.fnt")
                fontRenderer <- FontRendering.FontRenderer(fontFile, fontTexture)
            */
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

            // TODO: Add your update logic here
            /*
            override this.Update(gameTime) =
                let currentKeyState = Keyboard.GetState()
                let deltaTime = single(gameTime.ElapsedGameTime.TotalMilliseconds)

                let isKeyPressedSinceLastFrame key =
                    currentKeyState.IsKeyDown(key) && lastKeyState.IsKeyUp(key)

                match gameState with
                | MainMenu ->
                    let startGame numTracks =
                        tracks <- Track.createTracks this.GraphicsDevice.Viewport.Bounds spriteTexture numTracks
                        gameState <- Game

                    if isKeyPressedSinceLastFrame Keys.D1 then startGame 1
                    elif isKeyPressedSinceLastFrame Keys.D2 then startGame 2
                    elif isKeyPressedSinceLastFrame Keys.D3 then startGame 3
                    elif isKeyPressedSinceLastFrame Keys.D4 then startGame 4
                    elif isKeyPressedSinceLastFrame Keys.D5 then startGame 5
                | Game ->
                    if isKeyPressedSinceLastFrame Keys.P then
                        gameState <- GamePaused
                    else
                        for track in tracks do
                            track.Update(deltaTime, isKeyPressedSinceLastFrame)
                        if List.exists (fun (t : Track) -> t.HasCollisions()) tracks then
                            gameState <- GameOver
                | GamePaused ->
                    if isKeyPressedSinceLastFrame Keys.P then
                        gameState <- Game
                | GameOver ->
                    if isKeyPressedSinceLastFrame Keys.Space then
                        gameState <- MainMenu

                lastKeyState <- currentKeyState
            */

            base.Update(gameTime);
        }

        /// <summary>This is called when the game should draw itself.</summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            /*
            override this.Draw(gameTime) =
                this.GraphicsDevice.Clear Color.Black

                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied)

                let avoidedObstacles = List.sumBy (fun (o : Track) -> o.AvoidedObstacles) tracks

                match gameState with
                | MainMenu ->
                    fontRenderer.DrawText(spriteBatch, 100, 50, "Choose a difficulty.")
                    fontRenderer.DrawText(spriteBatch, 100, 150, "1 - Even Rudolph could handle it")
                    fontRenderer.DrawText(spriteBatch, 100, 200, "2 - Not nice")
                    fontRenderer.DrawText(spriteBatch, 100, 250, "3 - Ho ho HO NO")
                    fontRenderer.DrawText(spriteBatch, 100, 300, "4 - I don't get paid enough for this")
                    fontRenderer.DrawText(spriteBatch, 100, 350, "5 - This is why Santa drinks")
                | Game
                | GamePaused ->
                    for track in tracks do
                        track.Draw(spriteBatch, texture, fontRenderer)
                    fontRenderer.DrawText(spriteBatch, this.GraphicsDevice.Viewport.Bounds.Right - 60, 30,
                                          avoidedObstacles.ToString())
                | GameOver ->
                    for track in tracks do
                        track.Draw(spriteBatch, texture, fontRenderer)
                    fontRenderer.DrawText(spriteBatch, this.GraphicsDevice.Viewport.Bounds.Right - 60, 30,
                                          avoidedObstacles.ToString())
                    fontRenderer.DrawText(spriteBatch, 100, 100, "Game Over!")
                    fontRenderer.DrawText(spriteBatch, 100, 150, "Press Space to continue.")

                spriteBatch.End()
            */

            base.Draw(gameTime);
        }
    }
}