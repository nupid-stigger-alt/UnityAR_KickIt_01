using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameController : MonoBehaviour
{
    public Rigidbody BalonRigidBody;
    public float fuerza = 10f;
    public Transform BalonTransform;
    public Transform DestinoTransform;
    public Animator DireccionAnimator;
    public Animator ElevacionAnimator;
    // Start is called before the first frame update

    void Start()
    {
        DireccionAnimator.speed = 1;
        ElevacionAnimator.speed = 0;
    }
    public void PosicionarPelota()
    {
        DireccionAnimator.speed = 0;
        ElevacionAnimator.speed = 1;
        BalonRigidBody.velocity = Vector3.zero;
        BalonRigidBody.angularVelocity = Vector3.zero;
        BalonRigidBody.Sleep();
        BalonTransform.position = new Vector3(0f, 0.5f, 0f);
        BalonTransform.rotation = Quaternion.identity;
    }
    public void PatearPelota()
    {
        Vector3 Dirección = (DestinoTransform.position - BalonTransform.position).normalized;
        //print("Pelota Pateada");
        BalonRigidBody.AddForce(Dirección * fuerza, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
