using UnityEngine;

public class Ring : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Full_Emegence()
    {
        gameObject.SetActive(false);
    }
}
