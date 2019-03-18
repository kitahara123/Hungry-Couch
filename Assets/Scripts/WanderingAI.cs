using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderingAI : AIState
{
    [SerializeField] private float speed = 3;
    [SerializeField] private float obstacleRange = 5.0f;

    public override event Action<AIState> OnStateEnded;
    public override event Action<AIState> OnStateStarted;

    private void Update()
    {
        if (!Active || !creature.Alive) return;

        transform.Translate(Vector3.right * speed * Time.deltaTime);

        RaycastHit2D hit = Physics2D.BoxCast(transform.position, transform.localScale, 0, transform.right, obstacleRange,
            LayerMask.GetMask("Walls"));

        if (hit)
        {
            var angle = Random.Range(-110, 110);
            transform.Rotate(0, 0, angle);
        }
    }
}