using UnityEngine;
using UnityEngine.UI;
using TestWork.Core;
using TestWork.SceneManagement;
using TMPro;
using TestWork.Characters;

namespace TestWork.UI
{
    public class PlayerDeadView : MonoBehaviour, IGameView
    {
        private const UIViewType VIEW_TYPE = UIViewType.PlayerDead;

        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private TextMeshProUGUI _bestResult;

        public UIViewType GetViewType()
        {
            return VIEW_TYPE;
        }

        public void Init()
        {
            _playButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();

            _playButton.onClick.AddListener(OnPlayClicked);
            _quitButton.onClick.AddListener(OnQuitClicked);
        }

        public void Show()
        {
            CoreGameManager.GetInstance<SceneController>().LoadScene("menu");

            int currentSecondsResult = (int)CoreGameManager.GetInstance<EnemyController>().GetTimeAlive();
            int bestSecondsResult = (int)CoreGameManager.GetInstance<DataManager>().GetBestTime();

            _bestResult.text = "Best time is: " + bestSecondsResult + " seconds.";
            _text.text = "Your time is: " + currentSecondsResult + " seconds.";
            
        }

        public void Hide()
        {

        }

        private void OnPlayClicked()
        {
            CoreGameManager.GetInstance<SceneController>().LoadScene("game");
            CoreGameManager.GetInstance<UIManager>().Show(UIViewType.InGame);
        }

        private void OnQuitClicked()
        {
            Application.Quit();
        }
    }
}