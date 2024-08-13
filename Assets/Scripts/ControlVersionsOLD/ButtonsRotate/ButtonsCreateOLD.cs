using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using ArrowControlOLD;

namespace ArrowControlOLD
{
    public class ButtonsCreateOLD : MonoBehaviour
    {
        public static ButtonsCreateOLD Instance;

        public Dictionary<int, string> sides = new Dictionary<int, string>()
        {
            { 0, "Left"},
            { 1, "Right"},
            { 2, "Up"},
            { 3, "Down"},
        };

        public List<GameObject> _buttons = new List<GameObject>();
        [Range(1, 4)]
        public int _amountButtons;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
            }
        }

        public void AddButtons()
        {
            for (int i = _buttons.Count; _buttons.Count < _amountButtons; i++)
            {
                _buttons.Add(new GameObject("Arrow" + sides[i], typeof(Image), typeof(Button)));
                _buttons[i].transform.SetParent(CanvasCreateOLD.Instance._canvasObj.transform);
                _buttons[i].transform.localScale = Vector3.one;
            }
        }
    }
}