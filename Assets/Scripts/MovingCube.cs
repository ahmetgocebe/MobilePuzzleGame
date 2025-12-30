using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovingCube : MonoBehaviour
{
    public GridSystem gridSys;
    public int currentX = 3;
    public int currentY = 2;
    public InputActionReference moveAction;
    public float speed = 5f;
    private bool isPlayerInPlace = true;
    private bool isMoving = false;
    private void OnEnable()
    {
        StartCoroutine(InitPlayer());
    }
    IEnumerator InitPlayer()
    {
        yield return new WaitUntil(() => gridSys != null);
        yield return new WaitUntil(() => gridSys.gridCells.Count > 0);
        moveAction.action.Enable();
        moveAction.action.canceled += ctx => { Debug.Log("Move canceled"); };
        moveAction.action.performed += OnMovePerformed;
        PlacePlayer();
    }

    private void PlacePlayer()
    {
        transform.position = new Vector3(currentX, currentY, 0);
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>();
        Debug.Log("Move Input Received: " + input);
        //if (!isPlayerInPlace)
        //{
        //    Debug.Log("Player is still moving. Ignoring new input.");
        //    return;
        //}
        if(isMoving || !isPlayerInPlace)
        {
            Debug.Log("Player is already moving. Ignoring new input.");
            return;
        }
        if (input.y > 0)
        {
            isMoving = true;
            StartCoroutine(MoveUpward());
        }
        else if (input.y < 0)
        {
            isMoving = true;
            StartCoroutine(MoveDownward());
        }
        else if (input.x < 0)
        {
            isMoving = true;
            StartCoroutine(MoveLeft());
        }
        else if (input.x > 0)
        {
            isMoving = true;
            StartCoroutine(MoveRight());
        }
    }

    IEnumerator MoveUpward()
    {
        while (gridSys.gridCells.ContainsKey((currentX, currentY + 1)) && !gridSys.gridCells[(currentX, currentY + 1)].IsWall)
        {
            yield return new WaitUntil(() => isPlayerInPlace);
            currentY += 1;
            if (gridSys.gridCells[(currentX, currentY)].IsLava)
            {
                Debug.Log("Stepped on Lava at: " + currentX + "," + currentY);
                break;
            }
            if (gridSys.gridCells[(currentX, currentY)].IsBouncer)
            {
                Debug.Log("Hit a Bouncer at: " + currentX + "," + currentY);
                StartCoroutine(MoveDownward());
                break;
            }
            Debug.Log("Moved Upward to: " + currentY);
        }
        isMoving = false;
    }
    IEnumerator MoveDownward()
    {
        yield return new WaitUntil(() => isPlayerInPlace);

        while (gridSys.gridCells.ContainsKey((currentX, currentY - 1)) && !gridSys.gridCells[(currentX, currentY - 1)].IsWall)
        {
            currentY -= 1;
            if (gridSys.gridCells[(currentX, currentY)].IsLava)
            {
                Debug.Log("Stepped on Lava at: " + currentX + "," + currentY);
                break;
            }
            if (gridSys.gridCells[(currentX, currentY)].IsBouncer)
            {
                Debug.Log("Hit a Bouncer at: " + currentX + "," + currentY);
                StartCoroutine(MoveUpward());
                break;
            }
            Debug.Log("Moved Downward to: " + currentY);
        }
        isMoving = false;
    }
    IEnumerator MoveLeft()
    {
        yield return new WaitUntil(() => isPlayerInPlace);

        while (gridSys.gridCells.ContainsKey((currentX - 1, currentY)) && !gridSys.gridCells[(currentX - 1, currentY)].IsWall)
        {
            currentX -= 1;
            if (gridSys.gridCells[(currentX, currentY)].IsLava)
            {
                Debug.Log("Stepped on Lava at: " + currentX + "," + currentY);
                break;
            }
            if (gridSys.gridCells[(currentX, currentY)].IsBouncer)
            {
                Debug.Log("Hit a Bouncer at: " + currentX + "," + currentY);
                StartCoroutine(MoveRight());
                break;
            }
            Debug.Log("Moved Left to: " + currentX);
        }
        isMoving = false;
    }
    IEnumerator MoveRight()
    {
        yield return new WaitUntil(() => isPlayerInPlace);

        while (gridSys.gridCells.ContainsKey((currentX + 1, currentY)) && !gridSys.gridCells[(currentX + 1, currentY)].IsWall)
        {
            currentX += 1;
            if (gridSys.gridCells[(currentX, currentY)].IsLava)
            {
                Debug.Log("Stepped on Lava at: " + currentX + "," + currentY);
                break;
            }
            if (gridSys.gridCells[(currentX, currentY)].IsBouncer)
            {
                Debug.Log("Hit a Bouncer at: " + currentX + "," + currentY);
                StartCoroutine(MoveLeft());
                break;
            }
            Debug.Log("Moved Right to: " + currentX);
        }
        isMoving = false;
    }
    private void Update()
    {
        isPlayerInPlace = transform.position.x == currentX && transform.position.y == currentY;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(currentX, currentY, 0), speed * Time.deltaTime);
    }
}
