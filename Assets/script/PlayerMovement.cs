using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum Direction
{
    up, down, left, right
}

public class PlayerMovement : MonoBehaviour
{

    public float Speed = 0f;
    public LayerMask TileCollision;

    Animator Anim;
    Vector3 TargetPosition;
    Direction Direction;

    bool GetCollision
    {
        get
        {
            RaycastHit2D rh;

            Vector2 dir = Vector2.zero;

            if (Direction == Direction.down)
                dir = Vector2.down;

            if (Direction == Direction.left)
                dir = Vector2.left;

            if (Direction == Direction.right)
                dir = Vector2.right;

            if (Direction == Direction.up)
                dir = Vector2.up;

            rh = Physics2D.Raycast(transform.position, dir, 1, TileCollision);

            return rh.collider != null;
        }
    }


    private void Start()
    {
        Anim = GetComponent<Animator>();
        TargetPosition = new Vector2(transform.position.x, transform.position.y);
        Direction = Direction.down;

    }

    private void Update()
    {

        Vector2 AxisInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //Anim.SetInteger("Direccion", (int)Direction); //여기다가 애니메이션 넣으면 됩니다

        if (AxisInput != Vector2.zero && TargetPosition == transform.position)
        {
            if (Mathf.Abs(AxisInput.x) > Mathf.Abs(AxisInput.y))
            {

                if (AxisInput.x > 0)
                {
                    Direction = Direction.right;

                    if (!GetCollision)
                        TargetPosition += Vector3.right / 2;
                }
                else
                {
                    Direction = Direction.left;

                    if (!GetCollision)
                        TargetPosition += Vector3.left / 2;
                }



            }
            else
            {
                if (AxisInput.y > 0)
                {
                    Direction = Direction.up;

                    if (!GetCollision)
                        TargetPosition += Vector3.up/2;
                }
                else
                {
                    Direction = Direction.down;

                    if (!GetCollision)
                        TargetPosition += Vector3.down / 2;
                }
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, TargetPosition, Speed * Time.deltaTime);
        }
/*        void UpdateState()
        {
            bool isMoving = movement != Vector2.zero;
            animator.SetBool("isMove", isMoving);
            animator.SetFloat("xDir", movement.x);
            animator.SetFloat("yDir", movement.y);
        }*/
}