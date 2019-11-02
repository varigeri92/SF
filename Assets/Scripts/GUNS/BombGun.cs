using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGun : Gun
{
    	public Transform spawnPoint;
	public float fireRate;
	public GameObject projectile;
	public float projectileForce;

	[SerializeField]
	float timer = 1;

	[SerializeField]
	bool canShoot;

	private void OnEnable()
	{
		if (gunObject != null) {
			fireRate = gunObject.fireRate;
		}
	}

	private void Update()
	{
		if(!canShoot){
			timer += fireRate * Time.deltaTime;
			if (timer >= 1) {
				canShoot = true;
			}
		}
	}

	public override void Shooting(bool isPlayer)
	{
		if (ammo > 0 || ammo == -999) {
			if (canShoot) {
				timer = 0;
				canShoot = false;
				if (ammo != -999) {
					ammo--;
				}
				Shoot(isPlayer);

			}
		}
	}

	private void Shoot( bool isPlayer){
		GameObject go = Instantiate(projectile, spawnPoint.position, transform.rotation);
		if(isPlayer){
			go.layer = 12;
		}else{
			go.layer = 11;
		}
		base.Playsound();

		if(gunObject != null && isPlayer){
			go.GetComponent<ProjectileBehaviour>().dmg = gunObject.damage;	
		}
	}
}
