using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainController : MonoBehaviour
{

    public TMP_InputField tmpIfScoreTime;
    // Start is called before the first frame update
    void Start()
    {
        updateScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateScores()
    {
        float scoretime = PlayerPrefs.GetFloat("ScoreTime", -1);
        tmpIfScoreTime.text = scoretime < 0 ? "---" : scoretime.ToString("0.#");
    }

    public void StartLevel(string level)
    {
        SceneManager.LoadScene(level);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    public void ResetScores()
    {
        PlayerPrefs.DeleteAll();
        updateScores();
    }
}
