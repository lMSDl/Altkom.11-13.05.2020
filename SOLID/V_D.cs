using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    //class Mail
    //{
    //    public string Address { get; set; }
    //    public string Subject { get; set; }
    //    public string Content { get; set; }

    //    public void SendMail()
    //    {

    //    }
    //}

    //class SMS
    //{
    //    public string Number { get; set; }
    //    public string Content { get; set; }

    //    public void SendSMS()
    //    {

    //    }
    //}

    //class MMS
    //{
    //    public string Number { get; set; }
    //    public byte[] Content { get; set; }

    //    public void SendMMS()
    //    {

    //    }
    //}

    //class Messenger
    //{
    //    public SMS SMS { get; set; }
    //    public MMS MMS { get; set; }
    //    public Mail Mail { get; set; }

    //    public void SendMessage()
    //    {
    //        SMS?.SendSMS();
    //        MMS?.SendMMS();
    //        Mail?.SendMail();
    //    }
    //}

        interface IMessage
    {
        void Send();
    }

    class Mail : IMessage
    {
        public string Address { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }

        public void Send()
        {

        }
    }

    class SMS : IMessage
    {
        public string Number { get; set; }
        public string Content { get; set; }

        public void Send()
        {

        }
    }

    class MMS : IMessage
    {
        public string Number { get; set; }
        public byte[] Content { get; set; }

        public void Send()
        {

        }
    }

    class Messenger
    {
        public ICollection<IMessage> messages { get; set; }

        public void SendMessage()
        {
            messages?.ToList().ForEach(x => x.Send());
        }
    }
}
