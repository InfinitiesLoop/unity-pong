using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleBehavior : MonoBehaviour
{
    private float minX, maxX;
    private float minY, maxY;
    private float gameObjectExtentsHeight;
    private float gameObjectExtentsWidth;
    private float gameHeight, gameWidth;

    public enum PaddleMovementMode
    {
        Normal = 0,
        Inverse = 1,
        Rotated = 2
    }

    public PaddleMovementMode Mode;

    void Start()
    {
        var rect = Camera.main.pixelRect;
        minX = rect.xMin;
        maxX = rect.xMax;
        minY = rect.yMin;
        maxY = rect.yMax;

        minX = Camera.main.ScreenToWorldPoint(new Vector3(minX, 0, 0)).x;
        maxX = Camera.main.ScreenToWorldPoint(new Vector3(maxX, 0, 0)).x;
        minY = Camera.main.ScreenToWorldPoint(new Vector3(0, minY, 0)).y;
        maxY = Camera.main.ScreenToWorldPoint(new Vector3(0, maxY, 0)).y;

        gameHeight = Camera.main.ScreenToWorldPoint(new Vector3(0, rect.height, 0)).y;
        gameWidth = Camera.main.ScreenToWorldPoint(new Vector3(rect.width, 0, 0)).x;

        var extents = GetComponent<Renderer>().bounds.extents;
        gameObjectExtentsHeight = extents.y;
        gameObjectExtentsWidth = extents.x;
    }

    void Update()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var currentPos = transform.position;
        var newY = currentPos.y;
        var newX = currentPos.x;

        switch(Mode)
        {
            case PaddleMovementMode.Normal:
                newY = mousePos.y;
                break;
            case PaddleMovementMode.Inverse:
                newY = -mousePos.y;
                break;
            case PaddleMovementMode.Rotated:
                newY = mousePos.x;
                break;
        }

        var newYBottom = newY - gameObjectExtentsHeight;
        var newYTop = newY + gameObjectExtentsHeight;

        if (newYBottom < minY)
        {
            newY = minY + gameObjectExtentsHeight;
        }
        else if (newYTop > maxY)
        {
            newY = maxY - gameObjectExtentsHeight;
        }

        transform.position = new Vector3(newX, newY, currentPos.z);
    }
}
