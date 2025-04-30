using Abstracts;
using Addressables;
using Cysharp.Threading.Tasks;
using EventBus;
using EventBus.Events;
using Interfaces;
using UI.Widgets;
using UnityEngine;
using UnityEngine.U2D;

namespace UI.Windows
{
    public class GameplayWindow : BaseWindow
    {
        [SerializeField] private Transform _objectivesParent;
        [SerializeField] private ObjectiveWidget _objectiveWidgetPf;

        private ILevelData _currentLevelData;
        private SpriteAtlas _objectiveSpriteAtlas;
        protected async override UniTask OnInit(BaseWindowParameters parameters = null)
        {
            _currentLevelData = EventBus<GetCurrentLevelData, ILevelData>.Raise(new GetCurrentLevelData())[0];
            _objectiveSpriteAtlas =
                await AddressablesLoader.LoadAssetAsync<SpriteAtlas>(AddressablesKeys.GetKey(AddressablesKeys.AssetKeys.SA_Set1TileElements));
            await CreateObjectives();
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

        public async override UniTask OnHide()
        {
            await UniTask.CompletedTask;
        }
    }
}