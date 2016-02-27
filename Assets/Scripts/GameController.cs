using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private int _scoreValue;
    private int _livesValue;
    

    [SerializeField]
    private AudioSource _stageclearSound;


    // Public access methods for score and lives
    public int ScoreValue
    {
        get
        {
            return this._scoreValue;
        }

        set
        {

            this._scoreValue = value;
            if (this._scoreValue >= 10)
            {
                this._endGame();
            }
            else {
                this.ScoreLabel.text = "Score:" + this._scoreValue;

            }
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
            else
            {
                this.LivesLabel.text = "Lives:" + this._livesValue;
            }

        }
    }


    // PUBLIC  INSTANCE VARIABLES
    public int asteroidNumber = 4;
    public AsteroidController asteroids;
    public Text LivesLabel;
    public Text ScoreLabel;
    public Text InfoLabel;
    public Text GameOverLabel;
    public Text WinLabel;
    public Text TargetLabel;
    public Text TenTargetLabel;
    public UFOController UFO;
    public MapController map;
    public Text HighScoreLabel;
    public Button RestartButton;
    

    // Use this for initialization
    void Start () {
        this._initialize();
       
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //private methods
    private void _initialize()
    {
        this.ScoreValue = 0;
        this.LivesValue = 5;
        this.GameOverLabel.enabled = false;
        this.HighScoreLabel.enabled = false;
        
        this.RestartButton.gameObject.SetActive(false);
        this.InfoLabel.enabled = false;
        this.WinLabel.enabled = false;
        this.TargetLabel.enabled = false;
        this.TenTargetLabel.enabled = true;
        for (int asteroidCount=0; asteroidCount<this.asteroidNumber;asteroidCount++)
        {
            Instantiate(asteroids.gameObject);
        }

    }

    private void _endGame()
    {
        this.HighScoreLabel.text = "High Score:" + this._scoreValue;
        if(this._scoreValue<10)
        {
            this.GameOverLabel.enabled = true;
            this.InfoLabel.enabled = true;
            
            
        }
        else
        {
            this.TargetLabel.enabled = true;
            this.WinLabel.enabled = true;
            
        }
        this._stageclearSound.Play();
        this.UFO.gameObject.SetActive(false);
        this.map.gameObject.SetActive(false);
        this.LivesLabel.enabled = false;
        this.ScoreLabel.enabled = false;
        this.TenTargetLabel.enabled = false;
        this.HighScoreLabel.enabled = true;
        this.RestartButton.gameObject.SetActive(true);

    }

    public void RestartButtonClick()
    {
        Application.LoadLevel("Main");
    }

}
