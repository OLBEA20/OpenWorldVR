using System.Collections;
using System.Collections.Generic;
using Assets.Script.Src;
using UnityEngine;

public class DefaultGrabbable : IGrabbable {
    public void Grab(FixedJoint fixedJoint)
    {
        return;
    }

    public void Throw(FixedJoint fixedJoint, Vector3 velocity, Vector3 angularVelocity)
    {

    }
}
