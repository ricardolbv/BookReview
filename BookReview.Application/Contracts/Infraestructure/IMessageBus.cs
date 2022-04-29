using BookReview.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Contracts.Infraestructure
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessageBus message, string topicName);
    }
}
