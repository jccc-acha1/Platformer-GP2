using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDeath : MonoBehaviour
{
    public GameObject Player;
    private Scene scene;

    void Start() {
        scene = SceneManager.GetActiveScene();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == Player) {
            UIHandler.pickupScore = 0;
            UIHandler.health = UIHandler.maxHealth;
            SceneManager.LoadScene(scene.name);
        }
    }
}
