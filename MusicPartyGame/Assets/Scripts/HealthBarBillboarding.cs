using UnityEngine;

public class HealthBarBillboarding : MonoBehaviour
{
    public GameObject cam;

    void Update()
    {
        transform.rotation = cam.transform.rotation;
    }
}
