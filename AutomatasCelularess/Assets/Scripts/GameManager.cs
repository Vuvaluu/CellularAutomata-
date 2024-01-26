using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] int rows;
    [SerializeField] int cols;
    [SerializeField] int [] rule;
    [SerializeField] GameObject blankCell;
    [SerializeField] GameObject fullCell;
    [SerializeField] int inputRule = 54;
    [SerializeField] Button generateButton;
    [SerializeField] TMP_InputField sizeX_IF;
    [SerializeField] TMP_InputField sizeY_IF;
    [SerializeField] TMP_InputField rule_IF;
    [SerializeField] Toggle randomStartToggle;
    [SerializeField] Toggle steppedToggle;
    
    int rowCount;
    int [] cells;
    int nextCellsY;
    string binaryRule;
    bool randomStart;
    bool stepped;
    bool generate;

    void Start()
    {
        generate = false;
    }

    void Update()
    {
        if(generate == true && rowCount < rows) {
            rowCount ++;
            int[] nextCells = new int [cols];
            nextCellsY --;
            nextCells[0] = cells[0];
            nextCells[cells.Length - 1] = cells[cells.Length - 1];
            for (int i = 1; i < cells.Length - 1; i++)
            {
                int left = cells[i - 1];
                int right = cells [i + 1];
                int state = cells [i];
                int newState = CalcState(left, state, right, rule);
                nextCells [i] = newState;
            }
            cells = nextCells;
            for (int i = 0; i < cells.Length; i++)
            {
                foreach (var n in cells)
                {
                    Debug.Log("cell: " + n);
                }
                if (cells[i] == 0)
                {
                    Instantiate(blankCell, new Vector2(i, nextCellsY), Quaternion.identity);
                } else if (cells[i] == 1)
                {
                    Instantiate(fullCell, new Vector2(i, nextCellsY), Quaternion.identity);
                } else
                {
                    Debug.Log(cells[i]);
                }
            }
        }
        
    } 

   public void GenerateCA(){
    rows = int.Parse(sizeX_IF.text);
    generate = true;
    cols = int.Parse(sizeY_IF.text);
    inputRule = int.Parse(rule_IF.text);
    randomStart = randomStartToggle.isOn;
    stepped = steppedToggle.isOn;

    cells = new int[cols];
    nextCellsY = 0;
    CreateCells();
 
    binaryRule = Convert.ToString(inputRule, 2).PadLeft(8, '0');
    rule = new int [binaryRule.Length];
    for (int i = 0; i < binaryRule.Length; i++)
    {
        rule[i] = int.Parse(binaryRule[i].ToString());
    }
        Debug.Log(rule[0] + ", " + rule[1] + ", " + rule[2] + ", " + rule[3] + ", " + rule[4] + ", " + rule[5] + ", " + rule[6] + ", " + rule[7]);

    }
    int CalcState(int l, int s, int r, int[] binRule){
        if (l == 1 && s == 1 && r ==1) {
            return binRule[0]; 
        }
        else if (l == 1 && s == 1 && r == 0) {
            return binRule[1]; 
        }
        else if (l == 1 && s == 0 && r == 1) {
            return binRule[2]; 
        }
        else if (l == 1 && s == 0 && r == 0) {
            return binRule[3]; 
        }
        else if (l == 0 && s == 1 && r == 1) {
            return binRule[4]; 
        }
        else if (l == 0 && s == 1 && r == 0) {
            return binRule[5]; 
        }
        else if (l == 0 && s == 0 && r == 1) {
            return binRule[6]; 
        }
        else if (l == 0 && s == 0 && r == 0) {
            return binRule[7]; 
        } else {
           // Debug.Log ("l " + l + " s " + s + " r " + r);
            return 2;
        }
    }

    void CreateCells(){
        for (int i = 0; i < cols; i++)
        {
            if(randomStart == true){
                cells[i] = Random.Range(0,2);
            } else {
                 cells[i] = 0;
            }
            if(cells[i] == 0) {
                Instantiate(blankCell, new Vector2(i, 0), Quaternion.identity);
            } else if (cells[i] == 1)
            {
                Instantiate(fullCell, new Vector2(i, 0), Quaternion.identity);
            }
            
        } 
        if(randomStart == false){
            cells[cols/2] = 1;
        }
  
    }
}
