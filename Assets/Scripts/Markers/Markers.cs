using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Snap
{
    public class Markers : MonoBehaviour
    {
        public Camera configCamera; // ������ ��� ���������������� �����
        //public Camera mainCamera;  // ������ ������
        //public GameObject canvasConfig;

        public string saveFileName = "markers.json"; // ��� ����� ��� ����������

        public List<GameObject> MarkerObjects = new List<GameObject>();
        private List<CameraData> savedMarkers = new List<CameraData>();

        private void OnEnable()
        {
            LoadFromFile(); // �������� ������ �� ����� ��� ������
            LoadMarkers(); // �������������� �������� �� ����������� ������
        }

        //private void Start()
        //{
        //    ButtonConfig.e_OnButtonConfig += EnableConfigScene;
        //}

        public void EnableConfigScene(bool enable)
        {
            configCamera.gameObject.SetActive(true);
            //mainCamera.gameObject.SetActive(!mainCamera.gameObject.activeInHierarchy);

            // �������� ���������������� ������ � ��������� ������
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

            // ���������� � JSON ����
            SaveToFile();
            MakeMarkerObj(snapshot);
        }

        private void MakeMarkerObj(CameraData snapshot)
        {
            var snaphotPrefab = Resources.Load<GameObject>("Config/marker");
            GameObject snaphotObj = Instantiate(snaphotPrefab, snapshot.position, Quaternion.Euler(snapshot.rotation));
            // �������� ��������� Text � �������� Canvas � ������������� ���������� �����
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
            SaveToFile(); // ��������� ������ ������ � ����
            Debug.Log("JSON ���� ������.");

            for (int i = 0; i < MarkerObjects.Count; i++)
            {
                DestroyImmediate(MarkerObjects[i], true);
            }
            MarkerObjects.Clear(); // ������� ������ ��������
        }

        private void SaveToFile()
        {
            // ������������� ����� StreamingAssets
            string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
            string json = JsonUtility.ToJson(new CameraDataList { snapshots = savedMarkers }, true);
            File.WriteAllText(filePath, json);
            Debug.Log("������ ��������� � " + filePath);
        }

        public void LoadFromFile()
        {
            // ������������� ����� StreamingAssets
            string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                CameraDataList dataList = JsonUtility.FromJson<CameraDataList>(json);
                savedMarkers = dataList.snapshots;
            }
            else
            {
                Debug.LogWarning("���� �� ������: " + filePath);
            }
        }

        private void LoadMarkers()
        {
            MarkerObjects.Clear(); // ������� ������ �������� ����� ���������
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