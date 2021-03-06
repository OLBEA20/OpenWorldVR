﻿using UnityEngine;

namespace Assets.Script.Src.Interaction.Grab
{
    public interface IGrabbable
    {
        void Grab(FixedJoint fixedJoint);
        void Throw(FixedJoint fixedJoint, Vector3 velocity, Vector3 angularVelocity);
    }
}
