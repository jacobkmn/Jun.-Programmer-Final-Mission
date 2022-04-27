using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public static ParticleHandler instance;

    private void Awake()
    {
        instance = this;
        smokeStack.Play();
    }

    [SerializeField] ParticleSystem windRush;
    public ParticleSystem WindRush
    {
        get { return windRush; }
        set { WindRush = value; }
    }

    [SerializeField] ParticleSystem smokeStack;
    //public ParticleSystem SmokeStack
    //{
    //    get { return smokeStack; }
    //    set { SmokeStack = value; }
    //}
    [SerializeField] ParticleSystem rain;
    //public ParticleSystem Rain
    //{
    //    get { return rain; }
    //    set { rain = value; }
    //}

    public void ActivateWind()
    {
        windRush.gameObject.SetActive(true);
    }
    public void DeActivateWind()
    {
        windRush.gameObject.SetActive(false);
    }
    public void DeActivateSmoke()
    {
        smokeStack.Stop();
        smokeStack.gameObject.SetActive(false);
    }
    public void DeActivateRain()
    {
        rain.Stop();
        rain.gameObject.SetActive(false);
    }

}
