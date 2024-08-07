using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firepoint;
    [SerializeField] float firerate;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Shooting());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Shooting()
    {
        while (true) 
        {
            yield return new WaitForSeconds(firerate);
            // Atirar
            Instantiate(bulletPrefab, firepoint.position, transform.rotation);
            Debug.Log("Pew");
        }
    }
}
