//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ConnectR.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Message
    {
        public int MessageId { get; set; }
        public int ConversationId { get; set; }
        public string SenderName { get; set; }
        public System.DateTime Date { get; set; }
        public string Text { get; set; }
    
        public virtual Conversation Conversation { get; set; }
    }
}
