using UnityEngine;

public class FPSLimiter : MonoBehaviour
{
    public int fps;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = fps;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
