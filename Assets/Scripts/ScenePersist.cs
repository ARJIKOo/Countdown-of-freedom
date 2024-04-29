
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    private void Awake()
    {
        int numGamePersist = FindObjectsOfType<ScenePersist>().Length;

        if (numGamePersist > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public void ResetScenepersist()
    {
        Destroy(gameObject);
    }
}
