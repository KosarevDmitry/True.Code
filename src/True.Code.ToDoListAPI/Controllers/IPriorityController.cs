// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace True.Code.ToDoListAPI.Controllers;

public interface IPriorityController
{
    Task<ActionResult> GePriority();
    Task<ActionResult> AddPriority(Priority priority);
    Task<ActionResult> AddRange(int[]       levels);
    Task<ActionResult> DeletePriority(int   level);
}
