using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceHide : MonoBehaviour
{
    // ������������ ������������
    [SerializeField] private float minTransparency = 0.0f; // ����������� ������������
    [SerializeField] private float maxTransparency = 1.0f; // ������������ ������������

    [SerializeField] private float dist;
    // ���������
    [SerializeField] private float minDistance = 5.0f;  // ����������, ��� ������� ������������ ����������� ������������
    [SerializeField] private float maxDistance = 20.0f; // ����������, ��� ������� ������������ ������������ ������������
    float alpha;

    private void Update()
    {
        // ���������� ���������� �� ������ �� �������
        dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        print("Distance to other: " + dist);

        // ��������� ���� ����������� Image, ����������� ������ ������� � ��� ��������
        Image[] images = GetComponentsInChildren<Image>();

        // ���� ������ ����������� �� ������
        if (images.Length > 0)
        {
            // ���������� ������������ �� ������ ���������� � ��������
            //float t = Mathf.InverseLerp(minDistance, maxDistance, dist); // ������������ ���������� ����� minDistance � maxDistance
            //float alpha = Mathf.Lerp(minTransparency, maxTransparency, t); // ������������ ������������

            // ����������� �������� alpha, ����� �� ��������� ��������� ������������ ���������� ����������
            if (dist < minDistance)
            {
                alpha = maxTransparency;
            }
            else if (dist > maxDistance)
            {
                alpha = minTransparency; 
            }

            // ���������� ����� ������������ �� ���� ��������� ������������
            foreach (Image image in images)
            {
                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }
        }
    }
}
