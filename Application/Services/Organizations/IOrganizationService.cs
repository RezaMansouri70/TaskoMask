﻿using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaskoMask.Application.Services.Organizations.Dto;

namespace TaskoMask.Application.Services.Organizations
{
    public interface IOrganizationService
    {
        Task<Result> CreateAsync(OrganizationInput input);
        Task<IEnumerable<OrganizationOutput>> GetListByUserIdAsync(string userId);
        Task<long> CountAsync();
        Task<OrganizationOutput> GetByIdAsync(string id);
        Task<Result> UpdateAsync(OrganizationInput input);
    }
}
