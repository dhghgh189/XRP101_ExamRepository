# 6번 문제

주어진 프로젝트는 다음의 기능을 구현하고자 생성한 프로젝트이다.

### FPS 조작 구현
- 실행 시, 마우스 커서가 비활성화되며 마우스 회전으로 플레이어의 시야가 회전한다.
- 현재 바라보고 있는 방향 기준으로 W, A, S, D로 전, 후, 좌, 우 이동을 수행한다
- 마우스 좌클릭 시, 시야 정면 방향으로 레이를 발사하고 레이캐스트에 검출된 적의 이름을 콘솔에 로그로 출력한다.

위 기능들을 구현하고자 할 때
제시된 프로젝트에서 발생하는 `문제들을 모두 서술`하고 올바르게 동작하도록 `소스코드를 개선`하시오.

## 답안

### CameraController 수정
- followTarget의 position, rotation이 카메라의 position, rotation으로 잘못 설정되고 있는 문제 발생
  - 카메라의 position, rotation을 followTarget의 position, rotation으로 설정하도록 수정하여 해결
  ```cs
    private void SetTransform()
    {
        if (!_hasFollowTarget) return;

        // followTarget의 transform을 자신의 transform으로 설정하는게 아니라
        // 자신의 Transform을 target의 transform으로 설정해야 한다.
        //_followTarget.SetPositionAndRotation(
        //    transform.position,
        //    transform.rotation
        //    );

        // 자신의 Transform을 target의 transform으로 설정
        transform.SetPositionAndRotation(
            _followTarget.position,
            _followTarget.rotation
            );
    }
  ```

### Gun 수정
- 씬에 있는 플레이어 캐릭터 오브젝트의 Gun 컴포넌트에서 Target Layer를 Enemy로 설정하여 Raycast 가능하도록 수정   

- 총 발사 시 자신이 바라보는 방향이 아닌 월드기준 정면 방향으로 발사하는 문제 발생
  - Fire 함수에서 Ray를 만들 때 Vector3.forward가 아닌 origin.forward로 만들도록 수정하여 해결
  ```cs
    public void Fire(Transform origin)
    {
        // Vector3.forward는 월드 기준 정면이다.
        //Ray ray = new(origin.position, Vector3.forward);
        
        // 바라보는 방향을 기준으로 하고싶으면 transform.forward를 사용해야 한다.
        Ray ray = new(origin.position, origin.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * _range, Color.red);
        if (Physics.Raycast(ray, out hit, _range, _targetLayer))
        {
            Debug.Log($"{hit.transform.name} Hit!!");
        }
    }
  ```