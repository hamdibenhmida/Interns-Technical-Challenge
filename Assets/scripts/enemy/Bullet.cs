using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        Transform hitTransform = collision.transform;
            Debug.Log(hitTransform.gameObject.name);

        if (hitTransform.CompareTag("Player"))
        {
            hitTransform.GetComponent<PlayerHealth>().takeDamage(10);
        }
        if (hitTransform.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
            hitTransform.GetComponent<EnemyHealth>().takeDamage(100);

        }
        Destroy(gameObject);
    }
}
