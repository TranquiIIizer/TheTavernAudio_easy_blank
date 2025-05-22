using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class FMOD_Commands : MonoBehaviour
{
    #region EVENT EMITTER
    // EVENT EMITTER
    public StudioEventEmitter tavernEmitter; // Deklaracja publicznego pola, które przechowuje referencjê do event emittera na scenie.
    #endregion

    #region EVENT
    // EVENT
    EventInstance FootstepsSound; // Deklaracja zmiennej, która bêdzie przechowywaæ instancjê eventu Footsteps.
    public EventReference footstepsEvent; // Deklaracja publicznego pola, które przechowuje referencjê do pliku z eventem Footsteps.

    private void Footsteps()
    {
        // jednorazowe odtworzenie
        RuntimeManager.PlayOneShot(footstepsEvent); // Odtwarza event jednokrotnie bez zarz¹dzania jego instancj¹.

        // podstawowe zarz¹dzanie eventem
        FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent); // Tworzy now¹ instancjê eventu Footsteps.
        FootstepsSound.setParameterByNameWithLabel("Footsteps_surface", "Stone"); // Ustawia parametr o nazwie "Footsteps_surface" na wartoœæ "Stone".
        FootstepsSound.start(); // Uruchamia odtwarzanie eventu.
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // Stopuje odtwarzanie eventu bez fadeoutu.
        FootstepsSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Stopuje odtwarzanie eventu z fadeoutem.
        FootstepsSound.release(); // Zwolnia pamiêæ zajmowan¹ przez instancjê eventu.

        // zarz¹dzanie eventem z przypiêciami emittera do gameObjectu 
        FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent);
        FootstepsSound.set3DAttributes(RuntimeUtils.To3DAttributes(gameObject.transform)); // Przypiêcia emitter eventu do obiektu GameObject.
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
    EventInstance HealthSnap; // Deklaracja zmiennej, która bêdzie przechowywaæ instancjê snapshotu Health.
    public EventReference healthSnapshot; // Deklaracja publicznego pola, które przechowuje referencjê do pliku z snapshotem Health.

    private void StartSnapshot()
    {
        if (tavernEmitter != null && tavernEmitter.IsPlaying()) // Sprawdza, czy event emitter istnieje i jest aktywny.
        {
            HealthSnap = RuntimeManager.CreateInstance(healthSnapshot); // Tworzy now¹ instancjê snapshotu Health.
            HealthSnap.start(); // Uruchamia snapshot.
        }
        else if (tavernEmitter != null && tavernEmitter.IsPlaying())
        {
            HealthSnap.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // Stopuje snapshot bez fadeoutu.
            HealthSnap.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Stopuje snapshot z fadeoutem.
            HealthSnap.release(); // Zwolnia pamiêæ zajmowan¹ przez instancjê snapshotu.
        }
    }
    #endregion

    #region VCA
    // VCA
    FMOD.Studio.VCA GlobalVCA; // Deklaracja zmiennej, która bêdzie przechowywaæ referencjê do VCA o nazwie "Mute".

    private void VCA()
    {
        GlobalVCA = RuntimeManager.GetVCA("vca:/Mute"); // Pobiera referencjê do VCA o nazwie "Mute".
        GlobalVCA.setVolume(DecibelToLinear(0)); // Ustawia g³oœnoœæ VCA na maksimum (0 dB).
        GlobalVCA.setVolume(DecibelToLinear(-100)); // Obni¿a g³oœnoœæ VCA do minimalnego poziomu (-100 dB).
    }

    private float DecibelToLinear(float dB) // Funkcja przeliczaj¹ca wartoœæ decybelow¹ na skalê liniow¹.
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }
    #endregion

    #region EVENT / EMITTER Z MUZYK¥
    // EVENT / EMITTER Z MUZYK¥
    FMOD.Studio.EventInstance Music; // Deklaracja zmiennej, która bêdzie przechowywaæ instancjê eventu Music.
    public StudioEventEmitter tavernEmitter_Music; // Deklaracja publicznego pola, które przechowuje referencjê do event emittera na scenie.

    private void MusicSwitch()
    {
        // EVENT
        FootstepsSound = RuntimeManager.CreateInstance(footstepsEvent); // Tworzy now¹ instancjê eventu Footsteps.
        Music.setParameterByNameWithLabel("Switch_parts", "Part 2"); // Ustawia parametr o nazwie "Switch_parts" na wartoœæ "Part 2".
        Music.start(); // Uruchamia odtwarzanie eventu.
        Music.stop(FMOD.Studio.STOP_MODE.IMMEDIATE); // Stopuje odtwarzanie eventu bez fadeoutu.
        Music.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); // Stopuje odtwarzanie eventu z fadeoutem.
        Music.release(); // Zwolnia pamiêæ zajmowan¹ przez instancjê eventu.

        // EMITTER
        tavernEmitter_Music.SetParameter("Switch_parts", 0); // Ustawia parametr o nazwie "Switch_parts" na wartoœæ 0 dla event emittera.
        tavernEmitter_Music.Play(); // Uruchamia odtwarzanie na emitterze.
        tavernEmitter_Music.Stop(); // Stopuje odtwarzanie na emitterze.
    }
    #endregion
}