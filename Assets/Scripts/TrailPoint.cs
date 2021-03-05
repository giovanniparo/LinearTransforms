using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TrailPoint : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private float lineWidth;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, transform.position);
        }

        Destroy(this.gameObject, 3.0f);
    }
}