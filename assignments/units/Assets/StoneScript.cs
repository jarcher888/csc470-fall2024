using UnityEngine;

public class StoneScript : MonoBehaviour
{

    public int lifeRemaining;
    // Start is called before the first frame update
    void Start()
    {
        lifeRemaining = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeRemaining == 0){
            Destroy(gameObject);
        }
    }
}
