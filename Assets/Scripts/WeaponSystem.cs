using System.Linq;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private WeaponType[] weaponTypes;
    [SerializeField] private AudioSource soundSource;

    private WeaponType currentWeapon;
    public WeaponType CurrentWeapon => currentWeapon;

    private SpriteRenderer renderer;

    private void Start()
    {
        renderer = GetComponentInChildren<SpriteRenderer>();
        currentWeapon = weaponTypes[0];
    }

    private void Update()
    {
        if (Input.GetKeyDown("1") || Input.GetKeyDown("2"))
        {
            currentWeapon = weaponTypes.FirstOrDefault(e => e.HotKey == Input.inputString);
            renderer.sprite = CurrentWeapon?.Body;
            soundSource.PlayOneShot(CurrentWeapon?.PullSound);
        }
    }

    public void PlayShot()
    {
        soundSource.PlayOneShot(CurrentWeapon?.ShotSound);
    }
}