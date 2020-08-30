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
    public struct CreateCommand : IRequest<Models.Category>
    {
        public Models.Category Category { get; set; }
    }

    public class CreateCommandHandler : IRequestHandler<CreateCommand, Models.Category>
    {
        private readonly ApplicationDbContext _db;

        public CreateCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Models.Category> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            await _db.Categories.AddAsync(request.Category, cancellationToken);
            await _db.SaveChangesAsync(cancellationToken);
            return request.Category;
        }
    }
}    