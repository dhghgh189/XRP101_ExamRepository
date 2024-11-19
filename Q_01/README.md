# 1번 문제

주어진 프로젝트 내에서 CubeManager 객체는 다음의 책임을 가진다
- 해당 객체 생성 시 Cube프리팹의 인스턴스를 생성한다
- Cube 인스턴스의 컨트롤러를 참조해 위치를 변경한다.

제시된 소스코드에서는 큐브 인스턴스 생성 후 아래의 좌표로 이동시키는 것을 목표로 하였다
- x : 3
- y : 0
- z : 3

제시된 소스코드에서 문제가 발생하는 `원인을 모두 서술`하시오.

## 답안

### Null 참조
  - Awake에서 SetCubePosition 함수를 호출하며 해당 함수에서는 cubeController의 참조를 활용하는 동작이 존재하였다.
  - 하지만 Awake에서 SetCubePosition을 호출하는 시점에서는 아직 cubeController에 할당된 instance가 존재하지 않는다.
  - 이로 인해 Null 참조 에러가 발생했다.
  - cubeController의 instance는 CreateCube 함수 동작시 할당하므로 호출 순서를 변경하여 해결하였다.
  
    `CubeManager`
    ```cs
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
    ```

### 값 형식의 잘못된 사용
  - CreateCube 함수에서 큐브 생성후 controller의 instance를 가져온 다음, controller에 있는 SetPoint 프로퍼티를 cubeManager의 cubeSetPoint에 대입하였다.
  - 이후 SetCubePosition 에서 cubeSetPoint에 위치를 설정하고 controller instance를 통해 SetPosition 함수를 호출하였다.
  - CubeController의 SetPosition 함수에서는 SetPoint 프로퍼티 값을 통해 transform의 position값을 변경하려 한다.
  - 하지만 controller의 SetPoint와 cubeManager의 cubeSetPoint는 둘다 Vector3 형이며 이는 값 형식의 자료형이다.
  - 그러므로 controller의 SetPoint를 cubeManager의 cubeSetPoint에 대입해도 참조가 이어지지 않는다.
  - 그래서 SetCubePosition에서 아무리 값을 바꿔도 controller에 있는 SetPoint가 변경되는 일은 없다.
  - controller의 SetPosition 함수가 vector3 값을 전달받도록 수정하고 manager에서 값을 넘겨주도록 수정 후 해결하였다.
    
    `CubeManager`
    ```cs
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
    ```

    `CubeController`
    ```cs
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
    ```