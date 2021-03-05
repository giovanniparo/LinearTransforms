using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    [SerializeField] private float vectorWidth;
    [SerializeField] private GameObject trailPointPrefab;
    [SerializeField] private GameObject initPointPrefab;
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private int numSteps;
    [SerializeField] private TextMeshProUGUI transMatrixName;
    [SerializeField] private TextMeshProUGUI[] transMatrixElements;
    [SerializeField] private TextMeshProUGUI[] currentVectorElements;
    [SerializeField] private TextMeshProUGUI[] resultVectorElements;
    [SerializeField] private InputField[] vectorInputFields;
    [SerializeField] private GameObject[] planesPrefabs;
    
    private float rotationAngle = 0.0f;
    private GameObject currentPoint;
    float[,] transMat;
    float angleStep = 1.0f;

    private void Start()
    {
        transMatrixName.text = "";
        for (int n = 0; n < transMatrixElements.Length; n++)
            transMatrixElements[n].text = "0.00";
        for (int n = 0; n < vectorInputFields.Length; n++)
            vectorInputFields[n].text = "0.00";
        for (int n = 0; n < currentVectorElements.Length; n++)
            currentVectorElements[n].text = "0.00";
        for (int n = 0; n < resultVectorElements.Length; n++)
            resultVectorElements[n].text = "0.00";
    }

    private void PopMatrix(float[,] transMat, string name)
    {
        transMatrixName.text = name;
        if (transMat != null)
        {
            for (int n = 0; n < 3; n++)
            {
                for (int i = 0; i < 3; i++)
                {
                    transMatrixElements[i + 3 * n].text = transMat[n, i].ToString("F2");
                }
            }
        }
        else
            Debug.LogError("Tryied to Pop undefined matrix.");
    }

    private void PopVectorUI(TextMeshProUGUI[] uiVectorElements, Vector3 vectorPop)
    {
        for (int n = 0; n < uiVectorElements.Length && n < 3; n++)
        {
            uiVectorElements[n].text = vectorPop[n].ToString("F2");
        }
    }


    public void CreatePoint()
    {
        Instantiate(pointPrefab, new Vector3(float.Parse(vectorInputFields[0].text),
                                             float.Parse(vectorInputFields[1].text),
                                             float.Parse(vectorInputFields[2].text)),
                                             Quaternion.identity);
    }

    public void DeleteCurrentPoint()
    {
        if(currentPoint != null)
            Destroy(currentPoint);
    }

    public void ApplyRotation(int axis)
    {
        string axisName;
        switch(axis)
        {
            case 0:
                axisName = "X";
                rotationAngle = float.Parse(vectorInputFields[3].text);
                break;
            case 1:
                axisName = "Y";
                rotationAngle = float.Parse(vectorInputFields[4].text);
                break;
            case 2:
                axisName = "Z";
                rotationAngle = float.Parse(vectorInputFields[5].text);
                break;
            default:
                axisName = "NDF";
                break;
        }

        if (currentPoint != null)
        {
            Instantiate(initPointPrefab, currentPoint.transform.position, Quaternion.identity);
            GameObject dummyPoint = Instantiate(trailPointPrefab, currentPoint.transform.position, Quaternion.identity);
            angleStep = rotationAngle / numSteps;
            for(int n = 0; n < numSteps && n * angleStep < Mathf.Abs(rotationAngle); n++)
            {
                LinearTrans.Rot3D(dummyPoint, axis, angleStep);
                Instantiate(trailPointPrefab, dummyPoint.transform.position, Quaternion.identity);
            }

            PopVectorUI(currentVectorElements, (Vector3)currentPoint.transform.position);
            transMat = LinearTrans.Rot3D(currentPoint, axis, rotationAngle);
            PopVectorUI(resultVectorElements, (Vector3)currentPoint.transform.position);
            PopMatrix(transMat, "Rot" + axisName + " " + rotationAngle + "º");
        }
        else
            Debug.Log("Select a valid Point!");
    }

    public void ApplyMirror(int plane)
    {
        string planeName;
        GameObject planeObj;
        switch (plane)
        {
            case 0:
                planeName = "XY";
                planeObj = Instantiate(planesPrefabs[0], Vector3.zero, Quaternion.identity);
                Destroy(planeObj, 6.0f);
                break;
            case 1:
                planeName = "XZ";
                planeObj = Instantiate(planesPrefabs[1], Vector3.zero, Quaternion.identity);
                planeObj.transform.Rotate(new Vector3(90, 0, 0));
                Destroy(planeObj, 6.0f);
                break;
            case 2:
                planeName = "YZ";
                planeObj = Instantiate(planesPrefabs[2], Vector3.zero, Quaternion.identity);
                planeObj.transform.Rotate(new Vector3(0, 90, 0));
                Destroy(planeObj, 6.0f);
                break;
            default:
                planeName = "NDF";
                break;
        }

        if (currentPoint != null)
        {
            Instantiate(initPointPrefab, currentPoint.transform.position, Quaternion.identity);
            PopVectorUI(currentVectorElements, (Vector3)currentPoint.transform.position);
            transMat = LinearTrans.Mirror3D(currentPoint, plane);
            PopVectorUI(resultVectorElements, (Vector3)currentPoint.transform.position);
            PopMatrix(transMat, "Mirror " + planeName); 
        }
        else
            Debug.Log("Select a valid Point!");
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RaycastHit hit = new RaycastHit();
            if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit)
                && hit.collider.gameObject.CompareTag("Point"))
            {
                if (currentPoint != null)
                    currentPoint.GetComponent<Point>().isCurrent = false;

                currentPoint = hit.collider.gameObject;
                if(currentPoint != null)
                {
                    PopVectorUI(currentVectorElements, (Vector3)currentPoint.transform.position);
                    PopVectorUI(resultVectorElements, new Vector3(0.00f, 0.00f, 0.00f));
                    PopMatrix(new float[,] { { 0.00f, 0.00f, 0.00f }, { 0.00f, 0.00f, 0.00f }, { 0.00f, 0.00f, 0.00f } }, "");
                    currentPoint.GetComponent<Point>().isCurrent = true;
                    Debug.Log(currentPoint.transform.position);
                }
            }
        }
    }
}