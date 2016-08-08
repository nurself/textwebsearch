using System;

namespace websearch.Models
{
    class ProgramException : Exception
    {

        private string _strExtraInfo;

        public ProgramException(string strExtraInfo) : base()
        {
            _strExtraInfo = strExtraInfo;
        }

        public ProgramException(string message, string strExtraInfo) : base(message)
        {
            _strExtraInfo = strExtraInfo;
        }

        public ProgramException(string message, Exception e, string strExtraInfo)
            : base(message, e)
        {
            _strExtraInfo = strExtraInfo;
        }

        public string ExtraErrorInfo
        {
            get { return _strExtraInfo; }

            set { _strExtraInfo = value; }
        }
    }
}
