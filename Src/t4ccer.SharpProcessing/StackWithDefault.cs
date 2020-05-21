using System;
using System.Collections.Generic;
using System.Text;

namespace t4ccer.SharpProcessing
{
    internal class StackWithDefault<T> : Stack<T>
    {
        T defValue;

        public StackWithDefault(T defValue)
        {
            this.defValue = defValue;
        }

        public new T Peek()
        {
            if (Count > 0)
                return base.Peek();
            return defValue;
        }
        public new T Pop()
        {
            if (Count > 0)
                return base.Pop();
            return defValue;
        }
        public void SwapTop(T val)
        {
            Pop();
            Push(val);
        }
    }
}
