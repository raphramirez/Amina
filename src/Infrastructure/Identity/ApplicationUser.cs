// Copyright (c) Duende Software. All rights reserved.
// See LICENSE in the project root for license information.

using Finbuckle.MultiTenant;
using Microsoft.AspNetCore.Identity;

namespace Amina.Infrastructure.Identity;

[MultiTenant]
public class ApplicationUser : IdentityUser
{
}