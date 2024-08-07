using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    Slider slider;
    Life life;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        life = FindObjectOfType<Player>().GetComponent<Life>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Lifes: " + life.healthPoints);
        slider.value = life.healthPoints;
    }
}
