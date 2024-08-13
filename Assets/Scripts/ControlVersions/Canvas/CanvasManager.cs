using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CameraPreset
{
    [ExecuteInEditMode]
    [InitializeOnLoad]
    public class CanvasManager
    {
        // Сохраняем список всех IChoosable объектов при инициализации
        static List<IChoosable> canvasPresets = new List<IChoosable>();

        static CanvasManager()
        {
            // Подписываем метод на событие CameraPresetIsChange
            CameraPreset.CameraPresetIsChange += ChangeCameraPreset;
        }

        public static void ChangeCameraPreset(CameraPresets preset)
        {
            // Инициализация списка один раз при загрузке
            canvasPresets = GameObject.FindObjectsOfType<MonoBehaviour>(true).OfType<IChoosable>().ToList();
            // Логирование количества найденных объектов
            Debug.Log("Объектов IChoosable найдено: " + canvasPresets.Count);

            // Перебираем каждый canvasPreset и устанавливаем его активное состояние
            foreach (var canvas in canvasPresets)
            {
                if (canvas.AssociatedPreset == preset)
                {
                    canvas.SetActiveObj(true);
                    Debug.Log("Активирован: " + canvas.AssociatedPreset);
                }
                else
                {
                    canvas.SetActiveObj(false);
                    Debug.Log("Деактивирован: " + canvas.AssociatedPreset);
                }
            }
        }
    }
}


//    }
//    // Установка активности на основе метода ShouldCanvasBeActive
//    //monoCanvas.gameObject.SetActive(ShouldCanvasBeActive(monoCanvas, preset));
//    //canvas.SetActiveObj(true);
//}
//else
//{
//    canvas.SetActiveObj(false);
//    Debug.Log("кккккккк " + canvas);
//}
//    }
//}

//// Метод для проверки, соответствует ли данный канвас выбранному пресету
//private static bool ShouldCanvasBeActive(MonoBehaviour canvas, CameraPresets preset)
//{
//    if (canvas is IChoosable choosableCanvas)
//    {
//        Debug.Log("eeeeeeeeee " + canvas);
//        // Проверка соответствия канваса текущему пресету
//        return choosableCanvas.AssociatedPreset == preset;
//    }
//    return false;
//}
