using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class MovingCube : MonoBehaviour
{
    public GridSystem gridSys;
    public int currentX=3;
    public int currentY=2;
    public InputActionReference moveAction;
    public float speed=5f;
    private bool isPlayerInPlace = true;
    private void Start()
    {
        InitPlayer();
    }
    public void InitPlayer()
    {
        if(gridSys == null)
        {
            Debug.LogError("GridSystem reference is not set on MovingCube.");
        }
        if(gridSys.gridCells.Count == 0)
        {
            Debug.LogError("GridSystem has no grid cells. Ensure the grid is created before MovingCube starts.");
        }
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
        if(!isPlayerInPlace)
        {
            Debug.Log("Player is still moving. Ignoring new input.");
            return;
        }
        if (input.y > 0)
        {
            MoveUpward();
        }
        else if (input.y < 0)
        {
            MoveDownward();
        }
        else if (input.x < 0)
        {
            MoveLeft();
        }
        else if (input.x > 0)
        {
            MoveRight();
        }
    }

    private void MoveUpward() { 

        while (gridSys.gridCells.ContainsKey((currentX, currentY + 1)) && !gridSys.gridCells[(currentX, currentY + 1)].IsWall)
        {
            currentY += 1;
            if (gridSys.gridCells[(currentX, currentY)].IsLava)
            {
                Debug.Log("Stepped on Lava at: " + currentX + "," + currentY);
                break;
            }
            Debug.Log("Moved Upward to: " + currentY);
        }
    }
    private void MoveDownward()
    {
        while (gridSys.gridCells.ContainsKey((currentX, currentY - 1)) && !gridSys.gridCells[(currentX, currentY - 1)].IsWall)
        {
            currentY -= 1;
            if(gridSys.gridCells[(currentX, currentY)].IsLava)
            {
                Debug.Log("Stepped on Lava at: " + currentX + "," + currentY);
                break;
            }
            Debug.Log("Moved Downward to: " + currentY);
        }
    }
    private void MoveLeft()
    {
        while (gridSys.gridCells.ContainsKey((currentX - 1, currentY)) && !gridSys.gridCells[(currentX - 1, currentY)].IsWall)
        {
            currentX -= 1;
            if (gridSys.gridCells[(currentX, currentY)].IsLava)
            {
                Debug.Log("Stepped on Lava at: " + currentX + "," + currentY);
                break;
            }
            Debug.Log("Moved Left to: " + currentX);
        }
    }
    private void MoveRight()
    {
        while (gridSys.gridCells.ContainsKey((currentX + 1, currentY)) && !gridSys.gridCells[(currentX + 1, currentY)].IsWall)
        {
            currentX += 1;
            if (gridSys.gridCells[(currentX, currentY)].IsLava)
            {
                Debug.Log("Stepped on Lava at: " + currentX + "," + currentY);
                break;
            }
            Debug.Log("Moved Right to: " + currentX);
        }
    }
    private void Update()
    {
        isPlayerInPlace = transform.position.x == currentX && transform.position.y == currentY;
        transform.position =Vector3.MoveTowards(transform.position, new Vector3(currentX, currentY, 0),speed*Time.deltaTime);
    }
}
