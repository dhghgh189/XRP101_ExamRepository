using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotater : MonoBehaviour
{
    private void Update()
    {
        // ������ ���̷� ���� ������ ������� �ʾ� 
        // ��ǻ�� ���ɿ� ���� ȸ���ӵ��� ���̰� �߻�
        //transform.Rotate(Vector3.up * GameManager.Intance.Score);

        // deltaTime�� ���Ͽ� ������ ���̸� �����������ν� ���ɰ� �����ϰ� ������ �ӵ��� ȸ��
        transform.Rotate(Vector3.up * GameManager.Intance.Score * Time.deltaTime);
    }
}
