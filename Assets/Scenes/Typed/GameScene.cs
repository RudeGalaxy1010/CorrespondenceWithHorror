//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IJunior.TypedScenes
{
    using UnityEngine.SceneManagement;
    
    
    public class GameScene : TypedScene
    {
        
        private const string _sceneName = "GameScene";
        
        public static void Load(QuestData argument, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            LoadScene(_sceneName, loadSceneMode, argument);
        }
        
        public static UnityEngine.AsyncOperation LoadAsync(QuestData argument, LoadSceneMode loadSceneMode = LoadSceneMode.Single)
        {
            return LoadScene(_sceneName, loadSceneMode, argument);
        }
    }
}
