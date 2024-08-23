using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CameraPresets
{
    [InitializeOnLoad]
    [ExecuteAlways]
    public class CanvasTwoButtons : MonoBehaviour
    {
        public void CreateSelf()
        {
            Instantiate(gameObject);
        }
    }
}