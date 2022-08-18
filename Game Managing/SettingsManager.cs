using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GreyWolf
{
    public class SettingsManager : MonoBehaviour
    {

        //Input settings
        //Position
        enum InputPlacement { Default, Inverted };
        [SerializeField]InputPlacement placement;

        [SerializeField] RectTransform movementJoystickAnchor;
        [SerializeField] RectTransform aimingJoystickAnchor;
        [SerializeField] RectTransform shootingBtnAnchor;

        [SerializeField] RectTransform previewJoystick;

        // Joystick Handle
        [SerializeField] RectTransform h_movementJoystickAnchor;
        [SerializeField] RectTransform h_aimingJoystickAnchor;

        [SerializeField] RectTransform h_previewJoystick;

        [SerializeField] int xOffsetJoystick = 200;
        [SerializeField] int yOffsetJoystick = 200;

        Vector2 leftAnchor = new Vector2(0, 0);
        Vector2 rightAnchor = new Vector2(1, 0);


        //Scale
        Vector2 defaultInputSize = new Vector2(200, 200);
        Vector2 defaultHandleSize = new Vector2(63, 63);
        [SerializeField] Slider slider;
        float inputScaleMultiplier = 1;


        private void Start()
        {
            placement = InputPlacement.Default;
        }

        private void Update()
        {
            InputPlacementUI(placement);
            InputScaleUI();
        }


        void InputPlacementUI(InputPlacement state)
        {
            // m - Movement | a - Aiming
            Vector2 m_Joystick;
            Vector2 a_Joystick;
            Vector2 a_btn;

            Vector3 leftPosition = new Vector3(xOffsetJoystick, yOffsetJoystick, 0);
            Vector3 rightPosition = new Vector3(-xOffsetJoystick, yOffsetJoystick, 0);

            Vector3 m_JPost; // Movement Joystick position
            Vector3 a_JPost; // Aiming Joystick position
            Vector3 b_JPost; // Shooting Button position

            if (state == InputPlacement.Default)
            {
                m_Joystick = leftAnchor;
                a_Joystick = rightAnchor;
                a_btn = leftAnchor;

                m_JPost = leftPosition;
                a_JPost = rightPosition;
                b_JPost = leftPosition;
            }
            else
            {
                m_Joystick = rightAnchor;
                a_Joystick = leftAnchor;
                a_btn = rightAnchor;

                m_JPost = rightPosition;
                a_JPost = leftPosition;
                b_JPost = rightPosition;
            }

            movementJoystickAnchor.anchorMin = m_Joystick;
            movementJoystickAnchor.anchorMax = m_Joystick;
            movementJoystickAnchor.anchoredPosition = m_JPost;

            aimingJoystickAnchor.anchorMin = a_Joystick;
            aimingJoystickAnchor.anchorMax = a_Joystick;
            aimingJoystickAnchor.anchoredPosition = a_JPost;

            shootingBtnAnchor.anchorMin = a_btn;
            shootingBtnAnchor.anchorMax = a_btn;
            shootingBtnAnchor.anchoredPosition = b_JPost;
        }

        void InputScaleUI()
        {
            inputScaleMultiplier = slider.value;

            movementJoystickAnchor.sizeDelta = defaultInputSize * inputScaleMultiplier;
            aimingJoystickAnchor.sizeDelta = defaultInputSize * inputScaleMultiplier;
            shootingBtnAnchor.sizeDelta = defaultInputSize * inputScaleMultiplier;
            previewJoystick.sizeDelta = defaultInputSize * inputScaleMultiplier;

            h_movementJoystickAnchor.sizeDelta = defaultHandleSize * inputScaleMultiplier;
            h_aimingJoystickAnchor.sizeDelta = defaultHandleSize * inputScaleMultiplier;
            h_previewJoystick.sizeDelta = defaultHandleSize * inputScaleMultiplier;

            
        }

        public void SetDefaultLayout()
        {
            placement = InputPlacement.Default;
        }

        public void SetInvertedLayout()
        {
            placement = InputPlacement.Inverted;
        }
    }
}
