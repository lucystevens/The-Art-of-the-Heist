using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CameraFollowSprite : MonoBehaviour {
    public Transform target;
    public float distance;

    // Update is called once per frame
    void Update() {
        transform.position = new Vector3(
            target.position.x,
            target.position.y,
            distance
        );
    }
}
