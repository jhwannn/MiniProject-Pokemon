using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rigidbody2D;
    float playerSize = 0.0f;
    public Transform transform_W;
    public Transform transform_A;
    public Transform transform_S;
    public Transform transform_D;

    public Transform nowDir;

    public float moveDelay = 0.5f;
    public bool canMove = true;
    public bool nowMove = false;

    public float moveTime = 0.0f;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        Debug.Log("SIZE : " + transform.localScale.x + " : " + transform.localScale.y);
    }

    void Update()
    {
        //UpdateState();
        if (!nowMove) {
            if (Input.GetKeyDown(KeyCode.W))
            {
                transform.position = transform_W.position; nowMove = true; nowDir = transform_W;
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                transform.position = transform_S.position; nowMove = true; nowDir = transform_S;
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                transform.position = transform_A.position; nowMove = true; nowDir = transform_A;
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                transform.position = transform_D.position; nowMove = true; nowDir = transform_D;
            }
            //StartCoroutine(LockMove());
        }
        if(nowMove)
        {
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
            {
                nowMove = false;
                return;
            }
            moveTime += Time.deltaTime;
            if (moveTime >= moveDelay)
            {
                moveTime = 0;
                transform.position = nowDir.position;
            }

        }

    }
    


    IEnumerator StopMove()
    {

        yield return null;
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