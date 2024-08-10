using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [Header("Setting")]
    public Vector3[] movePositions;
    [Space]
    public float speed = 5f;

    public bool RandomPosition = false;

    public Vector3 nextPosition;

    private int moveIndex = 0;

    private const string playerTag = "Player";

    private void Start()
    {
        nextPosition = movePositions[moveIndex];
    }

    private void Update()
    {
        if (transform.position == nextPosition)
        {
            if (moveIndex == movePositions.Length - 1)
            {
                moveIndex = 0;
            }
            else
            {
                if (RandomPosition == true)
                {
                    moveIndex = Random.Range(0, movePositions.Length);
                }
                else
                {
                    moveIndex++;
                }
            }

            nextPosition = movePositions[moveIndex];
        }

        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);

        Debug.Log(nextPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        for (int  i = 0;  i < movePositions.Length;  i++)
        {
            for (int j = 0; j < i; j++)
            {
                Gizmos.DrawLine(movePositions[i], movePositions[j]);
            }

            Gizmos.DrawSphere(movePositions[i], 0.35f);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            other.transform.parent = transform;
        }
    }


    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag(playerTag))
        {
            other.transform.parent = null;
        }
    }
}
