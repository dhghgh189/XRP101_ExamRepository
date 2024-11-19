# 2번 문제

주어진 프로젝트 내에서 제공된 스크립트(클래스)는 다음의 책임을 가진다
- PlayerStatus : 플레이어 캐릭터가 가지는 기본 능력치를 보관하고, 객체 생성 시 초기화한다
- PlayerMovement : 유저의 입력을 받고 플레이어 캐릭터를 이동시킨다.

프로젝트에는 현재 2가지 문제가 있다.
- 유니티 에디터에서 Run 실행 시 윈도우에서는 Stack Overflow가 발생하고, MacOS의 유니티에서는 에디터가 강제종료된다.
- 플레이어 캐릭터가 X, Z축의 입력을 동시입력 받아서 대각선으로 이동 시 하나의 축 기준으로 움직일 때 보다 약 1.414배 빠르다.

두 가지 문제가 발생한 원인과 해결 방법을 모두 서술하시오.

## 답안

### 게임 시작 시 Overflow 되는 문제
- PlayerStatus에서 MoveSpeed 프로퍼티를 통해 get, set 할 때 자신을 참조하여 무한 루프가 일어나는 것으로 확인하였다.
- 원본 변수를 하나 만들어서 해당 변수를 참조하는 것으로 수정하여 문제를 해결했다.
- (또는 MoveSpeed의 get과 set을 default 형태로 선언하고 단독 사용해도 될 것 같다)

  `PlayerStatus`
  ```cs
    // 원본으로 활용할 변수
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
  ```

### 한 축 이동과 대각선 이동 시의 차이 해결
- PlayerMovement 에서 입력값을 받아 이동을 진행할 때 방향 벡터에 대한 정규화가 고려되지 않은 것으로 확인하였다.
- direction 벡터를 정규화하여 크기를 1로 만들어 주는 것으로 이동 차이를 해결하였다.
  
  `PlayerMovement`
  ```cs
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
  ```
