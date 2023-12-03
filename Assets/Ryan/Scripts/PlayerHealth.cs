using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private float health;
    private float pwr;
    private float lerpTimer;
    private LogicScript logic;
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public float maxPWR = 100f;
    public Image frontHealthBar;
    public Image backHealthBar;
    public Image frontPWRBar;
    public Image backPWRBar;
    // Start is called before the first frame update
    void Start()
    {
    
        health = maxHealth;
        pwr = 0;
        GameObject logicGameObject = GameObject.FindWithTag("Logic");
        if (logicGameObject == null)
        {
            Debug.Log("The logic game object tag cant be found");
        }

        // Get the LogicScript component attached to the "Logic" GameObject
        logic = logicGameObject.GetComponent<LogicScript>();
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        pwr = Mathf.Clamp(pwr, 0, maxPWR);
        UpdateHealthUI();
        UpdatePowerUpBar();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            TakeDamage(Random.Range(5, 10));
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            RestoreHealth(Random.Range(5, 10));
        }

        /* if (Input.GetKeyDown(KeyCode.R))
        {
            GainPWR(Random.Range(5, 10));
        } */

        if (health == 0)
        {
            Debug.Log("health is 0");
            logic.GameOver();
        }
    }
    public void UpdateHealthUI() 
    {
        Debug.Log(health);
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillBack > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }
        if (fillFront < hFraction)
        {
            backHealthBar.color = Color.blue;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }
    }
    public void UpdatePowerUpBar()
    {
        float fillFront = frontPWRBar.fillAmount;
        float fillBack = backPWRBar.fillAmount;
        float pFraction = pwr / maxPWR;
        frontPWRBar.fillAmount += 0.00003f;
        if (fillFront < pFraction)
        {
            backPWRBar.color = Color.green;
            backPWRBar.fillAmount = pFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            frontPWRBar.fillAmount = Mathf.Lerp(fillFront, backHealthBar.fillAmount, percentComplete);
        }

    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        lerpTimer = 0f;
    }

    public void RestoreHealth(float healAmount) 
    {
        health += healAmount;
        lerpTimer = 0f;
        
    }

    public void GainPWR(float pwrAmount)
    {
        pwr += pwrAmount;
        lerpTimer = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
