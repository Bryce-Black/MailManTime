using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MailBoxContoller : MonoBehaviour
{
    public List<GameObject> mailBoxSpawnLocations;
    private int numberOfMailBoxes;
    private int previousMailBoxIndex;
    private int randomNumber;
    private int randomBoxColorNumber;
    private Material normalMaterial;
    private Material lockedBoxNormalMaterial;
    private Material lockedBoxRedMaterial;
    private Material lockedBoxBlueMaterial;

    private Material unlockedMaterial;
    private bool GameStarted = false;
    public int PlayerScore;
    public TextMeshProUGUI PlayerScoreText;
    FirstPersonController firstPersonController;
    public GameObject arrowPoint;
    private Vector3 targetMailBoxPosition;
    private Transform targetMailBoxTransform;
    public GameObject player;
    public PointerScript pointerScript;
    private bool mailBoxUnlocked = false;
    private GameObject newMailBox;
    private void Start()
    {
        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        numberOfMailBoxes = mailBoxSpawnLocations.Count;
        NewMailBoxTarget();
    }
    public void NewMailBoxTarget()
    {
        if(GameStarted == true)
        {
            
        }
        RandomMailBoxGenerator();
        SpawnARandomMailBox();
        targetMailBoxTransform = mailBoxSpawnLocations[randomNumber].transform;
        pointerScript.UpdateTargetPosition(targetMailBoxTransform);
        GameStarted = true;
    }
    private void SpawnARandomMailBox()
    {
        int ranNum = Random.Range(0, 4);
        if (ranNum == 0)
        {
            newMailBox = Instantiate(Resources.Load<GameObject>("MailMailBox"));
            newMailBox.transform.position = mailBoxSpawnLocations[randomNumber].transform.position;
            newMailBox.transform.rotation = mailBoxSpawnLocations[randomNumber].transform.rotation;
            GameObject mailBoxDoor = GameObject.FindGameObjectWithTag("Door");
            int ranColorNum = Random.Range(0, 3);
            if(ranColorNum == 0)
            {
                mailBoxDoor.tag = "TargetNormal";
            }
            if(ranColorNum == 1)
            {
                mailBoxDoor.tag = "TargetRed";
            }
            if(ranColorNum ==2)
            {
                mailBoxDoor.tag = "TargetBlue";
            }
            

        }
        else if (ranNum == 1)
        {
            newMailBox = Instantiate(Resources.Load<GameObject>("FireMailBox"));
            newMailBox.transform.position = mailBoxSpawnLocations[randomNumber].transform.position;
            newMailBox.transform.rotation = mailBoxSpawnLocations[randomNumber].transform.rotation;
            GameObject mailBoxDoor = GameObject.FindGameObjectWithTag("Door");
            int ranColorNum = Random.Range(0, 3);
            if (ranColorNum == 0)
            {
                mailBoxDoor.tag = "TargetNormal";
            }
            if (ranColorNum == 1)
            {
                mailBoxDoor.tag = "TargetRed";
            }
            if (ranColorNum == 2)
            {
                mailBoxDoor.tag = "TargetBlue";
            }

        }
        else if (ranNum == 2)
        {
            newMailBox = Instantiate(Resources.Load<GameObject>("IceMailBox"));
            newMailBox.transform.position = mailBoxSpawnLocations[randomNumber].transform.position;
            newMailBox.transform.rotation = mailBoxSpawnLocations[randomNumber].transform.rotation;
            GameObject mailBoxDoor = GameObject.FindGameObjectWithTag("Door");
            int ranColorNum = Random.Range(0, 3);
            if (ranColorNum == 0)
            {
                mailBoxDoor.tag = "TargetNormal";
            }
            if (ranColorNum == 1)
            {
                mailBoxDoor.tag = "TargetRed";
            }
            if (ranColorNum == 2)
            {
                mailBoxDoor.tag = "TargetBlue";
            }

        }
        else
        {
            newMailBox = Instantiate(Resources.Load<GameObject>("MetalMailBox"));
            newMailBox.transform.position = mailBoxSpawnLocations[randomNumber].transform.position;
            newMailBox.transform.rotation = mailBoxSpawnLocations[randomNumber].transform.rotation;
            GameObject mailBoxDoor = GameObject.FindGameObjectWithTag("Door");
            int ranColorNum = Random.Range(0, 3);
            if (ranColorNum == 0)
            {
                mailBoxDoor.tag = "TargetNormal";
            }
            if (ranColorNum == 1)
            {
                mailBoxDoor.tag = "TargetRed";
            }
            if (ranColorNum == 2)
            {
                mailBoxDoor.tag = "TargetBlue";
            }

        }


    }
    private void RandomMailBoxGenerator()
    {
        randomNumber = Random.Range(0, numberOfMailBoxes);
    }

    public void MailHasBeenDelivered(int points)
    {
        if(mailBoxUnlocked)
        {
            PlayerScore += points;
            Debug.Log("Mail Delivered! Get Points: " + points);
            Debug.Log("Total Points: " + PlayerScore);
            NewMailBoxTarget();
            PlayerScoreText.text = PlayerScore.ToString();
        }
       
    }

    public void MailHasFailed()
    {
        PlayerScore -= 1;
        Debug.Log("Mail Failed! Total Points: " + PlayerScore);
        Destroy(newMailBox);
        NewMailBoxTarget();
        PlayerScoreText.text = PlayerScore.ToString();
    }

    public void KeyHasUnlockedBox()
    {
        mailBoxUnlocked = true;
        Animator doorOpen = GameObject.FindGameObjectWithTag("MailBox").GetComponent<Animator>();
        doorOpen.SetTrigger("MailBoxOpen");
    }
    public void KeyHasFailed()
    {
        NewMailBoxTarget();
    }
}