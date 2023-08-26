using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OliveController : ProjectileController
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }
    public void HitClone()
    {
        ProjectileController oliveShot = Instantiate(this, this.gameObject.transform.position, Quaternion.identity);
        oliveShot.projectileSpeed = 5;
        oliveShot.projectileDirection = this.projectileDirection + new Vector2(1,0);
        ProjectileController oliveShot2 = Instantiate(this, this.gameObject.transform.position, Quaternion.identity);
        oliveShot2.projectileSpeed = 5;
        oliveShot2.projectileDirection = this.projectileDirection + new Vector2(-1, 0);
    }
}
