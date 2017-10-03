using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Position : MonoBehaviour {

    private void OnDrawGizmos()
    {
        //adding a gizmo to the editor - makes the sphere that is present in editor regardless of what object selected
        //makes editing easier
        Gizmos.DrawWireSphere(transform.position, 1);                   
    }
}
