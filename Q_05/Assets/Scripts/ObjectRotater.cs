using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotater : MonoBehaviour
{
    private void Update()
    {
        // 프레임 차이로 인한 보정이 고려되지 않아 
        // 컴퓨터 성능에 따른 회전속도의 차이가 발생
        //transform.Rotate(Vector3.up * GameManager.Intance.Score);

        // deltaTime을 곱하여 프레임 차이를 보정해줌으로써 성능과 무관하게 동일한 속도로 회전
        transform.Rotate(Vector3.up * GameManager.Intance.Score * Time.deltaTime);
    }
}
