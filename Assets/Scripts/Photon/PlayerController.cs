using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Linq;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpForce = 50;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public bool onLadder;
    private Rigidbody2D _playerRigidbody;
    private PhotonView PV;
    private Collision col;
    private SpriteRenderer sr;
    private Animator animator;

    //[SerializeField] private TMP_Text coinText;

    [Header("PlayerStats")]
    [SerializeField] private int healthPoints;

    private void Awake()
    {

        healthPoints = 3;
        PV = GetComponent<PhotonView>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        col = GetComponent<Collision>();
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();

        if (PV.IsMine)
        {
            sr.color = Color.white;
            return;
        }
        else
        {
            Destroy(GetComponentInChildren<Camera>().gameObject); // On detruit la caméra des autres joueurs (elles sont en enfant des joueurs maintenant)
        }

        sr.color = Color.red;
    }

    private void Start()
    {
        if (PV.IsMine == false)
        {
            Destroy(_playerRigidbody);
            Destroy(GetComponent<LadderMovement>());
        }

        onLadder = false;
    }

    private void Update()
    {
        if (PV.IsMine == false)
            return;

        // Dans le cas ou c'est égal à true on fait toutes nos actions en local
        MovePlayer();

        if (onLadder)
        {
            MoveUpDown();
        }

        if (_playerRigidbody.velocity.y < 0)
        {
            _playerRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (_playerRigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            _playerRigidbody.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

    public PhotonView getPV()
    {
        return PV;
    }

    public int GetHealthPoints()
    {
        return healthPoints;
    }

    public void SetHealthPoints(int _hp)
    {
        healthPoints = _hp;
    }

    public void OnCoin(PhotonView coinPV)
    {
        PV.RPC("tookCoin", RpcTarget.All, coinPV.ViewID);
    }

    [PunRPC]
    public void tookCoin(int viewID)
    {
        if (PhotonNetwork.IsMasterClient) // Si master client on détruit la piece
        {
            Coin[] coinsToDestroy = FindObjectsOfType<Coin>().Where(x => x.GetComponent<PhotonView>().ViewID == viewID).ToArray();
            if (coinsToDestroy.Length > 0)
            {
                PhotonNetwork.Destroy(FindObjectsOfType<Coin>().Where(x => x.GetComponent<PhotonView>().ViewID == viewID).ToArray()[0].gameObject);
            }

            FindObjectsOfType<PlayerManager>().Where(x => x.getPV().IsMine).ToArray()[0].gotNewCoin();
        }

        //coinText.text = FindObjectsOfType<PlayerManager>().Where(x => x.getPV().IsMine).ToArray()[0].getCoin().ToString();
    }

    private void MoveUpDown()
    {
        var verticalInput = Input.GetAxisRaw("Vertical");

        _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, verticalInput * playerSpeed);
    }

    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        int side = 1;
        
        if (horizontalInput > 0)
        {
            side = -1;
            transform.localScale = new Vector3(side,1,1);
        }
        
        if (horizontalInput < 0)
        {
            side = 1;
            transform.localScale = new Vector3(side, 1, 1);
        }

        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);

        if (_playerRigidbody.velocity.x < 0 || _playerRigidbody.velocity.x > 0)
        {
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetButtonDown("Jump") && col.onGround)
        {
            animator.SetTrigger("Jump");
            Jump();
        }

    }

    private void Jump()
    {
        _playerRigidbody.velocity = new Vector2(_playerRigidbody.velocity.x, 0);
        _playerRigidbody.velocity += Vector2.up * jumpForce;
    }
}
