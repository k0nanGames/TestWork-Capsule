using UnityEngine;
using TestWork.Core;

namespace TestWork.UI {
    public class UIManager : MonoBehaviour, IGameSystem
    {
        private const string UI_DIRECTORY_PREFIX = "UI/";

        [SerializeField] private Transform _mainCanvas;

        private GameObject _spawnedObject;

        public void Init()
        {
            Show(UIViewType.MainMenu);
        }

        public void Show(UIViewType view)
        {
            if (_spawnedObject != null)
                CloseCurrentView();

            GameViewConfig config = Resources.Load<GameViewConfig>(UI_DIRECTORY_PREFIX + view.ToString());
            _spawnedObject = Instantiate(config.Prefab, _mainCanvas);

            if(_spawnedObject.TryGetComponent(out IGameView spawnedView))
            {
                spawnedView.Init();
                spawnedView.Show();

                Debug.Log("Spawned new view");
            }
            else
            {
                Destroy(_spawnedObject);
                _spawnedObject = null;
                Debug.LogError("Error when spawned new view");
            }
        }

        public void CloseCurrentView()
        {
            if (_spawnedObject != null)
            {
                Destroy(_spawnedObject);
                _spawnedObject = null;
            }
        }
    }
}