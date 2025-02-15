using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float tourque;
    private Rigidbody2D rb;
    private Vector2 direct;

    [SerializeField] private LayerMask layerGround;
    [SerializeField] private float distance;
    private bool grounded;

    [SerializeField] private GameObject smokePrefab;
    [SerializeField] private float timeEffectSpawn;
    private float timeEffectDuration;

    [SerializeField] private GameObject smokeDownPrefabs;
    private bool isFly;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        timeEffectDuration = 0;
        isFly = false;
    }

    // Update is called once per frame
    void Update()
    {
        direct.x = Input.GetAxisRaw("Horizontal");
        direct.y = Input.GetAxisRaw("Vertical");

        grounded = IsGround();
        if (Input.GetButtonDown("Jump") && grounded) {
            isFly = true;
            Jump();
            AudioManager.Instance.Play("jump");
        }

        if(rb.velocity.y <= 0f && grounded && isFly ) {

            Vector2 pos = new Vector2(transform.position.x, transform.position.y - 0.2f);
            GameObject eff = Instantiate(smokeDownPrefabs, pos, Quaternion.identity);

            AudioManager.Instance.Play("land");
          
            isFly = false;
            DestroyObject(eff);
        }


        if(timeEffectDuration < 5)
            timeEffectDuration += Time.deltaTime;
        if(direct.x !=0 && timeEffectDuration > timeEffectSpawn) {
            Invoke(nameof(InitEffect), .5f);
            timeEffectDuration = 0;
        }

    }

    private void FixedUpdate() {
        if(direct.magnitude > 0) {
            direct.Normalize();
            rb.velocity = new Vector2(direct.x * speed, rb.velocity.y);
        }

    }

    private void InitEffect() {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y - 0.5f);
        Instantiate(smokePrefab, pos, Quaternion.identity) ;
    }
    private void Jump() {
        rb.velocity = new Vector2(0, jumpPower);
    }

    private RaycastHit2D IsGround() {
        return Physics2D.Raycast(this.transform.position, Vector2.down, distance, layerGround);
    }

    private async void DestroyObject(GameObject obj) {
        await Task.Delay(500);
        Destroy(obj.gameObject);
    }
}
