using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 10f;

    public Rigidbody2D rb2D;

    private Vector2 move;

    [Header("UI Setup")]
    public TextMeshProUGUI xPositionTMP;
    public TextMeshProUGUI yPositionTMP;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        xPositionTMP.text = transform.position.x.ToString("0.00");
        yPositionTMP.text = transform.position.y.ToString("0.00");
    }

    private void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}
