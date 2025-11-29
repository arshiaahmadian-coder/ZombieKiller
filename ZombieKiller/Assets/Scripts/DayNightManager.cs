using UnityEngine;

public class DayNightManager : MonoBehaviour
{
    [SerializeField] GameObject directLight;
    [SerializeField] Vector3 dayDirection;
    [SerializeField] Vector3 nightDirection;

    public static DayNightManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeToDay();
    }

    public void ChangeToDay()
    {
        directLight.transform.rotation = 
          Quaternion.Euler(dayDirection.x, dayDirection.y, dayDirection.z);
    }

    public void ChangeToNight()
    {
        directLight.transform.rotation = 
          Quaternion.Euler(nightDirection.x, nightDirection.y, nightDirection.z);
    }
}
