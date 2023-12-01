using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public bool isMoving = true;

    private float moveSpeed = 0.5f;

    private Vector3 targetPosition;

    private GameObject king;
    public Image healthBarImage;

    public int hitpoints = 3, maxHitpoints;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        maxHitpoints = hitpoints;
        king = GameObject.Find("King");
    }

    void Update()
    {
        float fillAmount = (float)hitpoints / maxHitpoints;
        healthBarImage.fillAmount = fillAmount;

        targetPosition = king.transform.position;

        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
        else if (isMoving == false)
        {
            animator.SetBool("isMovingAnim", false);
        }

        if (targetPosition.x > transform.position.x)
        {
            spriteRenderer.flipX = false; // Moving right
        }
        else if (targetPosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true; // Moving left
        }

        if (hitpoints <= 0)
        {
            animator.SetBool("Death", true);
            Destroy(this.gameObject, 0.5f);
            //as long as death animation
        }
    }

}
