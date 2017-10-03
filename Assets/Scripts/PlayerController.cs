using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject laser;
    public float health = 150;
    public float speed = 15.0f;
    public float padding = 1;               //add padding to side of screen on player
    public float projectileSpeed;
    public float fireRate = 0.2f;           //rate of fire

    float xmin;
    float xmax;

    private void Start()
    {
        //With viewporttoworldpoint, x and y are between 0 and 1 - 0,0 is bottom left
        //1,1 is top right.  z will be distance to camera
        float distance = transform.position.z - Camera.main.transform.position.z;           //distance between camera and this object
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));       //leftmost position in the world
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));       //rightmost position in the world
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;
    }

    private void Fire()
    {
        Vector3 startPos = transform.position + new Vector3(0, 1, 0);
        GameObject beam = Instantiate(laser, startPos, Quaternion.identity) as GameObject;       //instantiate as a gameobject so we can use them
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Space))                                                    //player shoots laser
        {
            InvokeRepeating("Fire",0.000001f, fireRate);                //begin calling Fire method 
        }

        if (Input.GetKeyUp(KeyCode.Space))                                                    //player shoots laser
        {
            CancelInvoke("Fire");                                       //stop calling the fire method
        }

        if (Input.GetKey(KeyCode.LeftArrow))                            //while the left arrow is down, move left
        {
            // transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);       //Time.deltatime makes it framerate independent 
            transform.position += Vector3.left * speed * Time.deltaTime;                //same as above, slightly more readable
        }
            
        if (Input.GetKey(KeyCode.RightArrow))                           //while the right arrow is down, move right
        {
            transform.position += Vector3.right * speed * Time.deltaTime;                //Vector3.right is same as (1,0,0)
        }



        //restrict the player to the gamespace
        float newX = Mathf.Clamp(transform.position.x, xmin, xmax);         //use clamp to limit
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);     //reset position using newX and the old y and z positions
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("PLAYER IS HIT");
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if (missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Player COLLIDED");
    }

}

