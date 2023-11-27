using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    
    [SerializeField]
    private float moveSpeed = 1f;

    private Vector3 directionToPlayer;

    private Transform playerTransform;
    
    // Start is called before the first frame update
    void Start() 
    {
        
        playerTransform = GameObject.FindWithTag("Player").transform;

        /*
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // Rotate the enemy to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);
        */

        Vector3 playerPosition = playerTransform.transform.position;

        // Calculate the direction to the player
        Vector3 direction = playerPosition - transform.position;

        // Normalize the direction
        direction = direction.normalized;

        // Set the projectile's velocity
        GetComponent<Rigidbody>().velocity = direction * moveSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(directionToPlayer.normalized * moveSpeed * Time.deltaTime);
        //GetComponent<Rigidbody>().velocity = direction * moveSpeed;
        transform.rotation = Quaternion.LookRotation(GetComponent<Rigidbody>().velocity);

    }


    
    private void OnCollisionEnter(Collision other) {

        if (other.transform.tag != "Enemy") {
            Destroy(this.gameObject);
        }
        
    }
    
}
