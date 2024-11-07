using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    internal class SparseMatrix
    {
        private class Node
        {
            public int Row;
            public int Column;
            public double Value;
            public Node NextInRow;
            public Node NextInColumn;

            public Node(int row, int column, double value)
            {
                Row = row;
                Column = column;
                Value = value;
            }

        }

        private int rows;
        private int columns;
        private Node[] rowHeaders;
        private Node[] columnHeaders;

        public SparseMatrix(int rows, int columns)
        {
            this.rows = rows;
            this.columns = columns;
            rowHeaders = new Node[rows];
            columnHeaders = new Node[columns];
        }
        public void SetElement(int row, int column, double value)
        {
            if (value == 0) return;
            Node newNode = new Node(row, column, value);

            if (rowHeaders[row] == null || rowHeaders[row].Column > column)
            {
                newNode.NextInRow = rowHeaders[row];
                rowHeaders[row] = newNode;
            }
            else
            {
                Node current = rowHeaders[row];
                while (current.NextInRow != null && current.NextInRow.Column < column)
                {
                    current = current.NextInRow;
                }
                newNode.NextInRow = current.NextInRow;
                current.NextInRow = newNode;
            }
            if (columnHeaders[column] == null || columnHeaders[column].Row > row)
            {
                newNode.NextInColumn = columnHeaders[column];
                columnHeaders[column] = newNode;
            }
            else
            {
                Node current = columnHeaders[column];
                while (current.NextInColumn != null && current.NextInColumn.Row < row)
                    current = current.NextInColumn;

                newNode.NextInColumn = current.NextInColumn;
                current.NextInColumn = newNode;
            }
        }
        public void Multiply(double scalar)
        {
            for (int i = 0; i < rows; i++)
            {
                Node current = rowHeaders[i];
                while (current != null)
                {
                    current.Value *= scalar;
                    current = current.NextInRow;
                }
            }
        }
        public SparseMatrix Transpose()
        {
            SparseMatrix transposed = new SparseMatrix(columns, rows);
            for (int i = 0; i < rows; i++)
            {
                Node current = rowHeaders[i];
                while (current != null)
                {
                    transposed.SetElement(current.Column, current.Row, current.Value);
                    current = current.NextInRow;
                }
            }
            return transposed;
        }
        public SparseMatrix Add(SparseMatrix matrix)
        {
            if (rows != matrix.rows || columns != matrix.columns)
                throw new InvalidOperationException("Matrices must have the same dimensions to be added.");
            SparseMatrix result = new SparseMatrix(rows, columns);

            for (int i = 0; i < rows; i++)
            {
                Node currentA = rowHeaders[i];
                Node currentB = matrix.rowHeaders[i];

                while (currentA != null || currentB != null)
                {
                    if (currentA != null && (currentB == null || currentA.Column < currentB.Column))
                    {
                        result.SetElement(i, currentA.Column, currentA.Value);
                        currentA = currentA.NextInRow;
                    }
                    else if (currentB != null && (currentA == null || currentB.Column < currentA.Column))
                    {
                        result.SetElement(i, currentB.Column, currentB.Value);
                        currentB = currentB.NextInRow;
                    }
                    else
                    {
                        result.SetElement(i, currentA.Column, currentA.Value + currentB.Value);
                        currentA = currentA.NextInRow;
                        currentB = currentB.NextInRow;
                    }
                }
            }

            return result;
        }
        public void Print()
        {
            for (int i = 0; i < rows; i++)
            {
                Node current = rowHeaders[i];
                for (int j = 0; j < columns; j++)
                {
                    if (current != null && current.Column == j)
                    {
                        Console.Write(current.Value + " ");
                        current = current.NextInRow;
                    }
                    else
                    {
                        Console.Write("0 ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
