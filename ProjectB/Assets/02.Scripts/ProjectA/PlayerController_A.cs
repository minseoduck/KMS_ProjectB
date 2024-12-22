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
   
    //�������̰� ª�� �ݺ������� ����Ǿ���ϱ⶧�� 
    // �׷��� �ٸ� ������Ʈ ������ �����Ȥ�����
    //������ ���ϰ� �߻��ص� �����ӿ�  �ð��� ����ġ�� ���� ����Ǿ� �̵��ӵ� ���� 
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
