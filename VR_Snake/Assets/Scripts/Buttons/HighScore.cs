using System;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Button highscore;
    public Button highscore2;

    public Text place1;
    public Text place2;
    public Text place3;
    public Text place4;
    public Text place5;

    //shadow text
    public Text place1shadow;

    public Text place2shadow;
    public Text place3shadow;
    public Text place4shadow;
    public Text place5shadow;

    // Use this for initialization
    void Start()
    {
        highscore.onClick.AddListener(showHighscore);
        highscore2.onClick.AddListener(showHighscore);
    }

    private void showHighscore()
    {
        setHighscoreToTextFields();
        VariableManager.instance.showHighscoreMenu();
    }

    public void setHighscoreToTextFields()
    {
        try
        {
            string[] scores = VariableManager.instance.highScoreManager.getTopFiveHighscoreAsArray();
            place1.text = "1: \t" + scores[0];
            place2.text = "2: \t" + scores[1];
            place3.text = "3: \t" + scores[2];
            place4.text = "4: \t" + scores[3];
            place5.text = "5: \t" + scores[4];

            place1shadow.text = "1: \t" + scores[0];
            place2shadow.text = "2: \t" + scores[1];
            place3shadow.text = "3: \t" + scores[2];
            place4shadow.text = "4: \t" + scores[3];
            place5shadow.text = "5: \t" + scores[4];
        }
        catch (Exception)
        {
            Debug.Log("VariableManager not init");
        }
    }
}
