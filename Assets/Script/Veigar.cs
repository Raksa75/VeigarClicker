using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Veigar : MonoBehaviour
{
    // Variable affiché sur le HUD
    public TMP_Text moneyText;
    public TMP_Text attackSpeedText;
    public TMP_Text attackText;
    public TMP_Text apText;
    public TMP_Text regenManaText;
    public TMP_Text manaText;


    // Variables pour les statistiques de Veigar
    public float mana = 0f;
    public float maxMana = 100f;
    public float attackSpeed = 0f;
    public float attackDamage = 10f;
    public float abilityPower = 0f;
    public float manaRegenRate = 0f; // Mana regenerated per second

    // Variables pour la gestion des sorts
    public float manaCostOfSpell = 50f;
    public float spellCooldown = 5f;
    private float spellCooldownTimer = 0f;
    public float spellDamage = 20f; // Définir la valeur des dégâts du sort

    // Variables pour l'argent
    public float money = 0f;

    // Temps avant la prochaine attaque automatique
    private float nextAttackTime = 0f;

    // Référence à l'ennemi
    private GameObject enemy;

    // Méthode appelée au démarrage du jeu
    private void Start()
    {
        // Rechercher l'objet ennemi par son tag
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {
        // Si le cooldown du sort est en cours, réduire le temps restant
        if (spellCooldownTimer > 0)
        {
            spellCooldownTimer -= Time.deltaTime;
        }

        // Régénérer le mana
        RegenerateMana();

        // Vérifier si l'ennemi est toujours présent avant d'appeler AutoAttack()
        if (enemy != null)
        {
            AutoAttack();
        }

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        // Mettre à jour le texte de chaque objet texte avec les valeurs correspondantes
        moneyText.text = "Money: " + money.ToString();
        attackSpeedText.text = "Attack Speed: " + attackSpeed.ToString();
        attackText.text = "Attack: " + attackDamage.ToString();
        apText.text = "AP: " + abilityPower.ToString();
        regenManaText.text = "Regen Mana: " + manaRegenRate.ToString();
        manaText.text = "Mana: " + mana.ToString();
    }

    // Méthode pour régénérer le mana
    private void RegenerateMana()
    {
        mana += manaRegenRate * Time.deltaTime;
        mana = Mathf.Clamp(mana, 0f, maxMana); // S'assurer que le mana ne dépasse pas la valeur maximale
    }

    // Méthode pour attaquer
    public void Attack()
    {
        mana += attackDamage; // Chaque attaque donne du mana
    }

    // Méthode pour attaquer automatiquement en fonction de la vitesse d'attaque
    private void AutoAttack()
    {
        // Vérifier si l'attaque automatique est possible en fonction du temps écoulé depuis la dernière attaque
        if (Time.time >= nextAttackTime)
        {
            // Faites subir des dégâts à l'ennemi s'il est trouvé
            if (enemy != null)
            {
                enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
            }
            nextAttackTime = Time.time + 1f / attackSpeed; // Mettez à jour le temps de la prochaine attaque
        }
    }


    // Méthode pour lancer un sort
    public void CastSpell()
    {
        if (mana >= manaCostOfSpell && spellCooldownTimer <= 0)
        {
            // Faites subir des dégâts à l'ennemi avec le sort s'il est trouvé
            if (enemy != null)
            {
                enemy.GetComponent<EnemyController>().TakeDamage(spellDamage);
            }
            mana -= manaCostOfSpell; // Retirer le coût du sort du mana
            spellCooldownTimer = spellCooldown; // Définir le cooldown
        }
    }

    // Méthode pour augmenter les dégâts d'un montant spécifié
    public void IncreaseAttackDamage(float amount)
    {
        attackDamage += amount;
    }

    // Méthode pour augmenter l'AP d'un montant spécifié
    public void IncreaseAbilityPower(float amount)
    {
        abilityPower += amount;
    }

    // Méthode pour gagner de l'argent
    public void EarnMoney(float amount)
    {
        money += amount;
    }
}
