﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    public float speed;

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}