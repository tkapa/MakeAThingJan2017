using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simpleCharMovement : MonoBehaviour
{

    public float minAngle,maxAngle,normalAngle;
    public float animSpeed;
    private float currAngle;

    void Start()
    {
        currAngle = normalAngle;
    }

    // Update is called once per frame
    void FixedUpdate ()
	{
	    
        //if (movement){}

        //Testing 

	    if (currAngle <= minAngle)
	    {

	    }
	    else if(currAngle >= maxAngle )
	    {
	        
	    }

	    transform.eulerAngles = new Vector3(currAngle, transform.position.y, transform.position.z);
    }
}
