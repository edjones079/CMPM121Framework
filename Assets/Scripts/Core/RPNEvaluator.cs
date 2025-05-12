using UnityEngine;
using System.Collections.Generic;

public class RPNEvaluator
{

    Stack<int> stack = new Stack<int>();
    Stack<float> floatStack = new Stack<float>();

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

    public float EvalFloat(string expression, Dictionary<string, int> variables)
    {
        string[] tokens = expression.Split(' ');
        float final_result;

        foreach (string token in tokens)
        {
            float myFloat;
            float value1;
            float value2;
            float result;

            if (float.TryParse(token, out myFloat))
                floatStack.Push(myFloat);

            if (variables.ContainsKey(token))
                floatStack.Push(variables[token]);

            switch (token)
            {
                case "%":
                    value2 = floatStack.Pop();
                    value1 = floatStack.Pop();

                    result = value1 % value2;
                    floatStack.Push(result);
                    break;

                case "*":
                    value2 = floatStack.Pop();
                    value1 = floatStack.Pop();
                    result = value1 * value2;
                    floatStack.Push(result);
                    break;

                case "/":
                    value2 = floatStack.Pop();
                    value1 = floatStack.Pop();
                    result = value1 / value2;
                    floatStack.Push(result);
                    break;

                case "+":
                    value2 = floatStack.Pop();
                    value1 = floatStack.Pop();
                    result = value1 + value2;
                    floatStack.Push(result);
                    break;

                case "-":
                    value2 = floatStack.Pop();
                    value1 = floatStack.Pop();
                    result = value1 - value2;
                    floatStack.Push(result);
                    break;

            }
        }

        final_result = floatStack.Pop();
        return final_result;
    }
}