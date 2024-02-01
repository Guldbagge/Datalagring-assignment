using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Tests;

[CollectionDefinition("DatabaseCollection")]
public class DatabaseCollection : ICollectionFixture<ServiceProviderFixture>
{

    // This class is used to share the same ServiceProvider among test classes.
}