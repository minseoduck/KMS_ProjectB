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
        // 이동 처리
        Move(inputManager.MoveInput);
    }

    private void Move(Vector3 moveInput)
    {
        // 목표 위치 계산
        Vector3 targetPosition = rb.position + moveInput * moveSpeed * Time.fixedDeltaTime;

        // Rigidbody의 MovePosition으로 이동
        rb.MovePosition(targetPosition);
    }

    void Update()
    {
        // 회전 
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
