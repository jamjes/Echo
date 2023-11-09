using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewindHandler : MonoBehaviour
{
    public List<RewindPoint> rewindablePoints = new List<RewindPoint>();
    public bool run = true;
    public int maxSize;
    public float frameRate;
    public float timerRef = 0;

    private void Start()
    {
        frameRate = 1 / frameRate;
    }

    private void Update()
    {
        if (run)
        {
            timerRef += Time.deltaTime;
            if (timerRef >= frameRate)
            {
                RewindPoint newPoint = new RewindPoint();
                newPoint.position = transform.position;

                if (rewindablePoints.Count == maxSize)
                {
                    rewindablePoints.RemoveAt(0);
                }

                rewindablePoints.Add(newPoint);
                timerRef = 0;
            }
        }
    }
}
