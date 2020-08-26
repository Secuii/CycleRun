using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    void Start () {
        playerObj = GameObject.FindGameObjectWithTag ("Player");
        CameraPosition = GameObject.FindGameObjectWithTag ("MainCamera").transform;
        SpawnController = GameObject.FindGameObjectWithTag ("Spawn").GetComponent<SpawnerController> ();
        FloorController = GameObject.FindGameObjectWithTag ("Floor").GetComponent<FloorController> ();
        PlayerInitialPosition = playerObj.transform.position;
        IsStartGame = false;

        SetCameraParent ("Menu");
        HidePlayer ();
    }

    private void Update () {
        if (CameraParent != null) {
            MoveCamera ();
            RotateCamera ();
            MainMenuCameraPosition ();
        }
    }
    private void MoveCamera () {
        CameraPosition.transform.position = Vector3.Lerp (CameraPosition.position, CameraParent.position, 0.03f);
    }
    //TODO: hola
    private void RotateCamera () {
        CameraPosition.transform.rotation = Quaternion.Lerp (CameraPosition.rotation, CameraParent.rotation, 0.1f);
    }

    public void ResetTimerCoundown () {
        TextChange.timerCount = ResetCountDown;
        GameObject.FindGameObjectWithTag ("countDownPanel").GetComponent<TextChange> ().StopAnimation ();
    }

    //ANIMATION CUBE CODE
    public void StartCubeAnimation () {
        FloorController.CallStart ();
    }

    //CAMERA CODE
    public void SetCameraParent (string _index) {
        switch (_index) {
            case "Menu":
                CameraPosition.parent = menuPivot;
                CameraParent = menuPivot.transform;

                break;
            case "Player":
                CameraPosition.parent = playerPivot;
                CameraParent = playerPivot.transform;
                break;
        }
    }

    //SELECT PLAYER
    //TODO
    private void MainMenuCameraPosition () {
        playerObj.transform.GetChild (0).GetChild (0).localPosition = Vector3.Lerp (new Vector3 (playerObj.transform.GetChild (0).GetChild (0).localPosition.x, 0f, 0f), new Vector3 (characterPosX, 0f, 0f), 0.5f);
    }

    //SELECT PLAYER CODE
    public void SelectPlayerChange (int _value) {
        playerMeshSelected = _value;
        switch (_value) {
            case 0:
                characterPosX = 0f;
                playerObj.GetComponent<CycleController> ().SetCycleWallColor (new Color (0.55f, 0.98f, 0));
                break;
            case 1:
                characterPosX = -6f;
                playerObj.GetComponent<CycleController> ().SetCycleWallColor (new Color (0.84f, 0.25f, 0));
                break;
        }
    }

    private void HidePlayer () {
        switch (playerMeshSelected) {
            case 0:
                playerObj.transform.GetChild (0).GetChild (0).GetChild (1).gameObject.SetActive (false);
                break;
            case 1:
                playerObj.transform.GetChild (0).GetChild (0).GetChild (0).gameObject.SetActive (false);
                break;
        }
    }

    public void Controls (int _value) {
        switch (_value) {
            case 1:
                mouseControls.SetActive (true);
                joystickControls.SetActive (false);
                keyboardControls.SetActive (false);
                PheriphericalSelected = "Mouse";
                break;
            case 2:
                mouseControls.SetActive (false);
                joystickControls.SetActive (true);
                keyboardControls.SetActive (false);
                PheriphericalSelected = "Joystick";
                break;
            case 3:
                mouseControls.SetActive (false);
                joystickControls.SetActive (false);
                keyboardControls.SetActive (true);
                PheriphericalSelected = "Keyboard";
                break;
        }
    }

    //GAME STATE CODE
    public void ActiveDeadMenu () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        MainMenuPanel.SetActive (true);
        PulsePanel.SetActive (false);
        ResetTimerCoundown ();
        IsStartGame = false;
    }

    //MENU BUTTON CODE
    public void StartGame () {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerObj.transform.position = PlayerInitialPosition;

        SetCameraParent ("Player");

        Countdown.StartAnimation ();

        MainMenuPanel.SetActive (false);
        ScorePanel.SetActive (true);
        PulsePanel.SetActive (true);

        for (int i = 0; i < GameObject.FindGameObjectsWithTag ("wall").Length; i++) {
            Destroy (GameObject.FindGameObjectsWithTag ("wall") [i].gameObject);
        }
        for (int i = 0; i < GameObject.FindGameObjectsWithTag ("Point").Length; i++) {
            Destroy (GameObject.FindGameObjectsWithTag ("Point") [i].gameObject);
        }
        SpawnController.SpawnPointSphere ();
    }

    public void DestroyAllWall () {
        for (int i = 0; i < GameObject.FindGameObjectsWithTag ("wall").Length; i++) {
            Destroy (GameObject.FindGameObjectsWithTag ("wall") [i].gameObject);
        }
    }

    public void InstructionsMenu () {
        instructionPanel.SetActive (false);
        MainMenuPanel.SetActive (false);
        controlPanel.SetActive (true);
    }

    public void ControlsMenu () {
        instructionPanel.SetActive (true);
        controlPanel.SetActive (false);

    }

    public void BackToMenu () {
        MainMenuPanel.SetActive (true);
        controlPanel.SetActive (false);
        changeCyclePanel.SetActive (false);
        settingsPanel.SetActive (false);
        HidePlayer ();
    }

    public void OpenChangeCycleMenu () {
        MainMenuPanel.SetActive (false);
        changeCyclePanel.SetActive (true);

        for (int i = 0; i < playerObj.transform.GetChild (0).GetChild (0).childCount; i++) {
            playerObj.transform.GetChild (0).GetChild (0).GetChild (i).gameObject.SetActive (true);
        }
    }

    public void OpenSettingsMenu () {
        MainMenuPanel.SetActive (false);

        settingsPanel.SetActive (true);
    }

    public void ExitGame () {
        Application.Quit ();
    }

    [SerializeField] private GameObject mouseControls = null;
    [SerializeField] private GameObject joystickControls = null;
    [SerializeField] private GameObject keyboardControls = null;
    [SerializeField] private Slider mouseSensibility = null;
    [SerializeField] private Transform playerPivot = null;
    [SerializeField] private Transform menuPivot = null;
    [SerializeField] private GameObject controlPanel = null;
    [SerializeField] private GameObject MainMenuPanel = null;
    [SerializeField] private GameObject instructionPanel = null;
    [SerializeField] private GameObject settingsPanel = null;
    [SerializeField] private GameObject changeCyclePanel = null;
    [SerializeField] private GameObject ScorePanel = null;
    [SerializeField] private GameObject PulsePanel = null;

    [SerializeField] private TextChange Countdown = null;

    //[SerializeField] private GameObject PlayerCameraPosition = null;
    //[SerializeField] private GameObject MenuCameraPosition = null;

    public float mouseSens {
        get => mouseSensibility.value;
        private set => mouseSensibility.value = value;
    }
    public Vector3 cameraPivot {
        get => GameObject.FindGameObjectWithTag ("selectPivot").transform.position;
        set => GameObject.FindGameObjectWithTag ("selectPivot").transform.position = value;
    }

    public bool IsStartGame { get; set; }

    public string PheriphericalSelected { get; private set; } = "Mouse";

    private float characterPosX;
    private int playerMeshSelected = 0;

    private FloorController FloorController;
    private SpawnerController SpawnController;

    private GameObject playerObj;
    private Vector3 PlayerInitialPosition;

    private Transform CameraPosition;
    private Transform CameraParent;

    private int ResetCountDown = 3;
}