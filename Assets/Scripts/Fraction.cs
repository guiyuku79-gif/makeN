using System;

public readonly struct Fraction : IEquatable<Fraction>
{
    public int Numerator { get; }
    public int Denominator { get; }

    public Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
            throw new DivideByZeroException("分母は0にできません。");

        // 分母を正にする
        if (denominator < 0)
        {
            numerator = -numerator;
            denominator = -denominator;
        }

        int gcd = GCD(Math.Abs(numerator), denominator);

        Numerator = numerator / gcd;
        Denominator = denominator / gcd;
    }

    // 最大公約数
    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            int temp = a % b;
            a = b;
            b = temp;
        }
        return a == 0 ? 1 : a;
    }

    // 加算
    public static Fraction operator +(Fraction a, Fraction b)
    {
        return new Fraction(
            a.Numerator * b.Denominator + b.Numerator * a.Denominator,
            a.Denominator * b.Denominator);
    }

    // 減算
    public static Fraction operator -(Fraction a, Fraction b)
    {
        return new Fraction(
            a.Numerator * b.Denominator - b.Numerator * a.Denominator,
            a.Denominator * b.Denominator);
    }

    // 乗算
    public static Fraction operator *(Fraction a, Fraction b)
    {
        return new Fraction(
            a.Numerator * b.Numerator,
            a.Denominator * b.Denominator);
    }

    // 除算
    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.Numerator == 0)
            throw new DivideByZeroException();

        return new Fraction(
            a.Numerator * b.Denominator,
            a.Denominator * b.Numerator);
    }

    // 比較
    public static bool operator ==(Fraction a, Fraction b)
    {
        return a.Numerator == b.Numerator &&
               a.Denominator == b.Denominator;
    }

    public static bool operator !=(Fraction a, Fraction b)
    {
        return !(a == b);
    }

    public bool Equals(Fraction other) => this == other;

    public override bool Equals(object obj)
    {
        return obj is Fraction other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Numerator, Denominator);
    }

    public override string ToString()
    {
        if(Denominator == 1)
        {
            return Numerator.ToString();
        }
        return $"{Numerator}/{Denominator}";
    }
}