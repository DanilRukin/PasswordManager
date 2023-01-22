﻿using MediatR;
using PasswordManager.Application.Dtos;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.GroupAgregate.Commands
{
    public class UpdateGroupsDataCommand : IRequest<Result<GroupDto>>
    {
        public GroupDto Group { get; }

        public UpdateGroupsDataCommand(GroupDto group)
        {
            Group = group ?? throw new ArgumentNullException(nameof(group));
        }
    }
}
