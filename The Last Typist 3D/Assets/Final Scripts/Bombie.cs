using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombie : Enemies
{
    public int m_wordLength = 5;
    public float explosionRadius;
    public CapsuleCollider capsuleCollider;
    private bool exploded;

    BombZombie_Controller bombZombieController;

    private void Awake()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        base.Initialize();
        base.InitializeTextBoxWithLength(m_wordLength);
        bombZombieController = GetComponent<BombZombie_Controller>();
        if (m_speed != 0)
        {
            bombZombieController.StartOrStopSlowWalkAnimation();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "NPC")
        {
            Vector3 direction = (m_meatGrinder.transform.position - transform.position).normalized;
            Vector3 velocity = direction * m_speed;
            transform.LookAt(m_meatGrinder.transform.position);
            transform.position = (transform.position + velocity * Time.deltaTime);
        }
        if (killed)
        {
            bombZombieController.StartOrStopSlowWalkAnimation();
            bombZombieController.DeathAnimation();
            gameObject.GetComponent<Enemies>().enabled = false;
        }
        if (exploded)
        {
            bombZombieController.StartOrStopSlowWalkAnimation();
            bombZombieController.TriggerAttackAnimation();
            bombZombieController.DeathAnimation();
            GameHandler.currentEnemyNum--;
            Destroy(anchoredText, 2.916f);
            Destroy(gameObject, 2.917f);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[BOMBIE] Bomber zombie collided with object.");
        if (other.tag == "Player"
            || other.tag == "MeatGrinder"
            || other.tag == "SandBag"
            || other.tag == "SpikeTrap")
        {
            m_speed = 0;
            Debug.Log("[BOMBIE] Collided with "+other.tag);
            Explode();
        }
    }

    private void Explode()
    {
        exploded = true;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log("[BOMBIE] Caught in explosion: " + hitCollider.gameObject.tag);
            if (hitCollider.gameObject.tag == "SandBag"
                || hitCollider.gameObject.tag == "SpikeTrap")
            {
                Destroy(hitCollider.gameObject, 1.667f);
            }
            else if (hitCollider.gameObject.tag == "MeatGrinder")
            {
                Debug.Log("[BOMBIE] Meat grinder has taken damage!");
            }
        }
    }

    public bool getKilled()
    {
        return killed;
    }
}
