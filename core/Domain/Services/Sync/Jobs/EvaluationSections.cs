using System.Threading.Tasks;

namespace cm.frontend.core.Domain.Services.Sync.Jobs
{
    public class EvaluationSections : Base.Core<Domain.Models.EvaluationSection>
    {
        public EvaluationSections() : base(nameof(EvaluationSections))
        {

        }

        public override async Task RebuildModel(int localId)
        {
            var evaluationSectionsRealm = new Services.Realms.EvaluationSections();
            await evaluationSectionsRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                var evaluationsRealm = new Services.Realms.Evaluations();
                var evaluationId = item.EvaluationId;
                var evaluationItem = evaluationsRealm.Get(x => x.Id == evaluationId);
                item.Evaluation = evaluationItem;
            });
        }

        public override async Task UpdateModel(int localId)
        {
            var evaluationSectionsRealm = new Services.Realms.EvaluationSections();
            await evaluationSectionsRealm.WriteAsync(tempRealm =>
            {
                var item = tempRealm.Get(localId);

                if (item.Evaluation != null)
                {
                    item.EvaluationId = item.Evaluation.Id;
                }
            });
        }
    }
}
