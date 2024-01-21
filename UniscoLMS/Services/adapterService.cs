using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using UniscoLMS.DataBaseModels;
using UniscoLMS.Enums;
using UniscoLMS.ViewModels.Responses;
using System.Diagnostics.Contracts;
using System.Text;
using System;
using ViewModels;
using Microsoft.EntityFrameworkCore;
using static System.Collections.Specialized.BitVector32;
using UniscoLMS;
using UniscoLMS.Errors;

namespace UniscoLMS.Services
{
    public class adapterService
    {
        private readonly IConfiguration _configuration;
        private readonly UniscoDbContext _UniscoDbContext;

        public adapterService(IConfiguration configuration, UniscoDbContext UniscoDbContext)
        {
            _UniscoDbContext = UniscoDbContext;
            _configuration = configuration;
        }

        public async Task<bool> InitializeOrder(string trn, int paymentmethodId, Guid userId, int CourseId)
        {
            try
            {
                bool result = false;
                if (userId == null)
                    throw new ErrorUserNotFound();
                var order = new PaymentOrder()
                {
                    CreatedAt = DateTime.Now,
                    PaymentMethodId = paymentmethodId,
                    UserId = userId,
                    Trn = trn,
                    StatusId = (int)Statuses.IN_PROGRESS,
                };
                order.UserCourses.Add(new UserCourse()
                {
                    LearnerId = userId,
                    CourseId = CourseId
                });
                await _UniscoDbContext.PaymentOrders.AddAsync(order);
                await  _UniscoDbContext.SaveChangesAsync();
                if (order.Id > 0)
                    result= true;
                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task<Course> OTPResponse(PaymobPaymentResponse request)
        {
            var paymentOrder = await _UniscoDbContext.PaymentOrders
                .Include(x=> x.Status)
                .Include(x => x.User)
                .Include(x => x.UserCourses).ThenInclude(x=>x.Course)
                .Include(x => x.CardPaymentTypeInfos)
                .ThenInclude(x => x.UserCard)
                .Where(p => p.Trn == request.trn && p.StatusId != (long)Statuses.SUCCESS)
                .OrderBy(o => o.CreatedAt)
                .FirstOrDefaultAsync();
            var Course = paymentOrder.UserCourses.FirstOrDefault();
            var mainUser = paymentOrder.UserId;
            request.payMethod = (int)paymentOrder.PaymentMethodId;
            paymentOrder.UpdatedAt = DateTime.Now;
            paymentOrder.StatusId = (int)request.status;
            if (request.status == (int)Statuses.SUCCESS)
            {
                paymentOrder.StatusId = (int)Statuses.SUCCESS;
             
            }
            else
            {
                paymentOrder.StatusId = (int)Statuses.DECLINED;
        
            }
            _UniscoDbContext.Update(paymentOrder);
            _UniscoDbContext.SaveChanges();
            return Course.Course;

        }
        public async Task<int> InitializeCourse(Guid expert, string userName, DateTime meetingDate, int duration, int tagId, string notes, int CourseType , decimal price)
        {
            var loggedUser = await _UniscoDbContext.Users.FirstOrDefaultAsync(x => x.Username == userName);
            if (loggedUser == null)
                throw new ErrorUserNotFound();

            var Course = new Course()
            {
                ExpertUser = expert,
                Duration = duration,
                TagId = tagId,
                Notes = notes,
                SessinType = CourseType,
                Price = price
            };
           
            await _UniscoDbContext.Courses.AddAsync(Course);
            await _UniscoDbContext.SaveChangesAsync();
            return Course.Id;

        }

       
        public async Task<Course> GetCourseDetails(int CourseId)
        {
            var Course = await _UniscoDbContext.Courses.Include(x=>x.Tag).Where(x => x.Id == CourseId).FirstOrDefaultAsync();
            return Course;
        }
               
        public async Task<List<UserCourse>> GetCourseUsers(int CourseId)
        {
            var CourseUsers = await _UniscoDbContext.UserCourses
                .Where(x => x.CourseId == CourseId && x.Transaction != null && x.Transaction.StatusId == (int)Statuses.SUCCESS).ToListAsync();
            return CourseUsers;
        }
        public async Task<User> GetUser(string userName)
        {
            var user = await _UniscoDbContext.Users.FirstOrDefaultAsync(x => x.Username == userName);
            return user;
        }
        public async Task<Course> GetCourse(int CourseId)
        {
            var Course = await _UniscoDbContext.Courses.FirstOrDefaultAsync(x =>x.Id == CourseId);
            return Course;
        }
    }
}
