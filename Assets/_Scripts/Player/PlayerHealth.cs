using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Singleton<PlayerHealth>
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
    private Slider healthSlider;
    private int currentHealth;
    private bool canTakeDamage = true;

    private Knockback knockback;
    private Flash flash;

    protected override void Awake(){
        base.Awake();
        flash = GetComponent<Flash>();
        knockback = GetComponent<Knockback>();
    }

    private void Start(){
        currentHealth = maxHealth;
        UpdateHealthSlider();
    }
        public void HealPlayer(){
        if(currentHealth < maxHealth){
        currentHealth +=1;
        UpdateHealthSlider();
        }
    }
    private void OnCollisionStay2D(Collision2D other) {
        EnemyAi enemy = other.gameObject.GetComponent<EnemyAi>();

        if (enemy) {
            TakeDamage(1, other.transform);
        }
    }

    private void TakeDamage(int damageAmount, Transform hitTransform) {
        if (!canTakeDamage) { return; }

        knockback.GetKnockedBack(hitTransform, knockBackThrustAmount);
        StartCoroutine(flash.FlashRoutine());
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
        UpdateHealthSlider();
        CheckIfPlayerDeath();
    }

        private void CheckIfPlayerDeath() {
        if (currentHealth <= 0) {
            currentHealth = 0;
            Debug.Log("Player Death");
        }
    }
    private IEnumerator DamageRecoveryRoutine() {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void UpdateHealthSlider(){
        if(healthSlider ==null){
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }

}
