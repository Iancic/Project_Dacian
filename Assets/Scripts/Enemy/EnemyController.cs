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

    private bool isProcessingCollision;

    public int damagePower = 1;
    public float attackCooldown = 2f;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isProcessingCollision)
        {
            return;
        }

        if (collision.CompareTag("King"))
        {
            isProcessingCollision = true;
            isMoving = false;
            animator.SetBool("isAttacking", true);
            StartCoroutine(Damage_King(collision));
        }

        if (collision.CompareTag("Unit"))
        {
            isProcessingCollision = true;
            isMoving = false;
            animator.SetBool("isAttacking", true);
            StartCoroutine(Damage_Unit(collision));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("King"))
        {
            isProcessingCollision = false;
            isMoving = true;
            StopCoroutine(Damage_King(collision));
        }

        if (collision.CompareTag("King"))
        {
            isProcessingCollision = false;
            isMoving = true;
            StopCoroutine(Damage_Unit(collision));
        }
    }

    IEnumerator Damage_King(Collider2D collision)
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, collision.gameObject.transform.position.y, this.gameObject.transform.position.z);

        KingController king_script = collision.gameObject.GetComponent<KingController>();

        while (king_script.hitpoints > 0)
        {
            king_script.hitpoints -= damagePower;

            yield return new WaitForSeconds(attackCooldown);
        }

        animator.SetBool("isAttacking", false);
    }

    IEnumerator Damage_Unit(Collider2D collision)
    {
        this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, collision.gameObject.transform.position.y, this.gameObject.transform.position.z);

        UnitRTS unit_script = collision.gameObject.GetComponent<UnitRTS>();

        while (unit_script.hitpoints > 0)
        {
            unit_script.isMoving = false;
            unit_script.hitpoints -= damagePower;

            yield return new WaitForSeconds(attackCooldown);
        }

        animator.SetBool("isAttacking", false);
    }

}
