using UnityEngine;
using UnityEngine.UI;
using TestWork.Core;
using TestWork.SceneManagement;

namespace TestWork.UI
{
    public class MainMenuView : MonoBehaviour, IGameView
    {
        private const UIViewType VIEW_TYPE = UIViewType.MainMenu;

        [SerializeField] private Button _playButton;
        [SerializeField] private Button _quitButton;

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