using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public Rigidbody BalonRigidBody;
    public float fuerza = 10f;

    public Transform BalonTransform;
    public Transform DestinoTransform;
    public Transform TransformIndicador;

    public Animator DireccionAnimator;
    public Animator ElevacionAnimator;
    public Animator IndicadorAnimator;

    public float NivelDeFuerza1;  

    enum shotstate
    {
        Horizontalaim, 
        Verticalaim, 
        Strengthaim, 
        done
    }


    shotstate currentstate;
    void Start()
    {
        ResetShot();
    }

    public void ActionButton()
    {
        switch (currentstate)
        {
            case shotstate.Horizontalaim:
                lockHorizontal();
                break;

            case shotstate.Verticalaim:
                lockVertical();
                break;

            case shotstate.Strengthaim:
                lockPowerandShoot();
                break;

            case shotstate.done:
                ResetShot();
                break;
        }
    }
    public void ResetButton()
    {
        ResetShot();
    }
    void lockHorizontal()
    {
        DireccionAnimator.speed = 0;
        ElevacionAnimator.speed = 1;

        currentstate = shotstate.Verticalaim;
    }
    void lockVertical()
    {
        ElevacionAnimator.speed = 0;
        IndicadorAnimator.speed = 1;

        currentstate = shotstate.Strengthaim;
    }
    void lockPowerandShoot()
    {
        IndicadorAnimator.speed = 0;
        IndicadorAnimator.Update(0);

        float y = TransformIndicador.localPosition.y;
        NivelDeFuerza1 = Mathf.InverseLerp(0, 2080, y);

        kickball();

        currentstate = shotstate.done;
    }

    void kickball()
    {
        Vector3 dir = (DestinoTransform.position - BalonTransform.position).normalized;

        BalonRigidBody.velocity = Vector3.zero; 
        BalonRigidBody.angularVelocity = Vector3.zero;

        BalonRigidBody.AddForce(dir * fuerza * NivelDeFuerza1, ForceMode.Impulse);
    }
    void ResetShot()
    {
        BalonRigidBody.velocity = Vector3.zero;
        BalonRigidBody.angularVelocity = Vector3.zero;
        BalonRigidBody.Sleep();

        BalonTransform.position = new Vector3(0f, 0.5f, 0f);
        BalonTransform.rotation = Quaternion.identity;

        DireccionAnimator.Play(0, 0, 0f);
        DireccionAnimator.Update(0);
        DireccionAnimator.speed = 1;

        ElevacionAnimator.Play(0, 0, 0f);
        ElevacionAnimator.Update(0);
        ElevacionAnimator.speed = 0;

        IndicadorAnimator.Play(0, 0, 0f);
        IndicadorAnimator.Update(0);
        IndicadorAnimator.speed = 0;
        currentstate = shotstate.Horizontalaim;
    }
}
