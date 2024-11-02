using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    private List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior agentBehavior;

    public int startingCount = 250;
    const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultipler = 0.5f;
    
    private float squareMaxSpeed;
    private float squareNeighborRadius;
    private float squareAvoidanceRadius;
    public float SquareAvoidanceRadius {get {return squareAvoidanceRadius;}}

    private void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultipler * avoidanceRadiusMultipler;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent agent = Instantiate(agentPrefab
                , Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(Vector3.forward *Random.Range(0f, 360f))
                , transform
                );
            agent.name = "Agent" + i;
            agent.Initialize(this);
            agents.Add(agent);
        }
    }

    private void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);
            Vector2 move = agentBehavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            agent.Move(move);
        }
    }

    private List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }

        return context;
    }
}
