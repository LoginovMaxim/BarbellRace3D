using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Monos
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        private void ReloadScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        private void LoadScene(string sceneName)
        {
            StartCoroutine(LoadingScene(sceneName));
        }
        
        private void LoadScene(int sceneIndex)
        {
            StartCoroutine(LoadingScene(sceneIndex));
        }
        
        private IEnumerator LoadingScene(string sceneName)
        {
            yield return Loading(SceneManager.LoadSceneAsync(sceneName));
        }
        
        private IEnumerator LoadingScene(int index)
        {
            yield return Loading(SceneManager.LoadSceneAsync(index));
        }

        private IEnumerator Loading(AsyncOperation asyncOperation)
        {
            asyncOperation.allowSceneActivation = false;

            while (!asyncOperation.isDone)
            {
                yield return null;
            }

            asyncOperation.allowSceneActivation = true;
            yield return null;
        }

        #region ISceneLoader
        
        void ISceneLoader.ReloadScene()
        {
            ReloadScene();
        }
        
        void ISceneLoader.LoadScene(string sceneName)
        {
            LoadScene(sceneName);
        }

        void ISceneLoader.LoadScene(int sceneIndex)
        {
            LoadScene(sceneIndex);
        }

        #endregion
    }
}