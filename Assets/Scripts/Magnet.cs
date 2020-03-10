using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    /*
    Force beetween two magnets (Gilbert's model of "Force between two magnetic poles")
    Force = permeability*magneticCharge1*magneticCharge2/4*Mathf.Pi*distance*distance

    Force between magnet and metal (Gilbert's model of "Force between two nearby magnetized surfaces of area A")
    Force = fluxDestiny*fluxDestiny*magneticArea/2*permeabilityOfSpace
    where fluxDestiny accepted as 1/distance
    */
    public enum Polarization { Positive,Negative};
    public Polarization Pole;
    
    public float magneticCharge;
    public float permeability = 1;
    public float magneticArea = 4;
    float permeabilityOfSpace = 4f * Mathf.PI; //simplified for unit simplification

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        foreach (Magnet magnet in FindObjectsOfType<Magnet>())
        {
            if(this!=magnet)
            {
                float distance = Vector3.Distance(transform.position, magnet.transform.position);
                float magnetForce = permeability*magneticCharge * magnet.magneticCharge / (4 * Mathf.PI * distance * distance);
                Vector3 moveDirection = transform.position - magnet.transform.position;
                if(distance<1.5f)
                {
                    magnetForce = 0;
                }
                if (this.Pole != magnet.Pole)
                {
                    rb.AddForce(Vector3.Lerp(transform.position, -moveDirection, 1) * magnetForce * Time.fixedDeltaTime, ForceMode.Acceleration);
                }
                else
                {
                    rb.AddForce(Vector3.Lerp(transform.position, moveDirection, 1) * magnetForce * Time.fixedDeltaTime, ForceMode.Acceleration);
                }
            }
        }
        foreach (Metal metal in FindObjectsOfType<Metal>())
        {
            float distance = Vector3.Distance(transform.position, metal.transform.position);
            float fluxDestiny = 1 / distance;
            float magnetForce = fluxDestiny * fluxDestiny * magneticArea / 2 * permeabilityOfSpace;
            Vector3 moveDirection = metal.transform.position - transform.position;

            rb.AddForce(Vector3.Lerp(transform.position, moveDirection, 1) * magnetForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
}
