using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

namespace ArrowControlOLD
{
    public class ArrowsManagerOLD : MonoBehaviour
    {
        public void OnValidate()
        {
            Reset();
            // Отложенное удаление кнопок
            EditorApplication.delayCall += DeleteExcess;
            WindowControlDataOLD.Instance._resolutionScreen = WindowControlDataOLD.Instance.qualityDictionary[WindowControlDataOLD.Instance.Quality.ToString()];
        }

        private void Start()
        {
            EditorApplication.hierarchyChanged += DeleteExcess;
            //EditorApplication.hierarchyChanged += () => DoDelete();
        }

        public void Reset()
        {
            if (!CanvasCreateOLD.Instance._canvasObj || !CanvasCreateOLD.Instance._canvasObj.gameObject.activeInHierarchy)
            {
                //Debug.Log("Нет канваса");
                CanvasCreateOLD.Instance.CreateCanvas();
                CanvasCreateOLD.Instance.GetCanvasComponents();
            }
            if (ButtonsCreateOLD.Instance._buttons.Count < ButtonsCreateOLD.Instance._amountButtons)
            {
                CanvasCreateOLD.Instance.ResetCanvasComponent();
                ButtonsCreateOLD.Instance.AddButtons();
            }
        }

        [ContextMenu("Reset Canvas")]
        public void DeleteExcess()
        {
            // Производим итерацию задом наперед, так как RemoveAt пропускает данные в единице
            // https://ru.stackoverflow.com/questions/992441
            for (int i = ButtonsCreateOLD.Instance._buttons.Count - 1; i > 0; i--)
            {
                if (ButtonsCreateOLD.Instance._buttons[i] == null)
                {
                    ButtonsCreateOLD.Instance._buttons.RemoveAt(i);
                }
                if (i >= ButtonsCreateOLD.Instance._amountButtons)
                {
                    if (Application.isPlaying)
                        Destroy(ButtonsCreateOLD.Instance._buttons[i].gameObject);
                    else
                        DestroyImmediate(ButtonsCreateOLD.Instance._buttons[i].gameObject);
                    ButtonsCreateOLD.Instance._buttons.RemoveAt(i);
                }
            }
        }

        private void OnDestroy()
        {
            DestroyImmediate(CanvasCreateOLD.Instance._canvasObj);
        }
    }
}