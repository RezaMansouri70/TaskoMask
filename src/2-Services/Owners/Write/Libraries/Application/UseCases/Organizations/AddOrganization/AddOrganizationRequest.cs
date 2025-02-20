﻿using System.ComponentModel.DataAnnotations;
using TaskoMask.BuildingBlocks.Application.Commands;
using TaskoMask.BuildingBlocks.Contracts.Helpers;
using TaskoMask.BuildingBlocks.Contracts.Resources;

namespace TaskoMask.Services.Owners.Write.Application.UseCases.Organizations.AddOrganization
{
    public class AddOrganizationRequest: BaseCommand
    {
        public AddOrganizationRequest(string ownerId,string name, string description)
        {
            OwnerId = ownerId;
            Name = name;
            Description = description;
        }

        [Required(ErrorMessageResourceName = nameof(ContractsMetadata.Required), ErrorMessageResourceType = typeof(ContractsMetadata))]
        public string OwnerId { get; }


        [StringLength(DomainConstValues.Organization_Name_Max_Length, MinimumLength = DomainConstValues.Organization_Name_Min_Length, ErrorMessageResourceName = nameof(ContractsMetadata.Length_Error), ErrorMessageResourceType = typeof(ContractsMetadata))]
        [Required(ErrorMessageResourceName = nameof(ContractsMetadata.Required), ErrorMessageResourceType = typeof(ContractsMetadata))]
        public string Name { get; }


        [MaxLength(DomainConstValues.Organization_Description_Max_Length, ErrorMessageResourceName = nameof(ContractsMetadata.Max_Length_Error), ErrorMessageResourceType = typeof(ContractsMetadata))]
        public string Description { get; }

    }
}
