using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateSpeedButton : MonoBehaviour
{
    [SerializeField] private Button _plusButton;
    [SerializeField] private Button _minusButton;

    // 버튼을 누를 때의 speed 증가/감소를 위한 값
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

    // deltaTime으로 인한 보정이 들어갔으므로 amount 조정 필요
    private void PlusScore() => GameManager.Intance.Score += /*0.05f*/ _speedAmount;
    private void MinusScore() => GameManager.Intance.Score -= /*0.05f*/ _speedAmount;
}
