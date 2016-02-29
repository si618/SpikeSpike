using Microsoft.Xna.Framework;

// Shared amongst projects
// ReSharper disable once CheckNamespace

namespace SpikeSpike.WindowsDX
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

        public void Update(float deltaTime) => X = X + Constants.Speed * deltaTime;

        /*
        type Obstacle(startX, width, height) =
            let mutable x = startX

            member this.Visible
                with get() = int(x) + width > 0

            member this.GetBounds(trackBounds : Rectangle) =
                Rectangle(int(x), trackBounds.Bottom - height, width, height)

            member this.Update(deltaTime) =
                x <- x + speed * deltaTime

            member this.Draw(spriteBatch : SpriteBatch, texture, trackBounds : Rectangle) =
                spriteBatch.Draw(texture, this.GetBounds(trackBounds), Color.Green)

        [<CompilationRepresentation(CompilationRepresentationFlags.ModuleSuffix)>]
        module Obstacle =
            let rng = System.Random()

            let addNewObstacles (trackBounds : Rectangle) (obstacles : Obstacle list) =
                let isMostRecentlyAddedObstacleFullyVisible =
                    match obstacles with
                    | head :: tail -> head.GetBounds(trackBounds).Right < trackBounds.Right
                    | [] -> true

                if isMostRecentlyAddedObstacleFullyVisible then
                    let x = trackBounds.Right + 200 + rng.Next(200)
                    let width = rng.Next(minObstacleWidth, maxObstacleWidth)
                    let height = rng.Next(minObstacleHeight, maxObstacleHeight)
                    let newObstacle = Obstacle(single(x), width, height)
                    newObstacle :: obstacles
                else
                    obstacles

            let removeOldObstacles (obstacles : Obstacle list) =
                obstacles |> List.filter (fun o -> o.Visible)
        */
    }
}