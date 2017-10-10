using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameObject projectile;
    public float projectileSpeed;
    public float health = 150;
    public float shotsPerSecond = 0.5f;                             //firing frequency
    public int scoreValue = 150;
    private ScoreKeeper scoreKeeper;

    private void Start()
    {
       scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Projectile missile = collision.gameObject.GetComponent<Projectile>();
        if(missile)
        {
            health -= missile.GetDamage();
            missile.Hit();
            if(health <= 0)
            {
                scoreKeeper.Score(scoreValue);
                Destroy(gameObject);
            }
        }
    }

    private void Fire()
    {
        Vector3 startPos = transform.position + new Vector3(0, -1, 0);                                  //offset so lasers form below enemy to avoid collision
        GameObject beam = Instantiate(projectile, startPos, Quaternion.identity) as GameObject;       //instantiate as a gameobject so we can use them
        beam.GetComponent<Rigidbody2D>().velocity = new Vector3(0, projectileSpeed, 0);
    }

    private void Update()
    {
        //get a value to calculate probability of firing in a given frame
        float probability = shotsPerSecond * Time.deltaTime;                                        //the time interval 
        if (Random.value < probability)                                                             //random.value is between 0 and 1 and is homogenous (.8 will occur about 80% of the time)
        { Fire(); }
    }

}
