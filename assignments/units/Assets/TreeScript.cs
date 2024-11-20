using UnityEngine;

public class TreeScript : MonoBehaviour
{
    public int lifeRemaining;
    // Start is called before the first frame update
    void Start()
    {
        lifeRemaining = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeRemaining == 0){
            Destroy(gameObject);
        }
    }


}
