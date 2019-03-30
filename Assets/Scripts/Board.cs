using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    enum Cell {
        None = 0,
        Player1 = 1,
        Player2 = 2,
        Player3 = 4,
        Player4 = 8,
        Bug = -1
    }

    // Cell[][] board = {
    //     {0, 0, 0, 0, 0, 0, 0},
    //     {0, 0, 0, 0, 0, 0, 0},
    //     {0, 0, 0, 0, 0, 0, 0},
    //     {0, 0, 0, 0, 0, 0, 0},
    //     {0, 0, 0, 0, 0, 0, 0},
    //     {0, 0, 0, 0, 0, 0, 0},
    //     {0, 0, 0, 0, 0, 0, 0}
    // };

    // Start is called before the first frame update
    void Start() {
        Cell teste = Cell.Player1 | Cell.Player2;

        if ((teste & Cell.Player1) != 0)
            Debug.Log("teste contains Player1");
        if ((teste & Cell.Player2) != 0)
            Debug.Log("teste contains Player2");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
