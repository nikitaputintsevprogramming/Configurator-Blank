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
            //ButtonConfig.e_OnButtonConfig += EnableConfigScene;
        }

        private void Start()
        {
            LoadFromFile(); // �������� ������ �� ����� ��� ������
            LoadMarkers();
            EnableMarkers(false);
        }

        public void EnableConfigScene(bool enable)
        {
            configCamera.gameObject.SetActive(enable);

            //for (int i = 0; i < MarkerObjects.Count; i++)
            //{
            //    MarkerObjects[i].SetActive(enable);
            //}
            EnableMarkers(enable);
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

        public void EnableMarkers(bool active)
        {
            for (int i = 0; i < MarkerObjects.Count; i++)
            {
                //MarkerObjects[i].SetActive(!MarkerObjects[i].activeInHierarchy);
                MarkerObjects[i].SetActive(active);
            }
        }


        private void LoadMarkers()
        {
            MarkerObjects.Clear(); // ������� ������ �������� ����� ���������
            foreach (var snapshot in savedMarkers)
            {
                MakeMarkerObj(snapshot);
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