using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookReview.Application.Exceptions
{
    public class NotFoundException : ApplicationException   
    {
        public NotFoundException(string message, object key) : base($"{key}: {message}")
        {

        }
    }
}
