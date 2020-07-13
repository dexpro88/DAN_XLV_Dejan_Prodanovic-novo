using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XVL_Dejan_Prodanovic
{
    class EventClass
    {
        public delegate void ActionPerformedEventHandler(object source, TextToWriteEventArgs args);
        public event ActionPerformedEventHandler ActionPerformed;

        public void OnActionPerformed(string textToWrite)
        {
            if (ActionPerformed != null)
            {
                ActionPerformed(this, new TextToWriteEventArgs() { TextToWrite = textToWrite });
            }
        }
    }
}
