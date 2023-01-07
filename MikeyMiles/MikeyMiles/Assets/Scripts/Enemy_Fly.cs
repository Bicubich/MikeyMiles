using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Fly : MonoBehaviour
{

    [Header("Ground Settings")]
    public bool isGrounded;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;


    private void Start()
    {

    }

    private void Update()
    {

        CheckingGround();
    }

    void CheckingGround()
    {
        isGrounded = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
    }   
}
