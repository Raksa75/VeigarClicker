using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Veigar : MonoBehaviour
{
    // Variable affich� sur le HUD
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
    public float spellDamage = 20f; // D�finir la valeur des d�g�ts du sort

    // Variables pour l'argent
    public float money = 0f;

    // Temps avant la prochaine attaque automatique
    private float nextAttackTime = 0f;

    // R�f�rence � l'ennemi
    private GameObject enemy;

    // M�thode appel�e au d�marrage du jeu
    private void Start()
    {
        // Rechercher l'objet ennemi par son tag
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    private void Update()
    {
        // Si le cooldown du sort est en cours, r�duire le temps restant
        if (spellCooldownTimer > 0)
        {
            spellCooldownTimer -= Time.deltaTime;
        }

        // R�g�n�rer le mana
        RegenerateMana();

        // V�rifier si l'ennemi est toujours pr�sent avant d'appeler AutoAttack()
        if (enemy != null)
        {
            AutoAttack();
        }

        UpdateHUD();
    }

    private void UpdateHUD()
    {
        // Mettre � jour le texte de chaque objet texte avec les valeurs correspondantes
        moneyText.text = "Money: " + money.ToString();
        attackSpeedText.text = "Attack Speed: " + attackSpeed.ToString();
        attackText.text = "Attack: " + attackDamage.ToString();
        apText.text = "AP: " + abilityPower.ToString();
        regenManaText.text = "Regen Mana: " + manaRegenRate.ToString();
        manaText.text = "Mana: " + mana.ToString();
    }

    // M�thode pour r�g�n�rer le mana
    private void RegenerateMana()
    {
        mana += manaRegenRate * Time.deltaTime;
        mana = Mathf.Clamp(mana, 0f, maxMana); // S'assurer que le mana ne d�passe pas la valeur maximale
    }

    // M�thode pour attaquer
    public void Attack()
    {
        mana += attackDamage; // Chaque attaque donne du mana
    }

    // M�thode pour attaquer automatiquement en fonction de la vitesse d'attaque
    private void AutoAttack()
    {
        // V�rifier si l'attaque automatique est possible en fonction du temps �coul� depuis la derni�re attaque
        if (Time.time >= nextAttackTime)
        {
            // Faites subir des d�g�ts � l'ennemi s'il est trouv�
            if (enemy != null)
            {
                enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
            }
            nextAttackTime = Time.time + 1f / attackSpeed; // Mettez � jour le temps de la prochaine attaque
        }
    }


    // M�thode pour lancer un sort
    public void CastSpell()
    {
        if (mana >= manaCostOfSpell && spellCooldownTimer <= 0)
        {
            // Faites subir des d�g�ts � l'ennemi avec le sort s'il est trouv�
            if (enemy != null)
            {
                enemy.GetComponent<EnemyController>().TakeDamage(spellDamage);
            }
            mana -= manaCostOfSpell; // Retirer le co�t du sort du mana
            spellCooldownTimer = spellCooldown; // D�finir le cooldown
        }
    }

    // M�thode pour augmenter les d�g�ts d'un montant sp�cifi�
    public void IncreaseAttackDamage(float amount)
    {
        attackDamage += amount;
    }

    // M�thode pour augmenter l'AP d'un montant sp�cifi�
    public void IncreaseAbilityPower(float amount)
    {
        abilityPower += amount;
    }

    // M�thode pour gagner de l'argent
    public void EarnMoney(float amount)
    {
        money += amount;
    }
}
