using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class MailBoxContoller : MonoBehaviour
{
    public List<GameObject> mailBoxes;
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
    private void Start()
    {
        normalMaterial = Resources.Load<Material>("NormalMailBox");
        lockedBoxNormalMaterial = Resources.Load<Material>("lockedBoxNormalMaterial");
        lockedBoxRedMaterial = Resources.Load<Material>("lockedBoxRedMaterial");
        lockedBoxBlueMaterial = Resources.Load<Material>("lockedBoxBlueMaterial");
        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();

        unlockedMaterial = Resources.Load<Material>("UnlockedMailBox");
        numberOfMailBoxes = mailBoxes.Count;
        NewMailBoxTarget();
    }
    public void NewMailBoxTarget()
    {
        if(GameStarted == true)
        {
            mailBoxes[previousMailBoxIndex].tag = "Untagged";
            Renderer mailBoxRenderer = mailBoxes[previousMailBoxIndex].GetComponent<Renderer>();
            mailBoxRenderer.material = normalMaterial;
        }
        RandomMailBoxGenerator();
        
        previousMailBoxIndex = randomNumber;
        Renderer newMailBoxRenderer = mailBoxes[randomNumber].GetComponent<Renderer>();
        randomBoxColorNumber = Random.Range(0, 3);
        if(randomBoxColorNumber == 0)
        {
            mailBoxes[randomNumber].tag = "TargetNormal";
            newMailBoxRenderer.material = lockedBoxNormalMaterial;
        }
        else if(randomBoxColorNumber == 1)
        {
            mailBoxes[randomNumber].tag = "TargetRed";
            newMailBoxRenderer.material = lockedBoxRedMaterial;
        }
        else
        {
            mailBoxes[randomNumber].tag = "TargetBlue";
            newMailBoxRenderer.material = lockedBoxBlueMaterial;
        }
        
        
        GameStarted = true;
    }
    private void RandomMailBoxGenerator()
    {
        randomNumber = Random.Range(0, numberOfMailBoxes);
    }

    public void MailHasBeenDelivered(int points)
    {
        PlayerScore += points;
        Debug.Log("Mail Delivered! Get Points: " + points);
        Debug.Log("Total Points: " + PlayerScore);
        NewMailBoxTarget();
        PlayerScoreText.text = PlayerScore.ToString();
        firstPersonController.GenerateNewMailType();
    }

    public void MailHasFailed()
    {
        PlayerScore -= 1;
        Debug.Log("Mail Failed! Total Points: " + PlayerScore);
        NewMailBoxTarget();
        firstPersonController.GenerateNewMailType();
        PlayerScoreText.text = PlayerScore.ToString();
    }

    public void KeyHasUnlockedBox()
    {
        Renderer mailBoxRenderer = mailBoxes[previousMailBoxIndex].GetComponent<Renderer>();
        mailBoxRenderer.material = unlockedMaterial;
        mailBoxes[previousMailBoxIndex].tag = "Unlocked";
    }
    public void KeyHasFailed()
    {
        NewMailBoxTarget();
    }
}
