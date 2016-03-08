using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Shared amongst projects
// ReSharper disable once CheckNamespace

namespace SpikeSpike
{
    public class Obstacle
    {
        public Obstacle(float startX, int width, int height)
        {
            X = startX;
            Width = width;
            Height = height;
        }

        public float X { get; private set; }
        public int Width { get; }
        public int Height { get; }

        public bool Visible => (int)X + Width > 0;

        public Rectangle GetBounds(Rectangle trackBounds) =>
            new Rectangle((int)X, trackBounds.Bottom - Height, Width, Height);

        public void Update(float deltaTime) => X += Constants.Speed * deltaTime;

        public void Draw(SpriteBatch spriteBatch, Texture2D texture, Rectangle trackBounds) =>
            spriteBatch.Draw(texture, GetBounds(trackBounds), Color.Green);

        public static void AddNewObstacles(Rectangle trackBounds,
            IList<Obstacle> obstacles)
        {
            var head = obstacles?.FirstOrDefault();
            var isHeadFullyVisible = head != null
                                     && head.GetBounds(trackBounds).Right < trackBounds.Right;
            if (!isHeadFullyVisible) return;
            var rng = new Random();
            var x = trackBounds.Right + 200 + rng.Next(200);
            var width = rng.Next(Constants.MinObstacleWidth, Constants.MaxObstacleWidth);
            var height = rng.Next(Constants.MinObstacleHeight, Constants.MaxObstacleHeight);
            obstacles.Add(new Obstacle(x, width, height));
        }

        public static IEnumerable<Obstacle> RemoveOldObstacles(IEnumerable<Obstacle> obstacles)
        {
            return obstacles.Where(obstacle => obstacle.Visible);
        }
    }
}