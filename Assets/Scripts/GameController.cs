using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    // PRIVATE INSTANCE VARIABLES
    private int _scoreValue;
    private int _livesValue;
    private bool finish;

    [SerializeField]
    private AudioSource _gameoverSound;
    [SerializeField]
    private AudioSource _congratsSound;

    // PUBLIC ACCESS METHODS
    public int ScoreValue
    {
        get
        {
            return this._scoreValue;
        }

        set
        {
            this._scoreValue = value;
            this.ScoreLabel.text = "Score: " + this._scoreValue;
        }
    }

    public int LivesValue
    {
        get
        {
            return this._livesValue;
        }

        set
        {
            this._livesValue = value;
            if (this._livesValue <= 0)
            {
                this._endGame();
            }
            else {
                this.LivesLabel.text = "Lives: " + this._livesValue;
            }
        }
    }

    public bool Finish
    {
        get
        {
            return this.finish;
        }

        set
        {
            this.finish = value;
            this._endGame();
           
        }
    }

    // PUBLIC INSTANCE VARIABLES
    public Text LivesLabel;
    public Text ScoreLabel;
    public Text GameOverLabel;
    public Text HighScoreLabel;
    public Text CongratsLabel;
    public Button RestartButton;
    public HeroController hero;
    

    // Use this for initialization
    void Start()
    {
        this._initialize();

    }

    // Update is called once per frame
    void Update()
    {

    }

    //PRIVATE METHODS ++++++++++++++++++

    //Initial Method
    private void _initialize()
    {
        this.ScoreValue = 0;
        this.LivesValue = 5;
        this.GameOverLabel.gameObject.SetActive (false);

        this.HighScoreLabel.gameObject.SetActive (false);
        this.RestartButton.gameObject.SetActive(false);
        this.CongratsLabel.gameObject.SetActive(false);
    }

    private void _endGame()
    {
        if (this.finish == true)
        {
            this._congratsSound.Play();
            this.gameObject.SetActive(true);
            hero.gameObject.SetActive(false);
            this.CongratsLabel.gameObject.SetActive(true);
        }
        else {
            this._gameoverSound.Play();
            this.GameOverLabel.gameObject.SetActive(true);
            this.LivesLabel.gameObject.SetActive(false);
            this.ScoreLabel.gameObject.SetActive(false);
            hero.gameObject.SetActive(false);
        }
        this.HighScoreLabel.text = "High Score: " + this._scoreValue;
        this.HighScoreLabel.gameObject.SetActive(true);
        this.RestartButton.gameObject.SetActive (true);
    }

    // PUBLIC METHODS

    public void RestartButtonClick()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}