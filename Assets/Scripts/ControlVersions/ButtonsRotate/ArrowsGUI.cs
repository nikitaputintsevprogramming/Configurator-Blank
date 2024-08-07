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
    [SerializeField] private Vector2 _buttonSize; // Добавим возможность задавать размеры кнопок
    public GameObject _canvasObj;
    public GameObject buttonObj;

    [ContextMenu("Reload Canvas")]
    void DoSomething()
    {
        Reset();
    }

    private void Reset()
    {
        Debug.Log("Reset");

        CreateCanvas();
        // Создание кнопок
        CreateButton("ButtonLeft", new Vector2(-_buttonSize.x / 2 - 10, 0), _buttonSize);
        CreateButton("ButtonRight", new Vector2(_buttonSize.x / 2 + 10, 0), _buttonSize);
    }

    private void CreateCanvas()
    {
        // Создание Canvas
        if (!_canvasObj)
            _canvasObj = new GameObject("canvasArrows", typeof(Canvas), typeof(CanvasScaler));
        Canvas canvas = _canvasObj.GetComponent<Canvas>();
        CanvasScaler canvasScaler = _canvasObj.GetComponent<CanvasScaler>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(_resolutionScreen.x, _resolutionScreen.y);
        canvasScaler.matchWidthOrHeight = 0.5f;
    }

    private void CreateButton(string name, Vector2 anchoredPosition, Vector2 size)
    {
        if (!buttonObj)
            buttonObj = new GameObject(name, typeof(RectTransform), typeof(Button), typeof(Image));
        buttonObj.transform.SetParent(_canvasObj.transform);

        RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();
        rectTransform.sizeDelta = size;
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.anchorMin = new Vector2(0.5f, 0);
        rectTransform.anchorMax = new Vector2(0.5f, 0);
        rectTransform.pivot = new Vector2(0.5f, 0.5f);

        Button button = buttonObj.GetComponent<Button>();
        // Можно добавить настройку кнопки, например, текст или событие OnClick
    }

    private void OnDestroy()
    {
        DestroyImmediate(_canvasObj);
    }
}
