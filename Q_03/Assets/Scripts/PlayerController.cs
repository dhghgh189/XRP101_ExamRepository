using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [field: SerializeField]
    [field: Range(0, 100)]
    public int Hp { get; private set; }

    private AudioSource _audio;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _audio = GetComponent<AudioSource>();
    }
    
    public void TakeHit(int damage)
    {
        Hp -= damage;

        if (Hp <= 0)
        {
            Die();
        }
    }

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
}
