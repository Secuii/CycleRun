using UnityEngine;

public class SlowMoBarSc : BarParentSc {
    public override void ActivateBar () {
        Timer = 0f;
        if (BarSize.x >= MinBarSizeX) {
            ToggleBarCharged ();
        }
    }

    public override void BarDown () {
        PlayerController.CycleWallSpawnDelay = CycleWallSpawnSlowMoDelay;
        float reductionBar = Mathf.InverseLerp (0f, 10f, Timer);
        BarSize = new Vector2 (Mathf.Lerp (MaxBarSizeX, MinBarSizeX, reductionBar), BarSize.y);

        float playerSpeedReduction = Mathf.InverseLerp (0f, 0.2f, Timer);
        PlayerController.PlayerSpeed = Mathf.Lerp (PlayerController.OriginalPlayerSpeed, 3f, playerSpeedReduction);

    }

    public override void BarUp () {
        PlayerController.CycleWallSpawnDelay = CycleWallSpawnOriginalDelay;
        float playerSpeedReduction = Mathf.InverseLerp (0f, 0.5f, Timer);
        PlayerController.PlayerSpeed = Mathf.Lerp (3f, PlayerController.OriginalPlayerSpeed, playerSpeedReduction);
        MaxBarSizeX = BarSize.x;
        BarSize = new Vector2 (BarSize.x, BarSize.y);

    }

    public override void ResetValues () {
        PlayerController.CycleWallSpawnDelay = CycleWallSpawnOriginalDelay;
        Timer = 0f;
        BarSize = new Vector2 (MaxBarSizeX, BarSize.y);
    }

    private readonly float CycleWallSpawnSlowMoDelay = 0.5f;
    private readonly float CycleWallSpawnOriginalDelay = 0.2f;
}