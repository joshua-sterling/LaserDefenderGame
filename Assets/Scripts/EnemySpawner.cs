using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {


    public float speed = 5.0f;
    public float padding = 1;               //add padding to side of screen on player
    public float spawnDelay = 0.5f;

    float xmin;
    float xmax;

    private bool movingRight = true;

    public GameObject enemyPrefab;
    public float width = 10.0f;
    public float height = 5.0f;

	// Use this for initialization
	void Start () {

        //With viewporttoworldpoint, x and y are between 0 and 1 - 0,0 is bottom left
        //1,1 is top right.  z will be distance to camera
        float distance = transform.position.z - Camera.main.transform.position.z;           //distance between camera and this object
        Vector3 leftmost = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, distance));       //leftmost position in the world
        Vector3 rightmost = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, distance));       //rightmost position in the world
        xmin = leftmost.x + padding;
        xmax = rightmost.x - padding;

        SpawnUntilFull();                                                                      //spawn the first group of enemies
    }


    //This function will spawn enemies
    void Respawn()
    {
        //for every Transform item in this object's transform (aka the children, which are spawn positions)                                                                                           
        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;  //instaniate as a game object at the child position (child IS a position)
                                                                                                                       //for tidiness, keeps this in the hierarchy under enemySpawner
                                                                                                                       //as they spawn
            enemy.transform.parent = child;
        }
    }

    void SpawnUntilFull()
    {
        
        Transform freePosition = NextFreePosition();                                        //where will the enemy spawn

        if (freePosition)                                                                   //only spawn if there is a free position
        {
            GameObject enemy = Instantiate(enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;  //instaniate as a game object at the child position (child IS a position)
                                                                                                                    //for tidiness, keeps this in the hierarchy under enemySpawner
                                                                                                                    //as they spawn
            enemy.transform.parent = freePosition;
        }

        //call self recursively only if there is a next free position
        if (NextFreePosition()) {
            Invoke("SpawnUntilFull", spawnDelay);                                               //call spwanuntil full every spawnDelay seconds                                                                             
        }
    }



    public void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
    }

    // Update is called once per frame
    void Update () {

        if (movingRight)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;            
            
        }
        else
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

        float rightFormationEdge = transform.position.x + (0.4f * width);                                    //position is in the middle, so go out half of width
        float leftFormationEdge = transform.position.x - (0.4f * width);
        if(leftFormationEdge < xmin)                                           //if they hit an edge, change direction
        {
            movingRight = true;                                                                     //flip moving right
        }
        else if (rightFormationEdge > xmax)                                           //if they hit an edge, change direction
        {
            movingRight = false;                                                                     //flip moving right
        }

        if(AllMembersDead())
        {            
            Debug.Log("Empty Formation");
            SpawnUntilFull();
        }
        
    }

    Transform NextFreePosition()
    {
        foreach (Transform childPositionGameObject in transform)              //this script is attached ot the enemy formation, which has a transform
        {
            if (childPositionGameObject.childCount == 0)                     //is the child count zero (enemy dead) at this position
            {   
                return childPositionGameObject;                             //if so, return the position game object
            }
        }

        return null;
    }

    bool AllMembersDead()
    {
        foreach (Transform childPositionGameObject in transform)           //this script is attached ot the enemy formation, which has a transform
        {
            if (childPositionGameObject.childCount > 0)
            {
                return false;
            }           
        }
        
        return true;
    }

}
