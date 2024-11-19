using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private float moveSpeed;
    public float MoveSpeed
    {
        // MoveSpeed 프로퍼티의 get과 set에서 자신을 참조하고 있음
        // 이로 인해 get이나 set과정에서 무한 루프가 일어나 overflow 발생
        // 프로퍼티를 단독으로 사용할 거였으면 get; private set; 까지만 선언해도 됬을 것
        //get => MoveSpeed
        //private set => MoveSpeed = value;

        // 프로퍼티를 위와 같이 사용하려면 원본이 되는 변수를 하나 두고
        // 원본 변수를 참조하는 것으로 활용해야 맞는것 같다.
        get => moveSpeed;
        private set => moveSpeed = value;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        MoveSpeed = 5f;
    }
}
