# ContosoUniversity .NET Framework 4.8 to .NET 10.0 Upgrade Tasks

## Overview

This document tracks the execution of the ContosoUniversity project upgrade from .NET Framework 4.8 to .NET 10.0. The migration involves a complete architectural transformation from ASP.NET Framework to ASP.NET Core, including SDK-style project conversion, package updates, and comprehensive code migration.

**Progress**: 2/4 tasks complete (50%) ![0%](https://progress-bar.xyz/50)

---

## Tasks

### [✓] TASK-001: Verify prerequisites *(Completed: 2025-12-17 23:32)*
**References**: Plan §Prerequisites

- [✓] (1) Verify .NET 10.0 SDK installed per Plan §Prerequisites
- [✓] (2) .NET 10.0 SDK present and functional (**Verify**)

---

### [✓] TASK-002: Atomic framework and dependency upgrade with code migration *(Completed: 2025-12-18 00:15)*
**References**: Plan §Step 1-5, Plan §Package Update Reference, Plan §Breaking Changes Catalog, Plan §Code Modifications

- [✓] (1) Convert ContosoUniversity.csproj to SDK-style format per Plan §Step 1
- [✓] (2) Update TargetFramework property to net10.0 per Plan §Step 2
- [✓] (3) Remove 15 framework-included packages per Plan §Step 3
- [✓] (4) Remove 4 incompatible/deprecated packages per Plan §Step 3
- [✓] (5) Update 24 packages to target versions per Plan §Package Update Reference
- [✓] (6) Add ASP.NET Core and WebOptimizer packages per Plan §Step 3
- [✓] (7) All package references updated correctly (**Verify**)
- [✓] (8) Create Program.cs replacing Global.asax.cs per Plan §Step 4.1
- [✓] (9) Create appsettings.json and appsettings.Development.json per Plan §Step 4.2-4.3
- [✓] (10) Migrate configuration from web.config to appsettings.json per Plan §Step 4.4
- [✓] (11) Update all controller namespaces from System.Web.Mvc to Microsoft.AspNetCore.Mvc per Plan §Step 5 and Plan §Breaking Changes Catalog §1
- [✓] (12) Update all action results (HttpNotFound→NotFound, HttpStatusCodeResult→StatusCode/BadRequest) per Plan §Step 5.3
- [✓] (13) Update Bind attribute syntax per Plan §Step 5.3
- [✓] (14) Replace HttpPostedFileBase with IFormFile per Plan §Breaking Changes Catalog §1
- [✓] (15) Replace Server.MapPath with IWebHostEnvironment per Plan §Breaking Changes Catalog §1
- [✓] (16) Replace ConfigurationManager with IConfiguration per Plan §Breaking Changes Catalog §4
- [✓] (17) Abstract MSMQ dependencies per Plan §Breaking Changes Catalog §2
- [✓] (18) Update views: create/update _ViewImports.cshtml, update _Layout.cshtml, replace bundle references per Plan §Code Modifications
- [✓] (19) Reorganize static files to wwwroot structure per Plan §Breaking Changes Catalog §9
- [✓] (20) Delete App_Start folder and Global.asax.cs per Plan §Step 4
- [✓] (21) Restore all dependencies
- [✓] (22) All dependencies restored successfully (**Verify**)
- [✓] (23) Build solution and fix all compilation errors per Plan §Breaking Changes Catalog
- [✓] (24) Solution builds with 0 errors (**Verify**)
- [✓] (25) Commit changes with message: "TASK-002: Complete atomic upgrade from .NET Framework 4.8 to .NET 10.0"

---

### [▶] TASK-003: Execute tests and validate upgrade
**References**: Plan §Testing Strategy

- [✓] (1) Run all tests in test projects (if test projects exist)
- [⊘] (2) Fix any test failures referencing Plan §Breaking Changes Catalog for common issues
- [⊘] (3) Re-run tests after fixes
- [⊘] (4) All tests pass with 0 failures (**Verify**)
- [✓] (5) Verify application starts successfully with dotnet run
- [✓] (6) Application starts without errors (**Verify**)
- [✓] (7) Verify database connectivity per Plan §Testing Strategy §2.2
- [✓] (8) Database connection successful (**Verify**)
- [✓] (9) Verify key controller actions function per Plan §Testing Strategy §2.1
- [✓] (10) All tested controller actions work correctly (**Verify**)
- [▶] (11) Commit test fixes with message: "TASK-003: Complete testing and validation"

---

### [ ] TASK-004: Final verification
**References**: Plan §Success Criteria

- [ ] (1) Verify no security vulnerabilities with dotnet list package --vulnerable
- [ ] (2) No vulnerabilities detected (**Verify**)
- [ ] (3) Verify solution builds with 0 warnings
- [ ] (4) Solution builds with 0 warnings (**Verify**)

---















