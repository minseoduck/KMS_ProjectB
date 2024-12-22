using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(PlayerController_A))]
[RequireComponent(typeof(Gun_Controller_A))]
public class Player_A : LivingEntity
// 플레이어 움직임 입력 받자
{
    Camera viewCamera;
    public float moveSpeed = 5;
    PlayerController_A controller_A;
    Gun_Controller_A gun_Controller_A;

    protected override void Start()
    {
        base.Start();
        controller_A = GetComponent<PlayerController_A>();
        gun_Controller_A = GetComponent<Gun_Controller_A>();
        viewCamera = Camera.main;
    }
    void Update()
    {
      
        Vector3 moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        Vector3 moveVelocity = moveInput.normalized * moveSpeed;

        controller_A.Move(moveVelocity);

        Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray,out rayDistance))
        {
            Vector3 point = ray.GetPoint(rayDistance);
            Debug.DrawLine(ray.origin, point, Color.red);
            controller_A.LookAt(point);
        }

        if (Input.GetMouseButton(0))
        {
            gun_Controller_A.Shoot();
        }

    
    }
}
