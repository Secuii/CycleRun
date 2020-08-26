using UnityEngine;

public abstract class BarParentSc : MonoBehaviour
{
    private void Start()
    {
        GameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        PlayerController = GameObject.FindGameObjectWithTag("Player").GetComponent<CycleController>();
        camera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        MaxBarSizeX = barObj.sizeDelta.x;

}
    private void Update()
    {
        //TODO DO GET SET
        if (GameController.IsStartGame)
        {
            Timer += Time.deltaTime;
            if (IsTimeCharged)
            {
                BarDown();
            }
            else
            {
                BarUp();
            }
        }
    }

    #region
    public abstract void BarDown();

    public abstract void BarUp();

    public abstract void ActivateBar();

    public abstract void ResetValues();
    #endregion

    public void ToggleBarCharged()
    {
        IsTimeCharged = !IsTimeCharged;
    }

    [SerializeField] private RectTransform barObj = null;
    private new Camera camera;

    private GameController GameController;
    public CycleController PlayerController { get; private set; }
    public float MinBarSizeX { get; set; } = 0f;
    public float MaxBarSizeX { get; set; }

    public float Timer { get; set; } = 0f;

    public bool IsTimeCharged { get; private set; } = false;

    public Vector2 BarSize
    {
        get => barObj.sizeDelta;
        set => barObj.sizeDelta = value;
    }

    public float CameraFieldOfView
    {
        get => camera.fieldOfView;
        set => camera.fieldOfView = value;
    }
}