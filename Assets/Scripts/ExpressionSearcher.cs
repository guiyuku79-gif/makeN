using System;
using System.Collections.Generic;

public enum Operator
{
    Add,
    Sub,
    Mul,
    Div
}

public class ExpressionSearcher
{
    private readonly Operator[] operators =
    {
        Operator.Add,
        Operator.Sub,
        Operator.Mul,
        Operator.Div
    };

    private string[] opNames =
    {
        "+",
        "-",
        "*",
        "/",
    };


    /// <summary>
    /// 最も出現回数の少ない(0回は除く)1～20の整数を返す
    /// </summary>
    public (int,string) Search(Fraction[] numbers)
    {
        int[] count = new int[21];

        //作り方を保存しておく
        string[] howToMake = new string[21];

        foreach (Fraction[] permutation in GetPermutations(numbers))
        {
            for (int op1 = 0; op1 < 4; op1++)
            {
                for (int op2 = 0; op2 < 4; op2++)
                {
                    for (int op3 = 0; op3 < 4; op3++)
                    {
                        Operator[] ops =
                        {
                            operators[op1],
                            operators[op2],
                            operators[op3]
                        };

                        for (int pattern = 0; pattern < 5; pattern++)
                        {
                            try
                            {
                                Fraction result = Calculate(permutation, ops, pattern);

                                if (result.Denominator == 1)
                                {
                                    int value = result.Numerator;

                                    if (1 <= value && value <= 20)
                                    {
                                        count[value]++;

                                        howToMake[value] = EquationToString(permutation,op1,op2,op3,pattern);
                                    }
                                }
                            }
                            catch (DivideByZeroException)
                            {
                                // 0除算は無視
                            }
                        }
                    }
                }
            }
        }

        //カウントをする
        int minCount = int.MaxValue;
        int answer = -1;
        string howToMakeAnswer = "";

        for (int i = 1; i <= 20; i++)
        {
            if (count[i] != 0 && count[i] < minCount)
            {
                minCount = count[i];
                answer = i;
                howToMakeAnswer = howToMake[i];
            }
        }


        return (answer,howToMakeAnswer);
    }

    private string EquationToString(Fraction[] n,int op1,int op2,int op3,int pattern)
    {
        switch (pattern)
        {
            case 0:
                {
                    return $"(({n[0]}{opNames[op1]}{n[1]}){opNames[op2]}{n[2]}){opNames[op3]}{n[3]}";
                }
            case 1:
                {
                    return $"({n[0]}{opNames[op1]}({n[1]}{opNames[op2]}{n[2]})){opNames[op3]}{n[3]}";
                }
            case 2:
                {
                    return $"{n[0]}{opNames[op1]}(({n[1]}{opNames[op2]}{n[2]}){opNames[op3]}{n[3]})";
                }
            case 3:
                {
                    return $"{n[0]}{opNames[op1]}({n[1]}{opNames[op2]}({n[2]}{opNames[op3]}{n[3]}))";
                }
            case 4:
                {
                    return $"({n[0]}{opNames[op1]}{n[1]}){opNames[op2]}({n[2]}{opNames[op3]}{n[3]})";
                }
        }
        throw new Exception();
    }

    private Fraction Calculate(Fraction[] n, Operator[] op, int pattern)
    {
        switch (pattern)
        {
            // ((a b)c)d
            case 0:
            {
                Fraction r = Calc(n[0], n[1], op[0]);
                r = Calc(r, n[2], op[1]);
                r = Calc(r, n[3], op[2]);
                return r;
            }

            // (a(b c))d
            case 1:
            {
                Fraction r = Calc(n[1], n[2], op[1]);
                r = Calc(n[0], r, op[0]);
                r = Calc(r, n[3], op[2]);
                return r;
            }

            // a((b c)d)
            case 2:
            {
                Fraction r = Calc(n[1], n[2], op[1]);
                r = Calc(r, n[3], op[2]);
                r = Calc(n[0], r, op[0]);
                return r;
            }

            // a(b(c d))
            case 3:
            {
                Fraction r = Calc(n[2], n[3], op[2]);
                r = Calc(n[1], r, op[1]);
                r = Calc(n[0], r, op[0]);
                return r;
            }

            // (a b)(c d)
            case 4:
            {
                Fraction left = Calc(n[0], n[1], op[0]);
                Fraction right = Calc(n[2], n[3], op[2]);
                return Calc(left, right, op[1]);
            }
        }

        throw new Exception();
    }

    private Fraction Calc(Fraction a, Fraction b, Operator op)
    {
        switch (op)
        {
            case Operator.Add:
                return a + b;

            case Operator.Sub:
                return a - b;

            case Operator.Mul:
                return a * b;

            case Operator.Div:
                return a / b;
        }

        throw new Exception();
    }


    //数の組み合わせを作成
    private List<Fraction[]> GetPermutations(Fraction[] numbers)
    {
        List<Fraction[]> result = new List<Fraction[]>();

        Permute(numbers, 0, result);

        return result;
    }

    private void Permute(Fraction[] array, int index, List<Fraction[]> result)
    {
        if (index == array.Length)
        {
            Fraction[] copy = new Fraction[array.Length];
            Array.Copy(array, copy, array.Length);
            result.Add(copy);
            return;
        }

        for (int i = index; i < array.Length; i++)
        {
            Swap(array, index, i);

            Permute(array, index + 1, result);

            Swap(array, index, i);
        }
    }

    private void Swap(Fraction[] array, int a, int b)
    {
        Fraction temp = array[a];
        array[a] = array[b];
        array[b] = temp;
    }

}