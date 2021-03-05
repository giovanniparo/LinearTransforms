using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InitPoint : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField] private float lineWidth;

    public TextMeshProUGUI positionText;
    public Transform bg;
    public Vector3 textOffset;

    private void Start()
    {
        positionText = GetComponentInChildren<TextMeshProUGUI>();
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer != null)
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, transform.position);
        }
        bg.position = Camera.main.WorldToScreenPoint(transform.position - textOffset);
        positionText.text = "(" + transform.position.x.ToString("F2") + "," +
                              transform.position.y.ToString("F2") + "," +
                              transform.position.z.ToString("F2") + ")";

        Destroy(this.gameObject, 6.0f);
    }
}
