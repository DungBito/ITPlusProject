using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Controller : MonoBehaviour, IBombAction {
    #region Init, Config
    [Header("Config")]
    [SerializeField] GameObject bombOn;
    [SerializeField] GameObject bombOff;
    [SerializeField] GameObject explosion;
    [SerializeField] float maxTimeToExplosion;

    private float timeToExplosionLeft;
    private bool hasExplosion;
    private bool isBombOn;
    private Vector2 workSpace;

    public Rigidbody2D Rigid { get; private set; }

    private void Awake() {
        Rigid = bombOn.GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        bombOn.transform.SetPositionAndRotation(transform.position, transform.rotation);
        timeToExplosionLeft = maxTimeToExplosion;
        hasExplosion = false;
        isBombOn = true;
        bombOn.SetActive(true);
        bombOff.SetActive(false);
        explosion.SetActive(false);
    }
    #endregion

    #region Update
    private void Update() {
        if (isBombOn && timeToExplosionLeft > 0) {
            timeToExplosionLeft -= Time.deltaTime;
        }
        else if (isBombOn && timeToExplosionLeft <= 0 && !hasExplosion) {
            Explosion();
            hasExplosion = true;
            isBombOn = false;
        }
    }
    #endregion

    #region Other Functions
    public void AddForceWhenSpawn(float dir, float force) {
        workSpace.Set(dir, 1.75f);
        Rigid.AddForce(workSpace * force, ForceMode2D.Impulse);
    }

    private void Explosion() {
        explosion.transform.SetPositionAndRotation(bombOn.transform.position, bombOn.transform.rotation);
        bombOn.SetActive(false);
        bombOff.SetActive(false);
        explosion.SetActive(true);
    }
    #endregion

    #region Actions
    public void Push(float direction, float force) {
        workSpace.Set(direction * 3f * force, 5f * force);
        Rigid.velocity = Vector2.zero;
        Rigid.AddForce(workSpace, ForceMode2D.Impulse);
    }

    public void Blow() {
        throw new System.NotImplementedException();
    }

    public void Kick(float direction) {
        workSpace.Set(direction * 4.5f, 6.5f);
        Rigid.velocity = Vector2.zero;
        Rigid.AddForce(workSpace, ForceMode2D.Impulse);
    }

    public void Pick() {
        throw new System.NotImplementedException();
    }

    public void Throw() {
        throw new System.NotImplementedException();
    }
    #endregion
}
