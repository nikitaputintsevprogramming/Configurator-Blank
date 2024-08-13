using UnityEditor;
using UnityEngine;
using System.Collections;

namespace CameraPreset
{
    [InitializeOnLoad]
    public class CameraPresetAddToHierarchy
    {
        public delegate void CameraPresetHandler();
        public static event CameraPresetHandler CameraPresetAddToHierarhcy;

        static CameraPresetAddToHierarchy()
        {
            // Подписка на событие при загрузке Unity Editor
            EditorCameraPresetMenu.AddCameraPresetEvent += AddCameraPresetToHierarchy;
        }

        static void AddCameraPresetToHierarchy()
        {
            // Поиск объекта с именем "Camera preset" в иерархии
            GameObject existingPreset = GameObject.Find("Camera preset");

            if (existingPreset != null)
            {
                // Отображение диалогового окна с выбором
                bool shouldReplace = EditorUtility.DisplayDialog(
                    "Camera preset",
                    "Объект с именем 'Camera preset' уже существует в иерархии. Хотите удалить его и создать новый?",
                    "Да",
                    "Отмена"
                );

                if (!shouldReplace)
                {
                    // Если пользователь выбрал "Отмена", выходим из метода
                    return;
                }

                // Удаляем старый объект
                Undo.DestroyObjectImmediate(existingPreset);
            }

            // Создание нового объекта
            GameObject newPreset = new GameObject("Camera preset", typeof(CameraPreset));
            Undo.RegisterCreatedObjectUndo(newPreset, "Create " + newPreset.name);
            Selection.activeObject = newPreset;

            if (CameraPresetAddToHierarhcy != null)
                CameraPresetAddToHierarhcy();
        }
    }
}