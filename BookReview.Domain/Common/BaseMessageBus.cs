using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Domain.Common
{
    public class BaseMessageBus
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
