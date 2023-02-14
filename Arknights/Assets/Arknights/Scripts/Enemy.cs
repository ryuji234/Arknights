using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;

    private Transform target;
    private int wavepointIndex = 0;
    private bool Isrun = true;

    void Start()
    {
        target = Waypoint.points[0];
    }
    void Update()
    {
        if(Isrun)
        {
            Vector3 dir = target.position - transform.position;
            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

            if (Vector3.Distance(transform.position, target.position) <= 0.2f)
            {
                GetNextWaypoint();
            }
        }
        
    }

    void GetNextWaypoint()
    {
        if(wavepointIndex >= Waypoint.points.Length - 1)
        {
            Destroy(gameObject);
            return;
        }
        wavepointIndex++;
        target = Waypoint.points[wavepointIndex];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Operators")
        {
            Isrun = false;
        }
    }
}
