using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public enum CommunicationExceptionType { CannotLoadFile, CannotReadLine };

    class CommunicationException : Exception
    {

        public CommunicationExceptionType Fail { get; private set; }

        public CommunicationException(CommunicationExceptionType fail) : base()
        {
            this.Fail = fail;
        }
    }
}
