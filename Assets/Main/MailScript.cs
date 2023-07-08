using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailScript : MonoBehaviour
{
    MailScriptableObject mailScript;
    private bool pointsGiven = false;

    public string MailName;
    public float MailMass;
    public float MailSpeed;
    public int MailPoints;

    FirstPersonController firstPersonController;

    private void Start()
    {
        MailName = mailScript.mailName;
        MailMass = mailScript.mailMass;
        MailSpeed = mailScript.mailSpeed;
        MailPoints = mailScript.mailPoints;
        firstPersonController.ChangeMailInfo(MailName, MailSpeed);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Boundry")
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }
}
