using System;
using System.Collections.Generic;
using UnityEngine;

namespace TestWork.SceneManagement
{
    [CreateAssetMenu(fileName = "SceneConfig", menuName = "Configs/SceneConfig")]
    public class ScenesConfiguration : ScriptableObject
    {
        public List<SceneData> Scenes;
    }

    [Serializable]
    public class SceneData
    {
        public string SceneName;
        public int BuildIndex;
        public bool SpawnPlayer;
        public bool SpawnEnemies;
    }
}