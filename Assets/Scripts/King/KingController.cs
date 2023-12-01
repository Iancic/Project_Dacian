using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KingController : MonoBehaviour
{
    private float speed = 0.75f;
    private float range = 0.1f;
    private float maxDistance = 3;
    private Vector2 waypoint;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Coroutine moveCoroutine;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        moveCoroutine = StartCoroutine(SetNewDestination());
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, waypoint) > range)
        {
            // Move towards the waypoint
            transform.position = Vector2.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);

            // Check direction for sprite flipping
            if (waypoint.x > transform.position.x)
            {
                spriteRenderer.flipX = false; // Moving right
            }
            else if (waypoint.x < transform.position.x)
            {
                spriteRenderer.flipX = true; // Moving left
            }

            // Set walking animation
            animator.SetBool("isWalking", true);
        }
        else if (moveCoroutine == null)
        {
            // Stop walking animation and set new destination
            animator.SetBool("isWalking", false);
            moveCoroutine = StartCoroutine(SetNewDestination());
        }
    }

    private IEnumerator SetNewDestination()
    {
        // Gain currency (if this is part of your game logic)
        CurrencyController.Instance.ExchangeCoins(+25);

        // Wait for a random time before setting a new destination
        yield return new WaitForSeconds(Random.Range(3f, 5f));

        // Set a new random waypoint within the specified range
        waypoint = new Vector2(Random.Range(-maxDistance, maxDistance), Random.Range(-maxDistance, maxDistance));

        // Reset coroutine reference
        moveCoroutine = null;
    }
}