using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TestWork.Input;
using TMPro;
using TestWork.Core;

namespace TestWork.UI
{
    public class InGameView : MonoBehaviour, IGameView
    {
        private const UIViewType VIEW_TYPE = UIViewType.MainMenu;
        private const bool ENABLE_DEBUG_TEXT = false;


        [SerializeField] private InputStick _movementStick;
        [SerializeField] private InputStick _rotationStick;
        [SerializeField] private TextMeshProUGUI _debugText;

        public UIViewType GetViewType()
        {
            return VIEW_TYPE;
        }

        public void Init()
        {
            
        }

        public void Show()
        {
            
        }

        public void Hide()
        {
            
        }

        private void Update()
        {
            if (ENABLE_DEBUG_TEXT)
            {
                string text = string.Empty + CoreGameManager.GetInstance<InputController>().GetTouches().Count + " vs " + UnityEngine.Input.touchCount;
                foreach (Touch touch in UnityEngine.Input.touches)
                {
                    text += "\n";
                    text += "Finger id: " + touch.fingerId;
                }
                _debugText.text = text;
            }
        }
    }
}