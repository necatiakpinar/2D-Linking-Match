using Abstracts;
using Cysharp.Threading.Tasks;
using EventBus.Events;
using EventBusSystem;
using Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Widgets
{
    public class ObjectiveWidget : BaseWidget
    {
        [SerializeField] private Image _objectiveIcon;
        [SerializeField] private TMP_Text _objectiveRemainingAmountLabel;

        private ILevelObjectiveData _objectiveData;
        private readonly string _finishedText = "OK";

        private void OnEnable()
        {
            EventBusNew.Subscribe<UpdateLevelObjectiveUIEvent>(UpdateObjective);
        }

        private void OnDisable()
        {
            EventBusNew.Unsubscribe<UpdateLevelObjectiveUIEvent>(UpdateObjective);
        }

        public async UniTask Init(ILevelObjectiveData objectiveData, Sprite objectiveIcon)
        {
            _objectiveData = objectiveData;
            _objectiveIcon.sprite = objectiveIcon;
            var remainingAmount = objectiveData.ObjectiveAmount;
            var amountText = remainingAmount > 0 ? objectiveData.ObjectiveAmount.ToString() : _finishedText;
            SetAmountLabel(amountText);
            await UniTask.CompletedTask;
        }

        private void UpdateObjective(UpdateLevelObjectiveUIEvent updateLevelObjectiveUIEvent)
        {
            if (updateLevelObjectiveUIEvent.ObjectiveType == _objectiveData.ObjectiveType)
            {
                var remainingAmount = updateLevelObjectiveUIEvent.RemainingAmount;
                var amountText = remainingAmount > 0 ? remainingAmount.ToString() : _finishedText;
                SetAmountLabel(amountText);
            }
        }

        private void SetAmountLabel(string amountText)
        {
            _objectiveRemainingAmountLabel.text = amountText;
        }
    }
}