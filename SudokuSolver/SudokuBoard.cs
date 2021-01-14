using System;
using System.Text;

namespace SudokuSolver {
  public class SudokuBoard {
    private readonly int blocks = 3;
    public readonly int size = 9;

    private int[,] board;
    private bool[,] fixedNumbers;

    public SudokuBoard() {
      board = new int[size, size];
      fixedNumbers = new bool[size, size];
    }

    public SudokuBoard(int?[,] numbers) : this() {
      for (int i = 0; i < size; i++) {
        for (int j = 0; j < size; j++) {
          int n = numbers[i, j].GetValueOrDefault();
          if ((n > 0) && (n <= size)) {
            board[i, j] = n;
            fixedNumbers[i, j] = true;
          }
        }
      }
    }

    public void SetNumber(int i, int j, int number, bool fixedNumber = false) {
      board[i, j] = number;
      fixedNumbers[i, j] = fixedNumber;
    }
    public bool IsFixedNumber(int i, int j) {
      return fixedNumbers[i, j];
    }

    public BoardStatus Check() {
      BoardStatus status = BoardStatus.Solved;

      for (int i = 0; i < size; i++) {
        BoardStatus rowCheck = CheckRow(i);
        BoardStatus columnCheck = CheckColumn(i);
        BoardStatus squareCheck = CheckBlock(i);

        if ((rowCheck == BoardStatus.Unsolvable) || (columnCheck == BoardStatus.Unsolvable) || (squareCheck == BoardStatus.Unsolvable)) {
          return BoardStatus.Unsolvable;
        } else if ((rowCheck == BoardStatus.Unknown) || (columnCheck == BoardStatus.Unknown) || (squareCheck == BoardStatus.Unknown)) {
          status = BoardStatus.Unknown;
        }
      }
      return status;
    }

    private BoardStatus CheckRow(int row) {
      BoardStatus status = BoardStatus.Solved;
      Span<bool> foundNumbers = stackalloc bool[size + 1];

      for (int i = 0; i < size; i++) {
        int n = board[row, i];
        if ((n < 1) || (n > size)) {
          status = BoardStatus.Unknown;
        } else if (foundNumbers[n]) {
          return BoardStatus.Unsolvable;
        } else {
          foundNumbers[n] = true;
        }
      }
      return status;
    }
    private BoardStatus CheckColumn(int column) {
      BoardStatus status = BoardStatus.Solved;
      Span<bool> foundNumbers = stackalloc bool[size + 1];

      for (int i = 0; i < size; i++) {
        int n = board[i, column];
        if ((n < 1) || (n > size)) {
          status = BoardStatus.Unknown;
        } else if (foundNumbers[n]) {
          return BoardStatus.Unsolvable;
        } else {
          foundNumbers[n] = true;
        }
      }
      return status;
    }
    private BoardStatus CheckBlock(int square) {
      BoardStatus status = BoardStatus.Solved;
      Span<bool> foundNumbers = stackalloc bool[size + 1];
      int blockRow = square / blocks;
      int blockColumn = square % blocks;

      for (int i = 0; i < blocks; i++) {
        for (int j = 0; j < blocks; j++) {
          int n = board[blockRow * blocks + i, blockColumn * blocks + j];
          if ((n < 1) || (n > size)) {
            status = BoardStatus.Unknown;
          } else if (foundNumbers[n]) {
            return BoardStatus.Unsolvable;
          } else {
            foundNumbers[n] = true;
          }
        }
      }
      return status;
    }

    public override stringToString()
    {​​int digits =  1 + (int)Math.Log10(size); StringBuilder sb = new StringBuilder(); 
        
        for (int i = 0; i < size; i++)
    {​
        
        if (i > 0) {
            sb.AppendLine(); if (i % blocks == 0){
                for (int j = 0; j < size; j++)

        {​​
            if ((j >0) && (j % blocks == 0)) 
                sb.Append('┼'); sb.Append('─',digits + 2); }
                sb.AppendLine();
    }​​}
        for (int j= 0; j < size; j++)
    {​​if ((j >
            0) && (j % blocks
                   == 0)) sb.Append('│'); int n = board[i,
        j]; 
        sb.Append($"{​​((n< 1) || (n > 
                                       size) ? " " : n)}​​ ".PadLeft(digits+ 2));}​​}​​return sb.ToString();}​​


  }
}
