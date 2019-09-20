using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCR_GridBase : SCR_PuzzleComponent
{
    public int maxX;
    public int maxY;
    [SerializeField] private int currentX;
    [SerializeField] private int currentY;
    [SerializeField] private int correctX;
    [SerializeField] private int correctY;
    [SerializeField] private GameObject node;
    private Vector3 newPos;

    private void Awake()
    {
        newPos = node.transform.localPosition;
    }

    // Start is called before the first frame update
    public void MoveLeft()
    {
        if (currentX == maxX)
        {
            return;
        }
        currentX += 1;
        newPos.x += 1;
        Check();
    }
    public void MoveRight()
    {
        if (currentX == 0)
        {
            return;
        }
        currentX -= 1;
        newPos.x -= 1;
        Check();
    }
    public void MoveUp()
    {
        if (currentY == maxY)
        {
            return;
        }
        currentY += 1;
        newPos.y += 1;
        Check();
    }
    public void MoveDown()
    {
        if (currentY == 0)
        {
            return;
        }
        currentY -= 1;
        newPos.y -= 1;
        Check();
    }

    private void Update()
    {
        node.transform.localPosition =
            Vector3.Lerp(node.transform.localPosition, newPos, Time.deltaTime);
    }

    private void Check()
    {
        if (currentX == correctX && currentY == correctY)
        {
            complete = true;
        }
    }
}
