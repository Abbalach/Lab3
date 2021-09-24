using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    class Calculator
    {
        private readonly char[] Digits =  { '0','1','2','3','4','5','6','7','8','9' };
        private readonly char[] MathSymbols = { '+', '-', '/', '*', '(', ')'};
        public string ExpressionImporter()
        {
            string expression = "";
            bool helper = true;

            while (helper)
            {
                var CurrentLine = Console.ReadLine();
                if (CurrentLine == "")
                {
                    continue;
                }
                expression += CurrentLine;
                if (CurrentLine.Last() == '=')
                {
                    break;
                }
            }
            return ExpressionConverter(expression);
        }
        private string ExpressionConverter(string expression)
        {
            List<char> ExpressionSymbols = expression.ToList();
            int errorCounter = 0;
            if (ExpressionSymbols.Last() == '=')
            {
                ExpressionSymbols.RemoveAt(ExpressionSymbols.Count - 1);
            }
            for (int i = 0; i < ExpressionSymbols.Count; i++)
            {
                if (ExpressionSymbols.ElementAt(i) == ' ')
                {
                    ExpressionSymbols.RemoveAt(i);
                }
                if (ExpressionSymbols.ElementAt(i) == '.')
                {
                    if (Digits.Contains(ExpressionSymbols.ElementAt(i + 1)) && Digits.Contains(ExpressionSymbols.ElementAt(i - 1)))
                    {
                        ExpressionSymbols[i] = ',';
                    }
                    else
                    {
                        ExpressionSymbols.RemoveAt(i);
                    }
                }
                if (ExpressionSymbols.ElementAt(i) == ':')
                {
                    ExpressionSymbols[i] = '/';
                }
                if (!Digits.Contains(ExpressionSymbols.ElementAt(i)) && 
                    !MathSymbols.Contains(ExpressionSymbols.ElementAt(i)) &&  
                    ExpressionSymbols.ElementAt(i) != ',')
                {
                    if (i == ExpressionSymbols.Count - 1)
                    {
                        errorCounter += 2;
                        ExpressionSymbols.RemoveAt(i);
                    }
                    else
                    {
                        if (!Digits.Contains(ExpressionSymbols.ElementAt(i + 1)) &&
                        !MathSymbols.Contains(ExpressionSymbols.ElementAt(i + 1)) &&
                        ExpressionSymbols.ElementAt(i + 1) != ',')
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Данные имели неверный формат.");
                            Console.ForegroundColor = ConsoleColor.Gray;
                            return "";
                        }
                        else
                        {
                            errorCounter += 2;
                            ExpressionSymbols.RemoveAt(i);
                        }
                    }                   
                }
                if (ExpressionSymbols.Contains('(') && Digits.Contains(ExpressionSymbols[i]) && ExpressionSymbols[i + 1] == '(')
                {
                    ExpressionSymbols.Insert(i + 1, '*');
                }
            }
            for (int i = 0; i < ExpressionSymbols.Count; i++)
            {
                if (!Digits.Contains(ExpressionSymbols[i]) && ExpressionSymbols[i] != '(')
                {
                    if (ExpressionSymbols[i] == '*' || ExpressionSymbols[i] == '/' )
                    {
                        errorCounter++;
                        ExpressionSymbols.RemoveAt(i);
                    }
                }
                else
                {
                    break;
                }
            }
            ExpressionSymbols.Reverse();
            for (int i = 0; i < ExpressionSymbols.Count; i++)
            {
                if (!Digits.Contains(ExpressionSymbols[i]) && ExpressionSymbols[i] != ')')
                {
                    errorCounter++;
                    ExpressionSymbols.RemoveAt(i);
                }
                else
                {
                    break;
                }
            }
            ExpressionSymbols.Reverse();
            bool helper = true;
            while (helper)
            {
                bool additionalHelper = true;
                int firstSymbol = 0;
                int CountOfSymbols = 0;
                int count = 1;
                for (int i = 0; i < ExpressionSymbols.Count; i++)
                {
                    if (i == ExpressionSymbols.Count - 1)
                    {
                        helper = false;
                        break;
                    }
                    if (ExpressionSymbols[i] == '+' || ExpressionSymbols[i] == '-')
                    {
                        if (ExpressionSymbols[i] == '-')
                        {
                            count *= -1;
                        }
                        if (!Digits.Contains(ExpressionSymbols[i + 1]) && ExpressionSymbols[i + 1] != '(')
                        {
                            if (ExpressionSymbols[i + 1] == '*' || ExpressionSymbols[i + 1] == '/')
                            {
                                ExpressionSymbols.RemoveAt(i + 1);
                                i--;
                                errorCounter++;
                                continue;
                            }
                            if (ExpressionSymbols[i + 1] == '+')
                            {
                                if (additionalHelper)
                                {
                                    firstSymbol = i;
                                    additionalHelper = false;
                                }
                                CountOfSymbols++;
                            }
                            if (ExpressionSymbols[i + 1] == '-')
                            {
                                if (additionalHelper)
                                {
                                    firstSymbol = i;
                                    additionalHelper = false;
                                }
                                CountOfSymbols++;
                            }
                        }
                    }
                    if (!additionalHelper && Digits.Contains(ExpressionSymbols[i + 1]))
                    {
                        ExpressionSymbols.RemoveRange(firstSymbol + 1, CountOfSymbols);
                        if (count == 1)
                        {
                            ExpressionSymbols[firstSymbol] = '+';
                        }
                        if (count == -1)
                        {
                            ExpressionSymbols[firstSymbol] = '-';
                        }
                        helper = false;
                        break;
                    }
                }
            }
            helper = true;
            while (helper)
            {
                bool additionalHelper = true;
                int firstSymbol = 0;
                int CountOfSymbols = 0;
                int count = 1;
                for (int i = 0; i < ExpressionSymbols.Count; i++)
                {
                    if (i == ExpressionSymbols.Count - 1)
                    {
                        helper = false;
                        break;
                    }
                    if (ExpressionSymbols[i] == '*' || ExpressionSymbols[i] == '/')
                    {
                        if (ExpressionSymbols[i] == '/')
                        {
                            count *= -1;
                        }
                        if (!Digits.Contains(ExpressionSymbols[i + 1]) && ExpressionSymbols[i + 1] != '(')
                        {
                            if (ExpressionSymbols[i + 1] == '+' || ExpressionSymbols[i + 1] == '-')
                            {
                                ExpressionSymbols.RemoveAt(i + 1);
                                i--;
                                errorCounter++;
                                continue;
                            }
                            if (ExpressionSymbols[i + 1] == '*')
                            {
                                if (additionalHelper)
                                {
                                    firstSymbol = i;
                                    additionalHelper = false;
                                }
                                CountOfSymbols++;
                            }
                            if (ExpressionSymbols[i + 1] == '/')
                            {
                                if (additionalHelper)
                                {
                                    firstSymbol = i;
                                    additionalHelper = false;
                                }
                                CountOfSymbols++;
                            }
                        }
                    }
                    if (!additionalHelper && Digits.Contains(ExpressionSymbols[i + 1]))
                    {
                        ExpressionSymbols.RemoveRange(firstSymbol + 1, CountOfSymbols);
                        if (count == 1)
                        {
                            ExpressionSymbols[firstSymbol] = '*';
                        }
                        if (count == -1)
                        {
                            ExpressionSymbols[firstSymbol] = '/';
                        }
                        helper = false;
                        break;
                    }
                }
            }


            if (errorCounter >= 6)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Данные имели неверный формат.");
                Console.ForegroundColor = ConsoleColor.Gray;
                return "";
            }
            return new string(ExpressionSymbols.ToArray());
        }
        public double[] Arithmetic(string expression)
        {
            double mathexpression = 0;
            double errorhelper = 0;
            string localexpression = expression;
            if (localexpression.Contains('E'))
            {
                var DegreeSpliter = localexpression.Split('E');
                if (DegreeSpliter[1][0] == '+')
                {
                    localexpression = DegreeSpliter[0] + "*1" + new string('0', int.Parse(DegreeSpliter[1].Substring(1)));
                    expression = DegreeSpliter[0] + "*1" + new string('0', int.Parse(DegreeSpliter[1].Substring(1)));
                }
                if (DegreeSpliter[1][0] == '-')
                {
                    localexpression = DegreeSpliter[0] + "/1" + new string('0', int.Parse(DegreeSpliter[1].Substring(1)));
                    expression = DegreeSpliter[0] + "/1" + new string('0', int.Parse(DegreeSpliter[1].Substring(1)));
                }
            }
            if (localexpression.Contains('(') && localexpression.Contains(')'))
            {
                try
                {
                    var BracketExpressions = new List<string>();
                    var BracketList = new List<char>();
                    for (int i = 0; i < localexpression.Length; i++)
                    {
                        if (localexpression[i] == '(')
                        {
                            BracketList.Add('(');
                        }
                        if (localexpression[i] == ')')
                        {
                            BracketList.Add(')');
                        }
                    }
                    var ExternalSymbols = new List<char>();
                    bool helper = true;
                    while (helper)
                    {

                        int IndexOfFirstBracket = -1;
                        int IndexOfSecondBracket = -1;
                        int CounterOfBrackets = 0;
                        int counterOfSecondBracket = 0;
                        for (int i = 0; i < BracketList.Count; i++)
                        {
                            if (IndexOfFirstBracket == -1 && BracketList[i] == '(')
                            {
                                IndexOfFirstBracket = i;
                            }
                            if (BracketList.ElementAt(i) == '(')
                            {
                                CounterOfBrackets++;
                            }
                            if (BracketList.ElementAt(i) == ')')
                            {
                                counterOfSecondBracket++;
                                if (CounterOfBrackets == counterOfSecondBracket)
                                {
                                    IndexOfSecondBracket = i;
                                    break;
                                }
                            }
                        }
                        BracketList.RemoveRange(IndexOfFirstBracket, IndexOfSecondBracket - IndexOfFirstBracket + 1);
                        string InBracketExpression = "";
                        CounterOfBrackets = 0;
                        int IndexOfFirstBracketInExpression = 0;
                        int IndexOfSecondBracketInExpression = 0;
                        for (int i = 0; i < localexpression.Length; i++)
                        {

                            if (localexpression[i] == '(')
                            {
                                if (CounterOfBrackets == IndexOfFirstBracket)
                                {
                                    IndexOfFirstBracketInExpression = i;
                                }
                                CounterOfBrackets++;
                            }
                            if (localexpression[i] == ')')
                            {

                                if (CounterOfBrackets == IndexOfSecondBracket)
                                {
                                    IndexOfSecondBracketInExpression = i;
                                    break;
                                }
                                CounterOfBrackets++;
                            }
                        }
                        for (int i = 0; i < localexpression.Length; i++)
                        {
                            if (i > IndexOfFirstBracketInExpression && i < IndexOfSecondBracketInExpression)
                            {
                                InBracketExpression += localexpression[i];
                            }
                        }
                        BracketExpressions.Add(InBracketExpression);
                        localexpression = localexpression.Substring(IndexOfSecondBracketInExpression + 1);
                        if (BracketList.Count == 0)
                        {
                            break;
                        }
                    }
                    string ExternalExpression = expression;
                    foreach (var bracketExpression in BracketExpressions)
                    {
                        if (ExternalExpression.IndexOf(bracketExpression) >= 1)
                        {
                            ExternalExpression = ExternalExpression.Remove(ExternalExpression.IndexOf("(" + bracketExpression + ")"), bracketExpression.Length + 2);
                        }
                        else
                        {
                            ExternalExpression = ExternalExpression.Remove(ExternalExpression.IndexOf("(" + bracketExpression + ")"), bracketExpression.Length + 2);
                        }

                    }
                    var SeparatedExternalExpression = ExternalExpression.Split(new char[] { '+', '-', '*', '/' });
                    for (int i = 0; i < ExternalExpression.Length; i++)
                    {
                        if (ExternalExpression[i] == '+')
                        {
                            ExternalSymbols.Add('+');
                        }
                        if (ExternalExpression[i] == '*')
                        {
                            ExternalSymbols.Add('*');
                        }
                        if (ExternalExpression[i] == '/')
                        {
                            ExternalSymbols.Add('/');
                        }
                        if (ExternalExpression[i] == '-')
                        {
                            ExternalSymbols.Add('-');
                        }
                    }
                    if (ExternalSymbols.Count == 1 && ExternalSymbols[0] == '-' && BracketExpressions.Count == 1)
                    {
                        return new double[] { -1 * Arithmetic(BracketExpressions[0])[0], 0 };
                    }
                    int indexOfExtSymbol = 0;
                    int indexofBracketExpression = 0;
                    int indexOfExternalExpression = 0;
                    string FinalExpression = "";
                    if (SeparatedExternalExpression[0] == "")
                    {
                        FinalExpression += Arithmetic(BracketExpressions.ElementAt(indexofBracketExpression++))[0].ToString();
                        if (Arithmetic(BracketExpressions.ElementAt(indexofBracketExpression++))[1] == 1)
                        {
                            return new double[] { 0, 1 };
                        }
                    }
                    else
                    {
                        FinalExpression += Convert.ToDouble(SeparatedExternalExpression.ElementAt(indexOfExternalExpression++)).ToString();
                    }
                    for (int i = 1; i < SeparatedExternalExpression.Length; i++)
                    {

                        if (SeparatedExternalExpression[indexOfExternalExpression] == "")
                        {
                            if (indexOfExtSymbol <= ExternalSymbols.Count - 1 && ExternalSymbols.ElementAt(indexOfExtSymbol) == '+')
                            {
                                FinalExpression += "+" + Arithmetic(BracketExpressions.ElementAt(indexofBracketExpression))[0].ToString();
                                if (Arithmetic(BracketExpressions.ElementAt(indexofBracketExpression))[1] == 1)
                                {
                                    return new double[] { 0, 1 };
                                }
                                indexOfExtSymbol++;
                                indexofBracketExpression++;
                                indexOfExternalExpression++;
                                continue;
                            }
                            if (indexOfExtSymbol <= ExternalSymbols.Count - 1 && ExternalSymbols.ElementAt(indexOfExtSymbol) == '-')
                            {
                                FinalExpression += "-" + Arithmetic(BracketExpressions.ElementAt(indexofBracketExpression))[0].ToString();
                                if (Arithmetic(BracketExpressions.ElementAt(indexofBracketExpression))[1] == 1)
                                {
                                    return new double[] { 0, 1 };
                                }
                                indexOfExtSymbol++;
                                indexofBracketExpression++;
                                indexOfExternalExpression++;
                                continue;
                            }
                            if (indexOfExtSymbol <= ExternalSymbols.Count - 1 && ExternalSymbols.ElementAt(indexOfExtSymbol) == '*')
                            {
                                FinalExpression += "*" + Arithmetic(BracketExpressions.ElementAt(indexofBracketExpression))[0].ToString();
                                if (Arithmetic(BracketExpressions.ElementAt(indexofBracketExpression))[1] == 1)
                                {
                                    return new double[] { 0, 1 };
                                }
                                indexOfExtSymbol++;
                                indexofBracketExpression++;
                                indexOfExternalExpression++;
                                continue;
                            }
                            if (indexOfExtSymbol <= ExternalSymbols.Count - 1 && ExternalSymbols.ElementAt(indexOfExtSymbol) == '/')
                            {
                                FinalExpression += "/" + Arithmetic(BracketExpressions.ElementAt(indexofBracketExpression))[0].ToString();
                                if (Arithmetic(BracketExpressions.ElementAt(indexofBracketExpression))[1] == 1)
                                {
                                    return new double[] { 0, 1 };
                                }
                                indexOfExtSymbol++;
                                indexofBracketExpression++;
                                indexOfExternalExpression++;
                                continue;
                            }
                        }
                        else
                        {
                            if (indexOfExtSymbol <= ExternalSymbols.Count - 1 && ExternalSymbols.ElementAt(indexOfExtSymbol) == '+')
                            {
                                FinalExpression += "+" + Convert.ToDouble(SeparatedExternalExpression.ElementAt(indexOfExternalExpression)).ToString();
                                indexOfExternalExpression++;
                                indexOfExtSymbol++;
                                continue;
                            }
                            if (indexOfExtSymbol <= ExternalSymbols.Count - 1 && ExternalSymbols.ElementAt(indexOfExtSymbol) == '-')
                            {
                                FinalExpression += "-" + Convert.ToDouble(SeparatedExternalExpression.ElementAt(indexOfExternalExpression)).ToString();
                                indexOfExternalExpression++;
                                indexOfExtSymbol++;
                                continue;
                            }
                            if (indexOfExtSymbol <= ExternalSymbols.Count - 1 && ExternalSymbols.ElementAt(indexOfExtSymbol) == '*')
                            {
                                FinalExpression += "*" + Convert.ToDouble(SeparatedExternalExpression.ElementAt(indexOfExternalExpression)).ToString();
                                indexOfExternalExpression++;
                                indexOfExtSymbol++;
                                continue;
                            }
                            if (indexOfExtSymbol <= ExternalSymbols.Count - 1 && ExternalSymbols.ElementAt(indexOfExtSymbol) == '/')
                            {
                                FinalExpression += "/" + Convert.ToDouble(SeparatedExternalExpression.ElementAt(indexOfExternalExpression)).ToString();
                                indexOfExternalExpression++;
                                indexOfExtSymbol++;
                                continue;
                            }
                        }
                    }
                    mathexpression = Arithmetic(FinalExpression)[0];
                    if (Arithmetic(FinalExpression)[1] == 1)
                    {
                        return new double[] { 0, 1 };
                    }
                    return new double[] { mathexpression, 0 };
                }
                catch 
                {
                    return new double[] { 0, 1 };
                }
            }
            else
            {
                if (localexpression.Contains('*') || localexpression.Contains('/'))
                {
                    List<string> SeparatedExpressionOfFirstDegree = localexpression.Split(new char[] { '+', '-' }).ToList();
                    for (int i = 0; i < SeparatedExpressionOfFirstDegree.Count; i++)
                    {
                        if (SeparatedExpressionOfFirstDegree[i] == "")
                        {
                            SeparatedExpressionOfFirstDegree[i + 1] = "-" + SeparatedExpressionOfFirstDegree[i + 1];
                            SeparatedExpressionOfFirstDegree.RemoveAt(i);                           
                        }

                        if (SeparatedExpressionOfFirstDegree[i].Last() == '/' || SeparatedExpressionOfFirstDegree[i].Last() == '*')
                        {
                            SeparatedExpressionOfFirstDegree[i] = SeparatedExpressionOfFirstDegree[i] + "-" + SeparatedExpressionOfFirstDegree[i + 1];
                            SeparatedExpressionOfFirstDegree.RemoveAt(i + 1);
                        }
                        
                    }
                    var SymbolsOfFirstDegree = new List<char>();
                    
                    for (int i = 0; i < localexpression.Length; i++)
                    {
                        if (localexpression[i] == '+')
                        {
                            SymbolsOfFirstDegree.Add('+');
                        }
                        if (localexpression[i] == '-')
                        {
                            if (localexpression[i - 1] != '+' && 
                                localexpression[i - 1] != '-' &&
                                localexpression[i - 1] != '*' &&
                                localexpression[i - 1] != '/')
                            {
                                SymbolsOfFirstDegree.Add('-');
                            }                          
                        }
                    }

                    double[] AdditionalMathExpressions = new double[SeparatedExpressionOfFirstDegree.Count];
                    for (int i = 0; i < SeparatedExpressionOfFirstDegree.Count; i++)
                    {
                        var SeparatedExpressionOfSecondDegree = SeparatedExpressionOfFirstDegree[i].Split(new char[] { '*', '/' });
                        var SymbolsOfSecondDegree = new List<char>();
                        for (int j = 0; j < SeparatedExpressionOfFirstDegree[i].Length; j++)
                        {
                            if (SeparatedExpressionOfFirstDegree[i][j] == '*')
                            {
                                SymbolsOfSecondDegree.Add('*');
                            }
                            if (SeparatedExpressionOfFirstDegree[i][j] == '/')
                            {
                                SymbolsOfSecondDegree.Add('/');
                            }
                        }
                        double[] MathExpressionOfSecondDegree = new double[SeparatedExpressionOfSecondDegree.Length];
                        for (int j = 0; j < SeparatedExpressionOfSecondDegree.Length; j++)
                        {
                            MathExpressionOfSecondDegree[j] = Convert.ToDouble(SeparatedExpressionOfSecondDegree[j]);
                        }
                        AdditionalMathExpressions[i] += MathExpressionOfSecondDegree[0];
                        for (int j = 0; j < SymbolsOfSecondDegree.Count; j++)
                        {
                            if (SymbolsOfSecondDegree.ElementAt(j) == '*')
                            {
                                AdditionalMathExpressions[i] *= MathExpressionOfSecondDegree[j + 1];
                            }
                            if (SymbolsOfSecondDegree.ElementAt(j) == '/')
                            {
                                AdditionalMathExpressions[i] /= MathExpressionOfSecondDegree[j + 1];
                            }
                        }
                    }
                    mathexpression += AdditionalMathExpressions[0];
                    if (AdditionalMathExpressions.Length > 1)
                    {
                        for (int i = 0; i < SymbolsOfFirstDegree.Count; i++)
                        {
                            if (SymbolsOfFirstDegree.ElementAt(i) == '+')
                            {
                                mathexpression += AdditionalMathExpressions[i + 1];
                            }
                            if (SymbolsOfFirstDegree.ElementAt(i) == '-')
                            {
                                mathexpression -= AdditionalMathExpressions[i + 1];
                            }
                        }
                    }
                }
                else
                {
                    if (localexpression.Contains('+') || localexpression.Contains('-'))
                    {
                       List<string> SeparatedExpressionOfFirstDegree = localexpression.Split(new char[] { '+', '-' }).ToList();
                        var SymbolsOfFirstDegree = new List<char>();
                        for (int i = 0; i < SeparatedExpressionOfFirstDegree.Count; i++)
                        {
                            if (SeparatedExpressionOfFirstDegree[i] == "")
                            {
                                SeparatedExpressionOfFirstDegree[i + 1] = "-" + SeparatedExpressionOfFirstDegree[i + 1];
                                SeparatedExpressionOfFirstDegree.RemoveAt(i);                               
                            }
                        }
                        if (SeparatedExpressionOfFirstDegree.Count == 1)
                        {
                            return new double[] { double.Parse(SeparatedExpressionOfFirstDegree[0]), 0 };
                        }
                        for (int i = 0; i < localexpression.Length - 1; i++)
                        {
                            if (localexpression[i] == '+')
                            {
                                SymbolsOfFirstDegree.Add('+');
                            }
                            if (localexpression[i] == '-')
                            {
                                if (localexpression[i - 1] != '+' &&
                                localexpression[i - 1] != '-' &&
                                localexpression[i - 1] != '*' &&
                                localexpression[i - 1] != '/')
                                {
                                    SymbolsOfFirstDegree.Add('-');
                                }
                            }
                        }

                        double[] MathExpressionOfFirstDegree = new double[SeparatedExpressionOfFirstDegree.Count];
                        for (int i = 0; i < SeparatedExpressionOfFirstDegree.Count; i++)
                        {
                            MathExpressionOfFirstDegree[i] = Convert.ToDouble(SeparatedExpressionOfFirstDegree[i]);
                        }
                        mathexpression += MathExpressionOfFirstDegree[0];
                        for (int i = 0; i < SymbolsOfFirstDegree.Count; i++)
                        {
                            if (SymbolsOfFirstDegree.ElementAt(i) == '+')
                            {
                                mathexpression += MathExpressionOfFirstDegree[i + 1];
                            }
                            if (SymbolsOfFirstDegree.ElementAt(i) == '-')
                            {
                                mathexpression -= MathExpressionOfFirstDegree[i + 1];
                            }
                        }
                    }
                    else
                    {
                        return new double[] { double.Parse(expression), 0 };
                    }
                }
                
            }
            return new double[] { mathexpression, 0 };
        }

        
    }
}
