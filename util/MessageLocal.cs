using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace haisan.util
{
    class MessageLocal
    {
        private bool isSucess;
        private string message;

        public MessageLocal()
        {
            isSucess = false;
            message = "";
        }

        public bool IsSucess
        {
            get { return isSucess; }
            set { isSucess = value; }
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }

    }
}
