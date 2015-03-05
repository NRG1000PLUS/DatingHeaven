using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace DatingHeaven.Entities {
    [Table("Messages")]
    public class Message: BaseBusinessEntityWithId {

        public Message(){

            // the message is NOT read from beginning
            IsRead = false;
        }


        [Required]
        public int SenderId{
            get; 
            set; 
        }

        [Required]
        public int ReceiverId{
            get; 
            set; 
        }

        [Required]
        public bool IsRead{
            get; 
            set; 
        }

        [MaxLength(1000)]
        public string Header{
            get; 
            set; 
        }

        [MaxLength(8000)]
        public string Body{
            get; 
            set; 
        }
    }
}
