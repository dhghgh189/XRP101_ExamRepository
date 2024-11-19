using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;

    private CubeController _cubeController;
    private Vector3 _cubeSetPoint;

    private void Awake()
    {
        // ���� ť�갡 �������� ���� ������ ��ġ ������ �õ��ϸ�
        // cubeController instance�� �Ҵ���� �ʾ� null ���� ������ �߻��Ѵ�.
        //SetCubePosition(3, 0, 3);

        // ť���� ������ ���� �̷����� �Ѵ�.
        CreateCube();
    }

    private void Start()
    {
        // ť���� ������ Awake���� ���� ����
        //CreateCube();

        // ť�갡 �����Ǿ� cubeController�� instance�� �Ҵ� �� �Ŀ� ��ġ ������ �ؾ� �Ѵ�.
        SetCubePosition(3, 0, 3);
    }

    private void SetCubePosition(float x, float y, float z)
    {
        _cubeSetPoint.x = x;
        _cubeSetPoint.y = y;
        _cubeSetPoint.z = z;

        // ���� ���¶�� controller�� ������ ���� SetPosition�� ȣ���ص�
        // controller�� SetPoint�� ��ȭ�� ���� ������ ��ġ ��ȭ�� ����.
        //_cubeController.SetPosition();

        // ���� ������ ������ cubeSetPoint ���� ���ڷ� �Ѱ��� �� �ֵ��� �Ѵ�.
        _cubeController.SetPosition(_cubeSetPoint);
    }

    private void CreateCube()
    {
        GameObject cube = Instantiate(_cubePrefab);
        _cubeController = cube.GetComponent<CubeController>();

        // _cubeSetPoint�� Vector3 �ڷ����̸� �̴� �� �����̴�.
        // ���� _cubeController�� SetPoint�� �Ѱ��൵ �ǹ̰� ����.
        //_cubeSetPoint = _cubeController.SetPoint;
    }
}
