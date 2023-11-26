using UnityEngine;

public class BasicEnemyAI : MonoBehaviour {

    public float moveSpeed = 2.0f;
    public float rotationSpeed = 100.0f;


    private Transform playerTransform;
    //private Animator animator;

    void Start() {
        playerTransform = GameObject.FindWithTag("Player").transform;
        //animator = GetComponent<Animator>();
    }

    void Update() {
        // Calculate the direction to the player
        Vector3 directionToPlayer = playerTransform.position - transform.position;

        // Rotate the enemy to look at the player
        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Move the enemy towards the player
        transform.Translate(directionToPlayer.normalized * moveSpeed * Time.deltaTime);

        // Update the animator based on the enemy's movement
        /*
        if (directionToPlayer.magnitude > 0.1f) {
            animator.SetBool("IsMoving", true);
        } else {
            animator.SetBool("IsMoving", false);
        }
        */
    }

    

}
