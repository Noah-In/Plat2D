using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] int damagePoints;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Life>())
        {
            collision.gameObject.GetComponent<Life>().healthPoints -= damagePoints;

            if (collision.gameObject.GetComponent<HitEffects>())
            {
                collision.gameObject.GetComponent<HitEffects>().Effects();
            }
        }
    }
} 
