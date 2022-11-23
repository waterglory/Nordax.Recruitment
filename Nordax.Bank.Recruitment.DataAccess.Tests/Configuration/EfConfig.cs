﻿using Nordax.Bank.Recruitment.DataAccess.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nordax.Bank.Recruitment.DataAccess.Tests.Configuration
{
	public static class EfConfig
	{
		public static EfContexts<FileDbContext> File = new EfContexts<FileDbContext>();
		public static EfContexts<OptionDbContext> Option = new EfContexts<OptionDbContext>();
		public static EfContexts<SubscriptionDbContext> Subscription = new EfContexts<SubscriptionDbContext>();
	}
}