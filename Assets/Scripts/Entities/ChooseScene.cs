using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // Importa el SceneManager
using UnityEditor;  // Necesario para utilizar SceneAsset

[CreateAssetMenu(fileName = "NewChooseScene", menuName = "Data/New Choose Scene")]
[System.Serializable]
public class ChooseScene : GameScene
{
    public List<ChooseLabel> labels;
    
    [System.Serializable]
    public struct ChooseLabel
    {
        public string text;
        public StoryScene nextScene;
    }
}
