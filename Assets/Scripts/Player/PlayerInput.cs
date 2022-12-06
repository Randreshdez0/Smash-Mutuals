using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private int player = 1;
    public float movX;
    public float movY;
    public KeyCode jumpButton;
    public KeyCode attackButton;
    public enum InputState
    {
        Up,
        Left, 
        Right, 
        Down,
        Neutral
    }

    void Update()
    {
        movX = Input.GetAxisRaw("Horizontal" + player);
        movY = Input.GetAxisRaw("Vertical" + player);
    }

    public bool Holding(InputState dir)
    {
        if (dir == InputState.Up)
        {
            return (movY > 0); //Holding Up
        }
        if (dir == InputState.Down)
        {
            return (movY < 0); //Holding Up
        }
        if (dir == InputState.Right)
        {
            return (movX > 0); //Holding Up
        }
        if (dir == InputState.Left)
        {
            return (movX < 0); //Holding Up
        }
        if (dir == InputState.Neutral)
        {
            return (movY == 0 && movX == 0);
        }
        else return false;
    }
    public bool Holding(InputState[] dir)
    {
        foreach (InputState d in dir)
        {
            if (!Holding(d)) 
            {
                return false;
            }
        }
        return true;
    }
}
