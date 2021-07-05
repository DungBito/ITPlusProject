using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public class CT_Controller : MonoBehaviour, IDamageable {
    #region Init, Config
    [Header("Gameobjects")]
    [SerializeField] GameObject aliveGO;
    [SerializeField] GameObject deadGO;
    public GameObject AliveGO { get => aliveGO; }
    public GameObject DeadGO { get => deadGO; }

    [Header("Data")]
    [SerializeField] CT_Data data;

    [Header("Health System")]
    [SerializeField] int maxHealth;
    [SerializeField] int minHealth;

    //Components
    public Animator Animator { get; private set; }
    public CapsuleCollider2D Capsule { get; private set; }
    public Core Core { get; private set; }
    public CT_HitboxAttack Attack { get; private set; }

    //Variables
    public int CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    private void Awake() {
        Animator = aliveGO.GetComponent<Animator>();
        Capsule = aliveGO.GetComponent<CapsuleCollider2D>();
        Core = aliveGO.GetComponentInChildren<Core>();
        Attack = aliveGO.GetComponentInChildren<CT_HitboxAttack>();

        aliveGO.SetActive(true);
        deadGO.SetActive(false);
        Capsule.direction = CapsuleDirection2D.Vertical;
        Attack.gameObject.SetActive(false);

        CurrentHealth = maxHealth;
        IsDead = false;
    }
    #endregion

    #region State Machine
    public CT_StateMachine StateMachine { get; private set; }

    //States
    public CT_IdleState IdleState { get; private set; }
    public CT_PatrolState PatrolState { get; private set; }
    public CT_AttackState AttackState { get; private set; }
    public CT_LookState LookState { get; private set; }
    public CT_ScareState ScareState { get; private set; }
    public CT_HitState HitState { get; private set; }
    public CT_DeadState DeadState { get; private set; }
    public CT_GroundState GroundState { get; private set; }


    //Hash param animator
    private readonly int idleParam = Animator.StringToHash("idle");
    private readonly int patrolParam = Animator.StringToHash("patrol");
    private readonly int attackParam = Animator.StringToHash("attack");
    private readonly int scareParam = Animator.StringToHash("scare");
    private readonly int hitParam = Animator.StringToHash("hit");
    private readonly int deadParam = Animator.StringToHash("dead");
    private readonly int groundParam = Animator.StringToHash("ground");
    private readonly int lookParam = Animator.StringToHash("look");

    private void Start() {
        StateMachine = new CT_StateMachine();

        IdleState = new CT_IdleState(this, data, idleParam, false);
        PatrolState = new CT_PatrolState(this, data, patrolParam, false);
        AttackState = new CT_AttackState(this, data, attackParam, true);
        LookState = new CT_LookState(this, data, lookParam, true);
        ScareState = new CT_ScareState(this, data, scareParam, true);
        HitState = new CT_HitState(this, data, hitParam, false);
        DeadState = new CT_DeadState(this, data, deadParam, false);
        GroundState = new CT_GroundState(this, data, groundParam, true);

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

    public void OnDead () {
        this.PostEvent(EventID.EnemyDead);
    }
    #endregion
}
