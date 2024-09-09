using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Snap
{
    public class Markers : MonoBehaviour
    {
        public Camera configCamera; // Камера для конфигурационной сцены
        //public Camera mainCamera;  // Другая камера
        //public GameObject canvasConfig;

        public string saveFileName = "markers.json"; // Имя файла для сохранения

        public List<GameObject> MarkerObjects = new List<GameObject>();
        private List<CameraData> savedMarkers = new List<CameraData>();

        private void OnEnable()
        {
            LoadFromFile(); // Загрузка данных из файла при старте
            LoadMarkers(); // Восстановление объектов из загруженных данных
        }

        //private void Start()
        //{
        //    ButtonConfig.e_OnButtonConfig += EnableConfigScene;
        //}

        public void EnableConfigScene(bool enable)
        {
            configCamera.gameObject.SetActive(true);
            //mainCamera.gameObject.SetActive(!mainCamera.gameObject.activeInHierarchy);

            // Включаем конфигурационную камеру и выключаем другую
            //canvasConfig.gameObject.SetActive(!canvasConfig.gameObject.activeInHierarchy);

            //configCamera.gameObject.SetActive(!configCamera.gameObject.activeInHierarchy);
            //mainCamera.gameObject.SetActive(!mainCamera.gameObject.activeInHierarchy);


            //makeShapshot.gameObject.SetActive(!makeShapshot.gameObject.activeInHierarchy);
            //clearShapshot.gameObject.SetActive(!clearShapshot.gameObject.activeInHierarchy);

            //speedSlider.gameObject.SetActive(!speedSlider.gameObject.activeInHierarchy);
            //speedText.gameObject.SetActive(!speedText.gameObject.activeInHierarchy);

            for (int i = 0; i < MarkerObjects.Count; i++)
            {
                MarkerObjects[i].SetActive(!MarkerObjects[i].activeInHierarchy);
            }
        }

        public void SaveCurrentCameraState()
        {
            CameraData snapshot = new CameraData
            {
                position = configCamera.transform.position,
                rotation = configCamera.transform.rotation.eulerAngles,
            };
            savedMarkers.Add(snapshot);

            // Сохранение в JSON файл
            SaveToFile();
            MakeMarkerObj(snapshot);
        }

        private void MakeMarkerObj(CameraData snapshot)
        {
            var snaphotPrefab = Resources.Load<GameObject>("Config/marker");
            GameObject snaphotObj = Instantiate(snaphotPrefab, snapshot.position, Quaternion.Euler(snapshot.rotation));
            // Получаем компонент Text в дочернем Canvas и устанавливаем порядковый номер
            var textComponent = snaphotObj.GetComponentInChildren<Text>();
            if (textComponent != null)
            {
                textComponent.text = (MarkerObjects.Count + 1).ToString();
            }

            MarkerObjects.Add(snaphotObj);
        }

        public void ClearMarkers()
        {
            savedMarkers.Clear();
            SaveToFile(); // Сохраняем пустой список в файл
            Debug.Log("JSON файл очищен.");

            for (int i = 0; i < MarkerObjects.Count; i++)
            {
                DestroyImmediate(MarkerObjects[i], true);
            }
            MarkerObjects.Clear(); // Очищаем список объектов
        }

        private void SaveToFile()
        {
            // Использование папки StreamingAssets
            string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
            string json = JsonUtility.ToJson(new CameraDataList { snapshots = savedMarkers }, true);
            File.WriteAllText(filePath, json);
            Debug.Log("Данные сохранены в " + filePath);
        }

        public void LoadFromFile()
        {
            // Использование папки StreamingAssets
            string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                CameraDataList dataList = JsonUtility.FromJson<CameraDataList>(json);
                savedMarkers = dataList.snapshots;
            }
            else
            {
                Debug.LogWarning("Файл не найден: " + filePath);
            }
        }

        private void LoadMarkers()
        {
            MarkerObjects.Clear(); // Очищаем список объектов перед загрузкой
            foreach (var snapshot in savedMarkers)
            {
                MakeMarkerObj(snapshot);
            }

            for (int i = 0; i < MarkerObjects.Count; i++)
            {
                MarkerObjects[i].SetActive(!MarkerObjects[i].activeInHierarchy);
            }
        }

        [System.Serializable]
        public class CameraData
        {
            public Vector3 position;
            public Vector3 rotation;
        }

        [System.Serializable]
        public class CameraDataList
        {
            public List<CameraData> snapshots;
        }
    }
}