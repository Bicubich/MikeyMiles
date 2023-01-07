using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] float speed = 0.1f;
    [SerializeField] float powerJump = 0.1f;

    public SpriteRenderer SR;

    private void Update()
    {
        float movement = Input.GetAxis("Horizontal");
        float scaleJump = Input.GetAxis("Vertical");

        transform.position += new Vector3(movement, 0, 0) * speed * Time.deltaTime;
        transform.position += new Vector3(0, scaleJump, 0) * powerJump * Time.deltaTime;

        FlipX(movement);
    }

    private void FlipX(float movement)
    {
        if (movement > 0) SR.flipX = false;
        if (movement < 0) SR.flipX = true;
    }
}
