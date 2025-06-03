using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Transform player;
    [SerializeField] private GameObject marco;

    private void Start()
    {
        marco = GetComponent<GameObject>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 3f, transform.position.z);
        transform.position = new(
            Mathf.Clamp(player.position.x, -10f, 65f),
        Mathf.Clamp(player.position.y, -0.6f, 0f),
        transform.position.z);
    }
}
