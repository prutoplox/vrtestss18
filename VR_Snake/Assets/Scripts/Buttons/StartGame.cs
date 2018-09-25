using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Button start;
    public Button restart;

    private bool switcher;

    public void Start()
    {
        start.onClick.AddListener(startGame);
        restart.onClick.AddListener(startGame);
        switcher = false;
    }

    public void Update()
    {
        if (switcher == true)
        {
            initialiseStart();
            switcher = false;
        }
    }

    public void startGame()
    {
        Debug.Log("Start");
        VariableManager.instance.startGame = true;
        switcher = true;
    }

    public void initialiseStart()
    {
        MovementSnake.instance.Reset();
        AudioManager.instance.playGameMusic();
        VariableManager.instance.showMainMenu = false;
        VariableManager.instance.showGameOver = false;
        VariableManager.instance.startTime = Time.time;
    }
}
