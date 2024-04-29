using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


public class NextLevelsExit : MonoBehaviour
{
    [SerializeField] private float exitTime;


    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(NextLevelExit());
        AudioManager.Instance.PlaySFX("NextLevel");
        //AudioManager.Instance.musicSource.Stop();
    }


    IEnumerator NextLevelExit()
 {
     yield return new WaitForSecondsRealtime(exitTime);

     int correctSceneIndex = SceneManager.GetActiveScene().buildIndex;
     int nextSceneIndex = correctSceneIndex + 1;

     if (correctSceneIndex > SceneManager.sceneCountInBuildSettings)
     {
         nextSceneIndex = 0;
     }
     FindObjectOfType<ScenePersist>().ResetScenepersist();
     SceneManager.LoadScene(nextSceneIndex);
 }

}
