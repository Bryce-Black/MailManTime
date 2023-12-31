using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.SceneManagement;
public class MailBoxContoller : MonoBehaviour
{
    public List<GameObject> mailBoxSpawnLocations;
    private int numberOfMailBoxes;
    private int randomNumber;

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
    private float timerInitialTime = 20f;
    private float timerModifier = 0f;
    public TextMeshProUGUI timerInitialTimeText;
    private IEnumerator timerCountDownCoroutine;
    public TextMeshProUGUI powerUpScreenText;
    private IEnumerator mailBoxTester;
    private int MailBoxTesterIndex;
    public List<GameObject> playerHealthHearts;
    private int playerHealthIndex;
    public Animator damageAnim;
    private float totalTime;
    public GameObject youWinPanel;
    public TextMeshProUGUI totalTimeText;
    private bool gameOver = false;
    public GameObject optionsPanel;
    public bool gameIsPaused = false;
    public GameObject reticle;
    public AudioSource music;
    private void Start()
    {
        firstPersonController = GameObject.FindGameObjectWithTag("Player").GetComponent<FirstPersonController>();
        numberOfMailBoxes = mailBoxSpawnLocations.Count;
        MailBoxTesterIndex = numberOfMailBoxes -1;
        Debug.Log("Mailboxtesterindex: " + MailBoxTesterIndex);
        playerHealthIndex = playerHealthHearts.Count - 1;
        ////this is for testing all spawn locations
        //NewTestMailboxLocation();
        //mailBoxTester = MailBoxTester(3f);
        //StartCoroutine(mailBoxTester);
        NewMailBoxTarget();
        StartTimerCountDownCoroutine();
    }
    private void Update()
    {
        if(!gameOver)
        {
            totalTime += Time.deltaTime;

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleOptionsMenu();
        }

    }
    private void ToggleOptionsMenu()
    {
        if(optionsPanel.activeSelf == true)
        {
            optionsPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1f;
            gameIsPaused = false;
            reticle.SetActive(true);
        }
        else
        {
            Time.timeScale = 0f;
            optionsPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            gameIsPaused = true;
            reticle.SetActive(false);
        }
    }
    public void TimeResetPowerUp()
    {
        timerInitialTime += 5f;
    }
    private IEnumerator TimerCountDownCoroutine(float waitTime)
    {
        while(!(timerInitialTime <= 0f))
        {
            timerInitialTime -= .1f;
            yield return new WaitForSeconds(waitTime);
            //Debug.Log("countdown timer: " + timerInitialTime);
            float timerRounder = (float)Math.Round(timerInitialTime, 2);
            timerInitialTime = timerRounder;
            timerInitialTimeText.text = "TIME: " + timerInitialTime.ToString();
        }
        if(timerInitialTime <= 0)
        {
            timerInitialTimeText.text = "KABOOM";
            Debug.Log("Times UP!!");


            MailHasFailed();
            DecreaseMailSpawnTime();
            StartTimerCountDownCoroutine();
        }
    }
    private void NewTestMailboxLocation()
    {
        newMailBox = Instantiate(Resources.Load<GameObject>("MailMailBox"));
        newMailBox.transform.position = mailBoxSpawnLocations[MailBoxTesterIndex].transform.position;
        newMailBox.transform.rotation = mailBoxSpawnLocations[MailBoxTesterIndex].transform.rotation;
        targetMailBoxTransform = mailBoxSpawnLocations[MailBoxTesterIndex].transform;
        pointerScript.UpdateTargetPosition(targetMailBoxTransform);
    }

    private IEnumerator MailBoxTester(float waitTime)
    {
        while(true)
        {
            yield return new WaitForSeconds(waitTime);
            if (newMailBox != null)
            {
                Destroy(newMailBox);
            }
            newMailBox = Instantiate(Resources.Load<GameObject>("MailMailBox"));
            newMailBox.transform.position = mailBoxSpawnLocations[MailBoxTesterIndex].transform.position;
            newMailBox.transform.rotation = mailBoxSpawnLocations[MailBoxTesterIndex].transform.rotation;

            targetMailBoxTransform = mailBoxSpawnLocations[MailBoxTesterIndex].transform;
            pointerScript.UpdateTargetPosition(targetMailBoxTransform);
            Debug.Log("current test mailbox index is: " + MailBoxTesterIndex);
            MailBoxTesterIndex -= 1;
            if (MailBoxTesterIndex == -1)
            {
                MailBoxTesterIndex = mailBoxSpawnLocations.Count - 1;
            }
        }
        
        
    }

