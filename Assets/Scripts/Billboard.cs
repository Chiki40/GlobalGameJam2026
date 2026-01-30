using UnityEngine;

public class Billboard : MonoBehaviour
{
    protected GameObject PlayerObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerObject.transform);
    }
}
