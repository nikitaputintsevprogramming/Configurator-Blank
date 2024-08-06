using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

[ExecuteAlways]
public class ArrowsGUI : MonoBehaviour
{
    [SerializeField] private Vector2 _resolutionScreen;
    public GameObject _canvasObj;

    [ContextMenu("Add canvas")]
    void DoSomething()
    {
        Reset();
    }

    private void Reset()
    {
        _resolutionScreen = new Vector2(3840, 2160);
        Debug.Log("Reset");
        _canvasObj = new GameObject("canvasArrows", typeof(Canvas), typeof(CanvasScaler));
        Canvas canvas = _canvasObj.GetComponent<Canvas>();
        CanvasScaler canvasScaler = _canvasObj.GetComponent<CanvasScaler>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(_resolutionScreen.x, _resolutionScreen.y);
        canvasScaler.matchWidthOrHeight = 0.5f;
    }

    private void OnDestroy()
    {
        DestroyImmediate(_canvasObj);
    }
}