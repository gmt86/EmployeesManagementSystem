using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesManagement.Data;
using EmployeesManagement.Models;

namespace EmployeesManagement.Controllers
{
    public class LeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            var awaitingapproval = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovedStatus" && y.Code == "AwaitingApproval").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employe)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                 .Where(l => l.StatusId == awaitingapproval!.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employe)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // GET: LeaveApplications/Create
        public IActionResult Create()
        {
            //ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description");
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name");
            ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]       
        public async Task<IActionResult> Create(LeaveApplication leaveApplication)
        {
            //var pendingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Pending" && y.SystemCode.Code == "LeaveApprovedStatus").FirstOrDefaultAsync();
            var pendingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "AwaitingApproval" && y.SystemCode.Code == "LeaveApprovedStatus").FirstOrDefault();

            //if (ModelState.IsValid)
            //{
            leaveApplication.CreatedById = "0";
                leaveApplication.CreatedOn = DateTime.Now;            
            leaveApplication.StatusId = pendingStatus.Id;
            //leaveApplication.StatusId = 7;
           _context.Add(leaveApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovedStatus"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "FullName", leaveApplication.EmployeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var pendingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "awaitingapproval" && y.SystemCode.Code == "LeaveApprovedStatus").FirstOrDefault();
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication == null)
            {
                return NotFound();
            }
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovedStatus"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "FullName", leaveApplication.EmployeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            //ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return View(leaveApplication);
        }
              
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Bind("Id,EmployeId,NoOfDays,StartDate,EndDate,DurationId,LeaveTypeId,Attachement,Description,StatusId,ApprovedById,ApprovedOn,CreatedById,CreatedOn,ModifiedById,ModifiedOn")]
        public async Task<IActionResult> Edit(int id, LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.Id)
            {
                return NotFound();
            }
            var pendingStatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.Code == "Pending" && y.SystemCode.Code == "LeaveApprovedStatus").FirstOrDefaultAsync();
            //if (ModelState.IsValid)
            //{
                try
                {
                    leaveApplication.ModifiedById = "0";
                    leaveApplication.ModifiedOn = DateTime.Now;
                    //leaveApplication.StatusId = pendingStatus.Id;
                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovedStatus"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "FullName", leaveApplication.EmployeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            //ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var leaveApplication = await _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employe)
                .Include(l => l.LeaveType)
                //.Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.LeaveApplications.FindAsync(id);
            if (leaveApplication != null)
            {
                _context.LeaveApplications.Remove(leaveApplication);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(int id)
        {
            return _context.LeaveApplications.Any(e => e.Id == id);
        }

        #region Approve Leave

        public async Task<IActionResult> ApproveLeaveApplication()
        {
            var approvestatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovedStatus" && y.Code == "Approval").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employe)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == approvestatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }              

        [HttpGet]
        public async Task<IActionResult> ApproveLeave(int? id)
        {
            var leaveApplication = await _context.LeaveApplications
               .Include(l => l.Duration)
               .Include(l => l.Employe)
               .Include(l => l.LeaveType)
               .Include(l => l.Status)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "FullName", leaveApplication.EmployeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            //ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return View(leaveApplication);
        }

        [HttpPost]       
        public async Task<IActionResult> ApproveLeave(LeaveApplication leave)
        {            
            var approvestatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovedStatus" && y.Code == "Approval").FirstOrDefault();

            var leaveApplication = await _context.LeaveApplications
              .Include(l => l.Duration)
              .Include(l => l.Employe)
              .Include(l => l.LeaveType)
              .Include(l => l.Status)
              .FirstOrDefaultAsync(m => m.Id == leave.Id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.ApprovedById = "0";
            leaveApplication.StatusId = approvestatus.Id;
            leaveApplication.ApprovalNote= leave.ApprovalNote;
            _context.Update(leaveApplication);
            await _context.SaveChangesAsync();


            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovedStatus" ), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "FullName", leaveApplication.EmployeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            //ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return RedirectToAction(nameof(Index));
        }


        #endregion

        #region Reject Leave

        public async Task<IActionResult> RejectLeaveApplication()
        {
            var rejectedstatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovedStatus" && y.Code == "Rejected").FirstOrDefault();

            var applicationDbContext = _context.LeaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employe)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == rejectedstatus!.Id);
            return View(await applicationDbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> RejectLeave(int? id)
        {
            var leaveApplication = await _context.LeaveApplications
               .Include(l => l.Duration)
               .Include(l => l.Employe)
               .Include(l => l.LeaveType)
               .Include(l => l.Status)
               .FirstOrDefaultAsync(m => m.Id == id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveDuration"), "Id", "Description");
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "FullName", leaveApplication.EmployeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            //ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return View(leaveApplication);
        }

        [HttpPost]
        public async Task<IActionResult> RejectLeave(LeaveApplication leave)
        {
            var rejectedstatus = _context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovedStatus" && y.Code == "Rejected").FirstOrDefault();

            var leaveApplication = await _context.LeaveApplications
              .Include(l => l.Duration)
              .Include(l => l.Employe)
              .Include(l => l.LeaveType)
              .Include(l => l.Status)
              .FirstOrDefaultAsync(m => m.Id == leave.Id);
            if (leaveApplication == null)
            {
                return NotFound();
            }

            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.ApprovedById = "0";
            leaveApplication.StatusId = rejectedstatus.Id;
            leaveApplication.ApprovalNote = leave.ApprovalNote;
            _context.Update(leaveApplication);
            await _context.SaveChangesAsync();


            ViewData["DurationId"] = new SelectList(_context.SystemCodeDetails.Include(x => x.SystemCode).Where(y => y.SystemCode.Code == "LeaveApprovedStatus"), "Id", "Description", leaveApplication.DurationId);
            ViewData["EmployeId"] = new SelectList(_context.Employes, "Id", "FullName", leaveApplication.EmployeId);
            ViewData["LeaveTypeId"] = new SelectList(_context.LeaveTypes, "Id", "Name", leaveApplication.LeaveTypeId);
            //ViewData["StatusId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveApplication.StatusId);
            return RedirectToAction(nameof(Index));
        }


        #endregion
    }
}
