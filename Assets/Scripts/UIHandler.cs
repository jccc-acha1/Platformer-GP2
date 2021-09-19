using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public Text PickupText;
    public Text HealthText;
    public Text MaxHealthText;

    public static int pickupScore;
    public static int health = 3;
    public static int maxHealth = 3;

    // Update is called once per frame
    void Update()
    {
        PickupText.text = pickupScore.ToString();
        HealthText.text = health.ToString();
        MaxHealthText.text = maxHealth.ToString();
    }
}
