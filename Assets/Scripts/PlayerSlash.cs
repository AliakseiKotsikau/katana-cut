using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private Sprite defaultSprite;

    private SpriteRenderer spriteRenderer;
    private PlayerController playerController;
    private Animator animator;

    private Vector3[] points = new Vector3[0];
    private int currentPointIndex = 0;

    private bool facingRight;

    private float timeAfterDelay = 0f;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

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
        playerController.enabled = true;
        animator.enabled = true;
        currentPointIndex = 0;
        points = new Vector3[0];
        spriteRenderer.sprite = defaultSprite;
    }

    public void FollowLine(Vector3[] points)
    {
        spriteRenderer.sprite = attackSprite;
        playerController.enabled = false;
        //animator.enabled = false;
        this.points = points;
    }

    public void SetAttackPreparationSprite()
    {
        spriteRenderer.sprite = prepareSprite;
        animator.enabled = false;
    }
}
