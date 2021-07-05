using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;

public class BP_Controller : MonoBehaviour, IDamageable {
    #region Init, Config
    [Header("Gameobjects")]
    [SerializeField] GameObject aliveGO;
    [SerializeField] GameObject deadGO;
    public GameObject AliveGO { get => aliveGO; }
    public GameObject DeadGO { get => deadGO; }

    [Header("Data")]
    [SerializeField] BP_Data data;

    [Header("Health System")]
    [SerializeField] int maxHealth;
    [SerializeField] int minHealth;

    //Components
    public Animator Animator { get; private set; }
    public CapsuleCollider2D Capsule { get; private set; }
    public Core Core { get; private set; }
    public BP_Hitbox Attack { get; private set; }
    public BP_HealthBar HealthBar { get; private set; }

    //Variables
    public int CurrentHealth { get; private set; }
    public bool IsDead { get; private set; }

    private void Awake() {
        Animator = aliveGO.GetComponent<Animator>();
        Capsule = aliveGO.GetComponent<CapsuleCollider2D>();
        Core = aliveGO.GetComponentInChildren<Core>();
        Attack = aliveGO.GetComponentInChildren<BP_Hitbox>();
        HealthBar = aliveGO.GetComponentInChildren<BP_HealthBar>();

        aliveGO.SetActive(true);
        deadGO.SetActive(false);
        Capsule.direction = CapsuleDirection2D.Vertical;
        Attack.gameObject.SetActive(false);

        CurrentHealth = maxHealth;
        IsDead = false;
    }
    #endregion

    #region State Machine
    public BP_StateMachine StateMachine { get; private set; }

    //States
    public BP_IdleState IdleState { get; private set; }
    public BP_PatrolState PatrolState { get; private set; }
    public BP_AttackState AttackState { get; private set; }
    public BP_LookState LookState { get; private set; }
    public BP_HitState HitState { get; private set; }
    public BP_DeadState DeadState { get; private set; }
    public BP_GroundState GroundState { get; private set; }


    //Hash param animator
    private readonly int idleParam = Animator.StringToHash("idle");
    private readonly int patrolParam = Animator.StringToHash("patrol");
    private readonly int attackParam = Animator.StringToHash("attack");
    private readonly int hitParam = Animator.StringToHash("hit");
    private readonly int deadParam = Animator.StringToHash("dead");
    private readonly int groundParam = Animator.StringToHash("ground");
    private readonly int lookParam = Animator.StringToHash("look");

    private void Start() {
        StateMachine = new BP_StateMachine();

        IdleState = new BP_IdleState(this, data, idleParam, false);
        PatrolState = new BP_PatrolState(this, data, patrolParam, false);
        AttackState = new BP_AttackState(this, data, attackParam, true);
        LookState = new BP_LookState(this, data, lookParam, true);
        HitState = new BP_HitState(this, data, hitParam, false);
        DeadState = new BP_DeadState(this, data, deadParam, false);
        GroundState = new BP_GroundState(this, data, groundParam, true);

        StateMachine.Initialize(IdleState);
    }
    #endregion

    #region Update
    private void Update() {
        StateMachine.CurrentState.LogicUpdate();
        Core.LogicUpdate();
        HealthBar.transform.right = Vector3.right;
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
        HealthBar.SetSize((float)CurrentHealth / (float)maxHealth);
        StateMachine.ChangeState(HitState);
    }

    public void OnDead () {
        this.PostEvent(EventID.EnemyDead);
    }
    #endregion
}
