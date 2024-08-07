using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffects : MonoBehaviour
{
    [SerializeField] float multiplicadorEscalaX;
    [SerializeField] float scaleDuration;

    [SerializeField] SpriteRenderer spriteRend;
    [SerializeField] float colorDuration;
    [SerializeField] Color color;

    Vector3 originScale;
    // Start is called before the first frame update
    void Start()
    {
        originScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Effects()
    {
        StartCoroutine(Scale());
        StartCoroutine(SpriteColor());
    }

    IEnumerator Scale()
    {
        transform.localScale *= multiplicadorEscalaX;
        yield return new WaitForSeconds(scaleDuration);
        transform.localScale = originScale;
    }

    IEnumerator SpriteColor()
    {
        spriteRend.color = color;
        yield return new WaitForSeconds(colorDuration);
        spriteRend.color = Color.white;

    }
}
