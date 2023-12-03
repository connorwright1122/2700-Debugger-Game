using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController2 : MonoBehaviour {

    [SerializeField]private float _speed = 7f;
    [SerializeField]private float _mouseSensitivity = 5f;
    [SerializeField]private float _minCameraview = -70f, _maxCameraview = 80f;
    [SerializeField]private float _shootDelay = 1f;
    private float lastShoot = 0f;
    private CharacterController _charController;
    private Camera _camera;
    private float xRotation = 0f;

    public bool frozen = false;


    [SerializeField] private int _weaponType = 1;
    public int shootDamage = 50;
    public int meleeDamage = 30;

    public GameObject menuPanel;
    public GameObject inputField;
    public GameObject meleeImage;
    public GameObject gunImage;

    public GameObject meleeHitbox;

    [SerializeField] public GameObject particleSystemPrefab;

    public AudioManager audioManager;






    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        _camera = Camera.main;

        if(_charController == null)
        Debug.Log("No Character Controller Attached to Player");

        Cursor.lockState = CursorLockMode.Locked;
        frozen = false;
        Time.timeScale = 1f;

        //audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!frozen) {

        
            //Get WASD Input for Player
            float vertical = Input.GetAxis("Vertical");
            float horizontal = Input.GetAxis("Horizontal");
            //move player based on WASD Input
            Vector3 movement = transform.forward * vertical + transform.right * horizontal; //changed this line.
            _charController.Move(movement * Time.deltaTime * _speed);


            
            //Get Mouse position Input
            float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity; //changed this line.
            float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity; //changed this line.
            //Rotate the camera based on the Y input of the mouse
            xRotation -= mouseY;
            //clamp the camera rotation between 80 and -70 degrees
            xRotation = Mathf.Clamp(xRotation, _minCameraview, _maxCameraview);

            _camera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
            //Rotate the player based on the X input of the mouse
            transform.Rotate(Vector3.up * mouseX * 3);
        




            //Debug.Log(Time.time - lastShoot);
            if (Input.GetMouseButton(0) && Time.time - lastShoot > _shootDelay) {
                lastShoot = Time.time;

                switch(_weaponType) {
                    case 1:
                        Shoot();
                        break;
                    case 2:
                        Melee();
                        break;
                }

            }
        }


        if (Input.GetKeyDown(KeyCode.T)) {
            //menuPanel.SetActive(true); // Show the menu
            Cursor.lockState = CursorLockMode.Confined;
            inputField.GetComponent<TMP_InputField>().Select(); // Focus the text field
            Time.timeScale = 0f; // Freeze the game
            frozen = true;
        }


    }


    private void Shoot() {
        audioManager.PlayGunSound();
        Ray ray = _camera.ViewportPointToRay(new Vector3(.5f, .5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            Debug.Log("hit " + hit.transform.name);

            GameObject particleSystem = Instantiate(particleSystemPrefab, hit.point, Quaternion.identity);
            Destroy(particleSystem, 2.0f); // Destroy the particle system after 2 seconds

            if (hit.transform.tag == "Enemy") {
                hit.transform.gameObject.GetComponent<EnemyHealthManager>().takeDamage(shootDamage);
                

                //Debug.DrawRay(ray.origin, ray.direction * 10, Color.red);

            }
        }
    }

    private void Melee() {
        audioManager.PlayPunchSound();
        meleeHitbox.GetComponent<MeleeHitbox>().melee(meleeDamage);
    }


    public void OnSelectWeapon() {
        //string inputText = inputField.text.ToLower();
        string inputText = inputField.GetComponent<TMP_InputField>().text;

        if (inputText == "melee()") {
            _weaponType = 2;
            meleeImage.SetActive(true);
            gunImage.SetActive(false);
            Debug.Log("Melee activated");
        } else if (inputText == "shoot()") {
            _weaponType = 1;
            meleeImage.SetActive(false);
            gunImage.SetActive(true);
            Debug.Log("Gun activated");
        } else {
            Debug.Log("Invalid weapon selection: " + inputText);
        }

        //menuPanel.SetActive(false); // Hide the menu
        Cursor.lockState = CursorLockMode.Locked;
        inputField.GetComponent<TMP_InputField>().DeactivateInputField();
        //EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f; // Unfreeze the game
        frozen = false;
    }




}