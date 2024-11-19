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
        // AudioSource ������Ʈ�� Play �Լ����� ������� ����ߴٰ� �ϴ���
        // ������Ʈ�� ��Ȱ��ȭ�Ǹ� �ٷ� ��� �����ȴ�.
        //_audio.Play();

        // AudioSource.PlayClipAtPoint�� AudioSource Ŭ������ �ִ� ���� �Լ���
        // World ������ ���ϴ� ��ġ�󿡼� clip�� ��� ��ų �� �ִ�.
        // Main camera�� Listener�� �����Ƿ� �ش� ��ġ���� Ŭ�� ���
        AudioSource.PlayClipAtPoint(_audio.clip, Camera.main.transform.position);
        gameObject.SetActive(false);
    }
}
