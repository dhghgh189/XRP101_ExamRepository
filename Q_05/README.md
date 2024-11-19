# 5번 문제

주어진 프로젝트는 다음의 기능을 구현하고자 생성한 프로젝트이다.

### 01. Main Scene
- 실행 시, Start 버튼을 누르면 게임매니저를 통해 게임 씬이 로드된다.

### 02. Game Scene
- Go to Main을 누르면 메인 씬으로 이동한다
- `+`버튼을 누르면 큐브 오브젝트의 회전 속도가 증가한다
- `-`버튼을 누르면 큐브 오브젝트의 회전 속도가 감소한다 (-가 될 경우 역방향으로 회전한다)
- Popup 버튼을 누르면 게임 오브젝트가 정지(큐브의 회전이 정지)하며, Popup창을 출력한다. 이 때 출력된 팝업창은 2초 후 자동으로 닫힌다.

### 공통 사항
- 게임 실행 중 씬 전환 시에도 큐브 오브젝트의 회전 속도는 저장되어 있어야 한다.

위 기능들을 구현하고자 할 때
제시된 프로젝트에서 발생하는 `문제들을 모두 서술`하고 올바르게 동작하도록 `소스코드를 개선`하시오.

## 답안

### Main Scene
- UI 버튼 클릭 안되는 문제 발생
    - EventSystem이 존재하지 않아 EventSystem 오브젝트 추가하여 해결

### Game Scene
- UI 버튼 클릭 안되는 문제 발생
  - EventSystem이 존재하지 않아 EventSystem 오브젝트 추가하여 해결

### SingletonBehaviour
- 싱글톤 오브젝트가 게임내 다수 존재하는 문제 발생
  - SingletonInit 함수에서 instance 초기화 시 instance가 이미 할당되었는지를 확인하여 처리하도록 수정하여 해결
  ```cs
    protected void SingletonInit()
    {
        // 싱글톤을 초기화 할때는 instance가 할당되었는지를 확인 후 처리해야 한다.
        //_instance = GetComponent<T>();
        //DontDestroyOnLoad(gameObject);

        if (_instance == null)
        {
            _instance = GetComponent<T>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
  ```

### ObjectRotater
- 프레임에 따른 보정이 고려되지 않아 컴퓨터 성능에 따라 회전속도의 차이 발생
  - 회전 시 deltaTime을 곱하여 프레임 차이를 보정해주는 것으로 수정하여 해결
  ```cs
    private void Update()
    {
        // 프레임 차이로 인한 보정이 고려되지 않아 
        // 컴퓨터 성능에 따른 회전속도의 차이가 발생
        //transform.Rotate(Vector3.up * GameManager.Intance.Score);

        // deltaTime을 곱하여 프레임 차이를 보정해줌으로써 성능과 무관하게 동일한 속도로 회전
        transform.Rotate(Vector3.up * GameManager.Intance.Score * Time.deltaTime);
    }
  ```

### RotateSpeedButton
- speed 증가/감소 버튼을 누를 때의 amount를 변수로 조절할 수 있도록 수정
  ```cs
    // 버튼을 누를 때의 speed 증가/감소를 위한 값
    [SerializeField] private float _speedAmount;

    // deltaTime으로 인한 보정이 들어갔으므로 amount 조정 필요
    private void PlusScore() => GameManager.Intance.Score += /*0.05f*/ _speedAmount;
    private void MinusScore() => GameManager.Intance.Score -= /*0.05f*/ _speedAmount;
  ```
### PopupController
- 일시 정지 후 코루틴 내에서 시간이 지나지 않는 문제 발생
  - _wait 변수를 WaitForSecondsRealtime 타입으로 변경하여 TimeScale에 영향을 받지않도록 수정하여 해결
- 팝업 닫힌 후 timeScale이 돌아오지 않는 문제 발생
  - Deactivate 될 때 timeScale을 원복하도록 수정
  ```cs
    // WaitForSeconds는 TimeScale의 영향을 받는다
    //private WaitForSeconds _wait;

    // WaitForSecondsRealtime의 TimeScale의 영향을 받지 않는다.
    private WaitForSecondsRealtime _wait;

    private void Init()
    {
        //_wait = new WaitForSeconds(_deactiveTime);
        _wait = new WaitForSecondsRealtime(_deactiveTime);
        _popupButton = GetComponent<Button>();
        SubscribeEvent();
    }

    private void Deactivate()
    {
        // timeScale을 다시 원래대로 되돌려야 한다.
        GameManager.Intance.Resume();

        _popup.gameObject.SetActive(false);
    }
  ```