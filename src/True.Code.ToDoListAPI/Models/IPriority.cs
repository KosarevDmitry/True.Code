// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace True.Code.ToDoListAPI.Models;

public interface IPriority
{
    int                   Level     { get; set; }
    ICollection<ToDoItem> ToDoItems { get; }
}
