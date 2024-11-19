using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StateAttack : PlayerState
{
    private float _delay = 2;
    private WaitForSeconds _wait;
    
    public StateAttack(PlayerController controller) : base(controller)
    {
        
    }

    public override void Init()
    {
        _wait = new WaitForSeconds(_delay);
        ThisType = StateType.Attack;
    }

    public override void Enter()
    {
        Controller.StartCoroutine(DelayRoutine(Attack));
    }

    public override void OnUpdate()
    {
        Debug.Log("Attack On Update");
    }

    public override void Exit()
    {
        // ChangeState에서 CurrentState의 Exit를 호출하면서 무한 루프 발생
        //Machine.ChangeState(StateType.Idle);
    }

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

    public IEnumerator DelayRoutine(Action action)
    {
        yield return _wait;

        Attack();

        // Exit에서 State를 Change하게 되면 무한루프가 일어난다. (Exit 함수 주석 참고)
        //Exit();

        // 상태 변경 코드 수정
        Machine.ChangeState(StateType.Idle);
    }

}
