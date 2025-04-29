using UnityEngine;

public class GlobalDoctorState : MonoBehaviour
{
    public static GlobalDoctorState Instance;
    public bool doctorIsAwake = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}