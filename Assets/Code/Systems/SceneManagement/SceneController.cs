using UnityEngine;
using TestWork.Core;
using UnityEngine.SceneManagement;
using TestWork.Characters;

namespace TestWork.SceneManagement {
    public class SceneController : MonoBehaviour, IGameSystem
    {
        [SerializeField] private ScenesConfiguration _config;

        public void Init()
        {

        }

        public void LoadScene(string name)
        {
            SceneData data = _config.Scenes.Find(x => x.SceneName == name);

            if (data != null)
            {
                SceneManager.LoadScene(data.BuildIndex);

                if(data.SpawnPlayer)
                    CoreGameManager.GetInstance<PlayerController>().SpawnPlayer();

                if (data.SpawnEnemies)
                    CoreGameManager.GetInstance<EnemyController>().StartSpawning();
            }
            else
            {
                Debug.LogError("Error when loading new scene");
            }
        }
    }
}