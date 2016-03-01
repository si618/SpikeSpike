using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpikeSpike.WindowsDX
{
    public class Track
    {

    }
    /*
    type Track(color, bounds : Rectangle, spriteTexture, triggerKey) =
        let mutable obstacles = List.empty<Obstacle>
        let mutable avoidedObstacles = 0
        let santa = Santa(spriteTexture, triggerKey, bounds.Bottom)

        member this.AvoidedObstacles
            with get() = avoidedObstacles

        member this.Update(deltaTime, isKeyPressedSinceLastFrame) =
            santa.Update(deltaTime, isKeyPressedSinceLastFrame, bounds)

            for obstacle in obstacles do
                obstacle.Update(deltaTime)

            let oldObstaclesCount =
                obstacles
                |> List.filter (fun o -> not o.Visible)
                |> List.length
            avoidedObstacles <- avoidedObstacles + oldObstaclesCount

            obstacles <- obstacles
                |> Obstacle.removeOldObstacles
                |> Obstacle.addNewObstacles bounds

        member this.Draw(spriteBatch : SpriteBatch, texture, fontRenderer : FontRendering.FontRenderer) =
            spriteBatch.Draw(texture, bounds, color) // Track background
            for obstacle in obstacles do
                obstacle.Draw(spriteBatch, texture, bounds)
            santa.Draw(spriteBatch)
            fontRenderer.DrawText(spriteBatch, 10, 10 + bounds.Y, triggerKey.ToString())

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
