﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
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

            if (Physics.Raycast(rayOrigin, out hitInfo))
            {

                Debug.Log("Hit: " + hitInfo.collider.name);
            }
        }
    }
}