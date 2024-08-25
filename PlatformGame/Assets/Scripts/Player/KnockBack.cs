using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool gettingKnockedBack {  get; private set; }
    [SerializeField] private float knockBackTime = .2f;
    private Rigidbody2D rb;

    private void Awake()
    { rb = GetComponent<Rigidbody2D>(); }

    public void GetKnockedBack(Transform damageSource, float knockBackHit)
    {
        gettingKnockedBack = true;
        Vector2 diff = (transform.position - damageSource.position).normalized * knockBackHit * rb.mass;
        rb.AddForce(diff, ForceMode2D.Impulse);
        StartCoroutine(KnockRoutine());
    }

    private IEnumerator KnockRoutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        gettingKnockedBack = false;
    }
}
