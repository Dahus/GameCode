using System;

public struct Coefficients
{
    public float Attack;
    public float Defense;
    public float Accumulation;

    public Coefficients(float attack, float defence, float accumulation)
    {
        Attack = attack;
        Defense = defence;
        Accumulation = accumulation;
    }

    public static Coefficients MulttiplyCoeff(Coefficients a, Coefficients b)
    {
        return new Coefficients(a.Attack * b.Attack, a.Defense * b.Defense, a.Accumulation * b.Accumulation);
    }

    public static Coefficients AdditionCoeff(Coefficients a, Coefficients b)
    {
        return new Coefficients(a.Attack + b.Attack, a.Defense + b.Defense, a.Accumulation + b.Accumulation);
    }

    public float GetMaxValue()
    {
        return Math.Max(Math.Max(Attack, Defense), Accumulation);
    }
}
