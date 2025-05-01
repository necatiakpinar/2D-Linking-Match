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
    public class LevelFailedWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _levelFailedLabel;
        [SerializeField] private ButtonWidget _restartLevelButton;

        private LevelFailedWindowParameters _parameters;
        
        private readonly string _restartButtonKey = "Restart Level";
        private readonly string _levelKey = "Level";
        private readonly string _levelFailedKey = "Failed";
        
        protected async override UniTask OnInit(BaseWindowParameters parameters = null)
        {
            _parameters = parameters as LevelFailedWindowParameters;
            if (_parameters == null)
            {
                LoggerUtil.LogError("LevelFailedWindow: Parameters are null");
                return;
            }

            var levelFailedText = $"{_levelKey} {_parameters.LevelData.LevelIndex + 1} {_levelFailedKey}";
            _levelFailedLabel.text = levelFailedText;
            
            await _restartLevelButton.Init(_restartButtonKey, OnRestartLevelButtonClick);
            await UniTask.CompletedTask;
        }

        public async override UniTask OnHide()
        {
            await UniTask.CompletedTask;
        }

        private void OnRestartLevelButtonClick()
        {
            // Logic to load the next level
            Hide();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}