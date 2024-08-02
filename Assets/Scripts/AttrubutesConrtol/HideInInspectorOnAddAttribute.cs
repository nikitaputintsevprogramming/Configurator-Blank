﻿using System;
using UnityEngine;

namespace Assets.Scripts.ControlVersions
{
    [AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class HideInInspectorOnAddAttribute : Attribute
    {
    }
}
