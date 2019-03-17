using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderingAI : AIState
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float obstacleRange = 5.0f;

    public override event Action<AIState> OnStateEnded;

    public override bool IsSwitchNeeded()
    {
        return true;
    }

    private void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale, 0, transform.up, obstacleRange,
            LayerMask.GetMask("Walls"));

        if (hit)
        {
            var angle = Random.Range(-110, 110);
            transform.Rotate(0, 0, angle);
        }
    }
}