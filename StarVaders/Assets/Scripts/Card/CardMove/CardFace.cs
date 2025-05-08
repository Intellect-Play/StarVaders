using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardFace : MonoBehaviour
{
    public GameObject target;

    public float rotationSpeed;
    public float rotationAmount;

    Vector3 rotation;
    Vector3 movement;

    private float randomRot;

    private void Start()
    {
        randomRot = Random.Range(-rotationAmount, rotationAmount);
    }

    void Update()
    {
        transform.position = Vector2.Lerp(transform.position, target.transform.position, Time.deltaTime * 25);

        if (!target.GetComponent<Card>().Played)
        {   
            Vector3 pos = (transform.position - target.transform.position);
            Vector3 movementRotation;
            
            movement = Vector3.Lerp(movement, pos, 25 * Time.deltaTime);
            
            if (target.GetComponent<Card>().IsDragging)
                movementRotation = movement;
            else
                movementRotation = movement;
            
            rotation = Vector3.Lerp(rotation, movementRotation, rotationSpeed * Time.deltaTime);
            
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, Mathf.Clamp(movementRotation.x, -rotationAmount, rotationAmount));
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, randomRot);
        }
    }
}
