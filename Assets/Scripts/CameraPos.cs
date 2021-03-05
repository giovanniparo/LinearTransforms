using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{

    void Start()
    {
        transform.LookAt(Vector3.zero, Vector3.forward) ;
    }
}
