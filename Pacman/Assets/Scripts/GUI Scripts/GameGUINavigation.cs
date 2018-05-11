using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameGUINavigation : MonoBehaviour {

    public float initialDelay;

	void Start () 
	{
        StartCoroutine("Ready", value: initialDelay);
    }

	void Update () 
	{

	}

	public void ShowReady()
	{
		StartCoroutine("Ready", value: initialDelay);
	}

    public void ShowGameOver()
    {
        StartCoroutine("GameOver");
    }

	IEnumerator Ready(float seconds)
	{
		GameManager.gameState = GameManager.GameState.Init;
		yield return new WaitForSeconds(seconds);
		GameManager.gameState = GameManager.GameState.Game;
	}

    IEnumerator GameOver()
    {
        yield return new WaitForSeconds(2);
    }

    public void LoadLevel()
    {
        GameManager.Level++;
        SceneManager.LoadScene("game");
    }
}
