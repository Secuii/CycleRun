using System.Collections;
using UnityEngine;

public class CycleController : MonoBehaviour
{
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        ScoreController = transform.GetComponent<ScoreController>();

        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        SpawnController = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnerController>();
        FloorController = GameObject.FindGameObjectWithTag("Floor").GetComponent<FloorController>();

        StartCoroutine(SpawnCyclonWall());
    }

    private void Update()
    {
        if (GameController.IsStartGame)
        {
            transform.Translate(Vector3.forward * PlayerSpeed * Time.deltaTime);

            PlayerMovement();
            PlayeractionButton();
            IncreasePlayerSpeed();


            if (ScoreController.GameCountdownTime <= 0)
            {
                GameOver();
            }
        }
        Quaternion camTurnAngle = Quaternion.AngleAxis(PlayerMovementX * RotationSpeed, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, camTurnAngle, 0.5f);
    }

    private void IncreasePlayerSpeed()
    {
            PlayerIncreaseSpeedTimer += Time.deltaTime;
            PlayerSpeed += PlayerIncreaseSpeedTimer / 10;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall") || other.CompareTag("cubeFloor"))
        {
            GameOver();
        }

        else if (other.CompareTag("Point"))
        {
            PlayerScore ++;
            if(PlayerScore > PlayerMaxScore)
            {
                PlayerMaxScore++;
            }
            ScoreController.SetPlayerScore();
            ScoreController.AddTimeGameCountdown();
            AudioCoinSound.Play();

            if (!PulseBar.IsTimeCharged)
            {
                PulseBar.PulseCoinAdd += .3f;
            }

            if (PlayerScore % 5 == 0)
            {
                GameController.StartCubeAnimation();
                FloorController.ChangeLevelDifficult(5);

                if (PlayerScore % 10 == 0)
                {
                    GameController.DestroyAllWall();
                }
            }
            SpawnController.SpawnPointSphere();
            Destroy(other.gameObject);
        }
    }
    private void GameOver()
    {
        //Bars
        PulseBar.ResetValues();
        SlowBar.ResetValues();
        //Score
        PlayerScore = 0;
        ScoreController.ResetValues();

        PlayerIncreaseSpeedTimer = 0f;

        transform.position = PlayerDeadPosition;
        //STOP CUBE ANIMATION
        FloorController.StopAnim();
        GameController.cameraPivot = transform.position;
        GameController.SetCameraParent("Menu");

        GameController.ActiveDeadMenu();
        ExplotionSound.Play();
    }
    IEnumerator SpawnCyclonWall()
    {
        while (true)
        {
            yield return new WaitForSeconds(CycleWallSpawnDelay);
            if (GameController.IsStartGame)
            {
                Instantiate(CycleWall, WallSpawner.position, transform.rotation);
            }
        }
    }
    public void SetCycleWallColor(Color _color)
    {
        CycleWall.GetComponent<Renderer>().sharedMaterial.SetColor("colorWall", _color);
    }  

    #region PlayerMovement
    private void PlayeractionButton()
    {
        switch (GameController.PheriphericalSelected)
        {
            case "Mouse":
                if (Input.GetMouseButtonDown(0))
                {
                    PulseBar.ActivateBar();
                    AudioPulseSound.Play();
                }

                if (Input.GetMouseButtonDown(1))
                {
                    SlowBar.ActivateBar();
                }
                if (Input.GetMouseButtonUp(1))
                {
                    SlowBar.ActivateBar();
                }
                break;
            case "Joystick":

                if (Input.GetButton("Pulse") || Input.GetButton("Pulse2"))
                {
                    PulseBar.ActivateBar();
                    AudioPulseSound.Play();
                }
                if (Input.GetButtonDown("Slowmotion") || Input.GetButtonDown("Slowmotion2"))
                {
                    PulseBar.ActivateBar();
                }
                if (Input.GetButtonUp("Slowmotion") || Input.GetButtonUp("Slowmotion2"))
                {
                    PulseBar.ActivateBar();
                }

                break;
            case "Keyboard":

                if (Input.GetKeyDown(KeyCode.A))
                {
                    PulseBar.ActivateBar();
                    AudioPulseSound.Play();
                }
                if (Input.GetKey(KeyCode.D))
                {
                    PulseBar.ActivateBar();
                }
                if (Input.GetKeyUp(KeyCode.D))
                {
                    PulseBar.ActivateBar();
                }
                break;
        }
    }

    private void PlayerMovement()
    {
        switch (GameController.PheriphericalSelected)
        {
            case "Mouse":
                if (Input.GetAxis("Mouse X") >= 1f)
                {
                    PlayerMovementX += Input.GetAxis("Mouse X");
                }
                else if (Input.GetAxis("Mouse X") <= -1f)
                {
                    PlayerMovementX += Input.GetAxis("Mouse X");
                }
                break;
            case "Joystick":
                AxisMovement("HorizontalJ", .1f, 20, 60, 2);
                break;
            case "Keyboard":
                float MouseSensibility = Mathf.Lerp(20, 60, GameController.mouseSens);

                if (Input.GetKey(KeyCode.RightArrow))
                {
                    PlayerMovementX += MouseSensibility / 2;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    PlayerMovementX -= MouseSensibility / 2;
                }               
                break;
        }
    }

    private void AxisMovement(string _axis, float _axisMin, int _lerpMin, int _lerpMax, int _divided)
    {
        float MouseSensibility = Mathf.Lerp(_lerpMin, _lerpMax, GameController.mouseSens);

        if (Input.GetAxis(_axis) >= _axisMin)
        {
            PlayerMovementX += (MouseSensibility / _divided) * Input.GetAxis(_axis);
        }
        else if (Input.GetAxis(_axis) <= -_axisMin)
        {
            PlayerMovementX += (MouseSensibility / _divided) * Input.GetAxis(_axis);
        }
    }
    #endregion

    [SerializeField] private PulseBarSc PulseBar = null;
    [SerializeField] private SlowMoBarSc SlowBar = null;

    [SerializeField] private GameObject CycleWall = null;
    [SerializeField] private Transform WallSpawner = null;

    [SerializeField] private AudioSource AudioCoinSound = null;
    [SerializeField] private AudioSource AudioPulseSound = null;
    [SerializeField] private AudioSource ExplotionSound = null;

    public new Camera camera;

    public int PlayerScore { get; set; } = 0;
    public int PlayerMaxScore { get; set; } = 0;

    public float OriginalPlayerSpeed { get; private set; } = 12f;
    public float PlayerSpeed { get; set; } = 12f;

    public float CycleWallSpawnDelay { get; set; } = 0.2f;

    private Vector3 PlayerDeadPosition = new Vector3(999, 999, 999);

    private ScoreController ScoreController;
    private GameController GameController;
    private SpawnerController SpawnController;
    private FloorController FloorController;

    private float PlayerIncreaseSpeedTimer = 0f;

    private float PlayerMovementX = 0f;
    private readonly float RotationSpeed = 0.2f;
}