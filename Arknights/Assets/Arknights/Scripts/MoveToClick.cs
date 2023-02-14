using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToClick : MonoBehaviour
{
    Transform myTransform; //Object you want to rotate
    Transform target; //The game object that you want to face
    float rotationSpeed = 5;
 
    void Start()
    {

        myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);

    }
}
