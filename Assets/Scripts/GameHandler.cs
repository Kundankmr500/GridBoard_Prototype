using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance;
    public int RandomTileCount;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI RemainingChanceText;
    public TextMeshProUGUI GameOverText;
    public TextMeshProUGUI PlayerWinText;
    public int ScoreValuePerTile;
    public int MaxPlayerChance;
    public GameObject Player;
    public GameObject Grid;
    public GameObject RestartGameButton;

    private int _collectedTiles;
    private int _scoreValue;
    private int _playerRemainingChance;

    private void Awake()
    {
        Instance = this;
        InitializeGameSettings();
    }

    public void InitializeGameSettings()
    {
        _playerRemainingChance = MaxPlayerChance;
        RemainingChanceText.text = Constant.RemainingChancePrefix + _playerRemainingChance;
        ScoreText.text = Constant.ScorePrefix + _scoreValue;
    }

    public void CalculateScore()
    {
        _scoreValue += ScoreValuePerTile;
        ScoreText.text = Constant.ScorePrefix + _scoreValue;
        CheckForCollectedTiles();
        CheckForPlayerChance();
    }

    public void CheckForCollectedTiles()
    {
        _collectedTiles++;
        if (_collectedTiles == RandomTileCount)
        {
            ShowPlayerWinScreen();
        }
    }


    public void CheckForPlayerChance()
    {
        _playerRemainingChance--;
        RemainingChanceText.text = Constant.RemainingChancePrefix + _playerRemainingChance;
        if (_playerRemainingChance == 0)
        {
            GameOverScreen();
        }
    }

    public void GameOverScreen()
    {
        GameOverText.gameObject.SetActive(true);
        StartCoroutine(ResatGame());
    }

    public void ShowPlayerWinScreen()
    {
        PlayerWinText.gameObject.SetActive(true);
        StartCoroutine(ResatGame());
    }

    public IEnumerator ResatGame()
    {
        Player.SetActive(false);
        Grid.SetActive(false);
        yield return new WaitForSeconds(5f);
        RestartGameButton.SetActive(true);
    }

    public void ResatartGame()
    {
        SceneManager.LoadScene(0);
    }
}
