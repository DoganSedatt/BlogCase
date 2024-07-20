using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Comment:Entity<Guid>
{
    public string Commenter { get; set; }
    public string Content { get; set; }

    public Guid MemberId { get; set; }
    public Guid BlogPostId { get; set; }

    // Bir yorumun bir blog yazısı vardır
    public virtual BlogPost BlogPost{ get; set; }
    
    //Bir yorumun bir yazarı(member) vardır
    public virtual Member Member { get; set; }

}
