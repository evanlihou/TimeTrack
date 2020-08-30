using System;
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
    public struct FetchQuery : IRequest<Models.Category>
    {
        public string UserId { get; set; }
        public Guid CategoryId { get; set; }
    }

    public class FetchQueryHandler : IRequestHandler<FetchQuery, Models.Category>
    {
        private readonly ApplicationDbContext _db;

        public FetchQueryHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Models.Category> Handle(FetchQuery request, CancellationToken cancellationToken)
        {
            return await _db.Categories.Include(c => c.User).Include(c => c.Parent)
                .FirstOrDefaultAsync(c => c.UserId == request.UserId && c.Id == request.CategoryId, cancellationToken);
        }
    }
}    