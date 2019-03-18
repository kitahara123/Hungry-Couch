using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Спавнит заданные префабы
/// </summary>
public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnableObject[] prefList;
    [SerializeField] private int enemiesPerLevel = 40;
    [SerializeField] private float spawnCD = 1;
    private int enemiesKilledOnLevel;
    private bool cooldown;

    private void Start() => StartCoroutine(StartSpawner());

    /// <summary>
    /// Спавнит тот префаб экземпляров которого меньше всего на сцене
    /// </summary>
    private IEnumerator StartSpawner()
    {
        if (prefList.Length == 0) yield break;
        if (prefList.Count(e => e.Quantity > 0) == 0) yield break;

        while (enemiesPerLevel > enemiesKilledOnLevel)
        {
            var spawnIndex = -1;
            float rate = 1;
            for (var i = 0; i < prefList.Length; i++)
            {
                var pref = prefList[i];
                if (pref.Quantity == 0 || pref.CountActiveInstances >= pref.Quantity) continue;
                
                var tmpRate = (float) pref.CountActiveInstances / pref.Quantity;
                if (rate > tmpRate)
                {
                    spawnIndex = i;
                    rate = tmpRate;
                }
            }

            Spawn(spawnIndex);

            yield return new WaitForSeconds(spawnCD);
        }
    }

    private void Spawn(int spawnIndex)
    {
        if (spawnIndex < 0) return;

        var prefab = prefList[spawnIndex];
        var newEnemy = prefab.AddInstance();

        newEnemy.OnDeath += NewEnemyOnOnDeath;
        newEnemy.transform.position = transform.position;
        
        var angle = Random.Range(-110, 110);
        newEnemy.transform.Rotate(0, 0, angle);
    }

    private void NewEnemyOnOnDeath(Creature obj)
    {
        enemiesKilledOnLevel++;
        obj.OnDeath -= NewEnemyOnOnDeath;
    }
}