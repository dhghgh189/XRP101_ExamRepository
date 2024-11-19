using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private Transform _muzzlePoint;
    [SerializeField] private CustomObjectPool _bulletPool;
    [SerializeField] private float _fireCooltime;

    private Coroutine _coroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        Init();
    }

    // trigger enter를 감지하기 위해서는 자신과 other 둘 중 한곳에는
    // 리지드바디 컴포넌트가 존재해야 한다.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Fire(other.transform);
        }
    }

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

    private void Init()
    {
        _coroutine = null;
        _wait = new WaitForSeconds(_fireCooltime);
        _bulletPool.CreatePool();
    }

    private IEnumerator FireRoutine(Transform target)
    {
        Debug.Log("Fire Routine Start");

        while (true)
        {
            yield return _wait;

            transform.rotation = Quaternion.LookRotation(new Vector3(
                target.position.x,
                0,
                target.position.z)
            );

            PooledBehaviour bullet = _bulletPool.TakeFromPool();
            bullet.transform.position = _muzzlePoint.position;
            bullet.OnTaken(target);

        }
    }

    private void Fire(Transform target)
    {
        _coroutine = StartCoroutine(FireRoutine(target));
    }
}
