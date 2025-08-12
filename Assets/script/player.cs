using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 5f;
    [Header("Jump")]
    public float jumpForce = 8f;
    public float groundCheckDistance = 0.1f;
    public LayerMask groundLayer;
    public Transform groundCheck;

    private FezCameraController cameraController;
    public Rigidbody rb;
    private bool isGrounded;
    public Animator animator;

    void Start()
    {
        // ��ȡ�����������ȷ�����������FezCameraController�ű���
        cameraController = Camera.main.GetComponent<FezCameraController>();
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
    }
    void HandleMovement()
    {
        if (cameraController == null) return;

        // ���ݵ�ǰ�ӽǷ�������ƶ���
        Vector3 moveDirection = GetMovementDirection();

        if (moveDirection.magnitude > 0.1f)
        {
            transform.Translate(-moveDirection.normalized * speed * Time.deltaTime, Space.World);
        }
    }
    void HandleJump()
    {
        // �����⣨�ӽŲ����·������ߣ�
        isGrounded = Physics.Raycast(
            groundCheck.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer

        );

        //Debug.Log(isGrounded);

        // ��Ծ���루�ո����
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(rb.velocity);
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            print("111");
        }
    }


    Vector3 GetMovementDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        
        // ���ݵ�ǰ�ӽǷ��򷵻��ƶ������������������⣩
        switch (cameraController.currentViewIndex)
        {
            case 0: // ǰ�ӽǣ�A=��D=��
                return new Vector3(horizontalInput, 0, 0);
            case 1: // ���ӽǣ�A=Զ���������Z����D=�����������Z��
                return new Vector3(0, 0, horizontalInput);
            case 2: // ���ӽǣ�A=�ң�D=����Ҫ��ת��
                return new Vector3(-horizontalInput, 0, 0);
            case 3: // ���ӽǣ�A=�����������Z����D=Զ���������Z��
                return new Vector3(0, 0, -horizontalInput);
            default:
                return Vector3.zero;
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundCheckDistance);
    }
}
