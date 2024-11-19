using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerStatus _status;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _status = GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        MovePosition();
    }

    private void MovePosition()
    {
        Vector3 direction = Vector3.zero;
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.z = Input.GetAxisRaw("Vertical");

        if (direction == Vector3.zero) return;

        // 이동 할 때 방향 벡터에 대한 정규화가 고려되지 않았음
        // 이 경우 대각선 이동 시 direction 벡터의 크기가 1이 아니게 되며
        // 결과적으로 한축으로 이동 할 때와는 이동량이 달라져버린다.
        //transform.Translate(_status.MoveSpeed * Time.deltaTime * direction);

        // 균일한 움직임을 위해서는 direction 벡터를 정규화하여 크기를 1로 만들어줄 필요가 있다.
        transform.Translate(_status.MoveSpeed * Time.deltaTime * direction.normalized);
    }
}
