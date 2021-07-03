using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PS_Controller : MonoBehaviour, IDamageable {
    #region Init, Config
    [Header("Gameobjects")]
    [SerializeField] GameObject aliveGO;
    [SerializeField] GameObject deadGO;
    public GameObject AliveGO { get => aliveGO; }
    public GameObject DeadGO { get => deadGO; }

    [Header("Data")]
    [SerializeField] PS_Data data;

    [Header("Health System")]
    [SerializeField] int maxHealth;
    [SerializeField] int minHealth;

    //Components
    public Animator Animator { get; private set; }
    public Core Core { get; private set; }
    public PS_HitboxAttack Attack { get; private set; }

    //Variables
    public int CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    private void Awake() {
        Animator = aliveGO.GetComponent<Animator>();
        Core = aliveGO.GetComponentInChildren<Core>();
        Attack = aliveGO.GetComponentInChildren<PS_HitboxAttack>();

        aliveGO.SetActive(true);
        deadGO.SetActive(false);
        Attack.gameObject.SetActive(false);

        CurrentHealth = maxHealth;
        IsDead = false;
    }
    #endregion

    #region State Machine
    public PS_StateMachine StateMachine { get; private set; }

    //States
    public PS_IdleState IdleState { get; private set; }
    public PS_AttackState AttackState { get; private set; }
    public PS_HitState HitState { get; private set; }
    public PS_GroundState GroundState { get; private set; }
    public PS_DeadState DeadState { get; private set; }


    //Hash param animator
    private readonly int idleParam = Animator.StringToHash("idle");
    private readonly int attackParam = Animator.StringToHash("attack");
    private readonly int hitParam = Animator.StringToHash("hit");
    private readonly int deadParam = Animator.StringToHash("dead");
    private readonly int groundParam = Animator.StringToHash("ground");

    private void Start() {
        StateMachine = new PS_StateMachine();

        IdleState = new PS_IdleState(this, data, idleParam, false);
        AttackState = new PS_AttackState(this, data, attackParam, false);
        HitState = new PS_HitState(this, data, hitParam, false);
        GroundState = new PS_GroundState(this, data, groundParam, true);
        DeadState = new PS_DeadState(this, data, deadParam, false);

        StateMachine.Initialize(IdleState);
    }
    #endregion

    #region Update
    private void Update() {
        StateMachine.CurrentState.LogicUpdate();
        Core.LogicUpdate();
    }

    private void FixedUpdate() {
        StateMachine.CurrentState.PhysicsUpdate();
    }
    #endregion

    #region Destroy
    public void Destroy() {
        Destroy(aliveGO);
        Destroy(this);
    }
    #endregion

    #region Damageable
    public void Damageable(int dame, float xForce, float yForce) {
        CurrentHealth -= dame;
        if (CurrentHealth <= minHealth) {
            IsDead = true;
        }
        Animator.SetInteger("health", CurrentHealth);
        Core.Movement.SetZeroVelocity();
        Core.Movement.AddForce(new Vector2(xForce, yForce), ForceMode2D.Impulse);
        StateMachine.ChangeState(HitState);
    }
    #endregion
}
