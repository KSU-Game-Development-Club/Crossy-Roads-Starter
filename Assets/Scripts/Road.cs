using System.Collections.Generic;
using JetBrains.Rider.Unity.Editor;
using UnityEngine;

public class Road : MonoBehaviour
{
    [SerializeField] private Rigidbody vehiclePrefab;
    private float speed;
    private int direction;

    private List<Rigidbody> Cars = new();
    public void Init(int position)
    {
        transform.position = new(position, 0);
        speed = Random.Range(1f, 5f);
        direction = 2 * Random.Range(0, 2) - 1;

        int carCount = Random.Range(1, 5);
        float gap = Random.Range(2f, 6f);

        for (int i = 0; i < carCount; i++)
        {
            Rigidbody vic = Instantiate(vehiclePrefab,
                new Vector3(position, 0.7f, (i * gap) * direction),
                Quaternion.Euler(0, 180 * direction, 0),
                transform
                );
            Cars.Add(vic);
        }
    }

    private void FixedUpdate()
    {
        foreach(Rigidbody v in Cars)
        {
            Vector3 movAmount = new(0, 0, speed * direction * Time.deltaTime);
            v.MovePosition(v.position + movAmount);

            Vector3 pos = v.position;
            if(direction > 0 && pos.z > 11)
            {
                pos.z = -11;
                v.position = pos;
            }
            else if (pos.z < -11)
            {
                pos.z = 11;
                v.position = pos;
            }
        }
    }

}
