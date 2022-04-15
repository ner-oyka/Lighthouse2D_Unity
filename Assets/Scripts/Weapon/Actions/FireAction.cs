using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EventBusSystem;

public class FireAction : WeaponAction
{
    public float Damage;
    [Range(0, 1)]
    public float Scattering = 0.1f;
    public Transform Spawn;
    public GameObject ParticleTest;
    public GameObject TracerTest;

    public override bool IsCanUseAction()
    {
        return true;
    }

    public override void StartUseAction()
    {
        float distance = Vector2.Distance(Camera.main.ScreenToWorldPoint(PlayerInputManager.instance.MousePosition), Spawn.position);

        Vector3 pos = Camera.main.ScreenToWorldPoint(PlayerInputManager.instance.MousePosition);
        pos.z = 0;
        pos = Random.insideUnitSphere * Vector3.Distance(Spawn.position, pos) * Scattering + pos;
        pos.z = 0;

        RaycastHit2D hitFP = Physics2D.Linecast(Spawn.position, pos);
        if (hitFP)
        {
            SpawnDecal(hitFP.point);
            SpawnTracer(hitFP.point);
        }
        else
        {
            SpawnDecal(pos);
            SpawnTracer(pos);
        }

        RaycastHit2D hit = Physics2D.Linecast(Spawn.position, pos, LayerMask.GetMask("Entity"));
        if (hit)
        {
            EventBus.RaiseEvent<IEntityHandler>(h => h.OnWeaponHit(hit.transform.GetComponent<Entity>(), Damage));
            return;
        }

    }

    public override void StopUseAction()
    {
        //
    }

    private void SpawnDecal(Vector3 pos)
    {
        //test particle
        GameObject particle = Instantiate(ParticleTest);

        particle.transform.position = pos;
        Destroy(particle, particle.GetComponent<ParticleSystem>().main.duration);
    }

    private void SpawnTracer(Vector3 pos)
    {
        GameObject tracer = Instantiate(TracerTest);

        tracer.transform.position = Spawn.position;
        tracer.GetComponent<Tracer>().target = pos;
        Destroy(tracer, 0.5f);
    }
}
