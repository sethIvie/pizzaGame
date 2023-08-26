using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class PineappleController : ProjectileController
{
    private Stopwatch timer;
    // Start is called before the first frame update

    protected override void Start()
    {
        base.Start();
        timer = new Stopwatch();
        timer.Start();
        Debug.Log("pine apple time started");
    }

    // Update is called once per frame
     void Update()
    {
        MoveProjectile();
        timer.Stop();
        Debug.Log(timer.Elapsed.Seconds);
        if (timer.Elapsed.Seconds >= 1)
        {
            this.projectileSpeed = 5;
        } else
        {
            timer.Start();
        }
       
    }
}
