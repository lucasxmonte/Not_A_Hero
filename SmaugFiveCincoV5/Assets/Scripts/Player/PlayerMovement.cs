using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float dirX = 0f;
    private float runningSpeed = 5f;
    private float walkingSpeed = 3f;
    private float crouchingSpeed = 2f;

    [SerializeField] public PlayerData player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        float moveSpeed = ControlPlayerSpeed();
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (dirX > 0f)
        {
            player.facingSide = PlayerData.FacingSide.right;
            if (Input.GetKey(KeyCode.LeftShift)
            )
            {
                player.state = PlayerData.PlayerMovementState.running;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    player.state = PlayerData.PlayerMovementState.crouching;
                }
                else
                {
                    player.state = PlayerData.PlayerMovementState.walking;
                }
            }
        }
        else if (dirX < 0f)
        {
            player.facingSide = PlayerData.FacingSide.left;
            if (Input.GetKey(KeyCode.LeftShift)
            )
            {
                player.state = PlayerData.PlayerMovementState.running;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    player.state = PlayerData.PlayerMovementState.crouching;
                }
                else
                {
                    player.state = PlayerData.PlayerMovementState.walking;
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                player.state = PlayerData.PlayerMovementState.crouching;
            }
            else
            {
                player.state = PlayerData.PlayerMovementState.idle;
            }
        }
    }

    private float ControlPlayerSpeed()
    {
        return player.state switch
        {
            PlayerData.PlayerMovementState.walking => walkingSpeed,
            PlayerData.PlayerMovementState.running => runningSpeed,
            PlayerData.PlayerMovementState.crouching => crouchingSpeed,
            _ => 0f,
        };
    }
}
