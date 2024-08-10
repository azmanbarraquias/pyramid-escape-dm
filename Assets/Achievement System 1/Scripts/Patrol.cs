using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    #region Variables
    [Header("Setting")]
    public float speed;
    public float waitTime;
    public float startWaitTime;

    [Header("Move base on Spots")]
    public Transform[] moveSpots;
    private int randomSpot;

    [Space]
    public bool moveSpot = true;

    [Header("Move Min and Max")]
    public Transform centerPoint;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    #endregion Variables

    private void Start()
    {
        waitTime = startWaitTime;

        if (moveSpot.Equals(true))
        {
            randomSpot = Random.Range(0, moveSpots.Length);
        }
        else
        {
            centerPoint.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
    }

    private void Update()
    {
        if (moveSpot.Equals(true))
        {
            PatrolBaseOnSpots();
        }
        else
        {
            PatrolOnCenterMinMax();
        }
    }

    public void PatrolBaseOnSpots()
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);


        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }

    public void PatrolOnCenterMinMax()
    {
        this.transform.position = Vector2.MoveTowards(transform.position, centerPoint.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, centerPoint.position) < 0.2f)
        {
            if (waitTime <= 0)
            {
                // New location
                centerPoint.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}