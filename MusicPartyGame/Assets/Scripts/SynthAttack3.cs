using System.Collections;
using UnityEngine;

public class SynthAttack3 : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject player;
    Player2 playerScript;

    public float Frequency = 2;
    public float Amplitude = 2;
    public float Speed = 1;

    private Vector3 m_direction;
    private float m_lifetime;

    void Start()
    {
        m_lifetime = 0;

        rb = GetComponent<Rigidbody>();
        playerScript = player.GetComponent<Player2>();

        Vector3 target = playerScript.attackDirection.transform.position;
        Vector3 directionForce = target - transform.position;

        m_direction = directionForce;
        m_direction.Normalize();
        
        rb.AddForce(new Vector3(directionForce.x, 0, directionForce.z).normalized * Speed, ForceMode.Impulse);
        StartCoroutine(DestroyObjectAfterDelay(3f));
    }

    private void FixedUpdate()
    {
        m_lifetime += Time.fixedDeltaTime;
        rb.linearVelocity = GetProjectileVelocity(m_direction, Speed, m_lifetime, Frequency, Amplitude);
    }

    private Vector3 GetProjectileVelocity(Vector3 _forward, float _speed, float _time, float _frequency, float _amplitude)
    {
        Vector3 up = Vector3.Cross(_forward, Vector3.up);
        float up_speed = Mathf.Cos(_time * _frequency) * _amplitude * _frequency;
        return up * up_speed + _forward * _speed;
    }
    
    IEnumerator DestroyObjectAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
    
    
}