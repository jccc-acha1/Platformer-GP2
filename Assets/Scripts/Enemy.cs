using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public GameObject Player;
    public Scene scene;
    public int hurtAmount;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == Player) {
           UIHandler.health -= hurtAmount;
           print("Enemy Hurt OW!");
        }
    } 

    void Start() {
        scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (UIHandler.health < 1) {
            print("lol you died");
            UIHandler.pickupScore = 0;
            UIHandler.health = UIHandler.maxHealth;
            SceneManager.LoadScene(scene.name);
        }
    }
}
