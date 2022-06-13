using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playeraudiomanager : MonoBehaviour
{
    FMOD.Studio.EventInstance Dash, Lattack, Hattack, Pushback, Hurt,walk,attackvoice,attackhitlight,attackhitheavy, chargedsound,chargingsound,musicmodulate;

    void Start()
    {
        Dash = FMODUnity.RuntimeManager.CreateInstance("event:/dash");
        Lattack = FMODUnity.RuntimeManager.CreateInstance("event:/Attack light");
        Hattack = FMODUnity.RuntimeManager.CreateInstance("event:/Action heavy");
        Pushback = FMODUnity.RuntimeManager.CreateInstance("event:/Action pushback");
        Hurt = FMODUnity.RuntimeManager.CreateInstance("event:/Player hurt");
        walk = FMODUnity.RuntimeManager.CreateInstance("event:/Playerwalking");
        attackvoice = FMODUnity.RuntimeManager.CreateInstance("event:/Player attack voice");
        attackhitlight = FMODUnity.RuntimeManager.CreateInstance("event:/attack hit light");
        attackhitheavy = FMODUnity.RuntimeManager.CreateInstance("event:/attack hit heavy");
        chargedsound = FMODUnity.RuntimeManager.CreateInstance("event:/heavy attack trigger notif");
        chargingsound = FMODUnity.RuntimeManager.CreateInstance("event:/charging noise");
        musicmodulate = FMODUnity.RuntimeManager.CreateInstance("snapshot:/hit");
    }

    public void Dashsfx()
    {
        Dash.start();
    }

    public void Lattacksfx()
    {
        Lattack.start();
    }
    public void Hattacksfx()
    {
        Hattack.start();
    }
    public void Pushbacksfx()
    {
        Pushback.start();
    }
    public void Hurtsfx()
    {
        Hurt.start();
    }

    public void Walkingsfx()
    {
        walk.start();
    }

    public void attackvoicesfx()
    {
        attackvoice.start();
    }

    public void attackhitlightsfx()
    {
        attackhitlight.start();
    }

    public void attackhitheavysfx()
    {
        attackhitheavy.start();
    }

    public void attackhnotif()
    {
        chargedsound.start();
    }

    public void chargeup()
    {
        chargingsound.start();
    }

    public void startmodulate()
    {
        musicmodulate.start();
    }

    public void stopmodulate()
    {
        musicmodulate.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
