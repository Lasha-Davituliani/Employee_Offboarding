using Employee_Offboarding.Application.Abstractions.Services;
using Employee_Offboarding.Application.DTOs.Forms;
using Employee_Offboarding.Application.Templates;
using Employee_Offboarding.Domain.Entities;
using Employee_Offboarding.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Employee_Offboarding.Infrastructure.Services
{
    public sealed class ClaranceTamplateService : IClearanceTemplateService
    {
        private readonly AppDbContext _context;
        private readonly ITamplateService _tamplate;

        public ClaranceTamplateService(AppDbContext context, ITamplateService tamplate)
        {
            _context = context;
            _tamplate = tamplate;
        }

        public async Task<int> CreateFromTemplateAsync(int employeeId, int initiatedByUserId, bool isServiceCenter, int? serviceCenterId = null, CancellationToken ct = default)
        {
            var form = new ClearenceForm
            {
                EmployeeId = employeeId,
                InitiatedByUserId = initiatedByUserId,
                ServiceCenterId = serviceCenterId,
                Status = ClarenceStatus.InReview,
                CreatedAt = DateTime.Now

            };

            _context.ClearanceForms.Add(form);
            await _context.SaveChangesAsync(ct);

            var deptIds = isServiceCenter
                ? ClearanceFormTemplates.ServiceCenterDepartmentIds
                : ClearanceFormTemplates.HqDepartmentIds;
            var uniqueDeptIds = deptIds.Distinct().ToArray();
            var cfDeps = uniqueDeptIds.Select(d => new ClearenceFormDepartment
            {
                ClearenceFormId = form.Id,
                DepartmentId = d
            }).ToList();

            _context.ClearenceFormDepartments.AddRange(cfDeps);
            await _context.SaveChangesAsync(ct);

            var items = await _context.Items
                .Where(i => uniqueDeptIds.Contains(i.DepartmentId) && i.IsActive)
                .ToListAsync(ct);

            var now = DateTime.UtcNow;
            var cfItems = items.Select(it => new ClearenceFormItem
            {
                ClearanceFormId = form.Id,
                ItemId = it.Id,
                Status = ClearanceFormItemStatus.InReview,
                UpdatedAt = now
            }).ToList();

            _context.ClearenceFormItems.AddRange(cfItems);

            await _context.SaveChangesAsync(ct);
            return form.Id;


        }

        public async Task<FormPreviewDto> GetPreviewAsync(bool isServiceCenter, CancellationToken ct = default)
        {
           var deptIds = isServiceCenter 
                ? ClearanceFormTemplates.ServiceCenterDepartmentIds
                : ClearanceFormTemplates.HqDepartmentIds;
            var depts = await _context.Departments
                .Where(d => deptIds.Contains(d.Id))
                .Select(d => new {
                    d.Id,
                    d.Name
                })
                .ToListAsync(ct);

            var result = new List<DepartmentPreviewDto>(depts.Count);

            foreach (var d in depts)
            {
                var items = await _tamplate.GetItemsForDepartmentAsync(d.Id, ct);
                result.Add(new DepartmentPreviewDto(
                    d.Id,
                    d.Name,
                    items.Select(i => i.Name).ToList()
                    ));
            }
            return new FormPreviewDto(result);
        }
    }
}
