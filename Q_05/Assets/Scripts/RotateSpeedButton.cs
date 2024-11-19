using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateSpeedButton : MonoBehaviour
{
    [SerializeField] private Button _plusButton;
    [SerializeField] private Button _minusButton;

    // ��ư�� ���� ���� speed ����/���Ҹ� ���� ��
    [SerializeField] private float _speedAmount;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        SubscribeEvent();
    }

    private void SubscribeEvent()
    {
        _plusButton.onClick.AddListener(PlusScore);
        _minusButton.onClick.AddListener(MinusScore);
    }

    // deltaTime���� ���� ������ �����Ƿ� amount ���� �ʿ�
    private void PlusScore() => GameManager.Intance.Score += /*0.05f*/ _speedAmount;
    private void MinusScore() => GameManager.Intance.Score -= /*0.05f*/ _speedAmount;
}
