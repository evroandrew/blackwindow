using System.Collections.Generic;

namespace WindowsFormsApp2
{
    delegate void UnlockDelegate();

    internal class MyLock
    {
        char[] codeArr;
        List<char> input_stream = new List<char>();
        public MyLock(string code)
        {
            codeArr = code.ToCharArray();
        }
        public event UnlockDelegate Unlock;
        public int Check(char code)
        {
            int check = 0;
            input_stream.Add(code);
            if (input_stream.Count > codeArr.Length)
                input_stream.RemoveAt(0);
            if (input_stream.Count == codeArr.Length)
            {
                for (int i = input_stream.Count - 1; i > 0; i--)
                {
                    if (input_stream[i] != codeArr[i])
                    {
                        check = -1;
                        break;
                    }
                }
                if (check == 0)
                    Unlock?.Invoke();
            }
            return check;
        }
    }
}