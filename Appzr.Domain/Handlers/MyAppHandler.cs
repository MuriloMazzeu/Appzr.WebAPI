using Appzr.Domain.Commands;
using Appzr.Domain.Helpers;
using Appzr.Domain.Queries;
using Appzr.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Appzr.Domain.Handlers
{
    public class MyAppHandler
    {
        public MyAppHandler(CqrsHelper cqrsHelper)
        {
            CqrsHelper = cqrsHelper;
        }

        private CqrsHelper CqrsHelper { get; }

        //Chamado pela api...
        public async Task Handle(AddMyAppCommand command)
        {
            await CqrsHelper.RunAsync(command);
        }

        //Chamado pelo bus...
        public async Task Handle(SynchronizeMyAppCommand command)
        {
            await CqrsHelper.RunAsync(command);
        }

        //Chamado pela api...
        public async Task<IEnumerable<MyAppVM>> Handle(ListMyAppsQuery query)
        {
            return await CqrsHelper.RunAsync<ListMyAppsQuery, IEnumerable<MyAppVM>>(query);
        }
    }
}
