using Snap;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LookAt : MonoBehaviour
{

    private bool isConfig;

    private void Start()
    {
        ButtonConfig.e_OnButtonConfig += EnableConfigScene;
    }

    private void EnableConfigScene()
    {
        isConfig = !isConfig;
    }

    private void Update()
    {
        //if (isConfig) 
        //transform.LookAt(-Camera.main.transform.position);
        // �������� ������� ������
        Vector3 cameraPosition = Camera.main.transform.position;
        // ����������� �� ������� � ������
        Vector3 directionToCamera = cameraPosition - transform.position;
        // ������������ ����������� �� 180 ��������
        Vector3 directionAwayFromCamera = -directionToCamera;
        // ������������� ����������� ������� �������
        transform.LookAt(transform.position + directionAwayFromCamera);
        //transform.Rotate(-Camera.main.transform.position);
    }
}