using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PShoot : MonoBehaviour
{
    [SerializeField] GameObject Bullet;
    [SerializeField] float reloadTime;
    [SerializeField] float detectionRadius;

    public event Action Shooted;
    private bool isShooting = false;
    void Start()
    {
        UIManager.instance.ShootBtnPressed += ShootOut;
        Debug.Log(gameObject.name);
    }
    private void OnDestroy()
    {
        UIManager.instance.ShootBtnPressed -= ShootOut;
    }
    private void ShootOut()
    {
        if (!isShooting)
        {
            StartCoroutine(Soot());
        }
        else
        {
            Debug.Log("Reloading...");
        }
    }

    private IEnumerator Soot()
    {
        var target = FindClosestEnemy();
        if (target != null)
        {
            
            if (!InventoryData.instance.RemoveItem(4)) 
            {
                Debug.Log("No bullets!");
                yield break;
            }

            isShooting = true;


            var bullet = Instantiate(Bullet, transform.position, Quaternion.identity);
            Vector2 direction = (target.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bullet.GetComponent<Bullet>().BulletsSpeed;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle + 90);

            Shooted?.Invoke(); 

            yield return new WaitForSeconds(reloadTime);

            isShooting = false;
        }
        else
        {
            Debug.Log("No target in range.");
        }
    }
    private Transform FindClosestEnemy()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, detectionRadius/2);
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (var hit in hitEnemies)
        {
            if (hit.CompareTag("Enemy")) 
            {
                float distance = Vector2.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = hit.transform;
                }
            }
        }
        return closestEnemy;
    }
    
}
