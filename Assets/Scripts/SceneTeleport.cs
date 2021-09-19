using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleport : MonoBehaviour
{
    public GameObject Player;
    int nextSceneIndex;

    void Start()
    {
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == Player) {
            if (SceneManager.sceneCountInBuildSettings > nextSceneIndex) {
                SceneManager.LoadScene(nextSceneIndex);
            }
            print("teleporting to new scene . . . ");
        }
    }
}
