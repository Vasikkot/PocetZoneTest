using System;
using UnityEngine;

public class EMove : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRadius = 5f; 
    public float stopDistance = 3f;
    public event Action CloseToPlayer; 
     private Transform player;
    [SerializeField] Rigidbody2D rb;
    
    private bool isPlayerInRange = false;

    private void Start()
    {
        player = FindAnyObjectByType<PMove>().GetComponent<Transform>();
    }
    void Update()
    {
        if (isPlayerInRange)
        {
            MoveTowardsPlayer();
        }
        else {
            rb.velocity = Vector3.zero;
        }
    }


    void MoveTowardsPlayer()
    {
        
        Vector2 direction = (player.position - transform.position).normalized;

       
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        
        if (distanceToPlayer > stopDistance)
        {
           
            rb.velocity = direction * moveSpeed;
        }
        else
        {
            
            rb.velocity = Vector2.zero;

            
            CloseToPlayer?.Invoke();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true; 
        }
    }

    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
        }
    }

  
}