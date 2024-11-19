# 3번 문제

주어진 프로젝트 는 다음의 기능을 구현하고자 생성한 프로젝트이다.

### 1. Turret
- Trigger 범위 내로 플레이어가 들어왔을 때 1.5초에 한번씩 플레이어를 바라보면서 총알을 발사한다
- Trigger 범위 바깥으로 플레이어가 나가면 발사를 중지한다.
- 오브젝트 풀을 사용해 총알을 관리한다.

### 2. Bullet :
- 20만큼의 힘으로 전방을 향해 발사된다
- 발사 후 5초 경과 시 비활성화 처리된다
- 플레이어를 가격했을 경우 2의 데미지를 준다

### 3. Player
- 총알과 충돌했을 때, 데미지를 입는다
- 체력이 0 이하가 될 경우 효과음을 재생하며 비활성화된다.
- 플레이어의 이동은 씬 뷰를 사용해 이동하도록 한다.

위 기능들을 구현하고자 할 때
제시된 프로젝트에서 발생하는 `문제들을 모두 서술`하고 올바르게 동작하도록 `소스코드를 개선`하시오.

## 답안

### Turret
- trigger로 충돌을 체크하려 했지만, 플레이어랑 Turret에 둘다 리지드바디가 없는 상태에서는 충돌감지가 불가능
  - Turret에 rigidbody를 추가하여 충돌감지 가능하도록 수정
- 플레이어가 trigger를 벗어나도 발사를 중지하는 처리가 없음
  - OnTriggerExit를 통해 trigger를 벗어나면 발사를 중지하도록 처리
  ```cs
    // 플레이어가 벗어난 경우에 대한 처리 필요
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // 발사 중지
            StopCoroutine(_coroutine);
            Debug.Log("Fire Routine Stop");
        }
    }
  ```

### Bullet
- 프리팹에 Rigidbody가 추가되있지 않은데 스크립트에서 이를 참조하려고 하여 null 참조 발생
  - 프리팹에 Rigidbody 추가하여 해결
- 풀링 사용으로 인해 Bullet이 재사용 됨에 따라 Force가 계속 누적되는 문제 발생
  - Bullet 발사 직전 velocity를 초기화 해주도록 수정
  ```cs
    private void Fire()
    {
        // bullet을 풀링으로 사용하고 있기 때문에 Force가 계속 누적되는 상황
        // 그러므로 발사 속도의 유지를 위해 velocity를 한번 초기화 해줄 필요가 있다.
        _rigidbody.velocity = Vector3.zero;

        _rigidbody.AddForce(transform.forward * _force, ForceMode.Impulse);
    }
  ```
- Bullet이 플레이어와 부딪혀도 PlayerController instance를 가져오지 못하는 문제 발생
  - player의 collider를 body에서 본체로 옮겨 충돌 시 instance를 가져올 수 있도록 수정
- (Turret과 Bullet에 layer 지정하여 서로 부딪히지 않게 하고 bullet이 다른 물체에 충돌하면 바로 비활성화 하도록 수정 )

### Player
- 플레이어 사망 시 오디오 재생 후에 비활성화 하는 과정에서 오디오가 바로 중지되버리는 문제 발생
  - AudioSource.PlayClipAtPoint를 통해 비활성화 여부와 관계없이 오디오 재생되도록 수정하여 해결
  ```cs
    public void Die()
    {
        // AudioSource 컴포넌트의 Play 함수들은 오디오를 재생했다고 하더라도
        // 오브젝트가 비활성화되면 바로 재생 중지된다.
        //_audio.Play();

        // AudioSource.PlayClipAtPoint는 AudioSource 클래스에 있는 정적 함수로
        // World 공간의 원하는 위치상에서 clip을 재생 시킬 수 있다.
        // Main camera에 Listener가 있으므로 해당 위치에서 클립 재생
        AudioSource.PlayClipAtPoint(_audio.clip, Camera.main.transform.position);
        gameObject.SetActive(false);
    }
  ```