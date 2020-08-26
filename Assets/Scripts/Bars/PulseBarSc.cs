using UnityEngine;
using UnityEngine.UI;
public class PulseBarSc : BarParentSc {
    public override void ActivateBar () {
        if (BarCombo >= 1 && IsTurboActivated) {
            Timer = 0;
            IsTurboActivated = false;
            BarCombo--;

            ToggleBarCharged ();
            SetComboText ();
        }
    }
    public override void BarDown () {
        PulseCoinAdd = 0f;
        float playerSpeedReduction = Mathf.InverseLerp (1f, 1.5f, Timer);
        PlayerController.PlayerSpeed = Mathf.Lerp (PlayerController.OriginalPlayerSpeed, 24f, Timer);
        PlayerController.PlayerSpeed = Mathf.Lerp (PlayerMaxAcceleration, PlayerController.OriginalPlayerSpeed, playerSpeedReduction);

        Debug.Log(PlayerController.PlayerSpeed);
        BarSize = new Vector2 (Mathf.Lerp (MaxBarSizeX, MinBarSizeX, Timer), BarSize.y);
        CameraFieldOfView = Mathf.Lerp (60, 85, Timer * 2);

        if (Timer >= OneSecond) {
            IsTurboActivated = true;
            ToggleBarCharged ();
            Timer = 0f;
        }
    }
    public override void BarUp () {
        float secondDiv10 = Mathf.InverseLerp (0f, 10f, Timer);
        BarSize = new Vector2 (Mathf.Lerp (MinBarSizeX, MaxBarSizeX, secondDiv10 + PulseCoinAdd), BarSize.y);

        if (BarSize.x >= MaxBarSizeX && BarCombo < MaxComboCount) {
            Timer = 0;
            BarCombo++;
            SetComboText ();
            BarSize = new Vector2 (MinBarSizeX, BarSize.y);
            PulseCoinAdd = ResetPulseCoinAdd;
            MaxComboAnim.SetBool ("Play", true);
        } else if (BarSize.x >= .1f && BarSize.x <= 1) {
            MaxComboAnim.SetBool ("Play", false);
        }
        if (CameraFieldOfView > 60) {
            CameraFieldOfView = Mathf.Lerp (85, 60, Timer * 2);
        }
    }
    public override void ResetValues () {
        Timer = 0f;
        BarCombo = 0;
        PulseCoinAdd = 0;
        IsTurboActivated = true;
        BarSize = new Vector2 (MinBarSizeX, BarSize.y);
        CameraFieldOfView = 60;
        SetComboText ();
    }
    public void SetComboText () {
        ComboCounter.text = BarCombo.ToString ();
        MaxComboCounter.text = BarCombo.ToString ();
    }
    [SerializeField] private Text ComboCounter = null;
    [SerializeField] private Text MaxComboCounter = null;
    [SerializeField] private Animator MaxComboAnim = null;
    private int BarCombo = 0;
    public float PulseCoinAdd { get; set; }
    private bool IsTurboActivated = true;
    private readonly float ResetPulseCoinAdd = 0f;
    private readonly int MaxComboCount = 3;
    private readonly float OneSecond = 1.5f;

    private readonly float PlayerMaxAcceleration = 24f;
}