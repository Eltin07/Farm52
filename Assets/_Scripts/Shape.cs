using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Shape : MonoBehaviour
{
    private Shape_Base ShapeSpecs;

    public GameObject ShapeSquare;

    void Start()
    {
        ShapeSpecs = Systems.Instance.ResourceSystem.GetRandomShape();
        SetShapeImageColors();
    }

    private void SetShapeImageColors()
    {
        int NumOfSquares = ShapeSpecs.width * ShapeSpecs.height;

        Vector2 checkPos = new Vector2(0,0);
        Debug.Log(checkPos);
        for(int i = 0; i < NumOfSquares; i++)
        {
            Image image = transform.GetChild(i).GetComponent<Image>();
            int index = Mathf.Min(i, ShapeSpecs.SquarePositions.Count - 1);
            if (checkPos == ShapeSpecs.SquarePositions[index])
            {

            }
            else
            {
                var c = image.color;
                c.a = 0.0f;
                image.color = c;
            }

            checkPos.x++;

            if (checkPos.x == ShapeSpecs.width)
            {
                checkPos.x = 0;
                checkPos.y++;
            }
        }
    }
}
