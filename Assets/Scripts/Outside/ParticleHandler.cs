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
    public ParticleSystem SmokeStack
    {
        get { return smokeStack; }
        set { SmokeStack = value; }
    }
    [SerializeField] ParticleSystem rain;
    public ParticleSystem Rain
    {
        get { return rain; }
        set { rain = value; }
    }

}
