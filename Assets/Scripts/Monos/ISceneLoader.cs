namespace Monos
{
    public interface ISceneLoader
    {
        void ReloadScene();
        void LoadScene(string sceneName);
        void LoadScene(int sceneIndex);
    }
}