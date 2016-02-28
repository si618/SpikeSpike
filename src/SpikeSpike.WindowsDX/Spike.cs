using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

// Shared amongst projects
// ReSharper disable once CheckNamespace
namespace SpikeSpike
{
    public class Spike
    {
        public int SpikeX { get; } = 50;
        public int SpikeWidth { get; }
        public int SpikeHeight { get; }
        public Keys Trigger { get; }

        public float Y { get; set; }
        // ReSharper disable once InconsistentNaming
        public float DY { get; set; }
        public float SpriteTimer { get; set; }
        public int SpriteIndex { get; set; }
        public bool IsJumping { get; set; }

        public Spike(SpriteTexture spriteTexture, Keys trigger, int startBottom)
        {
            SpikeWidth = spriteTexture.SpriteWidth;
            SpikeHeight = spriteTexture.Texture.Height;
            Trigger = trigger;

            Y = startBottom - SpikeHeight;
            DY = 0.0f;
            IsJumping = false;
            SpriteTimer = 0.0f;
            SpriteIndex = 0;
        }

        public Rectangle Bounds => new Rectangle(SpikeX, (int) Y, SpikeWidth, SpikeHeight);

        /*
            type Santa(spriteTexture : SpriteTexture, trigger, startBottom) =

            let mutable y = single(startBottom - santaHeight)
            let mutable dy = 0.0f
            let mutable isJumping = false
            let mutable spriteTimer = 0.0f
            let mutable spriteIndex = 0

            member this.Bounds
                with get() = Rectangle(santaX, int(y), santaWidth, santaHeight)

            member this.Update(deltaTime, isKeyPressedSinceLastFrame : Keys -> bool, trackBounds : Rectangle) =
                // Should we start a jump?
                if not isJumping && isKeyPressedSinceLastFrame(trigger) then
                    isJumping <- true
                    dy <- -0.45f

                if isJumping then
                    // Physics!
                    y <- y + dy * deltaTime

                    let hasLanded = int(y) + santaHeight >= trackBounds.Bottom
                    if hasLanded then
                        y<- single(trackBounds.Bottom - santaHeight)
                        isJumping<- false
                        dy<- 0.0f
                    else
                        dy<- dy + gravity
                else
                    // Update sprite.
                    let spriteChangeTime = 80.0f
                    spriteTimer<- spriteTimer + deltaTime
                    if spriteTimer >= spriteChangeTime then
                        spriteTimer<- 0.0f
                        let wrap value max =
                            if value> max then 0 else value
                        spriteIndex<- wrap (spriteIndex + 1) (spriteTexture.numSprites - 1)

            member this.Draw(spriteBatch : SpriteBatch) =
                let spriteBounds =
                    Rectangle(spriteIndex * spriteTexture.spriteWidth, 0,
                              spriteTexture.spriteWidth, 
                              spriteTexture.texture.Height)
                spriteBatch.Draw(spriteTexture.texture, this.Bounds, 
                                 System.Nullable(spriteBounds),
                                 Color.White)

            // Used for pixel-perfect collision detection.
            member this.AnyNonTransparentPixels(x1, x2, y1, y2) =
                let xOffset = spriteTexture.spriteWidth * spriteIndex
                let pixelsInsideRegion = seq
                {
                    for y in y1..y2 - 1 do
                            for x in (x1 + xOffset)..(x2 + xOffset - 1) do
                                    let index = (y * spriteTexture.texture.Width) + x
                                yield spriteTexture.textureData.[index]
                }
                Seq.exists (fun c -> c <> Color.Transparent) pixelsInsideRegion

                }
            }
        */
    }
}