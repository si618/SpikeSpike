﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

// Shared amongst projects
// ReSharper disable once CheckNamespace

namespace SpikeSpike
{
    public class SpriteTexture
    {
        public Texture2D Texture { get; set; }
        public Color[] TextureData { get; set; }
        public int SpriteWidth { get; set; }
        public int NumSprites { get; set; }
    }
}