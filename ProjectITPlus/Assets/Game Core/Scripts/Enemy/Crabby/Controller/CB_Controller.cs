using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CB_Controller : MonoBehaviour, IDamageable {
    #region Init, Config
    [Header("Gameobjects")]
    [SerializeField] GameObject aliveGO;
    [SerializeField] GameObject deadGO;
    public GameObject AliveGO { get => aliveGO; }
    public GameObject DeadGO { get => deadGO; }

    [Header("Data")]
    [SerializeField] CB_Data data;

    [Header("Health System")]
    [SerializeField] int maxHealth;
    [SerializeField] int minHealth;

    //Components
    public Animator Animator { get; private set; }
    public Core Core { get; private set; }
    public CB_HitboxAttack Attack { get; private set; }

    //Variables
    public int CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    private void Awake() {
        Animator = aliveGO.GetComponent<Animator>();
        Core = aliveGO.GetComponentInChildren<Core>();
        Attack = aliveGO.GetComponentInChildren<CB_HitboxAttack>();

        aliveGO.SetActive(true);
        deadGO.SetActive(false);
        Attack.gameObject.SetActive(false);

        CurrentHealth = maxHealth;
        IsDead = false;
    }
    #endregion

    #region State Machine
    public CB_StateMachine StateMachine { get; private set; }

    //States
    public CB_IdleState IdleState { get; private set; }
    public CB_PatrolState PatrolState { get; private set; }
    public CB_AttackState AttackState { get; private set; }
    public CB_LookState LookState { get; private set; }
    public CB_HitState HitState { get; private set; }
    public CB_DeadState DeadState { get; private set; }
    public CB_GroundState GroundState { get; private set; }


    //Hash param animator
    private readonly int idleParam = Animator.StringToHash("idle");
    private readonly int patrolParam = Animator.StringToHash("patrol");
    private readonly int attackParam = Animator.StringToHash("attack");
    private readonly int hitParam = Animator.StringToHash("hit");
    private readonly int deadParam = Animator.StringToHash("dead");
    private readonly int groundParam = Animator.StringToHash("ground");
    private readonly int lookParam = Animator.StringToHash("look");

    private void Start() {
        StateMachine = new CB_StateMachine();

        IdleState = new CB_IdleState(this, data, idleParam, false);
        PatrolState = new CB_PatrolState(this, data, patrolParam, false);
        AttackState = new CB_AttackState(this, data, attackParam, true);
        LookState = new CB_LookState(this, data, lookParam, true);
        HitState = new CB_HitState(this, data, hitParam, false);
        DeadState = new CB_DeadState(this, data, deadParam, false);
        GroundState = new CB_GroundState(this, data, groundParam, true);

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
