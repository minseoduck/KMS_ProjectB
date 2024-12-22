using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_B : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
  

    [SerializeField]
    private InputManager_B inputManager;
    private Rigidbody rb;
    public Gun_Controller_B gun_Controller_B;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
      
    }

    void FixedUpdate()
    {
        // �̵� ó��
        Move(inputManager.MoveInput);
    }

    private void Move(Vector3 moveInput)
    {
        // ��ǥ ��ġ ���
        Vector3 targetPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;

        // Rigidbody�� MovePosition���� �̵�
        rb.MovePosition(targetPosition);
    }

    void Update()
    {
        // ȸ�� 
        RotateTowards(inputManager.MouseWorldPosition);
       
        if (Input.GetMouseButton(0))
        {
            gun_Controller_B.Shoot();
        }

    }

    void RotateTowards(Vector3 targetPosition)
    {
    
        Vector3 heightCorrectedPoint = new Vector3(targetPosition.x, transform.position.y, targetPosition.z);
        transform.LookAt(heightCorrectedPoint);
    }




}
