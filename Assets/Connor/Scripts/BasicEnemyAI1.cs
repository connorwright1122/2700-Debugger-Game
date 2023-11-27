using UnityEngine;

public class BasicEnemyAI1 : MonoBehaviour {

    public float moveSpeed = 2.0f;
    public float rotationSpeed = 100.0f;

    [SerializeField]private float _shootDelay = 1f;
    private float lastShoot = 0f;

    [SerializeField]
    public GameObject projectile;


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

        //Vector3 maxDistance = new Vector3(5, 5, 5);

        //if (this.transform.position.x - playerTransform.transform.position.x > maxDistance.x) {
            //transform.Translate(directionToPlayer.normalized * moveSpeed * Time.deltaTime);
        //} 



        if (Time.time - lastShoot > _shootDelay) {
            lastShoot = Time.time;
            Vector3 newPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + 1);
            GameObject projectile1 = Instantiate(projectile, newPos, Quaternion.identity);
        }



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
