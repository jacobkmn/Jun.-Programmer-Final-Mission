using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    public static ParticleHandler instance;

    [SerializeField] ParticleSystem windRush;
    [SerializeField] ParticleSystem smokeStack;
    [SerializeField] ParticleSystem rain;
    [SerializeField] ParticleSystem[] indoorParticles;

    //Properties 
    public ParticleSystem WindRush
    {
        get { return windRush; }
        set { WindRush = value; }
    }
    /*
    public ParticleSystem SmokeStack
    {
        get { return smokeStack; }
        set { SmokeStack = value; }
    } */
    public ParticleSystem Rain
    {
        get { return rain; }
        set { rain = value; }
    }

    private void Awake()
    {
        instance = this;
        smokeStack.Play();

        foreach (ParticleSystem ps in indoorParticles)
        {
            ps.gameObject.SetActive(false);
        }
    }

    //responses to events in inspector
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
    public void ActivateSmoke()
    {
        smokeStack.gameObject.SetActive(true);
        smokeStack.Play();
    }
    /*
    public void DeActivateRain()
    {
        rain.Stop();
        rain.gameObject.SetActive(false);
    } */

    //Gets called within the OpenDoor method of Door class
    public void PlayIndoorParticles()
    {
        foreach (ParticleSystem ps in indoorParticles)
        {
            ps.gameObject.SetActive(true);
            ps.Play();
        }
    }
    //called in inspector
    public void StopIndoorParticles()
    {
        foreach (ParticleSystem ps in indoorParticles)
        {
            ps.Stop();
            ps.gameObject.SetActive(false);
        }
    }
}
