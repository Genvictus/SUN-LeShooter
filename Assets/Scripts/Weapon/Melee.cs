using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
   [Header("References")]
    [SerializeField] private MeleeData meleeData;
    [SerializeField] private Transform cameraTransform;

    [Header("Visuals")]
    [SerializeField] private LineRenderer lineRenderer;

    float timeSinceLastAttack;
    private void Start()
    {
        PlayerAttack.attackInput += Attack;
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
    }

    private bool CanAttack()
    {
        return timeSinceLastAttack > meleeData.fireRate;
    }

    public void Attack()
    {

        if (CanAttack())
        {
            Debug.Log("Attack2");
            timeSinceLastAttack = 0;

            RaycastHit hit;
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, meleeData.maxDistance))
            {   
                Debug.Log(hit.transform.name + meleeData.damage);
                IDamageAble damageAble = hit.transform.GetComponent<IDamageAble>();
                damageAble?.Damage(meleeData.damage);

                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
            }
            else {
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, transform.position + transform.forward * meleeData.maxDistance);
            }
            OnMeleeAttack();
        }

    }

    private void OnMeleeAttack()
    {

    }


    public void DebuffAttack(float debuff)
    {
        meleeData.damage /= debuff;
    }

    public void BuffAttack(float buff)
    {
        meleeData.damage *= buff;
    }
}
