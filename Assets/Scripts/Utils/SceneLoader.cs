using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace kingcrimson.utils
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private string m_sceneToLoad;
        public void LoadScene()
        {
            SceneManager.LoadScene(m_sceneToLoad);
        }

        public void Quit()
        {
            Application.Quit();   
        }
    }
}