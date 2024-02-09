using UnityEngine;

public class Veigar : MonoBehaviour
{
    // Variables pour les statistiques de Veigar
    public float mana = 0f;
    public float maxMana = 100f;
    public float attackSpeed = 0f;
    public float attackDamage = 10f;
    public float abilityPower = 0f;
    public float manaRegenRate = 0f; // Mana regenerated per second
    public EnemyController enemy; //trouver l'enemy

    // Variables pour la gestion des sorts
    public float manaCostOfSpell = 50f;
    public float spellCooldown = 5f;
    private float spellCooldownTimer = 0f;
    public float spellDamage = 20f; // D�finir la valeur des d�g�ts du sort


    // Variables pour l'argent
    public float money = 0f;

    // Temps avant la prochaine attaque automatique
    private float nextAttackTime = 0f;

    // M�thode appel�e au d�marrage du jeu
    private void Start()
    {

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
            FindObjectOfType<EnemyController>().TakeDamage(attackDamage); // Faites subir des d�g�ts � l'ennemi
            nextAttackTime = Time.time + 1f / attackSpeed; // Mettez � jour le temps de la prochaine attaque
        }
    }


    // M�thode pour lancer un sort
    public void CastSpell()
    {
        if (mana >= manaCostOfSpell && spellCooldownTimer <= 0)
        {
            // Lancer le sort (ins�rer ici la logique du sort)
            FindObjectOfType<EnemyController>().TakeDamage(spellDamage); // Faites subir des d�g�ts � l'ennemi avec le sort
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
