using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform nextRoom;
    [SerializeField] Transform prevRoom;
    [SerializeField] CameraController cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                prevRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                nextRoom.GetComponent<Room>().ActivateRoom(false);
                prevRoom.GetComponent<Room>().ActivateRoom(true);
                cam.MoveToNewRoom(prevRoom);
            }
        }
    }

}
