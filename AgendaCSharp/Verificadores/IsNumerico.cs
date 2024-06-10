namespace AgendaCSharp.Verificadores;

public class IsNumerico
{
    public static bool isAllDigits(string s)
    {
        foreach (char c in s)
        {
            if (!char.IsDigit(c))
            {
                return false;
            }
        }
        return true;
    }
}
