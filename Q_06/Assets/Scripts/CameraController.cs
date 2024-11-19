using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private bool _hasFollowTarget;
    private Transform _followTarget;
    public Transform FollowTarget
    {
        get => _followTarget;
        set
        {
            _followTarget = value;
            if (_followTarget != null) _hasFollowTarget = true;
            else _hasFollowTarget = false;
        }
    }

    private void LateUpdate() => SetTransform();

    private void SetTransform()
    {
        if (!_hasFollowTarget) return;

        // followTarget의 transform을 자신의 transform으로 설정하는게 아니라
        // 자신의 Transform을 target의 transform으로 설정해야 한다.
        //_followTarget.SetPositionAndRotation(
        //    transform.position,
        //    transform.rotation
        //    );

        // 자신의 Transform을 target의 transform으로 설정
        transform.SetPositionAndRotation(
            _followTarget.position,
            _followTarget.rotation
            );
    }
}
