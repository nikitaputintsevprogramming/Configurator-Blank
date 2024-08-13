using Assets.Scripts.ControlVersions;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ArrowControlOLD
{
    public class CanvasCreateOLD : MonoBehaviour
    {
        public static CanvasCreateOLD Instance;

        public GameObject _canvasObj;
        public GridLayoutGroup gridGroup;
        public Canvas canvas;
        public CanvasScaler canvasScaler;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void CreateCanvas()
        {
            // Создание Canvas
            _canvasObj = new GameObject("canvasArrows", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster), typeof(GridLayoutGroup));
            _canvasObj.transform.SetParent(transform);
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
            canvasScaler.referenceResolution = new Vector2(WindowControlDataOLD.Instance._resolutionScreen.x, WindowControlDataOLD.Instance._resolutionScreen.y);
            canvasScaler.matchWidthOrHeight = 0.5f;
            gridGroup.childAlignment = TextAnchor.LowerCenter;

            //gridGroup.cellSize = sizeButtons;
        }
    }
}