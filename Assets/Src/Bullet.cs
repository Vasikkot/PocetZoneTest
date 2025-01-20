using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] public float BulletsSpeed;
    [SerializeField] float lifetime;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HP hP))
        {

            hP.TakeDmg();
            Destroy(gameObject);
        }

        Destroy(gameObject);

    }




}
