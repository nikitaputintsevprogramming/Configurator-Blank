﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.ControlVersions
{
    public class CameraRotate : MonoBehaviour
    {
        private void Start()
        {
            SwipeCameraController.e_OnDrag += OnDragCameraRotate;
        }

        private void OnDragCameraRotate(PointerEventData data)
        {

        }
    }
}