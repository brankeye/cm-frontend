using System.ComponentModel;
using cm.frontend.core.Domain.Attributes;
using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class EvaluationSection : RealmObject, ISyncableEntity, INotifyPropertyChanged
    {
        [Indexed]
        [IgnorePropertyMapping]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        [IgnorePropertyMapping]
        public int LocalId { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public float Score { get; set; }

        public int EvaluationId { get; set; }

        [IgnorePropertyMapping]
        public Evaluation Evaluation { get; set; }

        [Indexed]
        [IgnorePropertyMapping]
        public bool Synced { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
