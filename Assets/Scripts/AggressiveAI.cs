using System;
using UnityEngine;

public class AggressiveAI : AIState
{
    [SerializeField] private float meleeRange = 1;
    [SerializeField] private float speed = 3;
    private bool active = false;
    private Transform player;

    public override event Action<AIState> OnStateEnded;

    public override bool IsSwitchNeeded()
    {
        return active;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        active = true;
        player = other.transform;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        active = false;
        OnStateEnded?.Invoke(this);
    }

    private void Update()
    {
        if (!active) return;

        var direction = player.position - Camera.main.WorldToScreenPoint(transform.position);

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Vector3.Distance(player.position, transform.position) < meleeRange)
            transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}