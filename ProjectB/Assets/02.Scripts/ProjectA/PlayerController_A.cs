using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class PlayerController_A : MonoBehaviour
{
    Vector3 velocity;
    Rigidbody myRigidbody;
   
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }
   
    //정기적이고 짧게 반복적으로 실행되어야하기때문 
    // 그래야 다른 오브젝트 밑으로 들어가지안ㅅㅎ음
    //프레임 저하가 발생해도 프레임에  시간의 가중치를 곱해 실행되어 이동속도 유지 
    private void FixedUpdate()
    {
        myRigidbody.MovePosition(myRigidbody.position + velocity * Time.fixedDeltaTime);
    }
    public void LookAt(Vector3 lookPoint)
    {
        Vector3 heightCorretedPoint = new Vector3(lookPoint.x, transform.position.y, lookPoint.z);
        transform.LookAt(heightCorretedPoint);
    }




}
