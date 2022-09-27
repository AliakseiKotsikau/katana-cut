using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerSlash : MonoBehaviour
{
    [Header("Slash Settings")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float delayOnAngles = 0.2f;

    [Header("Sprites")]
    [SerializeField]
    private Sprite prepareSprite;
    [SerializeField]
    private Sprite attackSprite;

    [Header("Collisions")]
    [SerializeField]
    private LayerMask groundLayer;

    public Action SlashFinished;

    private Sprite defaultSprite;

    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private Animator animator;
    private Rigidbody2D rb;

    private Vector3[] points = new Vector3[0];
    private int currentPointIndex = 0;

    private bool facingRight;

    private float timeAfterDelay = 0f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        defaultSprite = spriteRenderer.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (points.Length == 0) return;

        if (transform.position == points[currentPointIndex])
        {
            currentPointIndex++;
            timeAfterDelay = Time.time + delayOnAngles;

            if (currentPointIndex >= points.Length)
            {
                ResetDefaultSetting();
                return;
            }

            if ((points[currentPointIndex].x > transform.position.x && !facingRight) || (points[currentPointIndex].x < transform.position.x && facingRight))
            {
                Flip();
            }
        }

        if(Physics2D.Raycast(transform.position, points[currentPointIndex] - transform.position, 1f, groundLayer))
        {
            ResetDefaultSetting();
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, points[currentPointIndex], speed * Time.deltaTime);
    }

    private void Flip()
    {
        if ((points[currentPointIndex].x > transform.position.x && !facingRight) || (points[currentPointIndex].x < transform.position.x && facingRight))
        {
            facingRight = !facingRight;

            transform.rotation = Quaternion.Euler(0, facingRight ? 0 : 180, 0);
        }
    }

    private void ResetDefaultSetting()
    {
        //AddImpulse();
        playerController.enabled = true;
        animator.enabled = true;
        currentPointIndex = 0;
        points = new Vector3[0];
        spriteRenderer.sprite = defaultSprite;

        SlashFinished?.Invoke();
    }

    public void FollowLine(Vector3[] points)
    {
        spriteRenderer.sprite = attackSprite;
        playerController.enabled = false;
        this.points = points;
    }

    public void SetAttackPreparationSprite()
    {
        spriteRenderer.sprite = prepareSprite;
        animator.enabled = false;
    }

    private void AddImpulse()
    {
        Vector2 direction = points[points.Length - 1] - points[points.Length - 2];
        rb.AddForce(direction.normalized * speed, ForceMode2D.Impulse);
    }
}
