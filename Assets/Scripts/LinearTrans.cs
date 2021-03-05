using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LinearTrans
{
    public static void ApplyTransform(GameObject point, float[,] transMat)
    {
        if (transMat != null)
        {
            Vector3 result = new Vector3();
            result.x = transMat[0, 0] * point.transform.position.x +
                        transMat[0, 1] * point.transform.position.y +
                        transMat[0, 2] * point.transform.position.z;
            result.y = transMat[1, 0] * point.transform.position.x +
                        transMat[1, 1] * point.transform.position.y +
                        transMat[1, 2] * point.transform.position.z;
            result.z = transMat[2, 0] * point.transform.position.x +
                        transMat[2, 1] * point.transform.position.y +
                        transMat[2, 2] * point.transform.position.z;

            point.transform.position = result;
        }
        else
            Debug.Log("Error Applying Transformation:Matrix not defined!");
    }

    public static float[,] Rot3D(GameObject point, int axis, float angle)
    {
        float[,] transMat;
        angle *= Mathf.Deg2Rad;
        switch (axis)
        {
            case 0: //X
                transMat = new float[3, 3] { { 1.0f, 0.0f, 0.0f},
                                             { 0.0f, Mathf.Cos(angle), (-1) * Mathf.Sin(angle)},
                                             { 0.0f, Mathf.Sin(angle), Mathf.Cos(angle)} };
                break;
            case 1: //Y
                transMat = new float[3, 3] { { Mathf.Cos(angle), 0.0f, (-1) * Mathf.Sin(angle)},
                                             { 0.0f, 1.0f, 0.0f},
                                             { Mathf.Sin(angle), 0.0f, Mathf.Cos(angle)} };
                break;
            case 2: //Z
                transMat = new float[3, 3] { { Mathf.Cos(angle), (-1) * Mathf.Sin(angle), 0.0f},
                                             { Mathf.Sin(angle), Mathf.Cos(angle), 0.0f},
                                             { 0.0f, 0.0f, 1.0f} };
                break;
            default: //ND
                transMat = new float[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                Debug.Log("axis not defined");
                break;
        }

        ApplyTransform(point, transMat);
        return transMat;
    }

    public static float[,] Mirror3D(GameObject point, int plane)
    {
        float[,] transMat;
        switch (plane)
        {
            case 0: //XY
                transMat = new float[3, 3] { { 1.0f, 0.0f, 0.0f},
                                             { 0.0f, 1.0f, 0.0f},
                                             { 0.0f, 0.0f, -1.0f} };
                break;
            case 1: //XZ
                transMat = new float[3, 3] { { 1.0f, 0.0f, 0.0f},
                                             { 0.0f, -1.0f, 0.0f},
                                             { 0.0f, 0.0f, 1.0f} };
                break;
            case 2: //YZ
                transMat = new float[3, 3] { { -1.0f, 0.0f, 0.0f},
                                             { 0.0f, 1.0f, 0.0f},
                                             { 0.0f, 0.0f, 1.0f} };
                break;
            default: //ND
                transMat = new float[3, 3] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } };
                Debug.Log("plane not defined");
                break;
        }

        ApplyTransform(point, transMat);
        return transMat;
    }
}
