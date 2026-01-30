using Unity.VisualScripting;
using UnityEngine;

public class SmartObjectInteractable : MonoBehaviour
{
    protected GameObject PlayerObject;
    private void Start()
    {
        PlayerObject = GameObject.FindGameObjectWithTag("Player");
    }

    virtual public void Interact()
    {
        Debug.Log(name + "Interacted");
    }
}
