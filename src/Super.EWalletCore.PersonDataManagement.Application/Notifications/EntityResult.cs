using Flunt.Notifications;
using System.Collections.Generic;

namespace Super.EWalletCore.PersonDataManagement.Application.Notifications
{
    public class EntityResult<T> : Result where T : class
    {
        public EntityResult()
        : base(new List<Notification>())
        {}

        public EntityResult( T entity)
         : base(new List<Notification>())
        {
            Entity = entity;
        }

        public EntityResult(IReadOnlyCollection<Notification> notifications, T entity) 
            : base(notifications)
        {
            Entity = entity;
        }

        public EntityResult(IReadOnlyCollection<Notification> notifications, ErrorCode errorCode)
          : base(notifications)
        {
            Entity = null;
            Error = errorCode;
        }

        public EntityResult(string errorMessage, ErrorCode errorCode)
        : base(errorMessage)
        {
            Entity = null;
            Error = errorCode;
        }

        public T Entity { get; }
    }
}
