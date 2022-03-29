using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other) 
    {
        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("This is the start.");
                break;
            case "Finish":
                Debug.Log("Congrats you finished.");
                break;
            default:
                Debug.Log("Exploded. Rip");
                break;
        }
    }
}
