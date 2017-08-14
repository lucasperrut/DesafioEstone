using System;
using System.Linq.Expressions;

namespace Stone.Schedulers
{
    public interface IJobRecurring
    {
        void Enqueue(Expression<Action> action, int interval);
    }
}
