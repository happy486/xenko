﻿// Copyright (c) 2014 Silicon Studio Corp. (http://siliconstudio.co.jp)
// This file is distributed under GPL v3. See LICENSE.md for details.

using SiliconStudio.Core;
using SiliconStudio.Core.Mathematics;

namespace SiliconStudio.Xenko.Particles.Modules
{
    [DataContract("GravityUpdater")]
    [Display("Gravity")]
    public class GravityUpdater : UpdaterBase
    {
        /// <summary>
        /// Direction and magnitude of the gravitational acceleration
        /// </summary>
        [DataMember(10)]
        public Vector3 GravitationalAcceleration;

        public GravityUpdater()
        {
            // In case of a conventional standard Earth gravitational acceleration, where Y is up. Change it depending on your needs.
            GravitationalAcceleration = new Vector3(0, -9.80665f, 0);

            RequiredFields.Add(ParticleFields.Position);
            RequiredFields.Add(ParticleFields.Velocity);
        }

        public override unsafe void Update(float dt, ParticlePool pool)
        {
            if (!pool.FieldExists(ParticleFields.Position) || !pool.FieldExists(ParticleFields.Velocity))
                return;

            var posField = pool.GetField(ParticleFields.Position);
            var velField = pool.GetField(ParticleFields.Velocity);

            var deltaVel = GravitationalAcceleration * dt;
            var deltaPos = deltaVel * (dt * 0.5f);

            foreach (var particle in pool)
            {
                (*((Vector3*)particle[posField])) += deltaPos;
                (*((Vector3*)particle[velField])) += deltaVel;
            }
        }

        public override void SetParentTRS(ref Vector3 Translation, ref Quaternion Rotation, float Scale) { }
    }
}
