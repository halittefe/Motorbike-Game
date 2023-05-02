using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float forceMagnitude = 10f;
    [SerializeField] private float explosionRadius = 5f;

    private Rigidbody coinRigidbody;
    private Transform playerTransform;
    private bool coinCollect = false;
    GameManager gm;
    private void Start()
    {
        coinRigidbody = GetComponent<Rigidbody>();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void FixedUpdate()
    {
        if (coinCollect)
        {
            Vector3 explosionPosition = transform.position;
            float explosionForce = forceMagnitude;
            float upwardsModifier = 0.5f;
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            coinRigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius, upwardsModifier, ForceMode.Impulse);
            coinRigidbody.AddForce(direction * forceMagnitude, ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coinCollect = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            coinCollect = false;
        }
    }
}
