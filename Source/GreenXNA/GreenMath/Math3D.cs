#region File Description
//-----------------------------------------------------------------------------
// Math3D.cs
//
// GreenXNA Open Source Crossplatform Game Development Framework
// Copyright (C) 2013-2014 Glen De Cauwsemaecker
// More information and details can be found at http://www.greenxna.com/
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Math Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
#endregion

namespace GreenXNA.GreenMath
{
    public static class Math3D
    {
        /// <summary>
        /// get the pitch of a quaternion variable
        /// </summary>
        /// <param name="quaternion">quaternion to take the pitch from</param>
        /// <returns>pitch value</returns>
        public static float GetPitch(this Quaternion quaternion)
        {
            return (float)Math.Atan2(2 * (quaternion.Y * quaternion.Z + quaternion.W * quaternion.X),
                quaternion.W * quaternion.W - quaternion.X * quaternion.X - quaternion.Y * quaternion.Y + quaternion.Z * quaternion.Z);
        }

        /// <summary>
        /// get the yaw of a quaternion variable
        /// </summary>
        /// <param name="quaternion">quaternion to take the pitch from</param>
        /// <returns>yaw value</returns>
        public static float GetYaw(this Quaternion quaternion)
        {
            return (float)Math.Asin(-2 * (quaternion.X * quaternion.Z - quaternion.W * quaternion.Y));
        }

        /// <summary>
        /// get the roll of a quaternion variable
        /// </summary>
        /// <param name="quaternion">quaternion to take the roll from</param>
        /// <returns>roll value</returns>
        public static float GetRoll(this Quaternion quaternion)
        {
            return (float)Math.Atan2(2 * (quaternion.X * quaternion.Y + quaternion.W * quaternion.Z),
                quaternion.W * quaternion.W + quaternion.X * quaternion.X - quaternion.Y * quaternion.Y - quaternion.Z * quaternion.Z);
        }

        /// <summary>
        /// create a ray based on 2 positions
        /// </summary>
        /// <param name="first_position">base position</param>
        /// <param name="second_position">goto position</param>
        /// <returns>ray based on the input parameters</returns>
        public static Ray CalculateRay(Vector3 first_position, Vector3 second_position)
        {
            Vector3 direction = second_position - first_position;
            direction.Normalize();

            // and then create a new ray using nearPoint as the source.
            return new Ray(first_position, direction);
        }

        /// <summary>
        /// Generate a direction vector based on a rotation
        /// </summary>
        /// <param name="pitch">pith value</param>
        /// <param name="yaw">yaw value</param>
        /// <param name="roll">roll value</param>
        /// <returns>generated direction vector</returns>
        public static Vector3 GetDirectionFromRotation(float pitch, float yaw, float roll)
        {
            return GetDirectionFromRotation(Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll));
        }

        /// <summary>
        /// Generate a direction vector based on a rotation
        /// </summary>
        /// <param name="quaternion">rotation</param>
        /// <returns>generated direction vector</returns>
        public static Vector3 GetDirectionFromRotation(Quaternion quaternion)
        {
            Matrix dirMatrix = Matrix.CreateTranslation(new Vector3(0, 1, 0)) * Matrix.CreateFromQuaternion(quaternion);
            Vector3 dir = dirMatrix.Translation;
            dir.Normalize();
            return dir;
        }
    }
}