using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public Vector3 SetPoint { get; private set; }

    // �Ű������� setPoint�� ���޹޴´�.
    public void SetPosition(Vector3 setPoint)
    {
        // SetPoint�� private set�̹Ƿ� �̰����� �����ϰ�
        // ���� �ٿ��� ���������� ��ġ�� �����Ѵ�.
        SetPoint = setPoint;
        transform.position = SetPoint;
    }
}
