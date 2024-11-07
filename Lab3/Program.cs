using Lab3;
SparseMatrix matrix1 = null;
SparseMatrix matrix2 = null;
while(matrix1 == null && matrix2 == null)
{
    Console.WriteLine("Write number of rows:");
    int rows = int.Parse(Console.ReadLine());
    Console.WriteLine("Write number of columns:");
    int columns = int.Parse(Console.ReadLine());
    matrix1 = new SparseMatrix(rows, columns);
    matrix2 = new SparseMatrix(rows, columns);
}
SparseMatrix active = matrix1;
while (true)
{
    Console.WriteLine("1 - add an element, 2 - multiply matrix on scalar, 3 - transpose matrix, 4 - add matrix to matrix2, 5 - print, 6 - change matrix (default matrix1)");
    int option;   
    if (active == matrix1)
    {
        Console.WriteLine("You are working with matrix1");
    }
    if (active == matrix2)
    {
        Console.WriteLine("You are working with matrix2");
    }
    if (int.TryParse(Console.ReadLine(), out option))
    {
        int row, col, value;
        switch (option)
        {
            case 1:               
                Console.WriteLine("Write row:");
                row = int.Parse(Console.ReadLine());
                Console.WriteLine("Write column:");
                col = int.Parse(Console.ReadLine());
                Console.WriteLine("Write value:");
                value = int.Parse(Console.ReadLine());
                active.SetElement(row, col, value);
                break;
            case 2:
                Console.WriteLine("Write value:");
                value = int.Parse(Console.ReadLine());
                active.Multiply(value);
                break;
            case 3:
                if (active == matrix1)
                {
                    matrix1 = matrix1.Transpose();
                    active = matrix1;
                }
                if (active == matrix2)
                {
                    matrix2 = matrix2.Transpose();
                    active = matrix2;
                }
                break;
            case 4:
                if(active == matrix1)
                {
                    matrix1 = matrix1.Add(matrix2);
                    active = matrix1;
                }
                if(active == matrix2)
                {
                    matrix2 = matrix2.Add(matrix1);
                    active = matrix2;
                }
                break;
            case 5:
                active.Print();
                break;
            case 6:
                if(active == matrix1)
                {
                    active = matrix2;
                    break;
                }
                if(active == matrix2)
                {
                    active = matrix1;
                }
                break;
        }
    }

}
//SparseMatrix matrix2 = new SparseMatrix(3, 3);
//SparseMatrix matrix2 = new SparseMatrix(3, 3);

//// Додавання елементів до першої матриці
//matrix2.SetElement(0, 0, 5);
//matrix2.SetElement(1, 1, 8);
//matrix2.SetElement(2, 2, 3);
//matrix2.SetElement(0, 1, 4);
//// Додавання елементів до другої матриці
//matrix2.SetElement(0, 1, 7);
//matrix2.SetElement(1, 2, 4);
//matrix2.SetElement(2, 0, 6);

//Console.WriteLine("Matrix 1:");
//matrix2.Print();

//Console.WriteLine("\nMatrix 2:");
//matrix2.Print();
//Console.WriteLine("\nMatrix 1 multiplied by 2:");
//matrix2.Multiply(2);
//matrix2.Print();

//Console.WriteLine("\nTranspose of Matrix 1:");
//SparseMatrix transposedMatrix1 = matrix2.Transpose();
//transposedMatrix1.Print();

//Console.WriteLine("\nMatrix 1 + Matrix 2:");
//SparseMatrix sumMatrix = matrix2.Add(matrix2);
//sumMatrix.Print();