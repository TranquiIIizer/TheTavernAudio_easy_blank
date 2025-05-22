using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMOD_Commands : MonoBehaviour
{
    #region EVENT EMITTER
    // EVENT EMITTER
    public StudioEventEmitter tavernEmitter; // Deklaracja publicznego pola, kt�re przechowuje referencj� do event emittera na scenie.
    #endregion

    #region EVENT
    // EVENT
    EventInstance FootstepsSound; // Deklaracja zmiennej, kt�ra b�dzie przechowywa� instancj� eventu Footsteps.
    public EventReference footstepsEvent; // Deklaracja publicznego pola, kt�re przechowuje referencj� do pliku z eventem Footsteps.

    private void Footsteps()
    {
        // jednorazowe odtworzenie
        RuntimeManager.PlayOneShot(footstepsEvent); // Odtwarza event jednokrotnie bez zarz�dzania jego instancj�.

        // podstawowe zarz�dzanie eventem
        FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent); // Tworzy now� instancj� eventu Footsteps.
        FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone"); // Ustawia parametr o nazwie "Footsteps_surface" na warto�� "Stone".
        FootstepsSound.start(); // Uruchamia odtwarzanie eventu.
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // Stopuje odtwarzanie eventu bez fadeoutu.
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Stopuje odtwarzanie eventu z fadeoutem.
        FootstepsSound.release(); // Zwolnia pami�� zajmowan� przez instancj� eventu.

        // zarz�dzanie eventem z przypi�ciami emittera do gameObjectu 
        FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent);
        FootstepsSound.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform)); // Przypi�cia emitter eventu do obiektu GameObject.
        FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone");
        FootstepsSound.start();
        FootstepsSound.setPaused(true);
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        FootstepsSound.release();
    }
    #endregion

    #region SNAPSHOT
    // SNAPSHOT
    EventInstance HealthSnap; // Deklaracja zmiennej, kt�ra b�dzie przechowywa� instancj� snapshotu Health.
    public EventReference healthSnapshot; // Deklaracja publicznego pola, kt�re przechowuje referencj� do pliku z snapshotem Health.

    private void StartSnapshot()
    {
        if (tavernEmitter != null && tavernEmitter.IsPlaying()) // Sprawdza, czy event emitter istnieje i jest aktywny.
        {
            HealthSnap = RuntimeManager.CreateInstance(healthSnapshot); // Tworzy now� instancj� snapshotu Health.
            HealthSnap.start(); // Uruchamia snapshot.
        }
        else if (tavernEmitter != null && tavernEmitter.IsPlaying())
        {
            HealthSnap.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // Stopuje snapshot bez fadeoutu.
            HealthSnap.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Stopuje snapshot z fadeoutem.
            HealthSnap.release(); // Zwolnia pami�� zajmowan� przez instancj� snapshotu.
        }
    }
    #endregion

    #region VCA
    // VCA
    FMOD.Studio.VCA GlobalVCA; // Deklaracja zmiennej, kt�ra b�dzie przechowywa� referencj� do VCA o nazwie "Mute".

    private void VCA()
    {
        GlobalVCA = RuntimeManager.GetVCA("vca:/Mute"); // Pobiera referencj� do VCA o nazwie "Mute".
        GlobalVCA.setVolume(DecibelToLinear(0)); // Ustawia g�o�no�� VCA na maksimum (0 dB).
        GlobalVCA.setVolume(DecibelToLinear(-100)); // Obni�a g�o�no�� VCA do minimalnego poziomu (-100 dB).
    }

    private float DecibelToLinear(float dB) // Funkcja przeliczaj�ca warto�� decybelow� na skal� liniow�.
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }
    #endregion

    #region EVENT / EMITTER Z MUZYK�
    // EVENT / EMITTER Z MUZYK�
    FMOD.Studio.EventInstance Music; // Deklaracja zmiennej, kt�ra b�dzie przechowywa� instancj� eventu Music.
    public StudioEventEmitter tavernEmitter_Music; // Deklaracja publicznego pola, kt�re przechowuje referencj� do event emittera na scenie.

    private void MusicSwitch()
    {
        // EVENT
        FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent); // Tworzy now� instancj� eventu Footsteps.
        Music.setParameterByNameWithLabel("Switch_parts", "Part 2"); // Ustawia parametr o nazwie "Switch_parts" na warto�� "Part 2".
        Music.start(); // Uruchamia odtwarzanie eventu.
        Music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // Stopuje odtwarzanie eventu bez fadeoutu.
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Stopuje odtwarzanie eventu z fadeoutem.
        Music.release(); // Zwolnia pami�� zajmowan� przez instancj� eventu.

        // EMITTER
        tavernEmitter_Music.SetParameter("Switch_parts", 0); // Ustawia parametr o nazwie "Switch_parts" na warto�� 0 dla event emittera.
        tavernEmitter_Music.Play(); // Uruchamia odtwarzanie na emitterze.
        tavernEmitter_Music.Stop(); // Stopuje odtwarzanie na emitterze.
    }
    #endregion
}