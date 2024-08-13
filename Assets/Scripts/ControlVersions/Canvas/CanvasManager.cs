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
        public List<IChoosable> canvasPresets = new List<IChoosable>();

        static CanvasManager()
        {
            // Подписываем метод на событие CameraPresetIsChange
            CameraPreset.CameraPresetIsChange += ChangeCameraPreset;
        }

        public static void ChangeCameraPreset(CameraPresets preset)
        {
            //Debug.Log("ChangeCameraPreset " + preset);
            // Создаем новый экземпляр CanvasManager, так как он не Singleton
            var manager = new CanvasManager();
            // Очищаем список перед каждым использованием
            manager.canvasPresets.Clear();
            // Находим все объекты, реализующие интерфейс IChoosable
            manager.canvasPresets.AddRange(GameObject.FindObjectsOfType<MonoBehaviour>().OfType<IChoosable>());
            // Логируем найденные объекты
            // Перебираем найденные объекты и скрываем те, которые не соответствуют выбранному пресету
            foreach (var canvas in manager.canvasPresets)
            {
                Debug.Log("Found IChoosable object: " + canvas);
                if (canvas is MonoBehaviour monoCanvas)
                {
                    if (ShouldCanvasBeActive(monoCanvas, preset))
                    {
                        monoCanvas.gameObject.SetActive(true); // Активируем нужный канвас
                    }
                    else
                    {
                        monoCanvas.gameObject.SetActive(false); // Деактивируем ненужные канвасы
                    }
                }
            }
        }

        // Метод для проверки, соответствует ли данный канвас выбранному пресету
        private static bool ShouldCanvasBeActive(MonoBehaviour canvas, CameraPresets preset)
        {
            // Предположим, у IChoosable есть свойство, которое возвращает CameraPresets
            if (canvas is IChoosable choosableCanvas)
            {
                // Здесь должно быть какое-то свойство или метод, который определяет, какой пресет соответствует этому канвасу
                // Например:
                // return choosableCanvas.GetPreset() == preset;

                // Но для примера просто возвращаем true, если хотим, чтобы этот канвас был активным
                return true; // Измените это в зависимости от вашей логики
            }
            return false;
        }
    }
}