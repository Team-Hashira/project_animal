using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private Transform _gameOverPanelTrm;

    public void GameOver()
    {
        Time.timeScale = 0;
        _gameOverPanelTrm.gameObject.SetActive(true);
    }
}
