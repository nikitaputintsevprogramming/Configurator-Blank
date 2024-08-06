using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

[ExecuteAlways]
public class ArrowsGUI : MonoBehaviour
{
    [SerializeField] Text testText;
    [SerializeField] Toggle testToggle;
    [SerializeField] private Vector2 _resolutionScreen;
    public GameObject _canvasObj;

    [ContextMenu("Add canvas")]
    void DoSomething()
    {
        //Reset();
        CheckSize();
    }

    private void Reset()
    {
        Debug.Log("Reset");
        _canvasObj = new GameObject("canvasArrows", typeof(Canvas), typeof(CanvasScaler));
        Canvas canvas = _canvasObj.GetComponent<Canvas>();
        CanvasScaler canvasScaler = _canvasObj.GetComponent<CanvasScaler>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        canvasScaler.referenceResolution = new Vector2(_resolutionScreen.x, _resolutionScreen.y);
        canvasScaler.matchWidthOrHeight = 0.5f;
    }

    private void OnDestroy()
    {
        DestroyImmediate(_canvasObj);
    }

    private void CheckSize()
    {
        //Screen.SetResolution(Convert.ToInt32(_resolutionScreen.x), Convert.ToInt32(_resolutionScreen.y), true);
        Screen.SetResolution(640, 480, true);
        //print(Display.main.systemWidth);
        print(Screen.width);
        testText.text = Screen.width.ToString();
        //print(Screen.currentResolution); // gives the desktop resolution
    }

    public void Set360()
    {
        // Screen.currentResolution.height - выводит разрешение экрана пк, Screen.height - выводит разрешение окна, но текст меняется при последующем нажатии
        testText.text = Screen.currentResolution.height.ToString() + " " + Screen.height.ToString();
        Screen.SetResolution(640, 360, Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen")));
    }

    public void Set480()
    {
        Screen.SetResolution(854, 480, Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen")));
        testText.text = Screen.currentResolution.height.ToString() + " " + Screen.height.ToString();
    }

    public void Set720()
    {
        Screen.SetResolution(1280, 720, Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen")));
        testText.text = Screen.currentResolution.height.ToString() + " " + Screen.height.ToString();
    }

    public void Set1080()
    {
        Screen.SetResolution(1920, 1080, Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen")));
        testText.text = Screen.currentResolution.height.ToString() + " " + Screen.height.ToString();
    }

    public void Set1440()
    {
        Screen.SetResolution(2560, 1440, Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen")));
        testText.text = Screen.currentResolution.height.ToString() + " " + Screen.height.ToString();
    }

    public void Set2160()
    {
        Screen.SetResolution(3840, 2160, Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen")));
        testText.text = Screen.currentResolution.height.ToString() + " " + Screen.height.ToString();
    }

    public void SetFullscreen()
    {
        PlayerPrefs.SetInt("Fullscreen", testToggle.GetComponent<Toggle>().isOn ? 1 : 0); // Convert.ToInt32(testToggle.isOn)
        print(Convert.ToInt32(PlayerPrefs.GetInt("Fullscreen")));
    }
}