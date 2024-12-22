using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager_B : MonoBehaviour
{
    public Vector3 MoveInput { get; private set; }
    public Vector3 MouseWorldPosition { get; private set; }
    

    void Update()
    {
        // WASD �̵� �Է�
        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical"); 
        MoveInput = new Vector3(horizontal, 0, vertical).normalized;

        // ���콺 ��ġ
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            MouseWorldPosition = hit.point;
        }

    }
}
