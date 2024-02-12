using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EnemyController : MonoBehaviour
{
    // Texte de l'ennemi
    public TMP_Text HpText;

    // Statistiques initiales de l'ennemi
    private float initialHealth;
    private float initialMoney;

    // Points de vie de l'ennemi
    public float health = 100f;
    public float money = 100f;

    // Référence au prefab de l'ennemi pour la réapparition
    public GameObject enemyPrefab;

    // Méthode appelée lorsqu'un clic gauche est détecté sur l'ennemi
    private void OnMouseDown()
    {
        // Vérifiez si le bouton gauche de la souris est enfoncé
        if (Input.GetMouseButtonDown(0))
        {
            // Obtenez le script Veigar attaché à l'objet Veigar
            Veigar veigar = FindObjectOfType<Veigar>();

            // Si un script Veigar est trouvé
            if (veigar != null)
            {
                // Faites subir des dégâts à l'ennemi équivalents aux dégâts d'attaque de Veigar
                TakeDamage(veigar.attackDamage);
            }
        }
    }

    // Méthode pour infliger des dégâts à l'ennemi
    public void TakeDamage(float damageAmount)
    {
        // Soustraire les dégâts des points de vie de l'ennemi
        health -= damageAmount;

        // Vérifier si l'ennemi est mort
        if (health <= 0)
        {
            Die(); // Si l'ennemi est mort, appelez la méthode Die()
        }
    }

    // Méthode appelée lorsque l'ennemi est mort
    private void Die()
    {
        // Désactiver le texte de l'ennemi
        HpText.enabled = false;

        // Attendre 0.5 seconde avant de faire réapparaître un nouvel ennemi
        StartCoroutine(RespawnEnemy(0.5f));

        // Gagner de l'argent lorsque l'ennemi meurt
        Veigar veigar = FindObjectOfType<Veigar>();
        if (veigar != null)
        {
            veigar.EarnMoney(money);
        }
    }

    // Coroutine pour la réapparition de l'ennemi après un délai
    private IEnumerator RespawnEnemy(float delay)
    {
        // Attendre pendant le délai spécifié
        yield return new WaitForSeconds(delay);

        // Activer le texte de l'ennemi
        HpText.enabled = true;

        // Instancier un nouvel ennemi à la position actuelle
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

        // Récupérer les statistiques initiales du nouvel ennemi
        EnemyController newEnemyController = newEnemy.GetComponent<EnemyController>();
        newEnemyController.health = initialHealth;
        newEnemyController.money = initialMoney;
    }

    private void Start()
    {
        // Sauvegarder les statistiques initiales de l'ennemi
        initialHealth = health;
        initialMoney = money;

        UpdateEnemyHUD();
    }

    private void UpdateEnemyHUD()
    {
        // Mettre à jour le texte de chaque objet texte avec les valeurs correspondantes
        HpText.text = "HP :" + health.ToString();
    }
}
