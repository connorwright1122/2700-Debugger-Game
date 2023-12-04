using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{
    public int health = 100;

    private GameObject player;

    public bool isBoss = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage) {
        health -= damage;

        if (health <= 0) {
            player.GetComponent<PlayerHealth>().RestoreHealth(10);
            if (isBoss) {
                player.GetComponentInChildren<LogicScript>().GameWin();
            }
            Destroy(this.gameObject);
        }
    }
}
