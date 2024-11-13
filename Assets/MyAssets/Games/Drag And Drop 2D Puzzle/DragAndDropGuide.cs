using UnityEngine;

public class DragAndDropGuide : MonoBehaviour
{
	public Transform placement;

	private Vector2 initialPosition;

	private float deltaX, deltaY;

	public bool locked;

	public static float i;

	public Color colorSetting = new Color (255, 255, 0, 255);

	private SpriteRenderer placementSprite;

    public GameObject effects;
    public GameObject[] toOpen;
    public GameObject[] toClose;

    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
	{
		initialPosition = transform.position;
		placementSprite = placement.GetComponent<SpriteRenderer>();
	}

	private void Update()
	{
		if (Input.touchCount > 0 && !locked)
		{
			Touch touch = Input.GetTouch(0);
			Vector2 touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
			Debug.Log(touchPosition);

			switch (touch.phase)
			{
				case TouchPhase.Began:
					if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPosition))
					{
						deltaX = touchPosition.x - transform.position.x;
						deltaY = touchPosition.y - transform.position.y;
					}
					break;

				case TouchPhase.Moved:
					if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPosition))
					{
						transform.position = new Vector2(touchPosition.x - deltaX, touchPosition.y - deltaY);
					}
					break;
				case TouchPhase.Stationary:
					break;
				case TouchPhase.Ended:
					if (Mathf.Abs(transform.position.x - placement.position.x) <= 0.5f &&
						Mathf.Abs(transform.position.y - placement.position.y) <= 0.5f )
					{
						transform.position = new Vector2(placement.position.x, placement.position.y);
						locked = true;
						placementSprite.color = colorSetting;
                        effects.SetActive(true);

                        audioSource.clip = audioClip;
                        audioSource.Play();

                        foreach (var item1 in toOpen)
                        {
                            item1.SetActive(true);
                        }
                        foreach (GameObject item in toClose)
                        {
                            item.SetActive(false);
                        }
					}
					else
					{
					    transform.position = new Vector2(initialPosition.x, initialPosition.y);
					}
					break;
				case TouchPhase.Canceled:
					break;
				default:
					break;
			}
		}
	}
}
