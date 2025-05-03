using Abstracts;
using Addressables;
using Cysharp.Threading.Tasks;
using EventBus.Events;
using EventBusSystem;
using Helpers;
using Interfaces;
using TMPro;
using UI.Widgets;
using UnityEngine;
using UnityEngine.U2D;

namespace UI.Windows
{
    public class GameplayWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _currentMovesLabel;
        [SerializeField] private TMP_Text _currentLevelLabel;
        [SerializeField] private Transform _objectivesParent;
        [SerializeField] private ObjectiveWidget _objectiveWidgetPf;

        private ILevelData _currentLevelData;
        private SpriteAtlas _objectiveSpriteAtlas;

        private readonly string _levelKey = "Level";

        private void OnEnable()
        {
            EventBusNew.Subscribe<MoveUsedUIEvent>(OnMoveUsed);
        }

        private void OnDisable()
        {
            EventBusNew.Unsubscribe<MoveUsedUIEvent>(OnMoveUsed);
        }

        protected async override UniTask OnInit(BaseWindowParameters parameters = null)
        {
            _currentLevelData = EventBusNew.RaiseWithResult<GetCurrentLevelDataEvent, ILevelData>(new GetCurrentLevelDataEvent());
            if (_currentLevelData == null)
            {
                LoggerUtil.LogError("Current level is null!");
                return;
            }

            _objectiveSpriteAtlas =
                await AddressablesLoader.LoadAssetAsync<SpriteAtlas>(AddressablesKeys.GetKey(AddressablesKeys.AssetKeys.SA_Set1TileElements));
            await CreateObjectives();

            var currentMovesText = _currentLevelData.MoveAmount.ToString();
            ;
            var currentLevelText = $"{_levelKey} {_currentLevelData.LevelIndex + 1}";
            _currentMovesLabel.text = currentMovesText;
            _currentLevelLabel.text = currentLevelText;

            await UniTask.CompletedTask;
        }

        private async UniTask CreateObjectives()
        {
            foreach (var objective in _currentLevelData.LevelObjectives)
            {
                var objectiveWidget = Instantiate(_objectiveWidgetPf, _objectivesParent);
                await objectiveWidget.Init(objective,
                    _objectiveSpriteAtlas.GetSprite(objective.ObjectiveType.ToString()));
            }
        }

        private void OnMoveUsed(MoveUsedUIEvent @event)
        {
            _currentMovesLabel.text = @event.RemainingMoves.ToString();
        }

        public async override UniTask OnHide()
        {
            ClearObjectives();
            await UniTask.CompletedTask;
        }

        private void ClearObjectives()
        {
            foreach (Transform child in _objectivesParent)
                Destroy(child.gameObject);
        }
    }
}