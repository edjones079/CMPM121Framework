using UnityEngine;
using System.Collections.Generic;

public class RPNEvaluator
{

    Stack<int> stack = new Stack<int>();

    public RPNEvaluator()
    {

    }

    public int Eval(string expression, Dictionary<string, int> variables)
    {
        string[] tokens = expression.Split(' ');
        int final_result;

        foreach (string token in tokens)
        {
            int myInt;
            int value1;
            int value2;
            int result;

            if (int.TryParse(token, out myInt))
                stack.Push(myInt);

            if (variables.ContainsKey(token))
                stack.Push(variables[token]);

            switch (token)
            {
                case "%":
                    value2 = stack.Pop();
                    value1 = stack.Pop();

                    result = value1 % value2;
                    stack.Push(result);
                    break;

                case "*":
                    value2 = stack.Pop();
                    value1 = stack.Pop();
                    result = value1 * value2;
                    stack.Push(result);
                    break;

                case "/":
                    value2 = stack.Pop();
                    value1 = stack.Pop();
                    result = value1 / value2;
                    stack.Push(result);
                    break;

                case "+":
                    value2 = stack.Pop();
                    value1 = stack.Pop();
                    result = value1 + value2;
                    stack.Push(result);
                    break;

                case "-":
                    value2 = stack.Pop();
                    value1 = stack.Pop();
                    result = value1 - value2;
                    stack.Push(result);
                    break;

            }
        }

        final_result = stack.Pop();
        return final_result;
    }
}