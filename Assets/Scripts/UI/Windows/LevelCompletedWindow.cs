using Abstracts;
using Cysharp.Threading.Tasks;
using Helpers;
using TMPro;
using UI.Widgets;
using UI.WindowParameters;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Windows
{
    public class LevelCompletedWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _levelCompletedLabel;
        [SerializeField] private ButtonWidget _nextLevelButton;

        private LevelCompletedWindowParameters _parameters;
        
        private readonly string _nextLevelButtonKey = "Next Level";
        private readonly string _levelKey = "Level";
        private readonly string _levelCompletedKey = "Completed";
        
        protected async override UniTask OnInit(BaseWindowParameters parameters = null)
        {
            _parameters = parameters as LevelCompletedWindowParameters;
            if (_parameters == null)
            {
                LoggerUtil.LogError("LevelCompletedWindow: Parameters are null");
                return;
            }

            var levelCompletedText = $"{_levelKey} {_parameters.LevelData.LevelIndex + 1} {_levelCompletedKey}";
            _levelCompletedLabel.text = levelCompletedText;
            
            _nextLevelButton.Init(_nextLevelButtonKey, OnNextLevelButtonClick);
            await UniTask.CompletedTask;
        }

        public async override UniTask OnHide()
        {
            await UniTask.CompletedTask;
        }

        private void OnNextLevelButtonClick()
        {
            Hide();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}