using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomArrangement : MonoBehaviour
{
    public GameObject cone;
    public GameObject[] spawnPos;
    // Start is called before the first frame update
    void Start()
    {
        SpikeSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        cone.transform.position = new Vector3(transform.localPosition.x, 0.01f, transform.localPosition.z);
    }

    // This method will spawn 7 (will see if increase nb) spikes for the spike trap at different positions
    // We'll keep track of the position number the spike has been instantiated in order to not use the same position
    // twice for the same trap
    private void SpikeSpawn()
    {
        Stack occupiedPos = new Stack();
        for(int i = 0; i < 8; i++)
        {
            int randomPos = Random.Range(0, spawnPos.Length); // Generate number between 0 & size of array with spawn positions

            // Find spawn position that is at randomPos
            for(int j = 0; j < spawnPos.Length; j++)
            {
                // Instantiate the spike at the random position
                if(j == randomPos && !occupiedPos.Contains(randomPos))
                {
                    occupiedPos.Push(randomPos); 
                    Instantiate(cone, spawnPos[randomPos].transform.position, Quaternion.identity);
                    cone.transform.localScale = new Vector3(0.2f, 1, 0.2f);
                    
                }
            }
        }
    }
}
