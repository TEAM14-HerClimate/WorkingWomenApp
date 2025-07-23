
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Microsoft.ApplicationInsights;
using Microsoft.Extensions.Options;

using WorkingWomenApp.Database.Core.CustomEntities;
using WorkingWomenApp.Database.Models.Users;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.BLL.UnitOfWork;

namespace WorkingWomenApp.BLL.Implementation
{
    public class UserService: IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly PaginationOptions _paginationOptions; 
        public UserService(IUnitOfWork unitOfWork, IOptions<PaginationOptions>options)
        {
            _unitOfWork = unitOfWork;
            _paginationOptions = options.Value;
        }

   
    }
}
