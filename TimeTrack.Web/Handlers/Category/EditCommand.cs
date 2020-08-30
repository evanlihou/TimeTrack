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
    public struct EditCommand : IRequest<Models.Category?>
    {
        public Models.Category Category { get; set; }
    }

    public class EditCommandHandler : IRequestHandler<EditCommand, Models.Category?>
    {
        private readonly ApplicationDbContext _db;

        public EditCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Models.Category?> Handle(EditCommand request, CancellationToken cancellationToken)
        {
            var modelFromForm = request.Category;
            var modelFromDb = await _db.Categories.SingleOrDefaultAsync(
                c => c.Id == modelFromForm.Id && c.UserId == modelFromForm.UserId, cancellationToken);
            if (modelFromDb == null)
                return null;

            modelFromDb.Name = modelFromForm.Name;
            modelFromDb.Description = modelFromForm.Description;
            modelFromDb.BillableRate = modelFromForm.BillableRate;

            await _db.SaveChangesAsync(cancellationToken);
            return modelFromDb;
        }
    }
}    