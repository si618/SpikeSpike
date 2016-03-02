using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

// Shared amongst projects
// ReSharper disable once CheckNamespace

namespace SpikeSpike
{
    public class Track
    {
        public Track(Color color, Rectangle bounds, SpriteTexture spriteTexture, Keys triggerKey)
        {
            Color = color;
            Bounds = bounds;
            SpriteTexture = spriteTexture;
            TriggerKey = triggerKey;
            Obstacles = new List<Obstacle>();
            AvoidedObstacles = 0;
            Spike = new Spike(spriteTexture, triggerKey, bounds.Bottom);
        }

        public int AvoidedObstacles { get; private set; }
        public List<Obstacle> Obstacles { get; }
        public Spike Spike { get; }
        public Color Color { get; }
        public Rectangle Bounds { get; }
        public SpriteTexture SpriteTexture { get; }
        public Keys TriggerKey { get; }

        public void Update(float deltaTime, Keys isKeyPressedSinceLastFrame, Rectangle bounds)
        {
            Spike.Update(deltaTime, isKeyPressedSinceLastFrame, bounds);
            Obstacles.ForEach(obstacle => obstacle.Update(deltaTime));
            var oldObstaclesCount = Obstacles.Count(obstacle => !obstacle.Visible);
            AvoidedObstacles += oldObstaclesCount;
            Obstacle.RemoveOldObstacles(Obstacles);
            Obstacle.AddNewObstacles(bounds, Obstacles);
        }

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, FontRenderer fontRenderer)
        {
            spriteBatch.Draw(texture, Bounds, Color); // Track background
            Obstacles.ForEach(obstacle => obstacle.Draw(spriteBatch, texture, Bounds));
            Spike.Draw(spriteBatch);
            fontRenderer.DrawText(spriteBatch, 10, 10 + Bounds.Y, TriggerKey.ToString());
        }

        public bool HasCollision()
        {
            return false; //TODO:
        }

        public static void CreateTacks(Rectangle gameBounds)
        {
        }
    }

    /*
        member this.HasCollisions() =
            let santaBounds = santa.Bounds

            let obstacleCollidingWithSanta (obstacle : Obstacle) =
                // First do simple intersection.
                let obstacleBounds = obstacle.GetBounds(bounds)

                if santaBounds.Intersects(obstacleBounds) then
                    // If the bounding rectangles overlap, then do pixel-perfect collision detection.
                    let x1 = max (obstacleBounds.X - santaBounds.X) 0
                    let x2 = min (obstacleBounds.Right - santaBounds.X) santaBounds.Width

                    let y1 = max (obstacleBounds.Y - santaBounds.Y) 0
                    let y2 = min (obstacleBounds.Bottom - santaBounds.Y) santaBounds.Height

                    santa.AnyNonTransparentPixels(x1, x2, y1, y2)
                else
                    false

            List.exists obstacleCollidingWithSanta obstacles

    [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
    module Track =
        let createTracks (gameBounds : Rectangle) spriteTexture numTracks =
            let padding = 10
            let totalPadding = (numTracks - 1) * padding
            let availableHeight = gameBounds.Height - totalPadding
            let trackHeight = availableHeight / numTracks

            let colors = [ Color.Red; Color.Blue; Color.Purple; Color.Brown; Color.Gold ]
            let keys = [ Keys.A; Keys.S; Keys.D; Keys.F; Keys.Space ]

            let makeTrack i =
                let trackBounds = Rectangle(0, i * (trackHeight + padding),
                                            gameBounds.Width, trackHeight)
                Track(colors.[i], trackBounds, spriteTexture,
                      keys.[i + (keys.Length - numTracks)])

            List.init numTracks makeTrack
    */
}