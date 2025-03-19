using UnityEngine;
using System.Collections;
using TMPro; // Para utilizar o TextMeshPro

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private float impactForce = 30f;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpeed = 30f;
    [SerializeField] private float bulletLifeTime = 3f;

    private float nextFireTime = 0f; // Controla o tempo entre os disparos
    private int currentAmmo = 30;    // Arma começa com 30 tiros
    private int maxAmmo = 30;        // Capacidade máxima do carregador
    private bool isReloading = false; // Para evitar disparos durante o recarregamento
    [SerializeField] private float reloadTime = 2f; // Tempo de recarga

    [Header("HUD Settings")]
    [SerializeField] private TextMeshPro ammoText; // Referência ao TextMeshPro de balas
    [SerializeField] private TextMeshPro reloadText; // Referência ao TextMeshPro de recarga

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateAmmoText(); // Inicializa a quantidade de balas na HUD
        reloadText.gameObject.SetActive(false); // Esconde a mensagem de recarga inicialmente
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;

        // Semi-automático: um tiro por clique
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime && currentAmmo > 0)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        // Automático: dispare enquanto o botão estiver pressionado
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && currentAmmo > 0)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate;
        }

        if (currentAmmo <= 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
    }

    private void Shoot()
    {
        // Instancia o projétil
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
        StartCoroutine(DestroyBullet(bullet));

        // Reduz a munição
        currentAmmo--;
        UpdateAmmoText(); // Atualiza a quantidade de balas na HUD
    }

    private IEnumerator DestroyBullet(GameObject bullet)
    {
        // Destrói o projétil após um certo tempo
        yield return new WaitForSeconds(bulletLifeTime);
        Destroy(bullet);
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        reloadText.gameObject.SetActive(true); // Exibe a mensagem de recarga

        // Espera o tempo de recarga passar
        yield return new WaitForSeconds(reloadTime);

        // Recarrega a arma
        currentAmmo = maxAmmo;
        isReloading = false;

        reloadText.gameObject.SetActive(false); // Esconde a mensagem de recarga
        UpdateAmmoText(); // Atualiza a HUD com a munição recarregada
    }

    private void UpdateAmmoText()
    {
        ammoText.text = "Bullets: " + currentAmmo + " / " + maxAmmo; // Atualiza o texto de balas
    }
}