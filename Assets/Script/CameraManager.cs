using UnityEngine;

public class CameraManager : MonoBehaviour
{
    
    public GameObject playerA;
    public GameObject playerB;

   // public flo
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float size = Vector3.Distance(playerA.transform.position, playerB.transform.position) / 2;
        Vector3 MediumPos = (playerA.transform.position + playerB.transform.position) / 2;

        MediumPos.z = -10;
        Camera.main.transform.position = MediumPos; 
        Camera.main.orthographicSize = size;
    }
}
