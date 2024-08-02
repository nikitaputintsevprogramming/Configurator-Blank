using System.Collections;
using UnityEngine;

namespace Assets.Scripts.ControlVersions
{
    public class SingletonCamera : MonoBehaviour
    {
        public static SingletonCamera Instance { get; private set; }

        private void Awake()
        {
            // Check if there is already an instance of SingletonCamera
            if (Instance == null)
            {
                // If not, set it to this
                Instance = this;
                // Optionally, mark this instance as persistent across scenes
                // DontDestroyOnLoad(gameObject);
            }
            else if (Instance != this)
            {
                // If there is another instance and it's not this one, destroy this instance
                Destroy(gameObject);
            }
        }

        public SettingsCamera settingsCamera;

        
        public float v;
        public float h;
    }
}