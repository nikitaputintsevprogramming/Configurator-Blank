using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Net.Http.Headers;

[ExecuteAlways]
public class ArrowsGUI : MonoBehaviour
{
    public float startX;
    public float startY;
    public float endX;
    public float endY;

    public static float width = Screen.width;
    public static float height = Screen.height;

    //public static float widthPanel = width / 10; // height / x
    //public static float heightPanel = height / 10; // width / x

    //public static float widthButton = width / 10; // height / x
    //public static float heightButton = height / 10; // width / x   
    
    public static float widthPanel = 1600; // height / x
    public static float heightPanel = 1900; // width / x

    public static float widthButton = 600; // height / x
    public static float heightButton = 200; // width / x

    public Rect windowRect = new Rect(Screen.width / 2 - widthPanel / 2, Screen.height - heightPanel, widthPanel, heightPanel);

    [ContextMenu("Do Something")]
    void DoSomething()
    {
        //Debug.LogFormat("Ширина панели: {0}, Высота панели: {1}, Ширина кнопки: {2}, Высота кнопки: {3]", widthPanel.ToString(), heightPanel.ToString(), widthButton.ToString(), heightButton.ToString());
        Debug.Log(Screen.height);
    }

    void OnGUI()
    {
        // Register the window. Notice the 3rd parameter
        windowRect = GUI.Window(0, windowRect, DoMyWindow, "My Window");
    }

    // Make the contents of the window
    void DoMyWindow(int windowID)
    {
        if (GUI.Button(new Rect(Screen.width / 2 - widthPanel / 2, Screen.height - heightPanel, widthPanel, heightPanel), "Hello World"))
        {
            print("Got a click");
        }
    }
}