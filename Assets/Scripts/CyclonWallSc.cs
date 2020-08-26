using UnityEngine;

public class CyclonWallSc : MonoBehaviour
{
    void Update()
    {
        if (timer <= 1f)
        {
            timer += Time.deltaTime;
            float lerp = Mathf.Lerp(0.1f, 0.8f, timer * 18);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, lerp);
        }
    }

    private float timer = 0f;
}
