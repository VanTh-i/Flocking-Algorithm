using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockAgent : MonoBehaviour
{
    private Flock agentFlock;
    public Flock AgentFlock{ get { return agentFlock; }}
    
    private Collider2D agentCollider;
    public Collider2D AgentCollider{get {return agentCollider; }}

    private void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Move(Vector2 velocity)
    {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime; 
    }
}
