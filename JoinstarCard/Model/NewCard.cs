using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinstarCard.Model
{
    public class NewCard
    {
        public string CardNo { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsAgree { get; set; }
        public  byte[] MemberImage { get; set; }

    }
}
