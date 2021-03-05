using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Point : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private MeshRenderer meshRenderer;
    [SerializeField] private float lineWidth;
    public Color[] colors;

    public TextMeshProUGUI positionText;
    public Transform bg;
    public Vector3 textOffset;
    public bool isCurrent;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        meshRenderer = GetComponent<MeshRenderer>();
        if(lineRenderer != null)
        {
            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;
        }
    }

    void Update()
    {
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5.0f, 5.0f),
                                         Mathf.Clamp(transform.position.y, -5.0f, 5.0f),
                                         Mathf.Clamp(transform.position.z, -5.0f, 5.0f));
        if(isCurrent)
        {
            meshRenderer.material.SetColor("_Color", colors[1]);
            bg.gameObject.SetActive(true);
            positionText.gameObject.SetActive(true);
            bg.position = Camera.main.WorldToScreenPoint(transform.position - textOffset);
            positionText.text = "(" + transform.position.x.ToString("F2") + "," +
                                  transform.position.y.ToString("F2") + "," +
                                  transform.position.z.ToString("F2") + ")";
        }
        else
        {
            meshRenderer.material.SetColor("_Color", colors[0]);
            bg.gameObject.SetActive(false);
            positionText.gameObject.SetActive(false);
        }

        if(lineRenderer != null)
        {
            lineRenderer.SetPosition(0, Vector3.zero);
            lineRenderer.SetPosition(1, transform.position);
        }
    }
}
