using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float initialLevelTime = 30;
    public float EndTextTimer = 10;
    public Canvas canvasEnd;
    public Canvas canvasWin;
    public TMP_InputField tmpIfPickups;
    public TMP_InputField tmpIfTimeLeft;
    public TMP_Text TextEndMessageLose;
    public TMP_Text TextEndMessageWin;
    public Transform pickups;
    public float force = 100;
    private Rigidbody rbPlayer;
    private float levelTime;
    private float EndTimer;

    // Start is called before the first frame update
    void Start()
    {
        levelTime = initialLevelTime;
        EndTimer = EndTextTimer;
        canvasEnd.enabled = false;
        canvasWin.enabled = false;
        rbPlayer = GetComponent<Rigidbody>();
        StartCoroutine("checkGame");
        StartCoroutine("calcLevelTime");
    }

    IEnumerator calcLevelTime()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.1f);
            if (levelTime > 0 && pickups.childCount > 0)          
                levelTime -= 0.1f;         
            tmpIfTimeLeft.text = levelTime.ToString("0.#");
            if (canvasEnd.enabled == true || canvasWin.enabled == true)
                EndTimer -= 0.1f;
            if (EndTimer <= 0)
            {
                Debug.Log("This Works ");
                TextEndMessageLose.enabled = false;
                TextEndMessageWin.enabled = false;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    IEnumerator checkGame()
    {
        while (true) // infinite loop
        {
            yield return new WaitForSeconds(1.0f);
            //Debug.Log("pickups left: " + pickups.childCount);
            Debug.Log("ENDTEXTTIME left: " + EndTimer);
            tmpIfPickups.text = pickups.childCount.ToString();
            if(levelTime <= 0)
            {
                canvasEnd.enabled = true;
            }
            if(pickups.childCount == 0 && levelTime >= 0) // win
            {
                canvasWin.enabled = true;
                float oldScore = PlayerPrefs.GetFloat("ScoreTime", float.MaxValue);
                float timeSpent = initialLevelTime - levelTime;
                if (timeSpent < oldScore)
                {
                    PlayerPrefs.SetFloat("ScoreTime", timeSpent);
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pickup")) //when player collides
        {
            Destroy(collision.gameObject);
        }
    }

    public void StartLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // loads any scene listed in build settings
    }


}
