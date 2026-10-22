using UnityEngine;

public class Grass : MonoBehaviour
{
    public void Init(int position)
    {
        transform.position = new(position, 0);
    }
}
