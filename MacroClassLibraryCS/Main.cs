using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Corel.Interop.VGCore;

namespace $safeprojectname$
{
	[System.AttributeUsage(System.AttributeTargets.Class)]
public class CgsAddInModule : System.Attribute { };

[System.AttributeUsage(System.AttributeTargets.Constructor)]
public class CgsAddInConstructor : System.Attribute { };

[System.AttributeUsage(System.AttributeTargets.Method)]
public class CgsAddInMacro : System.Attribute { };

public class Main
    {
	private Corel.Interop.VGCore.Application app;

	[CgsAddInConstructor]
	public Main(object _app)
	{
		app = _app as Corel.Interop.VGCore.Application;
		
	}
}
}
