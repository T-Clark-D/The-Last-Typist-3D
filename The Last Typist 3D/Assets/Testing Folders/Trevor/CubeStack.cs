using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeStack : MonoBehaviour
{ 
    public GameObject groundSP; //Ground object, will probably have a grid object? 
    public GameObject currCube; //Current cube object for transforms and references 
    public GameObject lowerCube; //Cube below current cube on stack, for position reference
    public bool isOnStack = false; //Used to keep track whether 


    private Vector3 newPos;

    public Rigidbody cubeRigid;


    private void Awake() {
        //Instantiates cube and rigidbody for collider
        currCube = this.gameObject;
        cubeRigid = currCube.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other) {
        //Ideally tags will be used to distinguish between stacking parts, such as whatever the object touches will determine the new stacked height
        //Make the on stack check a higher level check and lower level checks for tags to determine the correnct transform. 


        //This if first collision with ground, makes it the bottom on stack
        if (other.CompareTag("Ground")){

            groundSP = other.gameObject;
            isOnStack = true;
            cubeRigid.isKinematic = true;
            cubeRigid.useGravity = false;

        }
        //This is first collision is with a cube, only activates if current cube is not on a stack, so that it does not affect already placed cubes 
        if(other.CompareTag("Cube") && !this.GetComponent<CubeStack>().isOnStack){

                lowerCube = other.gameObject;
                newPos = lowerCube.transform.position;
                newPos.y += 1;
            
                currCube.transform.position = newPos;
                currCube.transform.rotation = Quaternion.identity; //reset any rotations from fall
                cubeRigid.isKinematic = true;
                cubeRigid.useGravity = false;
                isOnStack = true;
             


        }

    }
}
