using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitbox : MonoBehaviour
{
    private List<Collider> colliders = new List<Collider>();
    public List<Collider> GetColliders () { return colliders; }

    private void OnTriggerEnter (Collider other) {
        if (other.transform.tag == "Enemy") {
            if (!colliders.Contains(other)) { colliders.Add(other); }
        }
    }

    private void OnTriggerExit (Collider other) {
        colliders.Remove(other);
    }

    public void melee(int damage) {
        Debug.Log("Melee");
        foreach (Collider c in colliders) {
            if (c != null && c.transform.tag == "Enemy") {
                Debug.Log(c.transform.tag);
                c.gameObject.GetComponent<EnemyHealthManager>().takeDamage(damage);
                //GameObject particleSystem = Instantiate(particleSystemPrefab, hit.point, Quaternion.identity);
                //Destroy(particleSystem, 2.0f); // Destroy the particle system after 2 seconds
            }
        }
    }

}
