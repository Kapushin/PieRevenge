using UnityEngine;

public class TeleportColBehavior : MonoBehaviour, IInteracteable
{
    private GameObject _character;
    [SerializeField] private GameObject _newPoint;
    [SerializeField] private GameObject _fade;
    [SerializeField] private GameObject _bright;
    public string PromptText => "";

    private void Start()
    {
        _character = GameObject.FindGameObjectWithTag("Player");
    }

    public bool GetInteracted(InteractionsBehaviour target)
    {
        Teleportation();
        Debug.Log("teleportation");
        return true;
    }

    private void Teleportation()
    {
        Fading();
        Invoke("Brighting", 1.5f);
    }

    private void Fading()
    {
        _fade.SetActive(true);
        _bright.SetActive(false);
    }

    private void Brighting()
    {
        _character.transform.position = new Vector3(_newPoint.transform.position.x, _newPoint.transform.position.y,
    _newPoint.transform.position.z);
        _fade.SetActive(false);
        _bright.SetActive(true);
    }
}
