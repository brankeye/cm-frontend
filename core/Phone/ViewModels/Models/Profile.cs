using System;
using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using PropertyChanged;
using Realms;

namespace cm.frontend.core.Phone.ViewModels.Models
{
    [ImplementPropertyChanged]
    public class ProfileModel : ISyncableEntity
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
