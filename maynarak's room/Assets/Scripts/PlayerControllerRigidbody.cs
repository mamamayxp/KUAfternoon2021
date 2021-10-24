using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerRigidbody : MonoBehaviour
{
    public float speed = 2f;
    Rigidbody rb;
    float newRotY = 0;
    public float rotSpeed = 20f;
    public GameObject prefabBullet;
    public Transform GunPosition;
    public float gunPower = 15f;
    public float gunCooldown = 2f;
    public float gunCooldownCount = 0;
    public bool hasGun = false;
    public int bulletCount = 0;

    public int coinCount = 0;
    public PlaygroundSceneManager manager;
    public AudioSource audioCoin;
    public AudioSource audioFire;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<PlaygroundSceneManager>();
        if (manager == null)
        {
            print("Manager not found!!");
        }

        if (PlayerPrefs.HasKey ("CoinCount"))
        {
            coinCount = PlayerPrefs.GetInt("CoinCount");
        }
        manager.SetTextCoin(coinCount);
    }

    // Update is called once per frame
    void FixedUpdate()
    { 

    float horizontal = Input.GetAxis("Horizontal") * speed;
    float vertical = Input.GetAxis("Vertical") * speed;
        
        if(horizontal > 0)
        {
            newRotY = 90;
        }else if (horizontal < 0)
{
    newRotY = -90;
}

if (vertical > 0)
{
    newRotY = 0;
}
else if (vertical < 0)
{
    newRotY = 180;
}
rb.AddForce(horizontal, 0, vertical, ForceMode.VelocityChange);
transform.rotation = Quaternion.Lerp(Quaternion.Euler(0, newRotY, 0),
        transform.rotation,
        rotSpeed * Time.deltaTime);
    } 

    private void Update()
    {
        gunCooldownCount += Time.deltaTime;
        //Gun Shot
        if (Input.GetKeyDown(KeyCode.LeftControl) && (bulletCount > 0) && (gunCooldownCount >= gunCooldown))
        {
            gunCooldownCount = 0;
            GameObject bullet = Instantiate(prefabBullet, GunPosition.position, GunPosition.rotation);
            //bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 15f, ForceMode.Impulse);
            Rigidbody bRb = bullet.GetComponent<Rigidbody>();
            bRb.AddForce(transform.forward * gunPower, ForceMode.Impulse);

            Destroy(bullet, 2f);

            bulletCount--;
            manager.SetTextBullet(bulletCount);
            audioFire.Play();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            Destroy(collision.gameObject);
        
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.name == "Gun")
        
            {
                print("Yeah i have gun");
                Destroy(other.gameObject);
                hasGun = true;
                bulletCount += 10;
            manager.SetTextBullet(bulletCount);
            }
        if (other.gameObject.tag == "Collectable")
        {
            Destroy(other.gameObject);
            coinCount++;
            manager.SetTextCoin(coinCount);
            audioCoin.Play();
            PlayerPrefs.SetInt("CoinCount", coinCount);
        }

    }
}