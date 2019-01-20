using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class PlayerCannon : PlayerWeapon
{
	public Transform missileSpawnLocation;
	public GameObject missile;
    private float timeBetweenShots;
    public float spreadRiseSpeed;
	public float spreadLowerSpeed;
	public float maxSpreadAngle;

	private float lastShotTime;
	private float spreadAngleValue;

    public float TimeBetweenShots
    {
        get
        {
            return timeBetweenShots/(PlayerPerksManager.bonusPercentAttackSpeed+1f);
        }

        set
        {
            timeBetweenShots = value;
        }
    }

    void Start()
	{
		spreadAngleValue = 0f;
		lastShotTime = Time.time;
	}

	void FixedUpdate () 
	{
		if(Input.GetAxisRaw("Fire1")>0)
		{
            if (EventSystem.current.IsPointerOverGameObject())
                return;

			if(Time.time > lastShotTime + TimeBetweenShots)
				Shoot();
			spreadAngleValue = spreadAngleValue + spreadRiseSpeed > maxSpreadAngle ? maxSpreadAngle : spreadRiseSpeed + spreadAngleValue;
		}
		else if(!Input.GetKey(KeyCode.Mouse0))
			spreadAngleValue = spreadAngleValue <= spreadLowerSpeed ? 0 : spreadAngleValue - spreadLowerSpeed;
	}

	protected override void Shoot()
	{
		GameObject spawnedMissile=(GameObject)Instantiate(missile, missileSpawnLocation, false);
		spawnedMissile.transform.SetParent(null);
		spawnedMissile.transform.Rotate(0f, 0f, (Random.value-0.5f)*spreadAngleValue);
        spawnedMissile.GetComponent<Bullet>().damage = damage*(PlayerPerksManager.bonusPercentDamage+1);
        Destroy(spawnedMissile, 2f);
		lastShotTime = Time.time;
	}
}

