using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeController : MonoBehaviour
{
    public Vector3 SetPoint { get; private set; }

    // 매개변수로 setPoint를 전달받는다.
    public void SetPosition(Vector3 setPoint)
    {
        // SetPoint는 private set이므로 이곳에서 설정하고
        // 다음 줄에서 최종적으로 위치를 변경한다.
        SetPoint = setPoint;
        transform.position = SetPoint;
    }
}
