using System;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Abstracts
{
    public abstract class BaseButton : BaseWidget
    {
        protected Button Button;
        protected Image ButtonImage;
        protected TMP_Text ButtonLabel;

        private Action _onClick;

        protected virtual void OnEnable()
        {
            Button.onClick.AddListener(OnClick);
        }

        protected virtual void OnDisable()
        {
            Button.onClick.RemoveAllListeners();
        }

        private void Awake()
        {
            Button = GetComponent<Button>();
            ButtonImage = GetComponent<Image>();
            ButtonLabel = Button.GetComponentInChildren<TMP_Text>();
        }

        public async UniTask Init(string buttonName, Action onClick)
        {
            if (ButtonLabel == null)
            {
                Button = GetComponent<Button>();
                ButtonImage = GetComponent<Image>();
                ButtonLabel = Button.GetComponentInChildren<TMP_Text>();
            }

            if (ButtonLabel != null)
                ButtonLabel.text = buttonName;

            _onClick = onClick;
            await UniTask.CompletedTask;
        }

        protected virtual void OnClick()
        {
            _onClick?.Invoke();
        }

        public void UpdateButtonName(string buttonName)
        {
            if (ButtonLabel)
                ButtonLabel.text = buttonName;
        }

        public void UpdateButtonColor(Color buttonColor)
        {
            if (Button == null)
            {
                Button = GetComponent<Button>();
                ButtonImage = GetComponent<Image>();
                ButtonLabel = Button.GetComponentInChildren<TMP_Text>();
            }

            Button.image.color = buttonColor;
        }

        public void UpdateButtonInteractable(bool isInteractable)
        {
            Button.interactable = isInteractable;
        }

        public void UpdateButtonLabelColor(Color color)
        {
            if (ButtonLabel)
                ButtonLabel.color = color;
        }

        public void SetImageVisible(bool isVisible)
        {
            if (ButtonImage)
            {
                var transparentColor = ButtonImage.color;
                transparentColor.a = isVisible ? 1 : 0;
                ButtonImage.color = transparentColor;
            }
        }
    }
}