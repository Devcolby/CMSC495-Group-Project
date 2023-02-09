using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    float horizontal;

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
    }
}
