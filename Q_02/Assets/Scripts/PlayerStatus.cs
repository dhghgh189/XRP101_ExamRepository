using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private float moveSpeed;
    public float MoveSpeed
    {
        // MoveSpeed ������Ƽ�� get�� set���� �ڽ��� �����ϰ� ����
        // �̷� ���� get�̳� set�������� ���� ������ �Ͼ overflow �߻�
        // ������Ƽ�� �ܵ����� ����� �ſ����� get; private set; ������ �����ص� ���� ��
        //get => MoveSpeed
        //private set => MoveSpeed = value;

        // ������Ƽ�� ���� ���� ����Ϸ��� ������ �Ǵ� ������ �ϳ� �ΰ�
        // ���� ������ �����ϴ� ������ Ȱ���ؾ� �´°� ����.
        get => moveSpeed;
        private set => moveSpeed = value;
    }

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        MoveSpeed = 5f;
    }
}
