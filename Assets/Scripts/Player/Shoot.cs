using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bloodSplat;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Shooting();


    }

    void Shooting()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 center = new Vector3(.5f, .5f, 0);
            Ray rayOrigin = Camera.main.ViewportPointToRay(center);
            RaycastHit hitInfo;

            if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 0 | 1 << 9))
            {
                Health health = hitInfo.collider.GetComponent<Health>();

                if (health != null)
                {
                    //Instantiate blood
                    //postion of raycast hit
                    //rotate towards the hit normal position (surface normal)

                    Instantiate(_bloodSplat, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                    health.Damage(10);
                }

                Debug.Log("Hit: " + hitInfo.collider.name);
            }
        }
    }
}
