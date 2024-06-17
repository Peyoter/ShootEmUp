using System;
using System.Collections.Generic;

namespace Enemy.Agents
{
    public class CompositeCondition
    {
        private readonly List<Func<bool>> _conditions = new();
        
        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        public bool IsTrue()
        {
            return _conditions.Find(condition => !condition.Invoke()) != null;
        }

    }
}