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
            var obstaclesCollidingWithSanta = Obstacles.
                Where(obstacle =>
                {
                    var spikeBounds = Spike.Bounds;
                    var obstacleBounds = obstacle.GetBounds(Bounds);
                    if (!spikeBounds.Intersects(obstacleBounds)) return false;
                    // Do pixel-perfect collision detection as bounding rectangles overlap.
                    var x1 = obstacleBounds.X - spikeBounds.X;
                    var x2 = obstacleBounds.Right - spikeBounds.X;
                    var y1 = obstacleBounds.Y - spikeBounds.Y;
                    var y2 = obstacleBounds.Bottom - spikeBounds.Y;
                    return Spike.AnyNonTransparentPixels(x1, x2, y1, y2);
                });

            return obstaclesCollidingWithSanta.Any();
        }

        public static IEnumerable<Track> CreateTacks(Rectangle gameBounds,
            SpriteTexture spriteTexture, int numTracks)
        {
            const int padding = 10;
            var totalPadding = (numTracks - 1) * padding;
            var availableHeight = gameBounds.Height - totalPadding;
            var trackHeight = availableHeight / numTracks;
            var colors = new[] {Color.Red, Color.Blue, Color.Purple, Color.Brown, Color.Gold};
            var keys = new[] {Keys.A, Keys.S, Keys.D, Keys.F, Keys.Space};
            var tracks = new List<Track>();
            for (var index = 0; index < numTracks; index++)
            {
                var trackbounds = new Rectangle(0, index * (trackHeight + padding),
                    gameBounds.Width, trackHeight);
                var track = new Track(colors[index], trackbounds, spriteTexture,
                    keys[index + (keys.Length - numTracks)]);
                tracks.Add(track);
            }
            return tracks;
        }
    }
}