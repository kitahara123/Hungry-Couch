using System;
using System.Collections;
using UnityEngine;

public class AggressiveAI : AIState
{
    [SerializeField] private float meleeRange = 1;
    [SerializeField] private float speed = 3;
    [SerializeField] private int damage = 3;
    [SerializeField] private float attackCD = 2;
    
    private bool cooldown;
    private Transform player;

    public override event Action<AIState> OnStateEnded;
    public override event Action<AIState> OnStateStarted;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        player = other.transform;
        OnStateStarted?.Invoke(this);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        OnStateEnded?.Invoke(this);
    }

    private void Update()
    {
        if (!Active) return;
        if (!creature.Alive) return;

        var direction = player.position - transform.position;

        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);

        var distance = Vector3.Distance(player.position, transform.position);
        if (distance > meleeRange)
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        
        if (!cooldown && meleeRange >= distance)
            StartCoroutine(Attack());
        
    }
    
    private IEnumerator Attack()
    {
        cooldown = true;
        player.gameObject.GetComponent<Creature>().Hurt(damage);
        yield return new WaitForSeconds(attackCD);
        cooldown = false;
    }

}