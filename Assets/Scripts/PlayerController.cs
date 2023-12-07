using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public enum ActionType { Jump }
    public Dictionary<ActionType, InputAction.CallbackContext> inputDict = new Dictionary<ActionType, InputAction.CallbackContext>();

    [Header("References")] [Space(4)]
    public Rigidbody2D rb;
    // public Player player;
    public SpriteRenderer sprite;
    // public Animator fanAnim;
    // public Animator playerAnimator;
    public Collider2D coll;
    public Transform raycastedTransform;
    public Transform rotatedTransform;
	public PhysicsMaterial2D physicsMaterial;
	public GameObject bulletPrefab;

    [Header("Combat Variables")] [Space(4)]
    [Tooltip("distance from the player the attack is fired")]
    public float fireDist = 1.5f;
	[Tooltip("how long between shots")]
	public float fireCooldown = 0.25f;
	protected float fireCooldownTimer = 0f;

    [Header("Platformer Feel Variables")] [Space(4)]
    public float minRotateAngle = 30f;
    [Tooltip("How fast the player accelerates")]
    public float moveAcceleration;
    [Tooltip("Force Applied from Jumping")]
    public float jumpForce;
    [Tooltip("Max speed from moving")]
    public float movementSpeedCap;
	[Tooltip("How little the player slides")]
	public float groundedFriction = 1.5f;
    [Tooltip("Time before you can jump after jumping")]
    public float fallMultiplier = 2f;
    [Tooltip("Max possible velocity")]
    public float velocityCap;
    [Tooltip("Time you can jump while not grounded")]
    public float coyoteTime = 0.2f;
    protected float coyoteTimeTimer = 0f;
    [Tooltip("Time before you can jump after jumping")]
    public float jumpBuffer = 0.1f;
    protected float jumpBufferTimer = 0f;
    [Tooltip("Minimym Y velocity before your fall accelerates")]
    public float fallYThreshold = 2f;
    [Tooltip("Distance from the ground that the player is considered grounded")]
    public float minJumpDist = 0.2f;
    public float rotateSpeed;
    public PlayerInput input;
    private Vector2 moveDirection;
    private float baseGravityScale;
    private float baseLinearDrag;
    private bool jumped=false;
    private Vector2 fireDirection;
    [SerializeField]private bool canMove;
    [HideInInspector] public RigidbodyConstraints2D constraints;
    [HideInInspector] public bool dead = false;

    Quaternion smoothTilt;

    void Awake() {
		physicsMaterial.friction = groundedFriction;
		rb.sharedMaterial = physicsMaterial;
        constraints = rb.constraints;
    }
    void Start()
    {
        baseLinearDrag = rb.drag;
        baseGravityScale = rb.gravityScale;
        if (!canMove)
        {
            rb.gravityScale = 0f;
        }
    }
    // Update is called once per frame
    void Update()
    {
        UpdateTimers();
        if (!canMove) return;
        UpdatePhysics();
        UpdateFireDirection();
        ApplyMovement();
    }
    void ApplyMovement() {
        if (Mathf.Abs(rb.velocity.x) > movementSpeedCap) return;
        rb.AddForce(Time.deltaTime*moveAcceleration*moveDirection);
    }
    public void ToggleMovement(bool state)
    {
        canMove = state;
        rb.gravityScale = baseGravityScale;
    }
    #region Input Callbacks
    public void Move(InputAction.CallbackContext context) {
        moveDirection = new Vector2(context.ReadValue<Vector2>().x, 0);
    }
    public void Jump(InputAction.CallbackContext context) {
        if (!ManageAction(ActionType.Jump, context) || dead) return;
        if (!CanJump()) return;
		physicsMaterial.friction = 0;
		rb.sharedMaterial = physicsMaterial;
        jumped = true;
        coyoteTimeTimer = 0f;
        jumpBufferTimer = jumpBuffer;
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up*jumpForce, ForceMode2D.Impulse);
        // player.Play("jump", 0.5f, 0.1f);
    }

    public void FireDirection(InputAction.CallbackContext context) {
        if (!canMove) { return; }
        if (gameObject.IsDestroyed()) return;
        fireDirection = -GetRelativeMousePosition(transform.position);
        fireDirection.Normalize();
    }

	public void Fire(InputAction.CallbackContext context) {
		if (!canMove) { return; }
		if (gameObject.IsDestroyed()) return;
		if (fireCooldownTimer <= 0) {
			fireCooldownTimer = fireCooldown;
			Quaternion rotation = Quaternion.AngleAxis(Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg, Vector3.forward);
			GameObject bullet = Instantiate(bulletPrefab, (Vector2)transform.position+fireDirection*fireDist, rotation);
			bullet.GetComponent<Bullet>().bulletDirection = fireDirection;
		}
	}
    #endregion

    #region Helper Functions
    void UpdateTimers() {
        jumpBufferTimer = Mathf.Max(jumpBufferTimer-Time.deltaTime, 0f);
        coyoteTimeTimer = Mathf.Max(coyoteTimeTimer-Time.deltaTime, 0f);
		fireCooldownTimer = Mathf.Max(fireCooldownTimer-Time.deltaTime, 0f);
    }
    void UpdateFireDirection() {
        sprite.flipX = fireDirection.x < 0;
        float angle = Mathf.Atan2(-fireDirection.y, -fireDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        // fanAnim.transform.rotation = Quaternion.Slerp(fanAnim.transform.rotation, rotation, 50f*Time.deltaTime);;
    }
    void UpdatePhysics() {
        rb.velocity = velocityCap > 0 ? Vector2.ClampMagnitude(rb.velocity, velocityCap) : rb.velocity;
        // playerAnimator.SetFloat("yVelocity", rb.velocity.y);
        if (IsGrounded()) {
            UpdateGrounded();
        }
        else {
            UpdateAirTime();
        }
    }
    void UpdateGrounded() {
        // if (!dead) playerAnimator.Play("playerIdle");
        // if still able to jump
        if (jumpBufferTimer <= 0f) {
			if (physicsMaterial.friction == 0)
			{
				physicsMaterial.friction = groundedFriction;
				rb.sharedMaterial = physicsMaterial;
			}
            // playerAnimator.SetBool("grounded", true);
            //playerAnimator.SetBool("falling", false);
            rb.gravityScale = baseGravityScale;
            rb.drag = baseLinearDrag;
            coyoteTimeTimer = coyoteTime;
            jumped = false;
        }
    }
    void UpdateAirTime() {
        // playerAnimator.SetBool("grounded", false);
		if (physicsMaterial.friction != 0)
		{
			physicsMaterial.friction = 0;
			rb.sharedMaterial = physicsMaterial;
		}
        rb.drag = baseLinearDrag * 2f;
        rb.gravityScale = baseGravityScale;
        //rb.drag = baseLinearDrag * 0.15f;
        if (rb.velocity.y < fallYThreshold) {
            //playerAnimator.SetBool("falling", true);
            rb.gravityScale = baseGravityScale*fallMultiplier;
        }
        else if (rb.velocity.y > 0f && jumped && !inputDict.ContainsKey(ActionType.Jump)) {
            rb.gravityScale = baseGravityScale*fallMultiplier;
        }
    }
    bool CanJump() {
        return coyoteTimeTimer > 0f;
    }
    bool IsGrounded() {
        return Physics2D.OverlapCircle(GetBottomPoint(), minJumpDist, 1 << LayerMask.NameToLayer("Ground"));
    }
    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the bottom point
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(GetBottomPoint(), minJumpDist);
        /*float angle = (transform.rotation.eulerAngles.z+270)%360;
        Vector2 center = new Vector2(
            coll.transform.position.x + Mathf.Abs(coll.offset.y) * Mathf.Cos(Mathf.Deg2Rad * angle),
            coll.transform.position.y + Mathf.Abs(coll.offset.y) * Mathf.Sin(Mathf.Deg2Rad * angle)
        );
        Gizmos.DrawWireSphere(center, 1);*/
    }
    bool ManageAction(ActionType action, InputAction.CallbackContext context) {

        if (context.canceled) {
            inputDict.Remove(action);
            return false;
        }
        inputDict[action] = context;
        if (context.started) return true;
        return false;
    }
    private Vector2 GetBottomPoint(bool imSoSorry=true) {
        float angle = coll.transform.rotation.eulerAngles.z-90f;
        if (imSoSorry) angle = sprite.transform.rotation.eulerAngles.z-90;
        Vector2 offset = new Vector2(
            coll.bounds.extents.x * Mathf.Cos(Mathf.Deg2Rad * angle),
            coll.bounds.extents.y * Mathf.Sin(Mathf.Deg2Rad * angle)
        );
        Vector2 center = new Vector2(
            coll.transform.position.x + Mathf.Abs(coll.offset.y) * Mathf.Cos(Mathf.Deg2Rad * angle),
            coll.transform.position.y + Mathf.Abs(coll.offset.y) * Mathf.Sin(Mathf.Deg2Rad * angle)
        );
        /*Debug.Log($"Angle: {angle}");
        Debug.Log($"pos offset: {offset}");
        Debug.Log($"Center: {center}");*/
        return center+offset;
    }

	public static Vector2 GetRelativeMousePosition(Vector2 relPos) {
        Vector2 pos = relPos - GetMouseWorldPosition();
        pos.Normalize();
        return pos;
    }

	public static Vector2 GetMouseWorldPosition() {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    #endregion
}
