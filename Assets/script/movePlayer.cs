using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;  // ĳ������ �̵� �ӵ� ����

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // �Է��� �޾� ĳ���͸� �����Դϴ�.
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // Rigidbody2D�� ����Ͽ� ĳ���͸� �����Դϴ�.
        rb.velocity = moveDirection * moveSpeed;

        // ������ ���⿡ ���� ĳ���͸� ȸ����ŵ�ϴ�.
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
