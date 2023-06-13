using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TestWork.UI
{
    [CreateAssetMenu(fileName = "ViewConfig", menuName = "Configs/GameViewConfig")]
    public class GameViewConfig : ScriptableObject
    {
        public GameObject Prefab;
        public UIViewType ViewType;
    }
}
