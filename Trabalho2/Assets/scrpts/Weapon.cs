using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Settings")]
    [SerializeField] private float damage = 10f;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 15f; // Número de disparos por segundo
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // Se estiver recarregando, não faz nada até o tempo de recarga passar
        if (isReloading)
            return;

        // Automático: dispare enquanto o botão estiver pressionado, mas respeitando o limite de munição
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime && currentAmmo > 0)
        {
            Shoot();
            nextFireTime = Time.time + 1f / fireRate; // Define o próximo tempo permitido para o disparo
        }

        // Verifica se a munição acabou e inicia o processo de recarga
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
        Debug.Log("Recargando...");

        // Espera o tempo de recarga passar
        yield return new WaitForSeconds(reloadTime);

        // Recarrega a arma
        currentAmmo = maxAmmo;
        isReloading = false;

        Debug.Log("Recarga completa!");
    }
}
