// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace True.Code.ToDoListAPI.Controllers;

public interface IUserController
{
    Task<ActionResult<IEnumerable<User>>> Get();
    Task<ActionResult<IEnumerable<User>>> GetById(int     id);
    Task<ActionResult> AddAsync(string username);
}
