using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public enum GameState
{
	menu,
	inGame,
	gameOver
}

public class GameManager : NetworkBehaviour
{
	// Variables for UI stuff. 

	public Canvas menuCanvas;
	public Canvas inGameCanvas;
	public Canvas gameOverCanvas;
	[SerializeField]
	public TextMesh scoreText;

	public static GameManager instance;
	public GameState currentGameState = GameState.menu;

	void Update()
	{
	}

	void Awake()
	{
		instance = this;
	}

	void Start()
	{
		SetGameState(GameState.menu);
	}

	// When playing. 
	public void StartGame()
	{
		SetGameState(GameState.inGame);
	}

	// Call when player loses. 
	public void GameOver()
	{
		SetGameState(GameState.gameOver);
	}

	// Go back to menu. 
	public void BackToMenu()
	{
		SetGameState(GameState.menu);
	}

	// Sets game state... 
	void SetGameState(GameState newGameState)
	{
		if (newGameState == GameState.menu)
		{
			//setup Unity scene for menu state
			menuCanvas.enabled = true;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = false;

		}
		else if (newGameState == GameState.inGame)
		{
			//setup Unity scene for inGame state
			menuCanvas.enabled = false;
			inGameCanvas.enabled = true;
			gameOverCanvas.enabled = false;

		}
		else if (newGameState == GameState.gameOver)
		{
			//setup Unity scene for gameOver state
			menuCanvas.enabled = false;
			inGameCanvas.enabled = false;
			gameOverCanvas.enabled = true;

		}

		currentGameState = newGameState;
	}

}
