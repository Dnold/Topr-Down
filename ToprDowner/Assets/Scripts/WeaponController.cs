using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WeaponController : MonoBehaviour
{
    public AnimationController animController;
    public AttackController attackController;
    public Transform anchor;
    public Transform shootPoint;
    public float delay = 350;

    public GameObject bullet;

    void Start()
    {
         attackController = GetComponent<AttackController>();   
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        mousePos = transform.position-mousePos;
        
        mousePos.Normalize();
        float angle = Mathf.Atan2(-mousePos.x, mousePos.y) * Mathf.Rad2Deg -90f;
        animController.MouseAngleToAnimationDir(angle);
        attackController.attackAngle = angle;
        anchor.rotation = Quaternion.RotateTowards(anchor.rotation, Quaternion.Euler(0, 0, angle), delay * Time.deltaTime);
    }
}
