using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public Transform placement;

    private Vector2 initialPosition;

    private float deltaX, deltaY;

    public bool locked;

    public static float i;

    public Color colorSetting = new Color(255, 255, 0, 255);

    private SpriteRenderer placementSprite;

    public GameObject effects;

    public GameControl gameControl;
    private void Start()
    {
        initialPosition = transform.position;
        placementSprite = placement.GetComponent<SpriteRenderer>();

    }


    private void Update()
    {
        if (Input.touchCount == 0 || locked) return; // Early exit for performance

        Touch touch = Input.GetTouch(0);
        Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);

        // Cache the Collider2D to avoid repeated GetComponent calls
        Collider2D collider = GetComponent<Collider2D>();

        if (collider == null) return; // Safety check

        switch (touch.phase)
        {
            case TouchPhase.Began:
                // Check touch begins within the object's collider
                if (collider == Physics2D.OverlapPoint(touchPosition))
                {
                    deltaX = touchPosition.x - transform.position.x;
                    deltaY = touchPosition.y - transform.position.y;
                }
                break;

            case TouchPhase.Moved:
                // Only update position if touch is still within the collider
                if (collider == Physics2D.OverlapPoint(touchPosition))
                {
                    transform.position = new Vector2(touchPosition.x - deltaX, touchPosition.y - deltaY);
                }
                break;

            case TouchPhase.Ended:
                float distanceX = Mathf.Abs(transform.position.x - placement.position.x);
                float distanceY = Mathf.Abs(transform.position.y - placement.position.y);

                if (distanceX <= 0.5f && distanceY <= 0.5f)
                {
                    SnapToPlacement();
                }
                else
                {
                    ResetToInitialPosition();
                }
                break;
        }
    }

    private void SnapToPlacement()
    {
        transform.position = placement.position;
        GameControl.Instance.randomPuzzle();
        i++;
        locked = true;
        placementSprite.color = colorSetting;
        effects.SetActive(true);
        gameControl.PlayCorrectPlaceSound();

    }

    private void ResetToInitialPosition()
    {
        transform.position = initialPosition;
    }
}
