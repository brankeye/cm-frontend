using cm.frontend.core.Domain.Interfaces;
using Realms;

namespace cm.frontend.core.Domain.Models
{
    public class EvaluationSection : RealmObject, IEntity
    {
        [Indexed]
        public int Id { get; set; }

        [PrimaryKey]
        [Indexed]
        public int LocalId { get; set; }

        public string Name { get; set; }

        public string Body { get; set; }

        public float Score { get; set; }

        public int EvaluationId { get; set; }

        public Evaluation Evaluation { get; set; }
    }
}
