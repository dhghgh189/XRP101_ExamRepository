using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    private CubeController _cubeController;
    private Vector3 _cubeSetPoint;

    private void Awake()
    {
        // 아직 큐브가 생성되지 않은 시점에 위치 설정을 시도하면
        // cubeController instance가 할당되지 않아 null 참조 에러가 발생한다.
        //SetCubePosition(3, 0, 3);

        // 큐브의 생성이 먼저 이뤄져야 한다.
        CreateCube();
    }

    private void Start()
    {
        // 큐브의 생성은 Awake에서 먼저 진행
        //CreateCube();

        // 큐브가 생성되어 cubeController의 instance가 할당 된 후에 위치 설정을 해야 한다.
        SetCubePosition(3, 0, 3);
    }

    private void SetCubePosition(float x, float y, float z)
    {
        _cubeSetPoint.x = x;
        _cubeSetPoint.y = y;
        _cubeSetPoint.z = z;

        // 기존 상태라면 controller의 참조를 통해 SetPosition을 호출해도
        // controller의 SetPoint에 변화가 없기 때문에 위치 변화가 없다.
        //_cubeController.SetPosition();

        // 따라서 위에서 설정한 cubeSetPoint 값을 인자로 넘겨줄 수 있도록 한다.
        _cubeController.SetPosition(_cubeSetPoint);
    }

    private void CreateCube()
    {
        GameObject cube = Instantiate(_cubePrefab);
        _cubeController = cube.GetComponent<CubeController>();

        // _cubeSetPoint는 Vector3 자료형이며 이는 값 형식이다.
        // 따라서 _cubeController의 SetPoint를 넘겨줘도 의미가 없다.
        //_cubeSetPoint = _cubeController.SetPoint;
    }
}
