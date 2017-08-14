using Hangfire;
using Stone.Application.Interfaces;
using System;
using System.Linq.Expressions;

namespace Stone.Schedulers
{
    public class HangfireScheduler : IJobRecurring
    {
        private IJobApplicationService _job;
        public HangfireScheduler(IJobApplicationService job)
        {
            _job = job;
        }

        public void Enqueue(Expression<Action> action, int interval)
        {
            RecurringJob.AddOrUpdate(action, Cron.MinuteInterval(interval));
        }
    }
}
