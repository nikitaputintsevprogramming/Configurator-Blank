using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using System.Security.Cryptography;

namespace ArrowControl
{
    public enum QualityEnum
    {
        FullHD,
        UHD_4K,
    };

    [ExecuteAlways]
    public class ArrowsTest : MonoBehaviour
    {
        public static ArrowsTest Instance;

        static Dictionary<int, string> sides = new Dictionary<int, string>()
        {
            { 0, "Left"},
            { 1, "Right"},
            { 2, "Up"},
            { 3, "Down"},
        };

        static Dictionary<string, Vector2> qualityDictionary = new Dictionary<string, Vector2>()
        {
            { "FullHD", new Vector2(1920, 1080)},
            { "UHD_4K", new Vector2(3840, 2160)},
        };

        public Vector2 _resolutionScreen = qualityDictionary[QualityEnum.FullHD.ToString()];
        public GameObject _canvasObj;
        public List<GameObject> _buttons = new List<GameObject>();
        public QualityEnum Quality = QualityEnum.FullHD;//будет отображатся как дропдаун

        [Range(1, 4)]
        public int _amountButtons;

        //components
        public GridLayoutGroup gridGroup;
        public Canvas canvas;
        public CanvasScaler canvasScaler;

        public void OnValidate()
        {
            Reset();
            // Отложенное удаление кнопок
            EditorApplication.delayCall += DeleteExcess;
            _resolutionScreen = qualityDictionary[Quality.ToString()];
        }

        private void Start()
        {
            EditorApplication.hierarchyChanged += DeleteExcess;
            //EditorApplication.hierarchyChanged += () => DoDelete();
        }

        public void Reset()
        {
            if (!_canvasObj || !_canvasObj.gameObject.activeInHierarchy)
            {
                //Debug.Log("Нет канваса");
                CreateCanvas();
                GetCanvasComponents();
            }
            if (_buttons.Count < _amountButtons)
            {
                ResetCanvasComponent();
                AddButtons();
            }
        }

        [ContextMenu("Reset Canvas")]
        public void DeleteExcess()
        {
            // Производим итерацию задом наперед, так как RemoveAt пропускает данные в единице
            // https://ru.stackoverflow.com/questions/992441
            for (int i = _buttons.Count - 1; i > 0; i--)
            {
                if (_buttons[i] == null)
                {
                    _buttons.RemoveAt(i);
                }
                if (i >= _amountButtons)
                {
                    if (Application.isPlaying)
                        Destroy(_buttons[i].gameObject);
                    else
                        DestroyImmediate(_buttons[i].gameObject);
                    _buttons.RemoveAt(i);
                }
            }
        }

        public void CreateCanvas()
        {
            // Создание Canvas
            _canvasObj = new GameObject("canvasArrows", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(GridLayoutGroup));
            _canvasObj.transform.SetParent(transform);
        }

        public void AddButtons()
        {
            for (int i = _buttons.Count; _buttons.Count < _amountButtons; i++)
            {
                _buttons.Add(new GameObject("Arrow" + sides[i], typeof(Image), typeof(Button)));
                _buttons[i].transform.SetParent(_canvasObj.transform);
            }
        }

        public void GetCanvasComponents()
        {
            canvas = _canvasObj.GetComponent<Canvas>();
            canvasScaler = _canvasObj.GetComponent<CanvasScaler>();
            gridGroup = _canvasObj.GetComponent<GridLayoutGroup>();
        }

        public void ResetCanvasComponent()
        {
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(_resolutionScreen.x, _resolutionScreen.y);
            canvasScaler.matchWidthOrHeight = 0.5f;
            gridGroup.childAlignment = TextAnchor.LowerCenter; 
        }

        private void OnDestroy()
        {
            DestroyImmediate(_canvasObj);
        }
    }
}

//buttonObj.transform.SetParent(_canvasObj.transform);

//RectTransform rectTransform = buttonObj.GetComponent<RectTransform>();
//rectTransform.sizeDelta = size;
//rectTransform.anchoredPosition = anchoredPosition;
//rectTransform.anchorMin = new Vector2(0.5f, 0);
//rectTransform.anchorMax = new Vector2(0.5f, 0);
//rectTransform.pivot = new Vector2(0.5f, 0.5f);

//Button button = buttonObj.GetComponent<Button>();