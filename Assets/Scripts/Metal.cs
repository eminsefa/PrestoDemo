using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Metal : MonoBehaviour
{
    public Slider slider;

    public float magneticArea = 5;
    float permeabilityOfSpace = 4f * Mathf.PI; //simplified for unit simplification

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer==12)
        {
            slider.value++;
            if(slider.value>=2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Destroy(gameObject);
        }
    }
    void FixedUpdate()
    {
        foreach (Magnet magnet in FindObjectsOfType<Magnet>())
        {
            float distance = Vector3.Distance(transform.position, magnet.transform.position);
            float fluxDestiny = 1 / distance;
            float magnetForce = fluxDestiny * fluxDestiny * magneticArea / 2 * permeabilityOfSpace;
            Vector3 moveDirection = magnet.transform.position - transform.position;

            rb.AddForce(Vector3.Lerp(transform.position, moveDirection, 1) * magnetForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
}
