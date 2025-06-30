using UnityEngine;
using System.Collections;


    public static class Utilities
    {

        public static Vector3 FlattenedDir3D(Vector3 vectorA)
        {
            return (new Vector3(vectorA.x, 0, vectorA.z)).normalized;
        }

        //gets the signed angle (ie +/-)
        public static float GetSignedAngle(Vector3 prevDir, Vector3 currDir)
        {
            return Mathf.Sign(Vector3.Cross(prevDir, currDir).y) * Vector3.Angle(prevDir, currDir);
        }

    }
