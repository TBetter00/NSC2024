using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public NavMeshAgent agent;

    public GameObject player1;
    public GameObject player2;

    public LayerMask whatIsGround, whatIsPlayer;

    public GameObject bullet;

    // Patrol
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attack
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // State
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        // Ensure the agent updates correctly for 2D
        agent.updateRotation = false;  // Disable automatic rotation updates
        agent.updateUpAxis = false;    // Disable automatic up axis updates
    }

    void Update()
    {
        // Constrain the agent to the XY plane
        Vector3 position = agent.transform.position;
        position.z = 0;
        agent.transform.position = position;

        Transform nearestPlayer = GetNearestPlayer();

        if (nearestPlayer != null)
        {
            playerInSightRange = Physics2D.OverlapCircle(transform.position, sightRange, whatIsPlayer);
            playerInAttackRange = Physics2D.OverlapCircle(transform.position, attackRange, whatIsPlayer);
            
            // Uncomment these lines to enable patrolling and attacking behavior
            if (!playerInSightRange && !playerInAttackRange) Patrolling(); Debug.Log("Patrol");
            if (playerInSightRange && !playerInAttackRange) ChasePlayer(nearestPlayer); Debug.Log("Chase");
            if (playerInAttackRange && playerInSightRange) Attacking(nearestPlayer); Debug.Log("Attack");

            // For testing purposes, we are always in patrolling state
        }
    }

    void Patrolling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            Debug.Log("Patrolling to: " + walkPoint);
        }
        else
        {
            Debug.Log("Searching for walk point.");
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        // Walkpoint reached
        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
            Debug.Log("Reached walk point.");
        }
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomY = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + randomY, 0);

        // Adjust the length of the raycast
        float raycastLength = 5f;

        // Draw the raycast in the scene for debugging purposes
        Debug.DrawRay(walkPoint, Vector2.down * raycastLength, Color.red, 2f);

        // Perform the raycast and check if it hits the ground layer
        RaycastHit2D hit = Physics2D.Raycast(walkPoint, Vector2.down, raycastLength, whatIsGround);
        if (hit.collider != null)
        {
            walkPointSet = true;
            Debug.Log("Found new walk point: " + walkPoint);
        }
        else
        {
            Debug.Log("Failed to find a valid walk point at position: " + walkPoint + " with direction: " + Vector2.down);
        }
    }

    void ChasePlayer(Transform targetPlayer)
    {
        agent.SetDestination(targetPlayer.position);
    }

    void Attacking(Transform targetPlayer)
    {
        agent.SetDestination(transform.position);

        Vector3 direction = (targetPlayer.position - transform.position).normalized;
        transform.up = direction;

        if (!alreadyAttacked)
        {
            GameObject bulletInstance = Instantiate(bullet, transform.position, Quaternion.identity);
            Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
            rb.AddForce(direction * 12f, ForceMode2D.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

    private Transform GetNearestPlayer()
    {
        float distanceToPlayer1 = Vector3.Distance(transform.position, player1.transform.position);
        float distanceToPlayer2 = Vector3.Distance(transform.position, player2.transform.position);

        return distanceToPlayer1 < distanceToPlayer2 ? player1.transform : player2.transform;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
