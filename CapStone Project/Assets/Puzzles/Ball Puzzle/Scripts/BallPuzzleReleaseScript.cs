using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallPuzzleReleaseScript : PuzzleComponent
{
    [SerializeField] private bool pressed = false;
    [SerializeField] private GameObject ball;
    public override bool Click(Vector3 a)
    {
        if (pressed)
        {
            return base.Click(a);
        }
        ball.GetComponent<BallPuzzleBallScript>().released = true;
        pressed = true;
        return base.Click(a);
    }

    private void Awake()
    {
        ResetScriptBallPuzzle.ResetPuzzle += Reset;
    }
    public void Reset()
    {
        pressed = false;
    }
}
