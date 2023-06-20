using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {

        Transform hitTransform = collision.transform;

        if (hitTransform.CompareTag("Player"))
        {
            Debug.Log("hit player");
            hitTransform.GetComponent<PlayerHealth>().takeDamage(10);
        }
        if (hitTransform.CompareTag("Enemy"))
        {
            Debug.Log("hit enemy");
            //implement enemy health and health bar animation

        }
        Destroy(gameObject);
    }
}
