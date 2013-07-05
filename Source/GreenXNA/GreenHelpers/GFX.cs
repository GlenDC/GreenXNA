#region File Description
//-----------------------------------------------------------------------------
// GFX.cs
//
// GreenXNA Open Source Crossplatform Game Development Framework
// Copyright (C) 2013-2014 Glen De Cauwsemaecker
// More information and details can be found at http://greenxna.glendc.com/
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Xml;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
#endregion

namespace GreenXNA.GreenHelpers
{
    public static class GFX
    {
        /// <summary>
        /// Get a texture copy from a RenderTarget
        /// </summary>
        /// <param name="target">rendertarget to be copied</param>
        /// <returns>texture object, containing copied data of an excesting rendertarget</returns>
        public static Texture2D GetTextureFromRenderTarget(RenderTarget2D target)
        {
            RenderTarget2D clone = new RenderTarget2D(target.GraphicsDevice, target.Width,
                target.Height, target.LevelCount > 1, target.Format,
                target.DepthStencilFormat, target.MultiSampleCount,
                target.RenderTargetUsage);

            for (int i = 0; i < target.LevelCount; i++)
            {
                double rawMipWidth = target.Width / Math.Pow(2, i);
                double rawMipHeight = target.Height / Math.Pow(2, i);

                // make sure that mipmap dimensions are always > 0.
                int mipWidth = (rawMipWidth < 1) ? 1 : (int)rawMipWidth;
                int mipHeight = (rawMipHeight < 1) ? 1 : (int)rawMipHeight;

                var mipData = new Color[mipWidth * mipHeight];
                target.GetData(i, null, mipData, 0, mipData.Length);
                clone.SetData(i, null, mipData, 0, mipData.Length);
            }

            return (Texture2D)clone;
        }
    }
}
