using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace FreeFly
{
    public class FreeFlyCamera : MonoBehaviour
    {
        [SerializeField] private float k_MouseSensitivityMultiplier = 0.01f;
        [SerializeField] private float m_LookSpeedMouse = 24.0f;
        [SerializeField] private float m_MoveSpeed = 10.0f;
        [SerializeField] private float m_MoveSpeedIncrement = 2.5f;
        [SerializeField] private float m_Turbo = 3.0f;
        [SerializeField] private float m_RotationTurbo = 1.5f; // Множитель для ускорения поворота
        [SerializeField] private float m_HeightTurbo = 2.0f; // Множитель для ускорения высоты

        private float forwardBackward_f;
        private float rightLeft_f;
        private bool down_f;
        private bool up_f;
        private bool shiftBoostL;
        private bool shiftBoostR;

        private void Update()
        {
            if (!Input.GetButton("Fire2"))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                return;
            }

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            forwardBackward_f = Input.GetAxis("Vertical");
            rightLeft_f = Input.GetAxis("Horizontal");
            down_f = Input.GetKey(KeyCode.Q);
            up_f = Input.GetKey(KeyCode.E);
            shiftBoostL = Input.GetKey(KeyCode.LeftShift);
            shiftBoostR = Input.GetKey(KeyCode.RightShift);

            // Перемещение объекта
            Vector3 moveDirection = (transform.forward * forwardBackward_f + transform.right * rightLeft_f).normalized;
            float currentMoveSpeed = (shiftBoostL || shiftBoostR) ? m_MoveSpeed * m_Turbo : m_MoveSpeed;
            transform.position += moveDirection * currentMoveSpeed * Time.deltaTime;

            // Вращение объекта на основе движения мыши с бустом
            float mouseX = Input.GetAxis("Mouse X") * m_LookSpeedMouse * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * m_LookSpeedMouse * Time.deltaTime;

            // Определяем множитель для вращения
            //float rotationMultiplier = (shiftBoostL || shiftBoostR) ? m_RotationTurbo : 1.0f;

            transform.Rotate(Vector3.up, mouseX , Space.World);
            transform.Rotate(Vector3.left, mouseY , Space.Self);// *rotationMultiplier

            // Управление высотой (вверх/вниз) с бустом
            float currentHeightSpeed = (shiftBoostL || shiftBoostR) ? m_MoveSpeed * m_HeightTurbo : m_MoveSpeed;
            if (down_f)
            {
                transform.position += Vector3.down * currentHeightSpeed * Time.deltaTime;
            }
            if (up_f)
            {
                transform.position += Vector3.up * currentHeightSpeed * Time.deltaTime;
            }
        }
    }
}
