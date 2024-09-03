// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace True.Code.ToDoListAPI.Controllers;

public interface IToDoItemController
{
    Task<ActionResult<ToDoItemCTO?>> ItemByIdAsync(int       id);
    Task<ActionResult>               AddAsync(ToDoItemAddCTO toDoItemCTO);
    Task<ActionResult>               UpdateAsync(ToDoItemCTO item);
    Task<ActionResult> DeleteById(int          id);
}
