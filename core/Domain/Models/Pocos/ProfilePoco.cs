using System;
using cm.frontend.core.Domain.Interfaces;
using PropertyChanged;

namespace cm.frontend.core.Domain.Models.Pocos
{
    [ImplementPropertyChanged]
    public class ProfilePoco : ISyncableEntity
    {
        public int Id { get; set; }
        
        public int LocalId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string FullName => FirstName + " " + LastName;

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
        
        public DateTimeOffset StartDate { get; set; }

        public string Level { get; set; }
        
        public bool Synced { get; set; }
    }
}
