using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class BlogPost:Entity<Guid>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid MemberId { get; set; }
    public virtual Member Member { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }

    //Bir blog yazısının birden çok yorumu olabilir.
}
