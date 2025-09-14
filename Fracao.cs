using System;

public class Fracao
{
    public int Numerador { get; private set; }
    public int Denominador { get; private set; }

    public Fracao(int numerador, int denominador)
    {
        if (denominador == 0)
        {
            throw new ArgumentException("O denominador não pode ser zero!");
        }

        if (denominador < 0)
        {
            numerador *= -1;
            denominador *= -1;
        }

        int divisorComum = Mdc(numerador, denominador);
        this.Numerador = numerador / divisorComum;
        this.Denominador = denominador / divisorComum;
    }

    public Fracao(int numeroInteiro)
    {
        this.Numerador = numeroInteiro;
        this.Denominador = 1;
    }

    public Fracao(string textoFracao)
    {
        string[] partes = textoFracao.Split('/');
        int numerador = int.Parse(partes[0]);
        int denominador = int.Parse(partes[1]);

        if (denominador == 0)
        {
            throw new ArgumentException("O denominador não pode ser zero!");
        }
        if (denominador < 0)
        {
            numerador *= -1;
            denominador *= -1;
        }

        int divisorComum = Mdc(numerador, denominador);
        this.Numerador = numerador / divisorComum;
        this.Denominador = denominador / divisorComum;
    }

    public Fracao(double numeroDecimal)
    {
        string textoDecimal = numeroDecimal.ToString().Replace(",", ".");
        int casasDecimais = 0;
        if (textoDecimal.Contains("."))
        {
            casasDecimais = textoDecimal.Length - textoDecimal.IndexOf('.') - 1;
        }

        int denominador = (int)Math.Pow(10, casasDecimais);
        int numerador = (int)(numeroDecimal * denominador);
        
        int divisorComum = Mdc(numerador, denominador);
        this.Numerador = numerador / divisorComum;
        this.Denominador = denominador / divisorComum;
    }

    private int Mdc(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);

        while (b != 0)
        {
            int resto = a % b;
            a = b;
            b = resto;
        }
        return a;
    }

    public Fracao Somar(Fracao outraFracao)
    {
        int novoNumerador = (this.Numerador * outraFracao.Denominador) + (outraFracao.Numerador * this.Denominador);
        int novoDenominador = this.Denominador * outraFracao.Denominador;
        return new Fracao(novoNumerador, novoDenominador);
    }
    
    public Fracao Somar(int numeroInteiro)
    {
        return this.Somar(new Fracao(numeroInteiro));
    }

    public override string ToString()
    {
        return $"{this.Numerador}/{this.Denominador}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Fracao)
        {
            return false;
        }
        
        Fracao outra = (Fracao)obj;
        return this.Numerador == outra.Numerador && this.Denominador == outra.Denominador;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.Numerador, this.Denominador);
    }

    public static Fracao operator +(Fracao f1, Fracao f2) => f1.Somar(f2);
    public static Fracao operator +(Fracao f, int numero) => f.Somar(new Fracao(numero));
    public static Fracao operator +(Fracao f, double numero) => f.Somar(new Fracao(numero));
    public static Fracao operator +(Fracao f, string texto) => f.Somar(new Fracao(texto));

    public static bool operator ==(Fracao f1, Fracao f2) => f1.Equals(f2);
    public static bool operator !=(Fracao f1, Fracao f2) => !f1.Equals(f2);
    
    public static bool operator <(Fracao f1, Fracao f2) => (double)f1.Numerador / f1.Denominador < (double)f2.Numerador / f2.Denominador;
    public static bool operator >(Fracao f1, Fracao f2) => (double)f1.Numerador / f1.Denominador > (double)f2.Numerador / f2.Denominador;
    public static bool operator <=(Fracao f1, Fracao f2) => (double)f1.Numerador / f1.Denominador <= (double)f2.Numerador / f2.Denominador;
    public static bool operator >=(Fracao f1, Fracao f2) => (double)f1.Numerador / f1.Denominador >= (double)f2.Numerador / f2.Denominador;

    public bool IsPropria => Math.Abs(this.Numerador) < this.Denominador;
    public bool IsImpropria => Math.Abs(this.Numerador) >= this.Denominador;
    public bool IsAparente => this.IsImpropria && (this.Numerador % this.Denominador == 0);
    public bool IsUnitaria => this.Numerador == 1;
}