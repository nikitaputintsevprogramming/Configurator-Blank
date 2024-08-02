using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SettingsCamera", menuName = "ScriptableObjects/SettingsCamera", order = 1)]
public class SettingsCamera : ScriptableObject
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _speed;
}
