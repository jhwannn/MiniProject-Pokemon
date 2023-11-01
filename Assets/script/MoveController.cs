using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MoveController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rigidbody2D;
    public Vector3 Zv;
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        UpdateState();
        Zv.x = this.transform.position.x;
        Zv.y = this.transform.position.y;
        Zv.z = -10f;
        Camera.main.gameObject.transform.position = Zv;
    }

    void FixedUpdate()
    {
        MoveCharacter();

    }

    void MoveCharacter()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        // 대각선 이동을 비활성화합니다.
        if (horizontalInput != 0 && verticalInput != 0)
        {
            horizontalInput = 0;
            verticalInput = 0;
        }

        movement.x = horizontalInput;
        movement.y = verticalInput;
        movement.Normalize();

        rigidbody2D.velocity = movement * movementSpeed;
    }

    void UpdateState()
    {
        bool isMoving = movement != Vector2.zero;
        animator.SetBool("isMove", isMoving);
        animator.SetFloat("xDir", movement.x);
        animator.SetFloat("yDir", movement.y);
    }
}