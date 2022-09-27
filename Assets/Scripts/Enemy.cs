using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Sprite bloodSprite;
    [SerializeField]
    private float bloodDuration;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.Instance.PlayEnemyDeathSound(transform.position);
        StartCoroutine(ShowBlood());
    }

    private IEnumerator ShowBlood()
    {
        spriteRenderer.sprite = bloodSprite;
        float time = 0f;

        while(time / bloodDuration < 1)
        {
            Color color = spriteRenderer.color;
            color.a = 1 - time / bloodDuration;
            spriteRenderer.color = color;

            time += Time.deltaTime;

            yield return new WaitForEndOfFrame();
        }

        //yield return new WaitForSeconds(bloodDuration);

        Destroy(gameObject);
    }
}
