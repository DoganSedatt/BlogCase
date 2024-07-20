using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Member:Entity<Guid>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
    public virtual ICollection<BlogPost> BlogPosts { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }

}
