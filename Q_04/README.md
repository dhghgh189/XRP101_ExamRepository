# 4번 문제

주어진 프로젝트는 다음의 기능을 구현하고자 생성한 프로젝트이다.

### 1. Player
- 상태 패턴을 사용해 Idle 상태와 Attack 상태를 관리한다.
- Idle상태에서 Q를 누르면 Attack 상태로 진입한다
  - 진입 시 2초 이후 지정된 구형 범위 내에 있는 데미지를 입을 수 있는 적을 탐색해 데미지를 부여하고 Idle상태로 돌아온다
- 상태 머신 : 각 상태들을 관리하는 객체이며, 가장 첫번째로 입력받은 상태를 기본 상태로 설정한다.

### 2. NormalMonster
- 데미지를 입을 수 있는 몬스터

### 3. ShieldeMonster
- 데미지를 입지 않는 몬스터

위 기능들을 구현하고자 할 때
제시된 프로젝트에서 발생하는 `문제들을 모두 서술`하고 올바르게 동작하도록 `소스코드를 개선`하시오.

## 답안

### StateAttack 개선
- Attack 시 감지된 오브젝트로부터 IDamagable을 참조하여 TakeHit 호출할 때 null 참조 에러가 발생
  - 감지된 오브젝트중 IDamagable을 포함하지 않는 오브젝트가 있을 수 있기 때문에 null에 대한 예외 처리하여 해결
  ```cs
  private void Attack()
  {
      Collider[] cols = Physics.OverlapSphere(
          Controller.transform.position,
          Controller.AttackRadius
          );

      IDamagable damagable;
      foreach (Collider col in cols)
      {
          damagable = col.GetComponent<IDamagable>();
          // damagable을 포함하지 않는 오브젝트일 수 있으므로 
          // null 체크를 해줘야 한다.
          damagable?.TakeHit(Controller.AttackValue);
      }
  }
  ```

- Attack 후 Exit를 호출하여 ChangeState를 진행하지만 ChangeState에서 다시 Exit를 호출하면서 무한루프 발생
  - Exit에서 ChangeState 하지 않도록 수정하여 해결 
  ```cs
  public override void Exit()
  {
      // ChangeState에서 CurrentState의 Exit를 호출하면서 무한 루프 발생
      //Machine.ChangeState(StateType.Idle);
  }

  public IEnumerator DelayRoutine(Action action)
  {
      yield return _wait;

      Attack();

      // Exit에서 State를 Change하게 되면 무한루프가 일어난다. (Exit 함수 주석 참고)
      //Exit();

      // 상태 변경 코드 수정
      Machine.ChangeState(StateType.Idle);
  }
  ```