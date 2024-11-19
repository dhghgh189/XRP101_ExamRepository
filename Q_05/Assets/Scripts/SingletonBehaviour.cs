using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Intance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    protected void SingletonInit()
    {
        // �̱����� �ʱ�ȭ �Ҷ��� instance�� �Ҵ�Ǿ������� Ȯ�� �� ó���ؾ� �Ѵ�.
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
}
