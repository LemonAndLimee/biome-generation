using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareGeneration : MonoBehaviour
{
    public GameObject squarePrefab;
    public int xNum;
    public int yNum;
    public float interval;

    public GameObject[,] squares;

    // Start is called before the first frame update
    void Start()
    {
        squares = new GameObject[xNum, yNum];

        float xLim = ((xNum / 2) * interval) - (0.5f * interval);
        float yLim = ((yNum / 2) * interval) - (0.5f * interval);

        int xIndex = 0;
        int yIndex = 0;

        for (float x = -xLim; x <= xLim; x += interval)
        {
            yIndex = 0;
            for (float y = -yLim; y <= yLim; y+= interval)
            {
                GameObject currentObject = Instantiate(squarePrefab);
                currentObject.transform.position = new Vector3(x, y);
                squares[xIndex, yIndex] = currentObject;
                yIndex++;
            }
            xIndex++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
