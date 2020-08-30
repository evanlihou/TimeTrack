using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TimeTrack.Web.Data;

namespace TimeTrack.Web.Handlers.Category
{
    public struct ListQuery : IRequest<List<Models.Category>>
    {
        public string UserId { get; set; }
    }

    public class ListQueryHandler : IRequestHandler<ListQuery, List<Models.Category>>
    {
        private readonly ApplicationDbContext _db;

        public ListQueryHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        public Task<List<Models.Category>> Handle(ListQuery request, CancellationToken cancellationToken)
        {
            return _db.Categories.Where(c => c.UserId == request.UserId).ToListAsync(cancellationToken);
        }
    }
}    