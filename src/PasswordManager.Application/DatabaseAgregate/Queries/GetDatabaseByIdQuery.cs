﻿using MediatR;
using PasswordManager.Application.Dtos;
using PasswordManager.SharedKernel.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordManager.Application.DatabaseAgregate.Queries
{
    public class GetDatabaseByIdQuery : IRequest<Result<DatabaseDto>>
    {
        public int Id { get; }
        public bool IncludeAdditionalData { get; }

        public GetDatabaseByIdQuery(int id, bool includeAdditionalData)
        {
            Id = id;
            IncludeAdditionalData = includeAdditionalData;
        }
    }
}
