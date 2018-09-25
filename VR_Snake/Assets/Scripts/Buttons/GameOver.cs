using UnityEngine;

public class GameOver : MonoBehaviour
{
    private void showGameOver()
    {
        VariableManager.instance.showMainMenu = false;
        VariableManager.instance.showOptions = false;
        VariableManager.instance.showPause = false;
        VariableManager.instance.showHighscore = false;
        VariableManager.instance.showGameOver = true;
    }
}