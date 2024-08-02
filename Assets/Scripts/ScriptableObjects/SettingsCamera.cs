using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ControlVersions
{
    [CreateAssetMenu(fileName = "SettingsCamera", menuName = "ScriptableObjects/SettingsCamera", order = 1)]
    public class SettingsCamera : ScriptableObject
    {
        public float _speed;
    }
}