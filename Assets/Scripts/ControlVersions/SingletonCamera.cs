﻿using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ControlVersions
{
    public class SingletonCamera : MonoBehaviour
    {
        public static SingletonCamera instance { get; set; }

        public float v;
        public float h;
    }
}