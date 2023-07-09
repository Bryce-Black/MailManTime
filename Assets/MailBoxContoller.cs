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
    private Material normalMaterial;
    private Material highLightMaterial;
    private bool GameStarted = false;
    public int PlayerScore;
    public TextMeshProUGUI PlayerScoreText;
    private void Start()
    {
        normalMaterial = Resources.Load<Material>("NormalMailBox");
        highLightMaterial = Resources.Load<Material>("HighLightedMailBox");
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
        mailBoxes[randomNumber].tag = "Target";
        Renderer newMailBoxRenderer = mailBoxes[randomNumber].GetComponent<Renderer>();
        newMailBoxRenderer.material = highLightMaterial;
        previousMailBoxIndex = randomNumber;
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
    }

    public void MailHasFailed()
    {
        PlayerScore -= 1;
        Debug.Log("Mail Failed! Total Points: " + PlayerScore);
        NewMailBoxTarget();
        PlayerScoreText.text = PlayerScore.ToString();
    }
}
