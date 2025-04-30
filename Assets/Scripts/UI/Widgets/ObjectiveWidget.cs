using System;
using Abstracts;
using Cysharp.Threading.Tasks;
using EventBus;
using EventBus.Events;
using Interfaces;
using Miscs;
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
        private EventBinding<UpdateLevelObjectiveUIEvent> _updateLevelObjectiveUIEventBinding;
        private readonly string _finishedText = "OK";

        private void OnEnable()
        {
            _updateLevelObjectiveUIEventBinding = new EventBinding<UpdateLevelObjectiveUIEvent>(UpdateObjective);
            EventBus<UpdateLevelObjectiveUIEvent>.Register(_updateLevelObjectiveUIEventBinding);
        }

        private void OnDisable()
        {
            EventBus<UpdateLevelObjectiveUIEvent>.Deregister(_updateLevelObjectiveUIEventBinding);
        }

        public async UniTask Init(ILevelObjectiveData objectiveData, Sprite objectiveIcon)
        {
            _objectiveData = objectiveData;
            _objectiveIcon.sprite = objectiveIcon;
            var remainingAmount = objectiveData.ObjectiveAmount;
            _objectiveRemainingAmountLabel.text = remainingAmount > 0 ? objectiveData.ObjectiveAmount.ToString() : _finishedText;
            await UniTask.CompletedTask;
        }

        private void UpdateObjective(UpdateLevelObjectiveUIEvent updateLevelObjectiveUIEvent)
        {
            if (updateLevelObjectiveUIEvent.ObjectiveType == _objectiveData.ObjectiveType)
            {
                _objectiveRemainingAmountLabel.text = updateLevelObjectiveUIEvent.RemainingAmount.ToString();
            }
        }
    }
}