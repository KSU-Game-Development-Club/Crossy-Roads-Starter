using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] GameManager gm;

    private void OnCollisionEnter(Collision collision)
    {
        //todo: fix this
        Debug.Log("test");
        if(collision.gameObject.tag == "car")
        {
            gm.NewLevel();
        }
    }
}
