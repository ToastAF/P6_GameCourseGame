using UnityEngine;

public class SynthAttack3 : MonoBehaviour
{
    private Rigidbody rb;
    
    public float Frequency = 2;
    public float Amplitude = 2;
    public float Speed = 1;

    private Vector3 m_direction;
    private float m_lifetime;

    void Start()
    {
        m_lifetime = 0;
        m_direction = transform.right;
        m_direction.Normalize();

        rb = GetComponent<Rigidbody>();

        Vector3 target = GameObject.FindGameObjectWithTag("Player1").transform.position;
        Vector3 directionForce = target - transform.position;
        rb.AddForce(new Vector3(directionForce.x, 0, directionForce.z).normalized * Speed, ForceMode.Impulse);
        
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
}