    private void StartTimerCountDownCoroutine()
    {
        if(timerCountDownCoroutine != null)
        {
            StopCoroutine(timerCountDownCoroutine);
        }
        
        timerCountDownCoroutine = TimerCountDownCoroutine(.1f);
        StartCoroutine(timerCountDownCoroutine);
    }
    private void DecreaseMailSpawnTime()
    {
        timerInitialTime = 20f;
        if(timerModifier <= 4)
        {
            timerModifier += .25f;
        }
        
        timerInitialTime -= timerModifier;
        float timerRounder = (float)Math.Round(timerInitialTime, 2);
        timerInitialTime = timerRounder;
        timerInitialTimeText.text = "TIME: " + timerInitialTime.ToString();
    }
    public void NewMailBoxTarget()
    {
        if(GameStarted == true)
        {
            
        }
        RandomMailBoxGenerator();
        SpawnARandomMailBox();
        
        GameStarted = true;
    }
    private void SpawnARandomMailBox()
    {
        int ranNum = UnityEngine.Random.Range(0, 4);
        if (ranNum == 0)
        {
            newMailBox = Instantiate(Resources.Load<GameObject>("MailMailBox"));
            newMailBox.transform.position = mailBoxSpawnLocations[randomNumber].transform.position;
            newMailBox.transform.rotation = mailBoxSpawnLocations[randomNumber].transform.rotation;
            //SetDoorColorAndTag();


        }
        else if (ranNum == 1)
        {
            newMailBox = Instantiate(Resources.Load<GameObject>("FireMailBox"));
            newMailBox.transform.position = mailBoxSpawnLocations[randomNumber].transform.position;
            newMailBox.transform.rotation = mailBoxSpawnLocations[randomNumber].transform.rotation;
            //SetDoorColorAndTag();

        }
        else if (ranNum == 2)
        {
            newMailBox = Instantiate(Resources.Load<GameObject>("IceMailBox"));
            newMailBox.transform.position = mailBoxSpawnLocations[randomNumber].transform.position;
            newMailBox.transform.rotation = mailBoxSpawnLocations[randomNumber].transform.rotation;
            //SetDoorColorAndTag();

        }
        else
        {
            newMailBox = Instantiate(Resources.Load<GameObject>("MetalMailBox"));
            newMailBox.transform.position = mailBoxSpawnLocations[randomNumber].transform.position;
            newMailBox.transform.rotation = mailBoxSpawnLocations[randomNumber].transform.rotation;
            //SetDoorColorAndTag();

        }

        //Debug.Log("MailBox Spawned type is " + newMailBox.name);
    }
    private void SetDoorColorAndTag()
    {
        GameObject mailBoxDoor = GameObject.FindGameObjectWithTag("Door");
        MeshRenderer coloredDoorMesh = GameObject.FindGameObjectWithTag("DoorKeyColor").GetComponent<MeshRenderer>();
        int ranColorNum = UnityEngine.Random.Range(0, 3);
        if (ranColorNum == 0)
        {
            mailBoxDoor.tag = "TargetNormal";
            coloredDoorMesh.material = Resources.Load<Material>("NormalMailDoorMaterial");
        }
        if (ranColorNum == 1)
        {
            mailBoxDoor.tag = "TargetRed";
            coloredDoorMesh.material = Resources.Load<Material>("RedDoorMaterial");
        }
        if (ranColorNum == 2)
        {
            mailBoxDoor.tag = "TargetBlue";
            coloredDoorMesh.material = Resources.Load<Material>("BlueDoorMaterial");
        }
        targetMailBoxTransform = mailBoxDoor.transform;
        pointerScript.UpdateTargetPosition(targetMailBoxTransform);
        //Debug.Log("MailBox Spawned target color is: " + mailBoxDoor.tag);
    }
    public void MailBoxHasFinishedSpawning()
    {
        SetDoorColorAndTag();
    }
    private void RandomMailBoxGenerator()
    {
        randomNumber = UnityEngine.Random.Range(0, numberOfMailBoxes);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void MailHasBeenDelivered(int points)
    {
        if(mailBoxUnlocked)
        {
            Destroy(newMailBox);
            firstPersonController.CheckGround();
            if(!firstPersonController.isGrounded)
            {
                PlayerScore += points*2;
                firstPersonController.ScreenInfoActivate("+" + points*2 + " MID-AIR BONUS! X2");
            }
            else
            {
                PlayerScore += points;
                firstPersonController.ScreenInfoActivate("+" + points);
            }
            
            if(PlayerScore >= 100)
            {
                YouWin();
            }
            Debug.Log("Mail Delivered! Get Points: " + points);
            Debug.Log("Total Points: " + PlayerScore);
            NewMailBoxTarget();
            PlayerScoreText.text = "SCORE: " + PlayerScore.ToString() + "/100";
            DecreaseMailSpawnTime();
            StartTimerCountDownCoroutine();
        }
       
    }
    private void YouWin()
    {
        firstPersonController.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        gameOver = true;
        youWinPanel.SetActive(true);
        float roundedTime = (float)Math.Round(totalTime, 2);
        totalTime = roundedTime;
        totalTimeText.text = totalTime.ToString();

    }
    public void ToggleMusic()
    {
        if(music.enabled == true)
        {
            music.enabled = false;
        }
        else
        {
            music.enabled = true;
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MailManTime");
    }
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

    }

    public void MailHasFailed()
    {
        damageAnim.SetTrigger("Damaged");
        firstPersonController.ScreenInfoActivate("-1 HP");
        PlayerScore -= 1;
        Debug.Log("Mail Failed! Total Points: " + PlayerScore);
        Destroy(newMailBox);
        NewMailBoxTarget();
        PlayerScoreText.text = "SCORE: " + PlayerScore.ToString() + "/100";
        LoseOneHealthPoint();
    }
    private void LoseOneHealthPoint()
    {
        playerHealthHearts[playerHealthIndex].SetActive(false);
        playerHealthIndex -= 1;
        if(playerHealthIndex == -1)
        {
            GameOver();
        }
        
    }
    private void GameOver()
    {
        SceneManager.LoadScene("MailManTime");
    }
    public void KeyHasUnlockedBox()
    {
        if(timerInitialTime > 0)
        {
            timerInitialTime += 5f;
        }
        firstPersonController.ScreenInfoActivate("+5 Seconds!");
        mailBoxUnlocked = true;
        Animator doorOpen = GameObject.FindGameObjectWithTag("MailBox").GetComponent<Animator>();
        doorOpen.SetTrigger("MailBoxOpen");
    }
    public void KeyHasFailed()
    {
        Destroy(newMailBox);
        NewMailBoxTarget();
    }
}
