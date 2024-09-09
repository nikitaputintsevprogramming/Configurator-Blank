using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace Snap
{
    public class SnapshotsOLD : MonoBehaviour
    {
        public Camera configCamera; // ������ ��� ���������������� �����
        public Camera mainCamera;  // ������ ������
        public GameObject canvasConfig;

        public string saveFileName = "snapshots.json"; // ��� ����� ��� ����������

        public List<GameObject> SnapshotObjects = new List<GameObject>();
        private List<CameraData> savedSnapshots = new List<CameraData>();

        private void OnEnable()
        {
            LoadFromFile(); // �������� ������ �� ����� ��� ������
            LoadSnapshots(); // �������������� �������� �� ����������� ������
        }

        private void Start()
        {
            ButtonConfig.e_OnButtonConfig += EnableConfigScene;
        }

        public void EnableConfigScene()
        {
            // �������� ���������������� ������ � ��������� ������
            canvasConfig.gameObject.SetActive(!canvasConfig.gameObject.activeInHierarchy);

            configCamera.gameObject.SetActive(!configCamera.gameObject.activeInHierarchy);
            mainCamera.gameObject.SetActive(!mainCamera.gameObject.activeInHierarchy);
            //makeShapshot.gameObject.SetActive(!makeShapshot.gameObject.activeInHierarchy);
            //clearShapshot.gameObject.SetActive(!clearShapshot.gameObject.activeInHierarchy);

            //speedSlider.gameObject.SetActive(!speedSlider.gameObject.activeInHierarchy);
            //speedText.gameObject.SetActive(!speedText.gameObject.activeInHierarchy);

            for (int i = 0; i < SnapshotObjects.Count; i++)
            {
                SnapshotObjects[i].SetActive(!SnapshotObjects[i].activeInHierarchy);
            }
        }

        public void SaveCurrentCameraState()
        {
            CameraData snapshot = new CameraData
            {
                position = configCamera.transform.position,
                rotation = configCamera.transform.rotation.eulerAngles,
            };
            savedSnapshots.Add(snapshot);

            // ���������� � JSON ����
            SaveToFile();
            MakeSnapShotObj(snapshot);
        }

        private void MakeSnapShotObj(CameraData snapshot)
        {
            var snaphotPrefab = Resources.Load<GameObject>("Config/snapshot");
            GameObject snaphotObj = Instantiate(snaphotPrefab, snapshot.position, Quaternion.Euler(snapshot.rotation));
            // �������� ��������� Text � �������� Canvas � ������������� ���������� �����
            var textComponent = snaphotObj.GetComponentInChildren<Text>();
            if (textComponent != null)
            {
                textComponent.text = (SnapshotObjects.Count + 1).ToString();
            }

            SnapshotObjects.Add(snaphotObj);
        }

        public void ClearSnapshots()
        {
            savedSnapshots.Clear();
            SaveToFile(); // ��������� ������ ������ � ����
            Debug.Log("JSON ���� ������.");

            for (int i = 0; i < SnapshotObjects.Count; i++)
            {
                DestroyImmediate(SnapshotObjects[i], true);
            }
            SnapshotObjects.Clear(); // ������� ������ ��������
        }

        private void SaveToFile()
        {
            // ������������� ����� StreamingAssets
            string filePath = Path.Combine(Application.streamingAssetsPath, saveFileName);
            string json = JsonUtility.ToJson(new CameraDataList { snapshots = savedSnapshots }, true);
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
                savedSnapshots = dataList.snapshots;
            }
            else
            {
                Debug.LogWarning("���� �� ������: " + filePath);
            }
        }

        private void LoadSnapshots()
        {
            SnapshotObjects.Clear(); // ������� ������ �������� ����� ���������
            foreach (var snapshot in savedSnapshots)
            {
                MakeSnapShotObj(snapshot);
            }

            for (int i = 0; i < SnapshotObjects.Count; i++)
            {
                SnapshotObjects[i].SetActive(!SnapshotObjects[i].activeInHierarchy);
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